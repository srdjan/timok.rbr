//--
#include "common.h"
#include "ivrlogger.h"

IVRLogger::IVRLogger(String^ pLogType, String^ pLogFolder) { 
	LogLevel	= 1; //Default is to turn logging on.//
	log_type = pLogType;
	logFolder = pLogFolder;
	perChannelLogFolder = Path::Combine(logFolder, "PerChannel");
	createLogFolders();
}

IVRLogger::~IVRLogger() { }

int IVRLogger::Log(int iType, String^ pFile, long lLine, String^ sMessage) {
	return log(0, 0, SYSTEM, iType, pFile, lLine, sMessage);
}

int IVRLogger::Log(int pModuleNumber, int pChannelNumber, int iType, String^ pFile, long lLine, String^ sMessage) {
	return log(pModuleNumber, pChannelNumber, CHANNEL, iType, pFile, lLine, sMessage);
}

//---------------------------- Private --------------------------------------------------------------------------
int IVRLogger::log(int pModuleNumber, int pChannelNumber, int pRelated_to, int pType, String^ pFile, long pLine, String^ pMessage) {
	if (LogLevel == 0) {//Logging is turned off.//
		return RET_SUCCESS;
	}

	try	{
		createLogFolders();

		String^ _message;
		if (pType == SPC_1 || pType == SPC_2) {
			pRelated_to = pType;
			pType = DBG;
			_message = String::Format("[{0},{1}]{2}[{3}][{4}, {5}] - {6}", pModuleNumber, pChannelNumber, getLogType(pType), 
																																								 System::DateTime::Now.ToLongTimeString(), 
																																								 pLine, 
																																								 pFile, 
																																								 pMessage);
		}
		else {
		 _message = String::Format("{0}[{1}][{2}, {3}] - {4}", getLogType(pType), System::DateTime::Now.ToLongTimeString(), 
																																								 pLine, 
																																								 pFile, 
																																								 pMessage);
		}

		StreamWriter^ _sw = nullptr;
		try {
			Monitor::Enter(padlock);
			String^ _fileName;
			if (pRelated_to == SYSTEM) {
				_fileName = String::Format("{0}\\IVR_System.{1}.log", logFolder, DateTime::Now.ToString("yyyy-MM-dd-HH"));
			}
			else if (pRelated_to == SPC_1) {
				_fileName = String::Format("{0}\\IVR_Debug.{1}.log", logFolder, DateTime::Now.ToString("yyyy-MM-dd-HH"));
			}
			else if (pRelated_to == SPC_2) {
				_fileName = String::Format("{0}\\IVR_Debug_1.{1}.log", logFolder, DateTime::Now.ToString("yyyy-MM-dd-HH"));
			}
			else {
				_fileName = String::Format("{0}\\{1}_Channel.{2}-{3}_{4}.log", perChannelLogFolder, log_type, pModuleNumber, pChannelNumber, DateTime::Now.ToString("yyyy-MM-dd-HH"));
			}
			_sw = gcnew StreamWriter(_fileName, true);
			_sw->WriteLine(_message);
		}
		finally {
			if (_sw != nullptr) {
				_sw->Close();
			}
			Monitor::Exit(padlock);
		}
	}
	catch(Exception^ _ex) {
		EventLog::WriteEntry("Controller ServiceException - Log_message{%s}", _ex->ToString(), EventLogEntryType::Error);
		return RET_FAIL;
  }
	return RET_SUCCESS;
}

void IVRLogger::createLogFolders() {
	if ( ! Directory::Exists(logFolder)) {
		Directory::CreateDirectory(logFolder);
	}

	if ( ! Directory::Exists(perChannelLogFolder)) {
		Directory::CreateDirectory(perChannelLogFolder);
	}
}

String^ IVRLogger::getLogType(int iType) {
		if (iType == INF) {
			return LOG_INFO;
		}
		else if (iType == DBG) {
			return LOG_DEBUG;
		}
		else if (iType == ERR)	{
			return LOG_ERROR;
		}
		else if (iType == WRN) {
			return LOG_WARNING;
		}
		else if (iType == CRT) {
			return LOG_CRITICAL;
		}
		else {
			return "--???--";
		}
}
