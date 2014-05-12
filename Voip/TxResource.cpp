//--
#include "txresource.h"

namespace Timok_IVR {
TX_Resource::TX_Resource(Card^ pCard, int pModuleNumber, int pChannelNumber, IVRLogger^ pLogger) {
	card = pCard;
	module_number = pModuleNumber;
	call_number = pChannelNumber;
	logger = pLogger;

	created = false;	
	connected = false;
	running = false;	

	//--native, destroyed in dtor
	tx = new SM_VMPTX_CREATE_PARMS();
	INIT_ACU_SM_STRUCT(tx);
	tx->module = card->Card_info->module_id[module_number];
	tx_id = String::Empty;
}

TX_Resource::~TX_Resource() { 
	delete tx;
}

bool TX_Resource::CreateAndConnect(long pChannel_id) {
	if ( ! create()) {
		return false;
	}
	Channel_id = pChannel_id;
	connect();
	return wait_for_start();
}

bool TX_Resource::Config(SIP_DETAIL_PARMS pSIP_details) {
	int _result = 0;

	SM_VMPTX_CONFIG_PARMS	_config_params;
	INIT_ACU_SM_STRUCT(&_config_params);
	_config_params.vmptx = tx->vmptx; 
	_config_params.destination_rtp.sin_addr.s_addr = inet_addr(pSIP_details.media_session->received_media.connection_address.address);
	_config_params.destination_rtp.sin_port	= htons(pSIP_details.media_session->received_media.media_descriptions->port);
	_config_params.destination_rtcp.sin_port = pSIP_details.media_session->received_media.media_descriptions->port + 1;
	_config_params.source_rtp.sin_port = htons(pSIP_details.media_session->sent_media.media_descriptions->port);
	_config_params.source_rtp.sin_addr.s_addr = 0;
	if ((_result = sm_vmptx_config( &_config_params )) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sm_vmpTX_config.", gcnew String(error_2_string(_result))));
		return false;
	}

	//-- Codec setting ------------------------------------------
	SM_VMPTX_CODEC_PARMS	_codec;
	INIT_ACU_SM_STRUCT( &_codec );
	_codec.vmptx = tx->vmptx; 
	_codec.payload_type	= pSIP_details.media_session->sent_media.media_descriptions->payloads->payload.audio_video.rtp_payload_number;
	_codec.codec = Convert_payload_number_to_ting(_codec.payload_type);
	_codec.ptime = 20;		//a default value
	if (pSIP_details.media_session->sent_media.media_descriptions->payloads->payload.audio_video.packet_length != 0 )	{
		_codec.ptime = pSIP_details.media_session->sent_media.media_descriptions->payloads->payload.audio_video.packet_length;
	}

	if ((_result = sm_vmptx_config_codec( &_codec )) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sm_vmpTX_config_codec, Payload: {1}", gcnew String(error_2_string(_result)), pSIP_details.media_session->sent_media.media_descriptions->payloads->payload.audio_video.rtp_payload_number)); 
		return false;
	}
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "TX config_codec: Ok.");
	return true;
}

void TX_Resource::Destroy() {
	disconnect();
	if (stop()) {
		destroy();
	}
}

//------------------------------- Create/Destroy ----------------------------------------------------------
bool TX_Resource::create() {
	if (created) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| TX already created: {0}.", tx_id));
		return false;
	}

	int _result = 0;
	if ((_result = sm_vmptx_create(tx)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| ERROR: {0}, NO TX available at this time!!!", _result));
		return false;
	}
	char _vmptx_id[20] = {0};
	_snprintf(_vmptx_id, 20, "%u", tx->vmptx);
	tx_id = gcnew String(_vmptx_id);
	
	SM_VMPTX_EVENT_PARMS _evp;
	INIT_ACU_SM_STRUCT(&_evp);
	_evp.vmptx = tx->vmptx;
	if ((_result = sm_vmptx_get_event(&_evp)) != 0)	{
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| ERROR: {0}, TX get_event, {1}.", _result, tx_id));
		return false;
	}
	tx_event = _evp.event;
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("||| TX Created: {0}.", tx_id));
	return created = true;
}

bool TX_Resource::connect() {
	if (connected) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| TX already connected: {0}", tx_id));
		return false;
	}

	int _result;
	SM_CHANNEL_DATAFEED_PARMS	_datafeed_params;
	INIT_ACU_SM_STRUCT(&_datafeed_params);
	_datafeed_params.channel = (tSMChannelId) Channel_id; 
	if ((_result = sm_channel_get_datafeed(&_datafeed_params)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| ERROR: {0} TX sm_channel_get_datafeed, {1}, {2}.", _result, tx_id, Channel_id));
		return false;
	}

	SM_VMPTX_DATAFEED_CONNECT_PARMS	_datafeed_connect_params;
	INIT_ACU_SM_STRUCT(&_datafeed_connect_params);
	_datafeed_connect_params.vmptx = tx->vmptx; 
	_datafeed_connect_params.data_source = _datafeed_params.datafeed;
	if ((_result = sm_vmptx_datafeed_connect( &_datafeed_connect_params )) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| ERROR: {0} sm_vmptx_datafeed_connect, {1}, {2}.", _result, tx_id, Channel_id));
		return false;
	}
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("||| TX Connnected, {0}, {1}.", tx_id, Channel_id));
	return connected = true;
}

bool TX_Resource::wait_for_start() {
	if (running) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| TX already running: {0}.", tx_id));
		return false;
	}
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "||| TX wait_for_start().");

	int _waiting = 0;
	while (_waiting < 10000) { 
		wait_for_status_running();
		if (running) { 
			return true;
		}
		_waiting += 1000;
	}
	logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| ERROR: TX Start Timeout {0}.", tx_id));		
	return false;
}

void TX_Resource::wait_for_status_running() {
	int _result = WaitForSingleObject(tx_event, 1000);
	if (_result != WAIT_OBJECT_0)	{
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| ERROR: {0} TX waiting for status running.", _result));
		return;
	}
	ResetEvent(tx_event);

	SM_VMPTX_STATUS_PARMS	_status;
	INIT_ACU_SM_STRUCT( &_status );
	_status.vmptx = tx->vmptx; 
	if ((_result = sm_vmptx_status(&_status)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| ERROR: {0} TX get status, {1}.", _result, tx_id));
		return;
	}

	if (_status.status == kSMVMPtxStatusRunning) {
		logger->Log(module_number, call_number, INF, __FILE__, __LINE__,String::Format( "||| TX Started: {0}.", tx_id));		
		running = true;
	}
	else {
		logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("ERROR: {0}, TX unexpected Status: {1}.", (long) _status.status, tx_id));		
	}
}

void TX_Resource::disconnect() {
	if ( ! connected) {
		return;
	}

	int _result	= 0;
	SM_VMPTX_DATAFEED_CONNECT_PARMS _datafeed;
	INIT_ACU_SM_STRUCT(&_datafeed);
	_datafeed.vmptx = tx->vmptx; 
	_datafeed.data_source = kSMNullDatafeedId ;
	if ((_result = sm_vmptx_datafeed_connect( &_datafeed )) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| ERROR: {0}, Disconnect TX datafeed: {1}.", _result, tx_id));
		return;
	}
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("||| TX Disconnnected: {0}", tx_id));
	connected = false;
}

bool TX_Resource::stop() {
	if (! running) {
		return false;
	}

	int _result	= 0;
	SM_VMPTX_STOP_PARMS	_stop_parms;
	INIT_ACU_SM_STRUCT(&_stop_parms);
	_stop_parms.vmptx = tx->vmptx; 
	if ((_result = sm_vmptx_stop(&_stop_parms)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| ERROR: {0}, Stop TX: {1}.", _result, tx_id));
		return false;
	}
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("||| TX sm_vmptx_stop called {0}.", tx_id));

	int _waiting = 0;
	while (_waiting < 10000) { 
		wait_for_status_stopped();
		if ( ! running) { 
			return true;
		}
		else {
			_waiting += 1000;
		}
	}
	logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| ERROR: TX Stop Timeout {0}.", tx_id));		
	return false;
}

bool TX_Resource::wait_for_status_stopped() {
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("||| TX wait_for_status_stopped(): {0}.", tx_id));

	SM_VMPTX_STATUS_PARMS	_status;
	if (WaitForSingleObject(tx_event, 1000) != WAIT_OBJECT_0)	{
		logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("||| ERROR: {0}, TX waiting for stopped: {1}.", GetLastError(), tx_id));
		return false;
	}
	ResetEvent(tx_event);

	INIT_ACU_SM_STRUCT( &_status );
	_status.vmptx = tx->vmptx; 
	int _result;
	if ((_result = sm_vmptx_status(&_status)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| ERROR: {0} TX get status, {1}.", _result, tx_id));
		return false;
	}

	if (_status.status != kSMVMPtxStatusStopped) {
		logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("||| TX Unexpected Status: {0}.", (long) _status.status));		
		return false;
	}
	running = false;
	return true;
}

void TX_Resource::destroy() {
	if (! created) {
		return;
	}

	int _result;
	if ((_result = sm_vmptx_destroy(tx->vmptx)) != 0) { 
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("||| ERROR: {0} TX Destroy: {1}.", _result, tx_id));
		return;
	}
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("||| TX Destroyed: {0}.", tx_id));
	created = false;
}
}