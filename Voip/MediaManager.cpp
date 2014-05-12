//--
#include "mediamanager.h"

namespace Timok_IVR {
MediaManager::MediaManager(Card^ pCard, int pModuleNumber, int pCallNumber, IVRLogger^ pLogger) {
	card = pCard;
	module_number = pModuleNumber;
	call_number = pCallNumber;
	logger = pLogger;

	Channel_id = -1;

	rx = gcnew RX_Resource(card, module_number, call_number, logger); 
	tx = gcnew TX_Resource(card, module_number, call_number, logger); 

	player = gcnew Player(card, module_number, call_number, logger);
	recorder = gcnew Recorder(card, module_number, call_number, logger);
}

MediaManager::~MediaManager() { }

bool	MediaManager::Create() {
	if ( ! allocate_channel()) {
		return false;
	}

	if ( ! rx->CreateAndConnect(Channel_id)) {
		return false;
	}

	if ( ! tx->CreateAndConnect(Channel_id)) {
		return false;
	}
	return true;
}

bool MediaManager::Config(SIP_DETAIL_PARMS pSIP_details) {
	if ( ! rx->Config(pSIP_details)) {
		return false;
	}
	if ( ! tx->Config(pSIP_details)) {
		return false;
	}
	return true;
}

void MediaManager::Play_tone() {
	player->Play_tone(tx);
}

void MediaManager::Play(StringCollection^ pFileNames, bool pShouldResetDTMFBuffer) {
	if (pShouldResetDTMFBuffer) {
		rx->Reset();
	}
	player->Play(tx, pFileNames);
}

void MediaManager::Stop_play() {
	player->Close();
}

void MediaManager::Record(String^ pFileName, int pTimeLimit) {
	rx->Reset();
	recorder->Record(rx->Channel_id, pFileName, pTimeLimit);
}

void MediaManager::Stop_record() {
	recorder->Close();
}

void MediaManager::Destroy() {
	rx->Destroy();
	tx->Destroy();
	release_channel();
}

//------------------------------- Private ----------------------------------------------------------
bool MediaManager::allocate_channel() {
	if (Channel_id != -1) {
		logger->Log(ERR, __FILE__, __LINE__, String::Format("LEAK: {0}, Channel Still NOT Released!", Channel_id));
		return false;
	}

	int _result = 0;
	SM_MODULE_INFO_PARMS _module_info_parms;
	INIT_ACU_SM_STRUCT(&_module_info_parms);
	_module_info_parms.module = card->Card_info->module_id[module_number];
	if ((_result = sm_get_module_info(&_module_info_parms)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, Module: {1}, sm_get_module_info.", _result, module_number));
		return false;
	}

	SM_CHANNEL_ALLOC_PLACED_PARMS	_channel_alloc;
	INIT_ACU_SM_STRUCT(&_channel_alloc);
	_channel_alloc.type = kSMChannelTypeFullDuplex; //NOTE: full duplex is recomended...
	_channel_alloc.module = card->Card_info->module_id[module_number];
	if ((_result = sm_channel_alloc_placed(&_channel_alloc)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, NO Channels available!", _result));
		return false;
	}

	Channel_id = (long) _channel_alloc.channel;
	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Allocated channel: {0}.", Channel_id));
	return true;
}

void MediaManager::release_channel() {
	if (Channel_id == -1) {
		logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "Channel was not allocated.");
		return;
	}

	int _result = 0;
	SM_MODULE_INFO_PARMS _module_info_parms;
	INIT_ACU_SM_STRUCT(&_module_info_parms);
	_module_info_parms.module = card->Card_info->module_id[module_number]; 
	if ((_result = sm_get_module_info(&_module_info_parms)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, Deallocating, sm_get_module_info", _result));
		return;
	}
	if ((_result = sm_channel_release((tSMChannelId) Channel_id)) != 0) {
		logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, Deallocating sm_channel_release", _result));
		return;
	}

	logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Deallocated channel: {0}.", Channel_id));
	Channel_id = -1;
}
}