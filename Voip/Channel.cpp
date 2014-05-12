#include "channel.h"

namespace Timok_IVR {
	Channel::Channel(Card^ pCard, int pModuleNumber, int pCallNumber, IVRLogger^ pLogger) { 
		card = pCard;
		moduleNumber = pModuleNumber;
		callNumber = pCallNumber;
		logger = pLogger;
		
		callLeg1 = nullptr;
		callLeg2 = nullptr;

		total_time_ringing = 0;

		mediaManager1 = gcnew MediaManager(card, pModuleNumber, pCallNumber, pLogger); 
		media_offer = gcnew MediaOffer(card, pModuleNumber, pCallNumber, pLogger);
	}

	Channel::~Channel() { }

	bool Channel::WaitForCall() {
		String^ _temp = String::Empty;	//TODO: refactor!
		callID = String::Empty;
		dnis = String::Empty;
		ani = String::Empty;
		disconnect_cause = LC_UNKNOWN;
		disconnect_source = SOURCE_IVR;

		try {
			callLeg1 = gcnew CallLeg("Leg1", card, moduleNumber, callNumber, logger);

			SIP_IN_PARMS _sip_open_in;
			INIT_ACU_CL_STRUCT(&_sip_open_in);
			_sip_open_in.net = card->Card_info->sip_port_id[moduleNumber];
			_sip_open_in.queue_id = callLeg1->Call_handle->Event_queue->queue_id;
			_sip_open_in.request_notification_mask = ACU_SIP_INITIAL_INVITE_NOTIFICATION;
			int	_result	= 0;
			if ((_result = sip_openin(&_sip_open_in)) != 0) {
				log(ERR, __LINE__, String::Format("WaitForCall.sip_openin: {0}", gcnew String(error_2_string(_result))));
				if (_result == -507) {		//ERR_SERVER_NOT_RESPONDING
					card->Shutdown = true;
				}
				return false;
			}
			callLeg1->Call_handle->Sip_handle = _sip_open_in.handle;

			while ( ! card->Shutdown) {
				long _event_in = get_event_leg1(1000, false);
				if (_event_in == RET_NO_EVENT_AVAILABLE) {
					continue;
				}
				SIP_DETAIL_PARMS _details;
				INIT_ACU_CL_STRUCT(&_details);
				_details.handle = callLeg1->Call_handle->Sip_handle;
				if ((_result = sip_details(&_details)) != 0) {
					log(ERR, __LINE__, String::Format("WaitForCall.sip_details: {0}", gcnew String(error_2_string(_result))));
					return false;
				}

				try {
					switch (_event_in) {
						case EV_WAIT_FOR_INCOMING:
							log(INF, __LINE__, "WaitForCall: EV_WAIT_FOR_INCOMING");
							break;
							
						case EV_DETAILS:
							log(INF, __LINE__, "WaitForCall: EV_DETAILS");
							callID = extractCallID(_details.sip_message->message, _details.sip_message->message_length);
							log(INF, __LINE__, String::Format("WaitForCall: EV_DETAILS: CallID: {0}", callID));
							return true; 

						case EV_INCOMING_CALL_DET:
							log(INF, __LINE__, "WaitForCall: EV_INCOMING_CALL_DET");
									
							//-- called
							_temp = gcnew String(_details.destination_addr);
							dnis = callLeg1->Get_dnis(_temp);
							if (dnis->Length == 0) {
								log(ERR, __LINE__, String::Format("WaitForCall: EV_INCOMING_CALL_DET: Invalid called uri: {0}", _temp));		
								return false;
							}
							log(INF, __LINE__, String::Format("WaitForCall: EV_INCOMING_CALL_DET: called: {0}, DNIS: {1}", _temp, dnis));		

							//-- caller
							_temp = gcnew String(_details.originating_addr);
							ani = callLeg1->Get_ani(_temp);
							if (ani->Length == 0) {
								log(ERR, __LINE__, String::Format("WaitForCall: EV_INCOMING_CALL_DET: Invalid caller uri: {0}", _temp));		
								return false;
							}
							log(INF, __LINE__, String::Format("WaitForCall: EV_INCOMING_CALL_DET: caller: {0}, ANI: {1}", _temp, ani));		

							callLeg1->SignalingIPAddress = callLeg1->Get_sip_address(_temp);
							callLeg1->SignalingPort = callLeg1->Get_sip_port(_temp);
							log(INF, __LINE__, String::Format("WaitForCall: EV_INCOMING_CALL_DET: orig signaling address: {0}:{1}", callLeg1->SignalingIPAddress, callLeg1->SignalingPort));

							//-- remote media
							callLeg1->MediaIPAddress = gcnew String(_details.media_offer_answer->connection_address.address);
							callLeg1->MediaPort = _details.media_offer_answer->media_descriptions->port;
							log(INF, __LINE__, String::Format("WaitForCall: EV_INCOMING_CALL_DET: orig media address: {0}:{1}", callLeg1->MediaIPAddress, callLeg1->MediaPort));

							//-- set local Media
							if (media_offer->Populate(_details.media_offer_answer, card->Card_info->Media_IP_address, LOCAL_RTP_PORT) != RET_SUCCESS) {
								return false;
							}
							media_offer->Display();
							break;

						default:
							log(INF, __LINE__, String::Format("WaitForCall: UNEXPECTED EVENT: {0}", gcnew String(call_event_2_string(_event_in))));
							break;
					}
				}
				catch (Exception^ _ex) {
					log(ERR, __LINE__, String::Format("WaitForCall: process event Exception:\r\n{0}", _ex));
					return false;
				}
				finally {
					free_details(&_details);
				}
			}
		}
		catch (Exception^ _ex) {
			log(ERR, __LINE__, String::Format("WaitForCall: Exception:\r\n{0}", _ex));
		}
		return false;
	}

	void Channel::SendCallProgress() {		// Send call-progress to origination
		log(INF, __LINE__, "SendCallProgres: Start");
		SIP_PROGRESS_PARMS _progress;
		INIT_ACU_CL_STRUCT(&_progress);
		_progress.handle = callLeg1->Call_handle->Sip_handle;
		_progress.send_reliable_provisional_response = 0;
		_progress.media_offer_answer = media_offer->Current;

		int	_result	= 0;
		if ((_result = sip_progress(&_progress)) != 0) {
			log(ERR, __LINE__, String::Format("SendCallProgress.sip_progress: {0}", gcnew String(error_2_string(_result))));
			throw gcnew Exception(String::Format("ERROR: SendCallProgress.sip_progress: {0}", gcnew String(error_2_string(_result))));
		}
		log(INF, __LINE__, "SendCallProgres: Sent");
	}

	void Channel::SendRinging() {		// Send ringing to origination
		log(INF, __LINE__, "SendRinging: Start");
		SIP_INCOMING_RINGING_PARMS _ringing_parms;
		INIT_ACU_CL_STRUCT(&_ringing_parms);
		_ringing_parms.handle = callLeg1->Call_handle->Sip_handle;

		int	_result	= 0;
		if ((_result = sip_incoming_ringing(&_ringing_parms)) != 0)	{
			log(ERR, __LINE__, String::Format("SendRinging.sip_incoming_ringing: {0}", gcnew String(error_2_string(_result))));
			throw gcnew Exception(String::Format("ERROR: SendRinging.sip_incoming_ringing: {0}", gcnew String(error_2_string(_result))));
		}
		log(INF, __LINE__, "SendRinging: Sent");

		long _event_in = get_event_leg1(3000, true);
		if (_event_in != EV_WAIT_FOR_ACCEPT) {
			log(ERR, __LINE__, String::Format("SendRinging, expecting EV_WAIT_FOR_ACCEPT, got {0}:{1}", _event_in, gcnew String(error_2_string(_event_in))));
			throw gcnew Exception(String::Format("ERROR: SendRinging, expecting EV_WAIT_FOR_ACCEPT, got {0}:{1}", _event_in, gcnew String(error_2_string(_event_in))));
		}
		log(INF, __LINE__, "SendRinging: ACCEPT received");
	}	

	void Channel::SendAcceptCall(String^ pCallType) {		// Accept the call - sends a 200 OK response to the far end with SDP body.
		log(INF, __LINE__, "SendAcceptCall: Start");
		SIP_ACCEPT_PARMS _accept;
		INIT_ACU_CL_STRUCT(&_accept);
		_accept.handle = callLeg1->Call_handle->Sip_handle;
		_accept.media_offer_answer = *(media_offer->Current);
		//NOTE: in case of CallRouting vmp resources are not used so port will == zero, then we use port number from the original offer
		_accept.media_offer_answer.media_descriptions->port = mediaManager1->MediaPort == 0 ? _accept.media_offer_answer.media_descriptions->port : mediaManager1->MediaPort;

		int	_result	= 0;
		if ((_result = sip_accept(&_accept)) != 0) {
			log(ERR, __LINE__, String::Format("SendAcceptCall, sip_accept: {0}", gcnew String(error_2_string(_result))));
			throw gcnew Exception(String::Format("ERROR: SendAcceptCall.sip_accept, callType: {0}: {1}", pCallType, gcnew String(error_2_string(_result))));
		}
		log(INF, __LINE__, "SendAcceptCall: Sent");
	}

	void Channel::CreateResources() {		// Create TxRx resources and events
		if ( ! mediaManager1->Create()) {
			throw gcnew Exception("ERROR: CreateResources.callLeg1.Vmp_manager.Create()");
		}
	}

	void Channel::ConfigResources() {
		long _event = get_event_leg1(3000, false);
		if (_event != EV_MEDIA) {
			log(ERR, __LINE__, String::Format("ConfigResources: Unexpected Event: {0}.", gcnew String(error_2_string(_event))));
			throw gcnew RbrException(RbrResult::IVR_ConfigResources, "ConfigResources", String::Format("ERROR: Unexpected Event: {0}", gcnew String(error_2_string(_event))));
		}

		SIP_DETAIL_PARMS _details;
		INIT_ACU_CL_STRUCT(&_details);
		_details.handle = callLeg1->Call_handle->Sip_handle;
		int _result = 0;
		if ((_result = sip_details(&_details)) != 0) {
			log(ERR, __LINE__, String::Format("ConfigResources.sip_details: {0}", gcnew String(error_2_string(_result))));
			throw gcnew Exception(String::Format("ERROR: ConfigResources.sip_details: {0}", gcnew String(error_2_string(_result))));
		}

		//-- check if origination codec from EV_MEDIA is supported
		if (card->IsSupported(_details.media_session->sent_media.media_descriptions->payloads->payload.audio_video.rtp_payload_number) == false) {
			log(ERR, __LINE__, String::Format("ERROR: Codec from EV_MEDIA Not supported: {0}", _details.media_session->sent_media.media_descriptions->payloads->payload.audio_video.rtp_payload_number));
			throw gcnew Exception(String::Format("ERROR: Codec from EV_MEDIA Not supported: {0}", _details.media_session->sent_media.media_descriptions->payloads->payload.audio_video.rtp_payload_number));
		}
	
		//-- Configure vmp resources
		try {
			if ( ! mediaManager1->Config(_details)) {
				log(ERR, __LINE__, "Vmp_manager->Config()");
				throw gcnew Exception("ERROR: Vmp_manager->Config()");
			}
		}
		finally {
			free_details(&_details);
		}
	}

	void Channel::DestroyResources() {	
		mediaManager1->Destroy();
	}

	//-- add timer: maxTimeToWaitForConnection ?
	void Channel::WaitForEventIn() {		
		log(INF, __LINE__, "WaitForEventIn: Start");

		while ( ! card->Shutdown) {
			long _event_in = get_event_leg1(3000, true);
			switch (_event_in) {
				case EV_CALL_CONNECTED:
					log(INF, __LINE__, "WaitForEventIn: Ok: EV_CALL_CONNECTED");
					return;

				case EV_MEDIA:
					log(INF, __LINE__, "WaitForEventIn: EV_MEDIA");
					break;

				case EV_REMOTE_DISCONNECT:
					log(INF, __LINE__, "WaitForEventIn: EV_REMOTE_DISCONNECT");
					throw gcnew Exception("WaitForEventIn EV_REMOTE_DISCONNECT");
				
				case EV_IDLE:
					log(INF, __LINE__, "WaitForEventIn: EV_IDLE");
					throw gcnew Exception("WaitForEventIn EV_IDLE");
					
				default:
					log(INF, __LINE__, String::Format("WaitForEventIn: UNEXPECTED EVENT: {0}", gcnew String(call_event_2_string(_event_in))));
					break;
				}	
		}
		throw gcnew Exception("ERROR: WaitForEventIn()->Unexpected Exit!");
	}

	void Channel::Record(String^ pFileName, int pTimeLimit, bool pStopOnDTMFTerminator) {
		long _event_in = get_event_leg1(0, true);
		if (_event_in == EV_IDLE || _event_in == EV_REMOTE_DISCONNECT)	{
			throw gcnew RbrException(RbrResult::IVR_Leg1Disconnect, "Record", pFileName);
		}

		bool _should_disconnect = false;
		try	{
			mediaManager1->Record(pFileName, pTimeLimit);	
			while ( ! card->Shutdown) {
				if (pStopOnDTMFTerminator && mediaManager1->Got_terminator) {
					break;
				}

				_event_in = get_event_leg1(250, true);
				if (_event_in == EV_IDLE || _event_in == EV_REMOTE_DISCONNECT)	{
					_should_disconnect = true;
					break;
				}

				if (mediaManager1->Recording_complete) {
					break;
				}
			}

			if (card->Shutdown) {
				_should_disconnect = true;
			}
		}
		catch (Exception^ _ex) {
			log(DBG, __LINE__, String::Format("Record, Exception:\r\n{0}", _ex));
			_should_disconnect = true;
		}
		finally {
			mediaManager1->Stop_record();
		}

		if (_should_disconnect) {
			throw gcnew RbrException(RbrResult::IVR_Leg1Disconnect, "Record", String::Empty);
		}
	}

	void Channel::PlayTone() {
		mediaManager1->Play_tone();
	}

	void Channel::Play(StringCollection^ pFileNames, bool pStopOnDTMFTerminator, bool pShouldResetDTMFBuffer) {
		long _event_in = get_event_leg1(0, true);
		if (_event_in == EV_IDLE || _event_in == EV_REMOTE_DISCONNECT)	{
			throw gcnew RbrException(RbrResult::IVR_Leg1Disconnect, "Play", String::Empty);
		}

		bool _should_disconnect = false;
		try	{
			mediaManager1->Play(pFileNames, pShouldResetDTMFBuffer);
			while ( ! card->Shutdown) {
				if (pStopOnDTMFTerminator && mediaManager1->DTMF_received) {
					break;
				}

				_event_in = get_event_leg1(250, true);
				if (_event_in == EV_IDLE || _event_in == EV_REMOTE_DISCONNECT)	{
					_should_disconnect = true;
					break;
				}

				if (mediaManager1->Playing_complete) {
					break;
				}
			}

			if (card->Shutdown) {
				_should_disconnect = true;
			}
		}
		catch (Exception^ _ex) {
			log(DBG, __LINE__, String::Format("Play, Exception:\r\n{0}", _ex));
		}
		finally {
			mediaManager1->Stop_play();
		}

		if (_should_disconnect) {
			throw gcnew RbrException(RbrResult::IVR_Leg1Disconnect, "Play", String::Empty);
		}
	}

	bool Channel::CollectDTMF(int pLength, String^% pDTMFBuffer) {
		return CollectDTMF(pLength, pLength, pDTMFBuffer);
	}

	bool Channel::CollectDTMF(int pMinLength, int pMaxLength, String^% pDTMFBuffer) {
		pDTMFBuffer = String::Empty;
		int	_timeout = 0;
		int _dtmfBufferLength = mediaManager1->DTMF_buffer->Length;

		while (_timeout < DTMF_WAIT_TIMEOUT) {
			if (card->Shutdown) {
				throw gcnew RbrException(RbrResult::IVR_Collect_DTMF_Shutdown, "CollectDTMF", String::Empty);
			}

			int _event_in = get_event_leg1(100, true);
			if (_event_in == EV_IDLE || _event_in == EV_REMOTE_DISCONNECT)	{
				throw gcnew RbrException(RbrResult::IVR_Leg1Disconnect, "CollectDTMF", String::Empty);
			}

			if (_dtmfBufferLength < mediaManager1->DTMF_buffer->Length) {
				_dtmfBufferLength = mediaManager1->DTMF_buffer->Length;
				_timeout = 0;
			}
			else {
				_timeout += 100;
			}

			if (mediaManager1->DTMF_buffer->Length > pMaxLength)	{
				log(ERR, __LINE__, String::Format("CollectDTMF: Greater then Max Length: {0}", mediaManager1->DTMF_buffer));
				pDTMFBuffer = mediaManager1->DTMF_buffer->Substring(0, pMaxLength);
				return true;
			}

			if (mediaManager1->DTMF_buffer->Length == pMaxLength)	{
				log(INF, __LINE__, String::Format("CollectDTMF: Max Length, completed collection: {0}", mediaManager1->DTMF_buffer));
				pDTMFBuffer = mediaManager1->DTMF_buffer;
				return true;
			}
			
			if (mediaManager1->Got_terminator)	{
				log(INF, __LINE__, String::Format("CollectDTMF: Got Terminator, completed collection: {0}", mediaManager1->DTMF_buffer));
				if (mediaManager1->DTMF_buffer->Length >= pMinLength && mediaManager1->DTMF_buffer->Length <= pMaxLength)	{
					pDTMFBuffer = mediaManager1->DTMF_buffer;
					return true;
				}
				return false;
			}
		} 

		if (mediaManager1->DTMF_buffer->Length >= pMinLength && mediaManager1->DTMF_buffer->Length <= pMaxLength)	{
			log(INF, __LINE__, String::Format("CollectDTMF: Ok: {0}", mediaManager1->DTMF_buffer));
			pDTMFBuffer = mediaManager1->DTMF_buffer;
			return true;
		}

		log(INF, __LINE__, String::Format("CollectDTMF: timeout: {0}", mediaManager1->DTMF_buffer));
		return false;
	}

	bool Channel::MakeCall(String^ pANI, String^ pPrefixAndDestNumber, String^ pDestIPAddress, bool pShouldRing, String^ pCustomHeaderSource) {
		log(INF, __LINE__, String::Format("MakeCall: Start, DestIP: {0}", pDestIPAddress));
		total_time_ringing = 0;

		//initMakeCall(pANI, pDestNumber, pDestIPAddress, pCustomHeaderSource);
		callLeg2 = gcnew CallLeg("Leg2", card, moduleNumber, callNumber, logger);
		callLeg2->SignalingIPAddress = pDestIPAddress; 
		log(INF, __LINE__, String::Format("MakeCall: callLeg2 signaling Address: {0}", pDestIPAddress));

		SIP_OUT_PARMS _sip_out;
		INIT_ACU_STRUCT(&_sip_out);	
		_sip_out.net = card->Card_info->sip_port_id[moduleNumber]; 
		_sip_out.queue_id = callLeg2->Call_handle->Event_queue->queue_id;
		_sip_out.media_offer_answer = media_offer->Current;
		set_sip_out(&_sip_out, pANI, pDestIPAddress, pPrefixAndDestNumber, pCustomHeaderSource);

		int _result = 0;
		if ((_result = sip_openout( &_sip_out )) != 0)	{
			free_sip_out(&_sip_out);
			log(ERR, __LINE__, String::Format("ERROR: MakeCall: sip_openout: {0}", _result));
			throw gcnew RbrException(RbrResult::IVR_SIPOutFailed, String::Format("ERROR: initMakeCall: sip_openout: {0}", _result));
		}
		callLeg2->Call_handle->Sip_handle = _sip_out.handle;
		free_sip_out(&_sip_out);
		
		int _i = 0, _number_of_retries = 80;
		while ( ! card->Shutdown && _i++ < _number_of_retries) {
			int _rbrResult = checkEventOut();
			if (_rbrResult == (int) RbrResult::Success) {
				return true;
			}
			if (_rbrResult != (int) RbrResult::Continue) { 
				log(ERR, __LINE__, "ERROR: MakeCall: Check Outbound Event");
				throw gcnew RbrException((RbrResult)_rbrResult, "[ERROR] MakeCall: CheckOutbound Event");
			}

			if (tryRinging(pShouldRing) == false) {
				log(ERR, __LINE__, "ERROR: MakeCall: No Answer");
				throw gcnew RbrException(RbrResult::IVR_NoAnswer, "MakeCall: No Answer");
			}

			_rbrResult = checkEventIn();
			if (_rbrResult != (int) RbrResult::Success) {
				log(ERR, __LINE__, "ERROR: MakeCall: CheckInbound Event");
				throw gcnew RbrException((RbrResult)_rbrResult, "[ERROR] MakeCall: Check Inbound Event");
			}
		}
		if (_i >= _number_of_retries) {
			log(ERR, __LINE__, "ERROR: MakeCall: Check (out) Event timout!");
			throw gcnew RbrException(RbrResult::IVR_CheckOutboundEventTimeout, "[ERROR] MakeCall: Check (out) Event timout!");
		}
		return false;	
	}

	bool Channel::RedirectMedia() {
		log(INF, __LINE__, "RedirectMedia: Start");

		try {
			//------ Redirect Termination (Leg2) --------------------------
			if (callLeg1->MediaIPAddress == nullptr || callLeg1->MediaIPAddress->Length < 7) {
				callLeg1->MediaIPAddress = callLeg1->SignalingIPAddress;
			}
			media_offer->Update(callLeg1);
			log(INF, __LINE__, String::Format("RedirectMedia: Leg2 to Leg1 {0}:{1}", callLeg1->MediaIPAddress, callLeg1->MediaPort));
			media_offer->Display();

			callLeg2->MediaPort = callLeg2->RedirectTo(media_offer->Current);
			if (callLeg2->MediaPort == 0) { 
				log(ERR, __LINE__, "RedirectMedia: Leg2.MediaPort == 0");
				return false;
			}
			
			//------ Redirect Origination (Leg1) -------------------------
			if (callLeg2->MediaIPAddress == nullptr || callLeg2->MediaIPAddress->Length < 7) {
				callLeg2->MediaIPAddress = callLeg2->SignalingIPAddress;
			}
			media_offer->Update(callLeg2);
			log(INF, __LINE__, String::Format("RedirectMedia: Leg1 to Leg2 {0}:{1}", callLeg2->MediaIPAddress, callLeg2->MediaPort));
			media_offer->Display();
			callLeg1->MediaPort = callLeg1->RedirectTo(media_offer->Current);
			if (callLeg1->MediaPort == 0) {
				log(ERR, __LINE__, "RedirectMedia: Leg1.MediaPort == 0");
				return false;
			}
		}
		catch (Exception^ _ex) {
			log(ERR, __LINE__, String::Format("RedirectMedia: Exception:\r\n{0}", _ex));
			return false;
		}
		return true;
	}

	void Channel::WaitForCallEnd(int pWait_time) {
		log(INF, __LINE__, String::Format("WaitForCallEnd.Wait_time: {0}", pWait_time));

		try {	
			int _totalMiliSeconds = 0;
			long _event = 0;
			while ( ! card->Shutdown) {
				_event = get_event_leg1(0, true);
				if (_event != RET_NO_EVENT_AVAILABLE && _event != EV_MEDIA) {
					log(INF, __LINE__, String::Format("WaitForCallEnd: event IN: {0}", gcnew String(call_event_2_string(_event))));
					break;
				}

				//TODO: commented out 'break' statement, check if this is correct, we have been dropping calls by picking up EV_IDLE from term side
				//TODO: seems correct - but should try without and let originator be the only party that can disconnect the call 
				_event = get_event_leg2(0, true);
				if (_event != RET_NO_EVENT_AVAILABLE && _event != EV_MEDIA) {
					log(INF, __LINE__, String::Format("WaitForCallEnd, event OUT: {0}", gcnew String(call_event_2_string(_event))));
				//	break;
				}

				Sleep(1000);
				_totalMiliSeconds += 1000;
				if (_totalMiliSeconds >= pWait_time) {
					log(INF, __LINE__, "WaitForCallEnd: Max Call time reached, disconnecting");
					break;
				}
			}
		}
		catch (Exception^ _ex) {
			log(CRT, __LINE__, String::Format("WaitForCallEnd: CRITICAL: Exception:\r\n{0}", _ex));
		}
	}

	void Channel::DisconnectAll() {
		DisconnectLeg1();
		DisconnectLeg2();		
	}

	void Channel::DisconnectLeg1() {
		if (callLeg1->Disconnect_called) {
			log(INF, __LINE__, "DisconnectLeg1: already called");
			return;
		}
		callLeg1->Disconnect_called = true;

		try {
			DestroyResources();

			if (disconnect(true)) {
				callLeg1->FreeHandle();
			}
			
			media_offer->Free();
		}
		catch(Exception^ _ex) {
			log(ERR, __LINE__, String::Format("DisconnectLeg1: Exception:\r\n{0}", _ex));
		}
	}

	void Channel::DisconnectLeg2() {
		if (callLeg2->Disconnect_called) {
			log(ERR, __LINE__, "DisconnectLeg2: DisconnectLeg2 already called");
			return;
		}
		callLeg2->Disconnect_called = true;

		try {
			if (disconnect(false)) {
				callLeg2->FreeHandle();
			}
		}
		catch (Exception^ _ex) {
			log(CRT, __LINE__, String::Format("DisconnectLeg2: Exeption:\r\n{0}", _ex));
		}
	}

	//--------------------------------- Private -------------------------------------------------------------------
	int Channel::checkEventIn() {
		long _event_leg1 = get_event_leg1(250, true);
		switch (_event_leg1)	{
			case EV_REMOTE_DISCONNECT:
				log(ERR, __LINE__, "checkEventIn: EV_REMOTE_DISCONNECT");
				return (int) RbrResult::IVR_OriginationDisconnect;

			case EV_IDLE:
				log(ERR, __LINE__, "checkEventIn: EV_IDLE");
				return (int) RbrResult::IVR_OriginationIdle;

			case RET_NO_EVENT_AVAILABLE:	//this is expected
				break;

			default:
				log(ERR, __LINE__, String::Format("checkEventIn: UNKNOWN EVENT: {0}", _event_leg1));
				break;
		}
		return (int) RbrResult::Success;
	}

	int Channel::checkEventOut() {
		int _result = 0;

		SIP_DETAIL_PARMS _details;
		INIT_ACU_CL_STRUCT(&_details);
		try {
			_details.handle = callLeg2->Call_handle->Sip_handle;
			if ((_result = sip_details(&_details)) != ERR_NO_ERROR)	{
				log(ERR, __LINE__, String::Format("ERROR: checkEventOut: sip_details(): {0}", _result));
				return (int) RbrResult::IVR_OriginationIdle;
			}
		}
		catch (Exception^ _ex) {
			log(ERR, __LINE__, String::Format("EXCEPTION: checkEventOut: sip_details():\r\n{0}", _ex));
			return (int) RbrResult::IVR_SIPDetailsOutboundException;
		}

		try {
			long _event_leg2 = get_event_leg2(250, false);
			switch (_event_leg2) {
				case RET_NO_EVENT_AVAILABLE:	//this is expected
					break;

				case EV_DETAILS:
					log(INF, __LINE__, "checkEventOut: EV_DETAILS");
					callID = extractCallID(_details.sip_message->message, _details.sip_message->message_length);
					log(INF, __LINE__, String::Format("checkEventOut: CallID: {0}", callID));
					break; 

				case EV_MEDIA_PROPOSE:
					log(ERR, __LINE__, "ERROR: checkEventOut: EV_MEDIA_PROPOSE");
					break;

				case EV_WAIT_FOR_OUTGOING:
					log(INF, __LINE__, "checkEventOut: EV_WAIT_FOR_OUTGOING");
					break;
							
				case EV_OUTGOING_PROCEEDING:
					log(INF, __LINE__, "checkEventOut: EV_OUTGOING_PROCEEDING");
					break;
							
				case EV_PROGRESS:
					log(INF, __LINE__, "checkEventOut: EV_PROGRESS");
// Srdjan					if (callLeg2->MediaPort == -1 && _details.media_session != NULL) {
					if (_details.media_session != NULL) {
						callLeg2->MediaIPAddress = gcnew String(_details.media_session->received_media.connection_address.address);
						callLeg2->MediaPort = _details.media_session->received_media.media_descriptions->port;
						log(INF, __LINE__, String::Format("checkEventOut: EV_PROGRESS, callLeg2: {0}:{1}", callLeg2->MediaIPAddress, callLeg2->MediaPort));
					}
					break;

				case EV_OUTGOING_RINGING:
					log(INF, __LINE__, "checkEventOut: EV_OUTGOING_RINGING");
// Srdjan					if (callLeg2->MediaPort == -1 && _details.media_session != NULL) {
					if (_details.media_session != NULL) {
						callLeg2->MediaIPAddress = gcnew String(_details.media_session->received_media.connection_address.address);
						callLeg2->MediaPort = _details.media_session->received_media.media_descriptions->port;
						log(INF, __LINE__, String::Format("checkEventOut: EV_OUTGOING_RINGING, callLeg2: {0}:{1}", callLeg2->MediaIPAddress, callLeg2->MediaPort));
					}
					break;

				case EV_MEDIA:
					log(INF, __LINE__, "checkEventOut: EV_MEDIA");
// Srdjan					if (callLeg2->MediaPort == -1 && _details.media_session != NULL) {
					if (_details.media_session != NULL) {
						callLeg2->MediaIPAddress = gcnew String(_details.media_session->received_media.connection_address.address);
						callLeg2->MediaPort = _details.media_session->received_media.media_descriptions->port;
						log(INF, __LINE__, String::Format("checkEventOut: EV_MEDIA, callLeg2: {0}:{1}", callLeg2->MediaIPAddress, callLeg2->MediaPort));
					}
					break;

				case EV_CALL_CONNECTED:
					log(INF, __LINE__, "checkEventOut: EV_CALL_CONNECTED");
					if (callLeg2->MediaPort == -1) {
						log(ERR, __LINE__, "ERROR: checkEventOut: EV_CONNECTED, but MediaPort == -1");
						return (int) RbrResult::IVR_OutboundBadMediaPort;
					}
					return (int) RbrResult::Success;

				case EV_REMOTE_DISCONNECT:
					log(INF, __LINE__, "checkEventOut: EV_REMOTE_DISCONNECT");
					return (int) RbrResult::IVR_DestinationDisconnect;
							
					//TODO: should I comment out 'return false', check if this is correct
					//TODO: we have been dropping calls by picking up EV_IDLE from term side
					//TODO: seems correct - but should try without and let originator be the only 
					//TODO: party that can disconnect the call 
				case EV_IDLE:
					log(INF, __LINE__, "checkEventOut: EV_IDLE (OUT)");
					return (int) RbrResult::IVR_DestinationIdle;

				case EV_MEDIA_REJECT_PROPOSAL:
					log(INF, __LINE__, "checkEventOut: EV_MEDIA_REJECT_PROPOSAL");
					return (int) RbrResult::IVR_DestinationMediaReject;

				case EV_MEDIA_REQUEST_PROPOSAL:
					log(INF, __LINE__, "checkEventOut: EV_MEDIA_REQUEST_PROPOSAL");
					break;	

				case EV_MEDIA_REJECT_REQUEST_PROPOSAL:
					log(INF, __LINE__, "checkEventOut: EV_MEDIA_REJECT_REQUEST_PROPOSAL");
					return (int) RbrResult::IVR_DestinationMediaRejectRequest;

				default:
					log(ERR, __LINE__, String::Format("checkEventOut: Unexpected Event (OUT): {0}, {1}", _event_leg2, gcnew String(call_event_2_string(_event_leg2))));
					break;
			}	
		}
		catch (Exception^ _ex) {
			log(ERR, __LINE__, String::Format("ERROR: checkEventOut: Exception:\r\n{0}", _ex));
			return (int) RbrResult::ExceptionThrown;
		}
		finally {
			free_details(&_details);
		}
		return (int) RbrResult::Continue;
	}


	bool Channel::disconnect(bool pLeg1) {
		try {
			log(INF, __LINE__, String::Format("disconnect: Leg{0}", pLeg1 ? 1 : 2));

			int	_result = 0;
			CAUSE_XPARMS _cause_xparms;
			INIT_ACU_STRUCT(&_cause_xparms);
			_cause_xparms.handle = pLeg1 ? callLeg1->Call_handle->Acu_handle : callLeg2->Call_handle->Acu_handle; 
			if((_result = call_disconnect(&_cause_xparms)) != 0) {
				log(ERR, __LINE__, String::Format("ERROR: {0}, disconnect Leg{1}.", _result, pLeg1 ? 1 : 2));
				return false;
			}
		}
		catch(Exception^ _ex) {
			log(CRT, __LINE__, String::Format("Leg{0}, disconnect exception\r\n{1}.", pLeg1 ? 1 : 2, _ex));
			return false;
		}
		return true;
	}

	bool Channel::tryRinging(bool pShouldRing) {
		total_time_ringing += 250;

		if (total_time_ringing > 15000) {
			log(DBG, __LINE__, "Ringing: Max time elapsed...");
			return false;
		}

		if (total_time_ringing == 250 || total_time_ringing % 2500 == 0) {
			try { 
				if (pShouldRing) 
					Play(card->Ring_string_collection, false, true); 
			} 
			catch (Exception^ _ex) {
				log(CRT, __LINE__, String::Format("Ringing {0}", _ex));
				return false;
			}
		}
		return true;
	}

	long Channel::get_event_leg1(int pWaitTime, bool pDisposeDetails) {
		STATE_XPARMS _state_xparms = callLeg1->Get_event(pWaitTime, pDisposeDetails);
		switch (_state_xparms.state)	{
			case EV_REMOTE_DISCONNECT:
			case EV_IDLE:
				disconnect_source = ORIGINATION;
				disconnect_cause = get_cause(_state_xparms.handle, disconnect_source);
				break;
		}
		return _state_xparms.state;
	}

	long Channel::get_event_leg2(int pWaitTime, bool pDisposeDetails) {
		STATE_XPARMS _state_xparms = callLeg2->Get_event(pWaitTime, pDisposeDetails);
		switch (_state_xparms.state)	{
			case EV_REMOTE_DISCONNECT:
			case EV_IDLE:
				disconnect_source = DESTINATION;
				disconnect_cause = get_cause(_state_xparms.handle, disconnect_source);
				break;
		}
		return _state_xparms.state;
	}

	/*
		LC_NORMAL									=	0
		LC_NUMBER_BUSY						=	1
		LC_NO_ANSWER							=	2
		LC_NUMBER_UNOBTAINABLE		=	3
		LC_NUMBER_CHANGED					=	4
		LC_OUT_OF_ORDER						=	5
		LC_INCOMING_CALLS_BARRED	=	6
		LC_CALL_REJECTED					=	7
		LC_CALL_FAILED						=	8
		LC_CHANNEL_BUSY						=	9
		LC_NO_CHANNELS						=	10
		LC_CONGESTION							=	11
	*/
	int Channel::get_cause(ACU_CALL_HANDLE pCallHandle, int pDisconnectSource) {
		int _result;
		int _cause = LC_CALL_FAILED;

		CAUSE_XPARMS strCause_xparms;
		INIT_ACU_STRUCT(&strCause_xparms);
		strCause_xparms.handle = pCallHandle;
		if((_result = call_getcause(&strCause_xparms)) != 0) {
			log(DBG, __LINE__, String::Format("source({0}) error({1}).", pDisconnectSource, _result));
		}
		else {
			_cause = strCause_xparms.cause;
			log(DBG, __LINE__, String::Format("source({0}) cause({1}).", pDisconnectSource, strCause_xparms.cause));
		}
		return _cause;
	}

	void Channel::set_sip_out(SIP_OUT_PARMS* sip_out, String^ pANI, String^ pDestIPAddress, String^ pPrefixAndDestNumber, String^ pCustomHeaderSource) {
		StringUtilities::StringConvertor _localIPAddress(card->IVRConfig->LocalIPAddress);

		//-- set origination name
		sip_out->originating_addr = (ACU_CHAR*) malloc(_localIPAddress.ToString()->Length + pANI->Length + 2);
		memset(sip_out->originating_addr, 0, _localIPAddress.ToString()->Length + pANI->Length + 2);
		_snprintf(sip_out->originating_addr, _localIPAddress.ToString()->Length + pANI->Length + 1, "%s@%s", pANI, _localIPAddress.NativeCharPtr);

		//-- set originating display name
		sip_out->originating_display_name = (ACU_CHAR*) malloc(sizeof(EMPTY_DISPLAY_NAME) + 1);
		memset(sip_out->originating_display_name, 0, sizeof(EMPTY_DISPLAY_NAME) + 1);
		_snprintf(sip_out->originating_display_name, sizeof(EMPTY_DISPLAY_NAME), EMPTY_DISPLAY_NAME);

		//-- set contact address
		sip_out->contact_address = (ACU_CHAR*) malloc(_localIPAddress.ToString()->Length + 1);
		memset(sip_out->contact_address, 0, _localIPAddress.ToString()->Length + 1);
		_snprintf(sip_out->contact_address, _localIPAddress.ToString()->Length, _localIPAddress.NativeCharPtr);

		//-- set destination addres 
		sip_out->destination_addr = (ACU_CHAR*) malloc(pDestIPAddress->Length + pPrefixAndDestNumber->Length + 2);
		memset(sip_out->destination_addr, 0, pDestIPAddress->Length + pPrefixAndDestNumber->Length + 2);
		_snprintf(sip_out->destination_addr, pDestIPAddress->Length + pPrefixAndDestNumber->Length + 1, "%s@%s", pPrefixAndDestNumber, pDestIPAddress);

		//-- set destination display name 
		sip_out->destination_display_name = (ACU_CHAR*) malloc(sizeof(EMPTY_DISPLAY_NAME) + 1);
		memset(sip_out->destination_display_name, 0, sizeof(EMPTY_DISPLAY_NAME) + 1);
		_snprintf(sip_out->destination_display_name, sizeof(EMPTY_DISPLAY_NAME), EMPTY_DISPLAY_NAME);

		//-- set custom source header
		sip_out->custom_headers = NULL;
		if (pCustomHeaderSource != nullptr && pCustomHeaderSource->Length > 0) {
			sip_out->custom_headers = (ACU_CHAR*) malloc(pCustomHeaderSource->Length + 1);
			memset(sip_out->custom_headers, 0, pCustomHeaderSource->Length + 1);
			_snprintf(sip_out->custom_headers, pCustomHeaderSource->Length, "%s", pCustomHeaderSource);
		}
	}

	void Channel::free_sip_out(SIP_OUT_PARMS* sip_out) {
		free(sip_out->originating_addr);
		free(sip_out->originating_display_name);
		free(sip_out->contact_address);
		free(sip_out->destination_addr);
		free(sip_out->destination_display_name);
		
		if (sip_out->custom_headers != NULL) {
			free(sip_out->custom_headers);
		}
	}

	String^ Channel::extractCallID(unsigned char *pSIPMessage, int pLength) {
		char _sip_message[2048] = {0};
		if (pLength > 2048) {
			pLength = 2048;
		}
		_snprintf(_sip_message, pLength, "%s", pSIPMessage);
		String^ _sipMessage = gcnew String(_sip_message);
		int _index = _sipMessage->IndexOf("Call-ID:");
		String^ _callID = _sipMessage->Substring(_index + 9);
		_callID = _callID->Remove(_callID->IndexOf(Environment::NewLine));
		return _callID;
	}

	void Channel::log(int iType, long pLine, String^ pMessage) {
		logger->Log(moduleNumber, callNumber, iType, __FILE__, pLine, pMessage);
	}

	int Channel::free_details(SIP_DETAIL_PARMS *pDetails) {
		ACU_ERR _result = 0;
		if ((_result = sip_free_details(pDetails)) != 0) {
			throw gcnew Exception(String::Format("ERROR: {0}, free details.", gcnew String(error_2_string(_result))));
		}
		return _result;
	}
		
	//bool Channel::WaitForCallSync(CallLeg^ pCallContext) {
	//	try {
	//		waitForCallBegin(pCallContext);
	//		pCallContext->RegisteredHandle = ThreadPool::RegisterWaitForSingleObject(pCallContext->WaitObjectIn, gcnew WaitOrTimerCallback(Channel::waitForCallEnd), pCallContext, -1, true);
	//	}
	//	catch (Exception^ _ex) {
	//		pCallContext->Log(ERR, __FILE__, __LINE__, String::Format("{0}", _ex));
	//		DisconnectLeg1(pCallContext);
	//		return false;
	//	}
	//	return true;
	//}

	//void Channel::waitForCallBegin() {
	//	SIP_IN_PARMS _sip_open_in;
	//	INIT_ACU_CL_STRUCT(&_sip_open_in);
	//	_sip_open_in.net = card->Card_info->sip_port_id[moduleNumber];
	//	_sip_open_in.queue_id = callLeg1->Call_handle->Event_queue->queue_id;
	//	int	_result	= 0;
	//	if ((_result = sip_openin(&_sip_open_in)) != 0) {
	//		throw gcnew Exception(String::Format("ERROR, wait_for_call: {0}, sip_openin.", gcnew String(error_2_string(_result))));
	//	}
	//	callLeg1->Call_handle->Sip_handle = _sip_open_in.handle;

	//	DWORD _wait_result = WaitForSingleObject(callLeg1->Call_handle->Wait_object->wait_object, 1000);
	//	if (_wait_result != WAIT_OBJECT_0) {	
	//		throw gcnew Exception(String::Format("WaitForSingleObject: Unexpected result: {0}", _wait_result));
	//	}
	//		
	//	ACU_EVENT_QUEUE_PARMS	_event_from_queue;
	//	INIT_ACU_STRUCT(&_event_from_queue);
	//	_event_from_queue.queue_id = callLeg1->Call_handle->Event_queue->queue_id;
	//	if ((_result = acu_get_event_from_queue(&_event_from_queue)) != 0) {
	//		throw gcnew Exception(String::Format("ERROR, wait_for_call: {0}, acu_get_event_from_queue.", gcnew String(error_2_string(_result))));
	//	}
	//	callLeg1->Call_handle->Acu_handle = _event_from_queue.context;
	//						
	//	STATE_XPARMS _state_xparms;
	//	INIT_ACU_CL_STRUCT(&_state_xparms);
	//	_state_xparms.handle = callLeg1->Call_handle->Sip_handle;
	//	if ((_result = call_event(&_state_xparms)) != 0) {
	//		throw gcnew Exception(String::Format("ERROR, wait_for_call: {0}, acu_get_event_from_queue.", gcnew String(error_2_string(_result))));
	//	}

	//	if (_state_xparms.state != EV_WAIT_FOR_INCOMING) {
	//		throw gcnew Exception(String::Format("ERROR: wait_for_call expected: EV_WAIT_FOR_INCOMING, received: {0}", _state_xparms.state));
	//	}
	//	log(INF, __LINE__, "wait_for_call: EV_WAIT_FOR_INCOMING.");
	//}

	//void Channel::waitForCallEnd(Object^ pState, bool pTimedOut) {
 //   CallLeg^ _callContext = static_cast<CallLeg^>(pState);
	//	int	_result	= 0;
 //
	//	String^ _temp = String::Empty;	//TODO: refactor!
	//	while ( ! card->Shutdown) {
	//		DWORD _wait_result = WaitForSingleObject(callLeg1->Call_handle->Wait_object->wait_object, 1000);
	//		if (_wait_result != WAIT_OBJECT_0) {	
	//			continue;
	//		}
	//		ACU_EVENT_QUEUE_PARMS	_event_from_queue;
	//		INIT_ACU_STRUCT(&_event_from_queue);
	//		_event_from_queue.queue_id = callContext->Call_handle->Event_queue->queue_id;
	//		if ((_result = acu_get_event_from_queue(&_event_from_queue)) != 0) {
	//			log( ERR, __FILE__, __LINE__, String::Format("ERROR, wait_for_call: {0}, acu_get_event_from_queue.", gcnew String(error_2_string(_result))));
	//			return;
	//		}
	//						
	//		STATE_XPARMS _state_xparms;
	//		INIT_ACU_CL_STRUCT(&_state_xparms);
	//		_state_xparms.handle = callLeg1->Call_handle->Sip_handle;
	//		if ((_result = call_event(&_state_xparms)) != 0) {
	//			log(ERR, __LINE__, String::Format("wait_for_call: {0}, acu_get_event_from_queue.", gcnew String(error_2_string(_result))));
	//			return;
	//		}

	//		switch (_state_xparms.state) {
	//			case EV_INCOMING_CALL_DET:
	//				log(INF, __LINE__, "wait_for_call: EV_INCOMING_CALL_DET.");
	//										
	//				SIP_DETAIL_PARMS _details;
	//				INIT_ACU_CL_STRUCT(&_details);
	//				_details.handle = callLeg1->Call_handle->Sip_handle;
	//				if ((_result = sip_details(&_details)) != 0) {
	//					_callContext->Log(ERR, __FILE__, __LINE__, String::Format("ERROR, wait_for_call: {0}, sip_details.", gcnew String(error_2_string(_result))));
	//					return;
	//				}
	//				callLeg1->MediaPort = _details.media_offer_answer->media_descriptions->port;
	//				log(INF, __LINE__, String::Format("wait_for_call: origination_addr: [{0}:{1}]", gcnew String(_details.media_offer_answer->connection_address.address), _callContext->RTP_port_in));

	//				_temp = gcnew String(_details.connected_addr);
	//				log(INF, __LINE__, String::Format("wait_for_call: called_addr: [{0}]", _temp));		
	//				dnis = callLeg1->Trim_sip_address(_temp);
	//				dnis = dnis->Replace(URL_ENCODED_POUND_SIGN, POUND_SIGN);

	//				_temp = gcnew String(_details.originating_addr);
	//				log( INF, __FILE__, __LINE__, String::Format("wait_for_call: caller_addr: [{0}]", _temp));		
	//				ani = callLeg1->Trim_sip_address(_temp);
	//
	//				origAddr = gcnew String(_details.media_offer_answer->connection_address.address);

	//				if (callLeg1->media_offer->Populate(_details.media_offer_answer, card->Card_info->Signaling_IP_address) != RET_SUCCESS) {
	//					callLeg1->Free_details(&_details);
	//					return;
	//				}
	//				callLeg1->Free_details(&_details);
	//				//_callContext->media_offer->Display();

	//				//run script:
	//				//Interlocked::Increment(_callContext->Card->NumberOfCalls); //???
	//				if ( ! SessionDispatcher::Instance->Run(gcnew CallHandler(_callContext))) {
	//					throw gcnew Exception();
	//				}
	//				//Interlocked::Decrement(_callContext->Card->NumberOfCalls); //???
	//				return; 

	//			default:
	//				log( INF, __FILE__, __LINE__, String::Format("wait_for_call: UNEXPECTED EVENT: {0}.", gcnew String(call_event_2_string(_state_xparms.state))));
	//				break;
	//		}	
	//	}
	//	return;
	//}
}
