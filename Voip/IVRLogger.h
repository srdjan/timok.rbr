#pragma once

#include "common.h"

#define LOG_INFO			"[INF]"
#define LOG_DEBUG			"[DBG]"
#define LOG_ERROR			"[ERR]"
#define LOG_WARNING		"[WRN]"
#define LOG_CRITICAL	"[CRT]"

ref class IVRLogger {
private:
	static Object^ padlock = gcnew Object();
		
	String^ log_type;

	int log(int iModuleNumber, int iChannelNumber, int iRelated_to, int iType, String^ pFile, long lLine, String^ sMessage);
	String^ getLogType(int iType);
	String^ logFolder;
	String^ perChannelLogFolder;
	void createLogFolders();
public:
	IVRLogger(String^ pLogType, String^ pLogFolder);
	~IVRLogger();

	int	LogLevel;
	int Log(int iType, String^ pFile, long lLine, String^ sMessage);
	int Log(int iModuleNumber, int iChannelNumber, int iType, String^ pFile, long lLine, String^ sMessage);
};
