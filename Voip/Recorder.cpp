//--
#include "recorder.h"

namespace Timok_IVR {
Recorder::Recorder(Card^ pCard, int pModuleNumber, int pChannelNumber, IVRLogger^ pLogger) {
	card = pCard;
	module_number = pModuleNumber;
	call_number = pChannelNumber;
	logger = pLogger;

	record_parms = new SM_FILE_RECORD_PARMS();

	thread_start = gcnew ThreadStart(this, &Recorder::thread);
	worker_thread = gcnew Thread(thread_start);
}

Recorder::~Recorder() { 
	delete record_parms;
}

void Recorder::Record(long pChannelId, String^ pFile_path, int pTimeLimit) {
	Channel_id = pChannelId;
	file_to_record = pFile_path;
	time_limit = pTimeLimit * 1000;

	Is_complete = false;
	Should_abort = false;

	worker_thread->Start();
}

void Recorder::Close() {
	int	_result	= 0;
	if ((_result = sm_record_file_stop(record_parms, 0)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0} sm_record_file_stop, channel {1}.", gcnew String(error_2_string(_result)), Channel_id));
	}
	else {
		logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Ok: sm_record_file_stop, channel {0}.", Channel_id));
	}

	//-- wait_for_record_thread_stop
	if (false == worker_thread->Join(5000)) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: RecordThread DIDN'T joined main call thread, {0}", Channel_id));
	}
	
	worker_thread = gcnew Thread(thread_start);
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("RecordThread joined main call thread, {0}", Channel_id));
}

//------------------------------- Private ----------------------------------------------------------
void Recorder::thread() {
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Record thread started: Channel_id={0}", Channel_id));

	try {
		char _file_name[256] = {0};
		_snprintf(_file_name, 256, "%s", file_to_record);
		logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Recording to file: {0}", gcnew String(_file_name)));

		INIT_ACU_SM_STRUCT(record_parms);
		record_parms->record_parms.channel = (tSMChannelId) Channel_id;
		record_parms->record_parms.volume = card->IVRConfig->Volume;
		//record_parms->record_parms.agc = chanpar.agc;
		record_parms->record_parms.type = kSMDataFormatULawPCM;
		record_parms->record_parms.sampling_rate = 8000;
		//record_parms->record_parms.silence_elimination = chanpar.sil_elim;
		//record_parms->record_parms.tone_elimination_mode = chanpar.tone_elim;
		//record_parms->record_parms.tone_elimination_set_id = chanpar.tone_elim_set;
		record_parms->record_parms.max_silence = 5000;
		record_parms->record_parms.max_elapsed_time = time_limit;
		//record_parms->record_parms.max_octets = chanpar.max_octets;
		int _result = sm_record_wav_start(_file_name, record_parms);
		if (_result != 0) {
			throw gcnew Exception(String::Format("ERROR: {0} sm_record_wav_start, Channel_id={1}.", gcnew String(error_2_string(_result)), Channel_id));
		}

		//-- wait for recording to finish
		_result = sm_record_file_complete(record_parms);
		if(_result != 0) {
			throw gcnew Exception(String::Format("ERROR: {0} sm_record_file_complete, Channel_id={1}.", gcnew String(error_2_string(_result)), Channel_id));
		}

		//-- close recorded file
		if ((_result = sm_record_wav_close(record_parms)) != 0) {
			throw gcnew Exception(String::Format("ERROR: {0} sm_record_wav_close, channel {1}.", gcnew String(error_2_string(_result)), Channel_id));
		}
	}
	catch (Exception^ _ex) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, RecordThread.", _ex));
	}
	finally {
		Is_complete = true;
	}
}
}
