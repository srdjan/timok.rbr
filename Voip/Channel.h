#pragma once

#include "common.h"
#include "ivrlogger.h"
#include "card.h"
#include "mediaoffer.h"
#include "mediamanager.h"
#include "callleg.h"

namespace Timok_IVR {
	ref class Channel : public ISessionChannel {
	private:
		Card^ card;
		int moduleNumber;
		int callNumber;
		IVRLogger^ logger;

		CallLeg^ callLeg1;
		CallLeg^ callLeg2;

		MediaManager^ mediaManager1;
		//MediaManager^ mediaManager2;

		String^ callID;
		String^ dnis;
		String^ ani;
		String^ origAddr;
		MediaOffer^ media_offer;

		int disconnect_cause;
		int disconnect_source;

		int total_time_ringing;

		long get_event_leg1(int pWaitTime, bool pDisposeDetails);
		long get_event_leg2(int pWaitTime, bool pDisposeDetails);

		//void initMakeCall(String^ pANI, String^ pDestNumber, String^ pDestIPAddress, String^ pCustomHeaderSource);
		int checkEventOut();
		int checkEventIn();

		bool tryRinging(bool pShouldRing);

		bool disconnect(bool pLeg1);
		int get_cause(ACU_CALL_HANDLE pCallHandle, int pDisconnectSource);
		void set_sip_out(SIP_OUT_PARMS* sip_out, String^ pANI, String^ pDest_ip_address, String^ pDestNumber, String^ pCustomeHeaderSource);
		void free_sip_out(SIP_OUT_PARMS* sip_out);
		String^ extractCallID(unsigned char *pSIPMessage, int pLength);

		//void waitForCallBegin();
		//void waitForCallEnd(Object^ pState, bool pTimedOut);

		void log(int iType, long pLine, String^ pMessage);
		int free_details(SIP_DETAIL_PARMS *pDetails);

	public:
		Channel(Card^ pCard, int pModuleNumber, int pCallNumber, IVRLogger^ pLogger);
		~Channel();

		//-- External Interface IChannel
		//----------------------------------------------------------------------------------------------------
		virtual property String^ ChannelId { String^ get() { return String::Format("{0}-{1}-{2}", card->Number, moduleNumber, callNumber); }	}
		virtual property String^ CallId { String^ get() { return callID; }	}
		virtual property String^ OrigIPAddress { String^ get() { return callLeg1->SignalingIPAddress; }	}
		virtual property String^ AccessNumber { String^ get() { return dnis; }	}
		virtual property String^ ANI { String^ get() { return ani; }	}

		virtual property int DisconnectCause { int get() { return disconnect_cause; }	}		
		virtual property int DisconnectSource {	int get() { return disconnect_source; } }

		virtual bool WaitForCall();
		virtual void SendCallProgress();
		virtual void SendRinging();
		virtual void SendAcceptCall(String^ pCallType);
		virtual void WaitForEventIn();

		virtual void CreateResources();
		virtual void ConfigResources();
		virtual void DestroyResources();

		virtual void PlayTone();
		virtual void Play(StringCollection^ pFileNames, Boolean pStopPlayOnDTMF, bool pShouldResetDTMFBuffer);
		virtual void Record(String^ pFileName, int pTimeLimit, bool pShouldStopOnTerminator);
		virtual bool CollectDTMF(int pLength, String^% pDTMFBuffer);
		virtual bool CollectDTMF(int pMinLength, int pMaxLength, String^% pDTMFBuffer);

		virtual bool MakeCall(String^ pANI, String^ pPrefixAndDestNumber, String^ pDest_ip_address, bool pShouldRing, String^ pCustomHeaderSource);
		virtual bool RedirectMedia();
		virtual void WaitForCallEnd(int pTimeLimit);
		virtual void DisconnectLeg1(); 
		virtual void DisconnectLeg2(); 
		virtual void DisconnectAll();
	};
}
