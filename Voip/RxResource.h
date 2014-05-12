#pragma once

#include "common.h"
#include "ivrlogger.h"
#include "card.h"

namespace Timok_IVR {
ref class RX_Resource {
private:
	Card^ card;
	int	module_number;
	int	call_number;
	IVRLogger^ logger;

	long channel_id;

	SM_VMPRX_CREATE_PARMS* rx;
	HANDLE rx_event;
	String^ rx_id;

	ThreadStart^ thread_start;
	Thread^	rx_thread;
	void thread();

	ACU_USHORT rtp_port;

	HANDLE stopped_event;
	HANDLE started_event;

	String^	dtmf_buffer;
	bool got_terminator;

	bool create();
	bool connect();
	bool wait_for_start();
	void disconnect();
	bool stop();
	void destroy();

	bool created;
	bool started;
	bool connected;

public:
	RX_Resource(Card^ pCard, int pModuleNumber, int pChannelNumber, IVRLogger^ pLogger);
	~RX_Resource();

	property long Channel_id {
		long get() {
			return channel_id;
		}
	}

	property String^ DTMF_buffer {
		String^ get() { 
			return dtmf_buffer; 
		}
	}

	property bool Got_terminator {
		bool get() {
			return got_terminator;
		}
	}

	property ACU_USHORT RTP_port {
		ACU_USHORT get() {
			return rtp_port;
		}
	}

	bool CreateAndConnect(long Channel_id);
	bool Config(SIP_DETAIL_PARMS pSIP_details);
	void Reset();
	void Destroy();
};
}