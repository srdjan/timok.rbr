#pragma once

#include "common.h"
#include "ivrlogger.h"
#include "txresource.h"

namespace Timok_IVR {
ref class Player {
private:
	Card^ card;
	int	module_number;
	int	call_number;
	IVRLogger^ logger;

	String^ tx_id;
	TX_Resource^ tx;

	ThreadStart^ thread_start;
	Thread^	worker_thread;
	void thread();

	VoiceFileFormat fileType;

	array<String^>^ files_to_play;
	SM_FILE_REPLAY_PARMS	*play_parms;

	VoiceFileFormat getFileType(String^ pFileName);

	int play_raw(char *pFile_name);
	void abort_play_raw();

	int play_wav(char *pFile_name);
	//void abort_play_wav();

	bool should_abort;
	bool aborted;

public:
	Player(Card^ pCard, int pModuleNumber, int pCallNumber, IVRLogger^ pLogger);
	~Player();

	long Channel_id;
	bool Is_complete;
	void Play(TX_Resource^ pTx, StringCollection^ pFile_names_to_play);
	void Close();
	int Play_tone(TX_Resource^ pTx);
};
}