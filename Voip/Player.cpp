//--
#include "player.h"

namespace Timok_IVR {
Player::Player(Card^ pCard, int pModuleNumber, int pChannelNumber, IVRLogger^ pLogger) {
	card = pCard;
	module_number = pModuleNumber;
	call_number = pChannelNumber;
	logger = pLogger;

	play_parms = new SM_FILE_REPLAY_PARMS();

	thread_start = gcnew ThreadStart(this, &Player::thread);
	worker_thread = gcnew Thread(thread_start);
}

Player::~Player() { 
	delete play_parms;
}

void Player::Play(TX_Resource^ pTx, StringCollection^ pFile_names_to_play) {
	tx = pTx;

	char _tx_id[20] = {0};
	_snprintf(_tx_id, 20, "%u", tx->Vmptx);
	tx_id = gcnew String(_tx_id);

	files_to_play = gcnew array<String^>(pFile_names_to_play->Count);
  pFile_names_to_play->CopyTo(files_to_play, 0 );

	Is_complete = false;
	should_abort = false;
	aborted = false;

	worker_thread->Start();
}

void Player::Close() {
	if (fileType == Raw) {
		should_abort = true;
	}
	else {		//stop replay of .wav file 
		int	_result	= 0;
		if ((_result = sm_replay_file_stop(play_parms, 0)) != 0) {
			logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0} sm_replay_file_stop, channel {1}.", gcnew String(error_2_string(_result)), Channel_id));
		}
		else {
			//logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Ok: sm_replay_file_stop, channel {0}.", Channel_id));
		}
	}

	//-- Wait_for_play_thread_stop
	if (false == worker_thread->Join(5000)) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: PlayThread DIDN'T joined main call thread, {0}", Channel_id));
	}

	worker_thread = gcnew Thread(thread_start);
	//logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("PlayThread joined main call thread, Vmptx_id={0}", tx_id));
}

int Player::Play_tone(TX_Resource^ pTx) {
	SM_PLAY_TONE_PARMS sm_play_tone_parms;
	INIT_ACU_SM_STRUCT(&sm_play_tone_parms);
	sm_play_tone_parms.channel = (tSMChannelId) pTx->Channel_id;
	sm_play_tone_parms.duration	= 500;
	sm_play_tone_parms.wait_for_completion = 1;
	sm_play_tone_parms.tone_id = 5;
	int _result = sm_play_tone( &sm_play_tone_parms );
	if(_result != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0} Could not play tone, channel {1}.", gcnew String(error_2_string(_result)), pTx->Channel_id));
		return _result;
	}
	return _result;
}

//------------------------------- Private ----------------------------------------------------------
void Player::thread() {
	//logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Play thread started: {0}", tx->Channel_id));

	char _file_name[256] = {0};

	try {
		for (int _i=0; _i < files_to_play->Length; _i++) {
			logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Playing file: {0}", files_to_play[_i]));

			_snprintf(_file_name, 256, "%s", files_to_play[_i]);

			int	_play_file_result = RET_FAIL; 
			fileType = getFileType(files_to_play[_i]);
			if (fileType == Raw) {
				_play_file_result = play_raw(_file_name);
			}
			else {
				_play_file_result = play_wav(_file_name);
			}
			if (_play_file_result == RET_FAIL || aborted) {
				break;
			}
		}
	}
	catch (Exception^ _ex) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, PlayThread.", _ex));
	}
	finally {
		Is_complete = true;
	}
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Play thread ended: {0}", tx->Channel_id));
}

int Player::play_wav(char *pFile_name) {

	try {
		INIT_ACU_SM_STRUCT(play_parms);
		play_parms->replay_parms.channel = (tSMChannelId) tx->Channel_id;
		play_parms->replay_parms.type	  = kSMDataFormat8KHzALawPCM;
		int _result = sm_replay_wav_start(pFile_name, play_parms);
		if (_result != 0) {
			throw gcnew Exception(String::Format("ERROR: {0} sm_play_wav_start, Channel_id={1}.", gcnew String(error_2_string(_result)), Channel_id));
		}

		//-- wait for playing to finish
		_result = sm_replay_file_complete(play_parms);
		if(_result != 0) {
			throw gcnew Exception(String::Format("ERROR: {0} sm_play_file_complete, Channel_id={1}.", gcnew String(error_2_string(_result)), Channel_id));
		}
		
		//-- close played file
		if ((_result = sm_replay_wav_close(play_parms)) != 0) {
			throw gcnew Exception(String::Format("ERROR: {0} sm_replay_wav_close, channel {1}.", gcnew String(error_2_string(_result)), Channel_id));
		}	
	}
	catch (Exception^ _ex) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, PlayThread.", _ex));
	}
	finally {
		Is_complete = true;
	}
	return RET_SUCCESS;
}

int Player::play_raw(char *pFile_name) {
	int	_result;

	SM_CHANNEL_SET_EVENT_PARMS _play_event;
	INIT_ACU_SM_STRUCT(&_play_event);
	_play_event.channel = (tSMChannelId) tx->Channel_id;
	_play_event.event_type = kSMEventTypeWriteData;
	_play_event.issue_events = kSMChannelSpecificEvent;
	smd_ev_create(&_play_event.event, _play_event.channel, _play_event.event_type, _play_event.issue_events);
	if ((_result = sm_channel_set_event(&_play_event)) != 0)	{
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sm_channel_set_event.", gcnew String(error_2_string(_result))));
		return RET_FAIL;
	}

	FILE *_file_handle;
	if ((_file_handle = fopen(pFile_name, "rb")) == NULL)	{
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: Cannot open file: {0}", gcnew String(pFile_name)));
		return RET_FAIL;
	}

	try {
		SM_REPLAY_PARMS	_replay_parms;
		INIT_ACU_SM_STRUCT(&_replay_parms);
		_replay_parms.channel = (tSMChannelId) tx->Channel_id;
		_replay_parms.type = kSMDataFormatULawPCM;
		_replay_parms.sampling_rate = 8000;
		_replay_parms.volume = card->IVRConfig->Volume;
		if ((_result = sm_replay_start( &_replay_parms )) != 0) {
			logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sm_replay_start.", gcnew String(error_2_string(_result))));
			return RET_FAIL;
		}

		char buffer[kSMMaxReplayDataBufferSize];		
		SM_REPLAY_STATUS_PARMS _replay_status_parms;
		SM_TS_DATA_PARMS _ts_data_parms;
		while (true) {
			if (WaitForSingleObject(_play_event.event, 10000) != WAIT_OBJECT_0)	{
				logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, "TIMEOUT: Play file WaitForSingleObject.");	
				abort_play_raw();
				continue;
			}

			INIT_ACU_SM_STRUCT(&_replay_status_parms);
			_replay_status_parms.channel = (tSMChannelId) tx->Channel_id;
			if ((_result = sm_replay_status(&_replay_status_parms)) != 0)	{
				logger->Log(module_number, call_number, ERR, __FILE__, __LINE__,	String::Format("ERROR: {0}, sm_replay_status.", gcnew String(error_2_string(_result))));
				abort_play_raw();
				continue;
			}

			//-- check if play completed
			if (_replay_status_parms.status == kSMReplayStatusComplete) {
				//logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("PlayStatusComplete: {0}", tx->Channel_id));
				_result = RET_SUCCESS;
				break;
			}

			if (_replay_status_parms.status == kSMReplayStatusHasCapacity ) {
				if (should_abort) {
					//logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("ABORTED: Play file, {0}", tx->Channel_id));	
					should_abort = false;
					aborted = true;
					abort_play_raw();
					continue;
				}

				//logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Has capacity[{0}].", tx->Channel_id));
				_result = fread(buffer, sizeof(char), kSMMaxReplayDataBufferSize, _file_handle);
				if (_result != sizeof(char) * kSMMaxReplayDataBufferSize ) {	//  Did not read as much data as we could
					if (feof(_file_handle)) { 	//  End of file
						//logger->Log(module_number, call_number, DBG, __FILE__, __LINE__, String::Format("End of file, {0}.", tx->Channel_id));
						memset(&_ts_data_parms, 0, sizeof(SM_TS_DATA_PARMS));
						_ts_data_parms.channel = (tSMChannelId) tx->Channel_id;
						_ts_data_parms.data = buffer;
						_ts_data_parms.length = _result;
						if ((_result = sm_put_last_replay_data(&_ts_data_parms)) != 0) {
							logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sm_put_last_replay_data.", gcnew String(error_2_string(_result))));
						}
					}
					else {
						logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: fread!, {0}", tx->Channel_id));
						abort_play_raw();
						continue;
					}
				}
				else {
					memset(&_ts_data_parms, 0, sizeof(SM_TS_DATA_PARMS));
					_ts_data_parms.channel = (tSMChannelId) tx->Channel_id;
					_ts_data_parms.data = buffer;
					_ts_data_parms.length = _result;
					_result = sm_put_replay_data(&_ts_data_parms);
				}
			}
			else if (_replay_status_parms.status == kSMReplayStatusUnderrun) {
				logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("kSMReplayStatusUnderrun, {0}", tx->Channel_id));
			}
			else if (_replay_status_parms.status == kSMReplayStatusNoCapacity) {
				//logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "kSMReplayStatusNoCapacity");
			}
			else if (_replay_status_parms.status == kSMReplayStatusCompleteData) {
				//logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("kSMReplayStatusCompleteData, {0}", tx->Channel_id));
			}
			else {
				logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("UNKOWN _replay_status_parms.status, {0}", tx->Channel_id));;
			}
		}
	}
	catch (Exception^ _ex) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("Exception - Play file: {0}, {1}.", _ex, tx->Channel_id));
		_result = RET_FAIL;
	}
	finally {
		_play_event.issue_events = kSMChannelNoEvent;
		_result = sm_channel_set_event( &_play_event );
		if (_result != 0) {
			logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sm_channel_set_event.", gcnew String(error_2_string(_result))));
		}
		smd_ev_free(_play_event.event);

		fclose(_file_handle);
	}
	return _result;
}

void Player::abort_play_raw() {
	int	_result	= 0;

	SM_REPLAY_ABORT_PARMS	_replay_abort_parms;
	INIT_ACU_SM_STRUCT(&_replay_abort_parms);
	_replay_abort_parms.channel = (tSMChannelId) tx->Channel_id;
	if ((_result = sm_replay_abort(&_replay_abort_parms)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0} sm_replay_abort, channel {1}.", gcnew String(error_2_string(_result)), tx->Channel_id));
	}
}

VoiceFileFormat Player::getFileType(String^ pFileName) {
	int _index = pFileName->LastIndexOf('.');
	String^ _ext = pFileName->Substring(_index + 1);
	if (_ext == "raw") {
		return Raw;
	}
	else if (_ext == "wav") {
		return Wav;
	}
	return Invalid;
}
}
