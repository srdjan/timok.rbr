//--
#include "rxresource.h"

namespace Timok_IVR {
RX_Resource::RX_Resource(Card^ pCard, int pModuleNumber, int pChannelNumber, IVRLogger^ pLogger) {
	card = pCard;
	module_number = pModuleNumber;
	call_number = pChannelNumber;
	logger = pLogger;

	rx_event = NULL;
	stopped_event = CreateEvent(NULL, TRUE, FALSE, NULL);
	started_event = CreateEvent(NULL, TRUE, FALSE, NULL);

	//--native, destroyed in dtor
	rx = new SM_VMPRX_CREATE_PARMS();
	INIT_ACU_SM_STRUCT(rx);
	rx->module = card->Card_info->module_id[module_number];
	rx_id = String::Empty;

	created = false;
	started = false;
	connected = false;

	dtmf_buffer = String::Empty;
	got_terminator = false;

	thread_start = gcnew ThreadStart(this, &RX_Resource::thread);
}

RX_Resource::~RX_Resource() { 
	delete rx;
}

bool RX_Resource::CreateAndConnect(long pChannel_id) {
	if ( ! create()) {
		return false;
	}
	channel_id = pChannel_id;
	if (connect()) {
		rx_thread = gcnew Thread(thread_start);
		rx_thread->Name = String::Format("{0}-{1}", module_number, call_number);
		rx_thread->Start();
		if (wait_for_start()) {
			return true;
		}
	}
	return false;
}

bool RX_Resource::Config(SIP_DETAIL_PARMS pSIP_details) {
	int _result = 0;

	SM_VMPRX_CODEC_PARMS _codec;
	INIT_ACU_SM_STRUCT( &_codec );
	_codec.vmprx = rx->vmprx; 
	_codec.payload_type	= pSIP_details.media_session->sent_media.media_descriptions->payloads->payload.audio_video.rtp_payload_number;
	_codec.codec = Convert_payload_number_to_ting(_codec.payload_type);
	if ((_result = sm_vmprx_config_codec( &_codec )) != 0)	{
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sm_vmpRX_config_codec, Payload [{1}]", gcnew String(error_2_string(_result)),	pSIP_details.media_session->sent_media.media_descriptions->payloads->payload.audio_video.rtp_payload_number)); 
		return false;
	}

	if (pSIP_details.media_session->sent_media.media_descriptions->payloads->next )	{
		if (0 == strcmp( pSIP_details.media_session->sent_media.media_descriptions->payloads->next->payload.audio_video.rtp_payload_name, ACU_TELEPHONE_EVENT ))	{
			_codec.codec = kSMCodecTypeRFC2833;
			_codec.payload_type	= pSIP_details.media_session->sent_media.media_descriptions->payloads->next->payload.audio_video.rtp_payload_number;
			if ((_result = sm_vmprx_config_codec( &_codec )) != 0) {
				logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sm_vmpRX_config_codec.", gcnew String(error_2_string(_result))));
				return false;
			}
			logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "RX config_codec: Ok.");
		}
	}

	SM_VMPRX_TONE_PARMS	_tone_params;
	INIT_ACU_SM_STRUCT(&_tone_params);
	_tone_params.vmprx = rx->vmprx; 
	_tone_params.detect_tones = 1;
	_tone_params.regen_tones	= 0;//If to be done through prosody resource.//
	if ((_result = sm_vmprx_config_tones( &_tone_params )) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sm_vmprx_config_tones.", gcnew String(error_2_string(_result))));
		return false;
	}
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "RX config_tones: Ok.");

	return true;
}

void RX_Resource::Reset() {
	dtmf_buffer = String::Empty;
	got_terminator = false;
}

void RX_Resource::Destroy() {
	disconnect();
	if (stop()) {
		destroy();
	}
}

//------------------------------------------- Private ----------------------------------------------------------------
bool RX_Resource::create() {
	if (created) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("!!! ERROR: RX already created, Id: {0}.", rx_id));
		return false;
	}

	int _result;
	if ((_result = sm_vmprx_create(rx)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("!!! ERROR: NO RX available at this time!!!: {0}", _result));
		return false;
	}
	char _vmprx_id[20] = {0};
	_snprintf(_vmprx_id, 20, "%u", rx->vmprx);
	rx_id = gcnew String(_vmprx_id);

	SM_VMPRX_EVENT_PARMS _evpRx;
	INIT_ACU_SM_STRUCT(&_evpRx);
	_evpRx.vmprx = rx->vmprx;
	if ((_result = sm_vmprx_get_event(&_evpRx)) != 0)	{
		logger->Log(module_number, call_number,  ERR, __FILE__, __LINE__, String::Format("!!! ERROR: {0}, RX get_event.", _result));
		return false;
	}
	rx_event = _evpRx.event;
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("!!! RX Created: {0}", rx_id));
	return created = true;
}

bool RX_Resource::connect() {
	if (connected) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("!!! ERROR: RX already connected, ChannelId: {0}.", channel_id));
		return false;
	}

	int _result;
	SM_VMPRX_DATAFEED_PARMS	_datafeed_params;
	INIT_ACU_SM_STRUCT(&_datafeed_params);
	_datafeed_params.vmprx = rx->vmprx; 
	if ((_result = sm_vmprx_get_datafeed( &_datafeed_params )) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("!!! ERROR: {0} RX sm_vmprx_get_datafeed, {1}.", _result, rx_id));
		return false;
	}
	
	SM_CHANNEL_DATAFEED_CONNECT_PARMS	_datafeed_connect_params;
	INIT_ACU_SM_STRUCT(&_datafeed_connect_params);
	_datafeed_connect_params.channel = (tSMChannelId) channel_id;
	_datafeed_connect_params.data_source = _datafeed_params.datafeed;
	if ((_result = sm_channel_datafeed_connect(&_datafeed_connect_params)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("!!! ERROR: {0} RX sm_channel_datafeed_connect, {1}.", _result, rx_id));
		return false;
	}
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("!!! RX Connected: {0}", rx_id));
	return connected = true;
}

void RX_Resource::disconnect() {
	if ( ! connected) {
		return;		
	}

	int _result	= 0;
	SM_CHANNEL_DATAFEED_CONNECT_PARMS	_datafeed;
	INIT_ACU_SM_STRUCT(&_datafeed);
	_datafeed.channel = (tSMChannelId) channel_id;
	_datafeed.data_source = kSMNullDatafeedId;
	if ((_result = sm_channel_datafeed_connect(&_datafeed)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("!!! ERROR: {0}, Disconnect RX datafeed, {1}.", _result, rx_id));
		return;
	}
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("!!! RX Disconnnected, {0}.", rx_id));
	connected = false;
}

bool RX_Resource::stop() {
	if ( ! started) {
		return false;		
	}

	int _result	= 0;
	SM_VMPRX_STOP_PARMS	_stop_parms;
	INIT_ACU_SM_STRUCT(&_stop_parms );
	_stop_parms.vmprx = rx->vmprx; 
	if ((_result = sm_vmprx_stop( &_stop_parms )) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("!!! ERROR: {0}, RX Stop: {1}", _result, rx_id));
		return false;
	}

	if (WaitForSingleObject(stopped_event, 10000) != WAIT_OBJECT_0)	{
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("!!! ERROR: RX waiting for stop: {0}.", rx_id));
		return false;
	}
	ResetEvent(stopped_event);

	started = false;
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("!!! RX Stop complete: {0}", rx_id));		
	return true;
}

void RX_Resource::destroy() {
	if ( ! created) {
		return;		
	}

	int _result;
	if ((_result = sm_vmprx_destroy(rx->vmprx)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("!!! ERROR: {0}, Destroy RX: {1}", _result, rx_id));
		return;
	}

	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("!!! RX Destroy complete: {0}", rx_id));		
	created = false;
}

void RX_Resource::thread() {
	int	_result = 0;
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "RX thread started.");

	SM_VMPRX_STATUS_PARMS _status;
	INIT_ACU_SM_STRUCT(&_status);
	_status.vmprx = rx->vmprx; 

	bool _collecting = true;
	while (_collecting) {
		_result = WaitForSingleObject(rx_event, 1000);
		if (_result == WAIT_TIMEOUT)	{
			continue;
		}
		if (_result != WAIT_OBJECT_0)	{
			logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("LEAK: RX waiting for event Result: {0}, Error: {1}.", _result, GetLastError()));
			break;
		}
		if ((_result = sm_vmprx_status(&_status)) != 0) {
			logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("LEAK: {0} RX: {1}, get status.", _result, rx_id));
			break;
		}
		switch (_status.status) {
			case kSMVMPrxStatusStopped:
				logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("RX Stopped, Thread {0} {1}.", rx_thread->Name, rx_id));		
				rtp_port = 0;
				SetEvent(stopped_event);
				_collecting = false;
				break;

			case kSMVMPrxStatusGotPorts:
				logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("RX GotPorts {0}", rx_id));
				SM_VMPRX_PORT_PARMS	_rx_port_info;
				INIT_ACU_SM_STRUCT(&_rx_port_info );
				_rx_port_info.vmprx	= rx->vmprx; 
				_rx_port_info.nowait = 1;
				if ((_result = sm_vmprx_get_ports(&_rx_port_info)) != 0)	{
					logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("LEAK: {0} RX get_ports {1}", _result, rx_id));
					break;
				}
				rtp_port = (ACU_USHORT) _rx_port_info.RTP_port;
				SetEvent(started_event);
				break;
				
			case kSMVMPrxStatusDetectTone:
				if (_status.u.tone.id == 11)	{	//-- terminator: '#'
					got_terminator = true;
				}
				else if (_status.u.tone.id != 10) {	//-- skipping star '*'
					dtmf_buffer = String::Concat(dtmf_buffer, _status.u.tone.id);
				}
				break;

			case kSMVMPrxStatusRunning:
				logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("RX Running, Thread {0} {1}.", rx_thread->Name, rx_id));		
				break;
		}
	}
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("RX thread ended: {0}.", rx_thread->Name));
}

bool RX_Resource::wait_for_start() {
	int _result;
	if ((_result = WaitForSingleObject(started_event, 10000)) == WAIT_OBJECT_0)	{
		started = true;
		ResetEvent(started_event);
		return true;
	}
	logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, Waiting for RX StartedEvent: {1}.", _result, rx_id));
	return false;
}
}