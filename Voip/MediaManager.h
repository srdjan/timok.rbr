#pragma once

#include "common.h"
#include "ivrlogger.h"
#include "card.h"
#include "player.h"
#include "recorder.h"

namespace Timok_IVR {
ref class MediaManager {
private:
	Card^ card;
	int	module_number;
	int	call_number;
	IVRLogger^ logger;

	RX_Resource^ rx;
	TX_Resource^ tx;

	Player^ player;
	Recorder^ recorder;

	bool allocate_channel();
	void release_channel();

public:
	MediaManager(Card^ pCard, int pModuleNumber, int pChannelNumber, IVRLogger^ pLogger);
	~MediaManager();

	long Channel_id;

	property String^ DTMF_buffer {
    String^ get() { 
			return rx->DTMF_buffer; 
		}
	}

	property bool DTMF_received {
		bool get() {
			return rx->DTMF_buffer->Length > 0 || rx->Got_terminator;
		}
	}

	property bool Got_terminator {
		bool get() {
			return rx->Got_terminator;
		}
	}
	
	property ACU_USHORT MediaPort {
		ACU_USHORT get() {
			return rx->RTP_port;
		}
	}

	property bool Playing_complete {
		bool get() {
			return player->Is_complete;
		}
	}

	property bool Recording_complete {
		bool get() {
			return recorder->Is_complete;
		}
	}

	bool Create();
	bool Config(SIP_DETAIL_PARMS pSIP_details);
	void Play_tone();

	void Play(StringCollection^ pFileNames, bool pShouldResetDTMFBuffer);
	void Stop_play();

	void Record(String^ pFileName, int pTimeLimit);
	void Stop_record();

	void Destroy();
};
}