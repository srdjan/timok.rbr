#pragma once
#define TiNGTYPE_WINNT

#include "common.h"
#include "ivrlogger.h"
#include "txresource.h"
#include "rxresource.h"

namespace Timok_IVR {
ref class Recorder {
private:
	Card^ card;
	int	module_number;
	int	call_number;
	IVRLogger^ logger;

	SM_FILE_RECORD_PARMS* record_parms;

	ThreadStart^ thread_start;
	Thread^	worker_thread;
	void thread();

	String^ file_to_record;

	int time_limit;

public:
	Recorder(Card^ pCard, int pModuleNumber, int pCallNumber, IVRLogger^ pLogger);
	~Recorder();

	long Channel_id;

	bool Should_abort;
	bool Is_complete;

	void Record(long pChannelId, String^ pFile_path, int pTimeLimit);
	void Close();
};
}