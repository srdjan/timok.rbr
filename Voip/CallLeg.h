#pragma once

#include "common.h"
//#include "ivrlogger.h"
#include "card.h"
#include "callhandle.h"
#include "mediaoffer.h"
#include "mediamanager.h"

namespace Timok_IVR {
	ref class CallLeg {
	private:
		String^ name;
		int module_number;
		int call_number;
		ACU_RESOURCE_ID resource_id;
		IVRLogger^ logger;

	public:
		String^ SignalingIPAddress;
		ACU_SHORT	SignalingPort;

		String^ MediaIPAddress;
		ACU_USHORT	MediaPort;

		CallHandle^ Call_handle;
		bool Disconnect_called;
		
//		AutoResetEvent^ autoResetEvent;
//		RegisteredWaitHandle^ RegisteredHandle;

		CallLeg(String^ pCallLegName, Card^ pCard, int pModuleNumber, int pCallNumber, IVRLogger^ pLogger) {
			name = pCallLegName;
			resource_id = pCard->Card_info->sip_port_id[pModuleNumber];
			module_number = pModuleNumber;
			call_number = pCallNumber;
			logger = pLogger;
			
			SignalingIPAddress = String::Empty;
			SignalingPort = -1;
			MediaIPAddress = String::Empty;
			MediaPort = (ACU_USHORT) -1;

			Call_handle = gcnew CallHandle(pCard);
			Disconnect_called = false;
			AlocHandle();
//			autoResetEvent = gcnew AutoResetEvent(false);
//			RegisteredHandle = nullptr;
//			autoResetEventIn->Handle = *(gcnew IntPtr(Call_handle_in->WaitObject));
		}		

		~CallLeg() {}
		
//		virtual property AutoResetEvent^ WaitObjectIn { AutoResetEvent^ get() { return autoResetEventIn; }	}		

		void AlocHandle() {
			Call_handle->Aloc(resource_id);
		}

		void Display() {
			logger->Log(module_number, call_number, INF, __FILE__, __LINE__, 
									String::Format("Display: {0}, acu_handle: {1}, sip_handle: {2}", 
									name, Call_handle->Acu_handle, Call_handle->Sip_handle));
		}

		STATE_XPARMS Get_event(int pWaitTime, bool pDisposeDetails) {
			int	_result = 0;
			STATE_XPARMS _state_xparms;
			INIT_ACU_CL_STRUCT(&_state_xparms);
			_state_xparms.state = RET_NO_EVENT_AVAILABLE;

			DWORD _wait_result = WaitForSingleObject(Call_handle->Wait_object->wait_object, pWaitTime);
			if (_wait_result == WAIT_TIMEOUT) {
				return _state_xparms;
			}
			if (_wait_result != WAIT_OBJECT_0) {
				throw gcnew Exception(String::Format("ERROR: UNEXPECTED: get_event.WaitForSingleObject: {0}", _result));
			}

			ACU_EVENT_QUEUE_PARMS	_event_queue_parms;
			INIT_ACU_STRUCT(&_event_queue_parms);
			_event_queue_parms.queue_id = Call_handle->Event_queue->queue_id; 
			if ((_result = acu_get_event_from_queue(&_event_queue_parms)) != 0) {
				throw gcnew Exception(String::Format("ERROR: get_event: {0}, acu_get_event_from_queue()", _result));
			}

			_state_xparms.timeout = 0;
			_state_xparms.handle = Call_handle->Acu_handle == 0 ? Call_handle->Sip_handle : _event_queue_parms.context;
			if ((_result = call_event(&_state_xparms)) != 0)	{
				throw gcnew Exception(String::Format("ERROR: get_event: {0}, call_event()", _result));
			}
			//if it is a first call set context Acu_handle
			if (Call_handle->Acu_handle == 0) {
				Call_handle->Acu_handle = _event_queue_parms.context;
				//logger->Log(module_number, call_number, INF, __FILE__, __LINE__, 
				//					String::Format("Get_event: Leg: {0}, acu_handle: {1}, sip_handle: {2}", 
				//					name, Call_handle->Acu_handle, Call_handle->Sip_handle));
			}

			if (pDisposeDetails) {
				SIP_DETAIL_PARMS _details;
				INIT_ACU_CL_STRUCT(&_details);
				_details.handle = Call_handle->Sip_handle;
				if ((_result = sip_details(&_details)) != 0) {
					throw gcnew Exception(String::Format("ERROR: get_event: {0}, sip_details.", gcnew String(error_2_string(_result))));
				}
				free_details(&_details);
			}
			return _state_xparms;
		}

		ACU_USHORT RedirectTo(ACU_MEDIA_OFFER_ANSWER* pMediaOffer) {
			SIP_MEDIA_PROPOSE_PARMS propose_parms;
			INIT_ACU_STRUCT(&propose_parms);
			propose_parms.handle = Call_handle->Acu_handle;
			propose_parms.media_offer_answer = *pMediaOffer;
			int	_result = 0;
			if ((_result = sip_media_propose(&propose_parms)) != 0) {
				throw gcnew Exception(String::Format("ERROR: RedirectTo: sip_media_propose: {0}", _result));
			}

			//-- wait for EV_MEDIA or EV_MEDIA_REJECT_PROPOSAL
			STATE_XPARMS _evt = Get_event(15000, false);	//TODO: make configurable
			if (_evt.state == RET_NO_EVENT_AVAILABLE) {		//timeout
				throw gcnew Exception("ERROR: RedirectTo: Timeout !");
			}
			
			if (_evt.state == EV_MEDIA_REJECT_PROPOSAL) {		//TODO: chance to send another media offer
				throw gcnew Exception(String::Format("RedirectTo: received: EV_MEDIA_REJECT_PROPOSAL !"));
			}

			if (_evt.state != EV_MEDIA) {
				throw gcnew Exception(String::Format("ERROR: RedirectTo: Unexpected: {0}: {1}", _evt.state, gcnew String(call_event_2_string(_evt.state))));
			}

			ACU_USHORT _port = 0;
			SIP_DETAIL_PARMS _details;
			INIT_ACU_CL_STRUCT(&_details);
			try {
				_details.handle = Call_handle->Acu_handle;
				if ((_result = sip_details(&_details)) != 0)	{
					throw gcnew Exception(String::Format("ERROR: Redirect.sip_details: {0}", _result));
				}
				_port = _details.media_session->received_media.media_descriptions->port;
			}
			finally {
				free_details(&_details);
			}
			return _port;
		}

		//----------------------- Private --------------------------------------------------
		int free_details(SIP_DETAIL_PARMS *pDetails) {
			ACU_ERR _result = 0;
			if ((_result = sip_free_details(pDetails)) != 0) {
				throw gcnew Exception(String::Format("ERROR: {0}, free details.", gcnew String(error_2_string(_result))));
			}
			return _result;
		}

		String^ Get_dnis(String^ pString) {
			if (pString->StartsWith(SIP_ADDR_PREFIX)) {
				pString = pString->Remove(0, 4);
			}

			int _index = pString->IndexOf(SIP_ADDR_SUFIX_HEADER);
			if (_index <= 0) {
				return String::Empty;
			}
			pString = pString->Remove(_index);

			pString = pString->TrimStart('"')->TrimEnd('"');
			pString = pString->Replace(URL_ENCODED_POUND_SIGN, POUND_SIGN);
			return pString;
		}

		// <sip:54983%2318764704077@204.8.53.150>;tag=ds-77d2-16b38392
		// <sip:001@216.48.184.52:5060;user=phone>;tag=15183
		String^ Get_ani(String^ pString) {
			if (pString->StartsWith(SIP_ADDR_PREFIX)) {
				pString = pString->Remove(0, 4);
			}

		// 54983%2318764704077@204.8.53.150>;tag=ds-77d2-16b38392
		// 001@216.48.184.52:5060;user=phone>;tag=15183
			int _index = pString->IndexOf(SIP_ADDR_SUFIX_HEADER);
			if (_index > 0) {
				pString = pString->Remove(_index);
			}

		// 54983%2318764704077@204.8.53.150>;tag=ds-77d2-16b38392
		// 001@216.48.184.52:5060;user=phone>;tag=15183
			return pString->TrimStart('"')->TrimEnd('"');
		}

		//sip:192.168.1.1
		//sip:1231231@129.1.1.12
		//sip:1231231@129.1.1.12:4567
		//sip:54983%2318764704077@204.8.53.150>;tag=ds-77d2-16b38392
		//sip:001@216.48.184.52:5060;user=phone>;tag=15183
		String^ Get_sip_address(String^ pString) {
			if (pString->StartsWith(SIP_ADDR_PREFIX)) {
				pString = pString->Remove(0, 4);
			}

			// remove prefix
			int _index = pString->IndexOf(SIP_ADDR_SUFIX_HEADER);
			if (_index > 0) {
				pString = pString->Remove(0, _index+1);
			}

			// remove port
			_index = pString->IndexOf(SIP_PORT_DELIMETER);
			if (_index > 0) {
				pString = pString->Remove(_index);
			}

			pString = pString->TrimStart('"')->TrimEnd('"');
			//HACK:
			if (pString->StartsWith("192.")) {
				return "192.168.1.10";
			}
			return pString;
		}

		// <sip:54983%2318764704077@204.8.53.150>;tag=ds-77d2-16b38392
		// <sip:001@216.48.184.52:5060;user=phone>;tag=15183
		ACU_SHORT Get_sip_port(String^ pString) {
			//-- remove: sip:
			if (pString->StartsWith(SIP_ADDR_PREFIX)) {
				pString = pString->Remove(0, 4);
			}

			//-- remove: everything before the port number, including ':'
			int _index = pString->IndexOf(SIP_PORT_DELIMETER);
			if (_index <= 0) {
				return 5060;
			}			
			pString = pString->Remove(0, _index+1);
			
			//-- remove everything after the port number
			_index = pString->IndexOf(SIP_MESSAGE_DELIMETER);
			if (_index > 0) {
				pString = pString->Remove(_index);
			}			

			pString = pString->TrimStart('"')->TrimEnd('"');
			Int16 _port = System::Convert::ToInt16(pString);
			return _port > 0 ? _port : 5060;
		}

		void FreeHandle() {
			Call_handle->Free();
		}
	};
}
