#pragma once

#include "common.h"
#include "ivrlogger.h"
#include "card.h"

namespace Timok_IVR {
ref class TX_Resource {
private:
	Card^ card;
	int	module_number;
	int	call_number;
	IVRLogger^ logger;

	SM_VMPTX_CREATE_PARMS	*tx;
	HANDLE tx_event;
	String^ tx_id;

	bool create();
	bool wait_for_start();
	bool connect();
	void disconnect();
	bool stop();
	void destroy();
	
	bool created;
	bool connected;
	bool running;

	void wait_for_status_running();
	bool wait_for_status_stopped();

public:
	TX_Resource(Card^ pCard, int pModuleNumber, int pChannelNumber, IVRLogger^ pLogger);
	~TX_Resource();

	long Channel_id;

	property tSMVMPtxId Vmptx {
		tSMVMPtxId get() {
			return tx->vmptx;
		}
	}

	bool CreateAndConnect(long Channel_id);
	bool Config(SIP_DETAIL_PARMS pSIP_details);
	void Destroy();
};
}