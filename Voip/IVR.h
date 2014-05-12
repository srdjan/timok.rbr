#pragma once

#include "ivrlogger.h"
#include "common.h"
#include "channel.h"

#define TiNGTYPE_WINNT
#include "smdrvr.h"

#define _CRTDBG_MAP_ALLOC
#include <stdlib.h>
#include <crtdbg.h>

namespace Timok_IVR {
	ref class IVR : public IIVRPlugin {
	private:
		array<Card^>^ cards;
		IVRLogger^ logger;
		IVRConfiguration^ ivrConfig;
		int	totalPorts;
		int totalCards;
		int totalModules;
		bool shutdown;
		int numberOfChannels;

		bool init_hardware();
		bool cleanup_hardware();	
		void initialize_credentials();

	public:
		IVR();
		~IVR();

		virtual property int IVR::NumberOfChannels { 
			int get() { 
				return numberOfChannels; 
			}	
			void set(int pValue) { 
				numberOfChannels = pValue; 
			} 
		}

		virtual property bool IVR::Shutdown { 
			bool get() { 
				return cards[0]->Shutdown; 
			}	
			void set(bool pValue) { 
				cards[0]->Shutdown = pValue; 
			} 
		}

		virtual void IVR::Start(IVRConfiguration^ pIVRConfiguration) {
			//DEBUGGING:
			//goes to the end of the program:
			//_CrtDumpMemoryLeaks();
			//goes to the begining of the program:
			//_CrtSetDbgFlag ( _CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF );

			ivrConfig = pIVRConfiguration;

			logger = gcnew IVRLogger(gcnew String("CC"), Path::Combine(ivrConfig->RbrRoot, "Log"));
			logger->Log(INF, __FILE__, __LINE__, "Initializing hardware...");

			Thread::Sleep(ivrConfig->TimeToWaitBeforeHWInit * 1000);
			if (false == init_hardware()) {
				throw gcnew Exception("\n Error Initializing Hardware...");
			}
			logger->Log(INF, __FILE__, __LINE__, "Initialized hardware...");
		}
		
		virtual ISessionChannel^ IVR::CreateChannel(int pModuleNumber, int pChannelNumber) {
			return gcnew Channel(cards[0], pModuleNumber, pChannelNumber, logger);
		}

		virtual void IVR::Stop() {
			logger->Log(INF, __FILE__, __LINE__, "Cleanup hardware...");
			if (false == cleanup_hardware()) {
				throw gcnew Exception("\n Error Cleaningup hardware...");
			}
		}	
	};
}