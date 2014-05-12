//--
#include "mediaoffer.h"

namespace Timok_IVR {
	MediaOffer::MediaOffer(Card^ pCard, int pModuleNumber, int pChannelNumber, IVRLogger^ pLogger) {	
		card = pCard;
		module_number = pModuleNumber;
		call_number = pChannelNumber;
		logger = pLogger; 
		Current = NULL;
	}

	MediaOffer::~MediaOffer() { }

	int MediaOffer::Populate(const ACU_MEDIA_OFFER_ANSWER* pOffer, char* pRTPAddress, ACU_SHORT pRTPPort) {
		Free();
		//logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Local Media IPAddress: {0}:{1}", gcnew String(pIp_address), pRTP_Port));

		if (clone_media_offer(pOffer) != RET_SUCCESS) {
			return RET_FAIL;
		}

		ACU_MEDIA_DESCRIPTION *temp_desc;
		ACU_PAYLOAD *temp_payload, *telephone_event = NULL;
		bool found_supported_codec = false;//, found_telephone_event = 0;
	
		for (temp_desc = Current->media_descriptions; NULL != temp_desc; temp_desc = temp_desc->next)	{
			for (temp_payload = temp_desc->payloads; NULL != temp_payload; temp_payload = temp_payload->next)	{
				if ( ! found_supported_codec) {		// Look for first supported codec - essential
					found_supported_codec = card->IsSupported(temp_payload->payload.audio_video.rtp_payload_number);
					if (found_supported_codec)	{					//	// Found supported codec. Need to copy all details into the media description.
						memset(Current, 0, sizeof(ACU_MEDIA_OFFER_ANSWER));
						Current->connection_address.address_type = ACU_IPv4;
						//SRDJAN: Marshal::FreeHGlobal(IntPtr(Current->connection_address.address));
						//SRDJAN: Current->connection_address.address = (char*) Marshal::StringToHGlobalAnsi(IVRConfig::Instance->LocalIPAddress).ToPointer();
						Current->connection_address.address = (char*)malloc(strlen(pRTPAddress) + 1);
						strcpy(Current->connection_address.address, pRTPAddress);
						
						Current->media_descriptions = (ACU_MEDIA_DESCRIPTION*)malloc(sizeof(ACU_MEDIA_DESCRIPTION));
						memset(Current->media_descriptions, 0, sizeof(ACU_MEDIA_DESCRIPTION));

						/*Current->media_descriptions->connection_address.address_type = ACU_IPv4;
						Current->media_descriptions->connection_address.address = (char*)malloc(strlen(pRTPAddress) + 1);
						strcpy(Current->media_descriptions->connection_address.address, pRTPAddress);*/
		
						Current->media_descriptions->media_direction = ACU_SEND_RECV;
						Current->media_descriptions->media_type = ACU_AUDIO;
						Current->media_descriptions->packet_length = temp_desc->packet_length;
						Current->media_descriptions->port = pRTPPort;
						
						Current->media_descriptions->payloads = (ACU_PAYLOAD*)malloc(sizeof(ACU_PAYLOAD));
						memset(Current->media_descriptions->payloads, 0, sizeof(ACU_PAYLOAD));
						Current->media_descriptions->payloads->payload.audio_video.clock_rate = temp_payload->payload.audio_video.clock_rate;
						Current->media_descriptions->payloads->payload.audio_video.packet_length = temp_payload->payload.audio_video.packet_length;
						Current->media_descriptions->payloads->payload.audio_video.rtp_payload_number = temp_payload->payload.audio_video.rtp_payload_number;

						if (NULL != temp_payload->payload.audio_video.payload_specific_options) {
							char *options;
							options = (char*)malloc(strlen(temp_payload->payload.audio_video.payload_specific_options) + 1);
							strcpy(options, temp_payload->payload.audio_video.payload_specific_options);
							Current->media_descriptions->payloads->payload.audio_video.payload_specific_options = options;
						}
						if (NULL != temp_payload->payload.audio_video.rtp_payload_name) {
							char *payload_name;
							payload_name = (char*)malloc(strlen(temp_payload->payload.audio_video.rtp_payload_name) + 1);
							strcpy(payload_name, temp_payload->payload.audio_video.rtp_payload_name);
							Current->media_descriptions->payloads->payload.audio_video.rtp_payload_name = payload_name;
						}
					}
				}
				
				if (temp_payload->payload.audio_video.rtp_payload_name) {	// Look for telephone event - not essential
					if (telephone_event == NULL) {													// Found telephone event. Need to copy all details into the media description.
						if ( ! strcmp(temp_payload->payload.audio_video.rtp_payload_name, ACU_TELEPHONE_EVENT))	{
							telephone_event = (ACU_PAYLOAD*)malloc(sizeof(ACU_PAYLOAD));
							memset(telephone_event, 0, sizeof(ACU_PAYLOAD));
							telephone_event->payload.audio_video.clock_rate = temp_payload->payload.audio_video.clock_rate;
							telephone_event->payload.audio_video.packet_length = temp_payload->payload.audio_video.packet_length;
							if (NULL != temp_payload->payload.audio_video.payload_specific_options) {
								char *options;
								options = (char*)malloc(strlen(temp_payload->payload.audio_video.payload_specific_options) + 1);
								strcpy(options, temp_payload->payload.audio_video.payload_specific_options);
								telephone_event->payload.audio_video.payload_specific_options = options;
							}
							if (NULL != temp_payload->payload.audio_video.rtp_payload_name)	{
								char *payload_name;
								payload_name = (char*)malloc(strlen(temp_payload->payload.audio_video.rtp_payload_name) + 1);
								strcpy(payload_name, temp_payload->payload.audio_video.rtp_payload_name);
								telephone_event->payload.audio_video.rtp_payload_name = payload_name;
							}
							telephone_event->payload.audio_video.rtp_payload_number = temp_payload->payload.audio_video.rtp_payload_number;
						}
					}
				}
			}
		
			if (found_supported_codec) {
				if (telephone_event != NULL) {
					Current->media_descriptions->payloads->next = telephone_event;
				}
				break;
			}
		}
		
		// If the return value is NULL the application should reject the call as no supported codec has been found.
		if ( ! found_supported_codec) {		
			logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, "ERROR: No supported codec has been found.");
			return RET_FAIL;
		}
		return RET_SUCCESS;
	}

	void MediaOffer::Update(CallLeg^ pCallLeg) {
		//SRDJAN:Marshal::FreeHGlobal(IntPtr(Current->connection_address.address));
		free(Current->connection_address.address);

		Current->connection_address.address = (char*) Marshal::StringToHGlobalAnsi(pCallLeg->MediaIPAddress).ToPointer();
		Current->media_descriptions->port = pCallLeg->MediaPort;//LOCAL_RTP_PORT;
	}	

	void MediaOffer::Free() {
		if (Current == NULL) {
			return;
		}
		logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Free: Media_offer: {0}", (int) Current));
		
		// Free contents of global connection address
		//SRDJAN: Marshal::FreeHGlobal(IntPtr(Current->connection_address.address));
		free(Current->connection_address.address);

		// Free the raw SDP char* buffer
		free(Current->raw_sdp);
		Current->raw_sdp = NULL;

		// Free any media descriptions
		free_media_descriptions(&(Current->media_descriptions));

		// Free the ACU_MEDIA_OFFER_ANSWER structure
		free(Current);
		Current = NULL;
	}

	void MediaOffer::Display() {
		
		logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Signaling address: {0}", gcnew String(Current->connection_address.address)));

		ACU_PAYLOAD *payload;
		ACU_UINT i = 1;
		ACU_MEDIA_DESCRIPTION *desc;
		for (desc = Current->media_descriptions; NULL != desc; desc = desc->next)	{
			String^ _ip = NULL != desc->connection_address.address ? gcnew String(desc->connection_address.address) : String::Empty;
			logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Media address: {0}", _ip));
			
			switch (desc->media_direction) {
				case ACU_SEND_RECV:
					logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "Media direction: ACU_SEND_RECV");
					break;
				case ACU_SEND_ONLY:
					logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "Media direction: ACU_SEND_ONLY");
					break;
				case ACU_RECV_ONLY:
					logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "Media direction: ACU_RECV_ONLY");
					break;
				case ACU_INACTIVE:
					logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "Media direction: ACU_INACTIVE");
					break;
				default:
					break;
			}

			logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Packet length = {0}", desc->packet_length));
			logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("RTP port = {0}", desc->port));

			switch (desc->media_type) {
				case ACU_AUDIO:
				case ACU_VIDEO:
					if (desc->media_type == ACU_AUDIO)
						logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "Media Type: ACU_AUDIO");
					else
						logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "Media Type: ACU_VIDEO");
					for (payload = desc->payloads; NULL != payload; payload = payload->next) {
						StringBuilder^ _strb = gcnew StringBuilder(String::Format("Payload {0}: {1} {2}/{3} ", i++, payload->payload.audio_video.rtp_payload_number, gcnew String(payload->payload.audio_video.rtp_payload_name), payload->payload.audio_video.clock_rate));
						
						if (payload->payload.audio_video.packet_length > 0) {
							_strb->Append(String::Format(" length/{0}", payload->payload.audio_video.packet_length));
						}
						if (NULL != payload->payload.audio_video.payload_specific_options) {
							_strb->Append(String::Format(" options/{0}", gcnew String(payload->payload.audio_video.payload_specific_options)));
						}
						logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("{0}", _strb->ToString()));
					}
					break;
				case ACU_IMAGE:
					logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "Media Type: ACU_IMAGE, NOT SUPPORTED !");
					break;
				case ACU_CONTROL:
					logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "Media Type: ACU_CONTROL");
					for (payload = desc->payloads; NULL != payload; payload = payload->next) {
						logger->Log(module_number, call_number, INF, __FILE__, __LINE__, String::Format("Payload {0}, Dummy: {1}", i++, payload->payload.control.dummy));
					}
					break;
				case ACU_TEXT:
					logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "Media Type: ACU_TEXT");
					break;
				case ACU_APPLICATION:
					logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "Media Type: ACU_APPLICATION");
					break;
				default:
					logger->Log(module_number, call_number, INF, __FILE__, __LINE__, "Media Type: ACU_UNKNOWN_MEDIA_TYPE");
					break;
			}
		}
	}

	//------------------------------- private ------------------------------------------------------
	int MediaOffer::clone_media_offer(const ACU_MEDIA_OFFER_ANSWER* pOffer) {
		if (NULL == pOffer) {
			logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, "ERROR: No media offer present.");
			return RET_FAIL;
		}

		Current = (ACU_MEDIA_OFFER_ANSWER*)malloc(sizeof(ACU_MEDIA_OFFER_ANSWER));
		memset(Current, 0, sizeof(ACU_MEDIA_OFFER_ANSWER));

		// Copy connection address information
		Current->connection_address.address_type = pOffer->connection_address.address_type;
		if (NULL != pOffer->connection_address.address) {
			//Current->connection_address.address = (ACU_CHAR*)malloc(strlen(pOffer->connection_address.address) + 1);
			//strcpy(Current->connection_address.address, pOffer->connection_address.address);
			Marshal::FreeHGlobal(IntPtr(Current->connection_address.address));
			Current->connection_address.address = (char*) Marshal::StringToHGlobalAnsi(gcnew String(pOffer->connection_address.address)).ToPointer();
		}
		
		// Copy raw SDP
		if (NULL != pOffer->raw_sdp) {
			Current->raw_sdp = (ACU_CHAR*)malloc(strlen(pOffer->raw_sdp) + 1);
			strcpy(Current->raw_sdp, pOffer->raw_sdp);
		}

		Current->media_descriptions = clone_media_descriptions(pOffer->media_descriptions);

		return RET_SUCCESS;
	}

	ACU_MEDIA_DESCRIPTION* MediaOffer::clone_media_descriptions(ACU_MEDIA_DESCRIPTION *media_descriptions) {
		if(NULL == media_descriptions) {
			logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, "There are no media descriptions to copy");
			return NULL;
		}

		// Although there may be several media descriptions this function is
		// recursive so only enough memory for one media description needs to be allocated here.
		ACU_MEDIA_DESCRIPTION *_new_desc = (ACU_MEDIA_DESCRIPTION*)malloc(sizeof(ACU_MEDIA_DESCRIPTION));
		memset(_new_desc, 0, sizeof(ACU_MEDIA_DESCRIPTION));

		if(NULL != media_descriptions->next) {	// The next media description in the list is not NULL so it needs to be copied first.
			_new_desc->next = clone_media_descriptions(media_descriptions->next);
		}

		// Now copy all of the media attributes contained in the media description
		_new_desc->connection_address.address_type = media_descriptions->connection_address.address_type;
		if(NULL != media_descriptions->connection_address.address) {
//SRDJAN 0-24-09:
//			_new_desc->connection_address.address = (ACU_CHAR*)malloc(MAXADDR);
//			strcpy(_new_desc->connection_address.address, media_descriptions->connection_address.address);
		}
		
		if(NULL != media_descriptions->transport) {
			_new_desc->transport = (ACU_CHAR*)malloc(strlen(media_descriptions->transport) + 1);
			strcpy(_new_desc->transport, media_descriptions->transport);
		}
		_new_desc->packet_length    = media_descriptions->packet_length;
		_new_desc->media_direction  = media_descriptions->media_direction;
		_new_desc->media_type       = media_descriptions->media_type;
		_new_desc->port             = media_descriptions->port;

		// Copy the payloads given in this media description
		_new_desc->payloads = clone_payloads(media_descriptions->payloads, (const ACU_MEDIA_TYPES) media_descriptions->media_type);

		// Copy any miscellaneous attributes
		_new_desc->miscellaneous_attributes = clone_misc_attributes(media_descriptions->miscellaneous_attributes);
		return _new_desc;
	}

	void MediaOffer::free_media_descriptions(ACU_MEDIA_DESCRIPTION **media_descriptions) {
		if(NULL == *media_descriptions) {  // There is no media description to free
			return;
		}

		if (NULL != (*media_descriptions)->next) { // The next media description in the list is not NULL so needs to be freed first.
			free_media_descriptions( &(*media_descriptions)->next);
		}

		// Free the local connection address
		free((*media_descriptions)->connection_address.address);
		(*media_descriptions)->connection_address.address = NULL;

		// Free any miscellaneous attributes
		free_misc_attributes(&((*media_descriptions)->miscellaneous_attributes));

		// Free all payloads
		free_payloads(&((*media_descriptions)->payloads), (ACU_MEDIA_TYPES)(*media_descriptions)->media_type);

		// Free the transport char* buffer
		free((*media_descriptions)->transport);
		(*media_descriptions)->transport = NULL;

		// Free the ACU_MEDIA_DESCRIPTION structure
		free(*media_descriptions);
		*media_descriptions = NULL;
	}

	ACU_MISCELLANEOUS_MEDIA_ATTRIBUTE* MediaOffer::clone_misc_attributes(ACU_MISCELLANEOUS_MEDIA_ATTRIBUTE *misc_attributes) {
		 ACU_MISCELLANEOUS_MEDIA_ATTRIBUTE *new_attribute;

		 if(NULL == misc_attributes) {
				// There are no miscellaneous attributes to copy
				return NULL;
		 }

		 // Although there may be several attributes this function is recursive
		 // so only enough memory for one attribute needs to be allocated here.
		 new_attribute = (ACU_MISCELLANEOUS_MEDIA_ATTRIBUTE*)malloc(sizeof(ACU_MISCELLANEOUS_MEDIA_ATTRIBUTE));
		 memset(new_attribute, 0, sizeof(ACU_MISCELLANEOUS_MEDIA_ATTRIBUTE));

		 if(NULL != misc_attributes->next) {
				// The next attribute in the list is not NULL so it needs to be copied first.
				new_attribute->next = clone_misc_attributes(misc_attributes->next);
		 }

		 // Copy miscellaneous attribute
		 if(NULL != misc_attributes->attribute) {
				new_attribute->attribute = (ACU_CHAR*)malloc(strlen(misc_attributes->attribute) + 1);
				strcpy(new_attribute->attribute, misc_attributes->attribute);
		 }

		 return new_attribute;
	}


	void MediaOffer::free_misc_attributes(ACU_MISCELLANEOUS_MEDIA_ATTRIBUTE **misc_attributes) {
		if(NULL == *misc_attributes) {   // There are no attributes to free
			return;
		}

		if(NULL != (*misc_attributes)->next) {   // The next attribute in the list is not NULL so needs to be freed first.
			free_misc_attributes(&((*misc_attributes)->next));
		}

		// Free other elements of the ACU_MISCELLANEOUS_ATTRIBUTE structure
		free((*misc_attributes)->attribute);
		(*misc_attributes)->attribute = NULL;
		free(*misc_attributes);
		*misc_attributes = NULL;
	}

	ACU_PAYLOAD* MediaOffer::clone_payloads(const ACU_PAYLOAD *payloads, const ACU_MEDIA_TYPES media_type) {
		ACU_PAYLOAD *new_payload = NULL;

		if(NULL == payloads) {      // There are no payloads to copy
			return NULL;
		}

		switch(media_type) {
			case ACU_AUDIO:
			case ACU_VIDEO:
				new_payload = clone_audio_video_payloads(payloads);
				break;
			case ACU_IMAGE:
				//new_payload = clone_image_payloads(payloads);
				break;
			case ACU_CONTROL:
				new_payload = clone_control_payloads(payloads);
				break;
			case ACU_TEXT:
			case ACU_APPLICATION:
			default:
				// Nothing to copy
				break;
		 }
		 return new_payload;
	}

	void MediaOffer::free_payloads(ACU_PAYLOAD **payloads, const ACU_MEDIA_TYPES media_type) {
		switch(media_type) {
			case ACU_AUDIO:
			case ACU_VIDEO:
				free_audio_video_payloads(payloads);
				break;
			case ACU_IMAGE:
				logger->Log(module_number, call_number, ERR, __FILE__, __LINE__, "ACU_IMAGE free: cloning NOT SUPPORTED !");
				//free_image_payloads(payloads);
				break;
			case ACU_CONTROL:
			case ACU_TEXT:
			case ACU_APPLICATION:
				// No dynamically allocated memory here. 
				// Only need to free the pointer to the payload.
				free(*payloads);
				*payloads = NULL;
				break;
			default:
				break;
		 }
	}

	ACU_PAYLOAD *MediaOffer::clone_audio_video_payloads(const ACU_PAYLOAD *payloads) {
		ACU_PAYLOAD *new_payload = NULL;
	   
		// Although there may be several payloads this function is recursive
		// so only enough memory for one payload needs to be allocated here.
		new_payload = (ACU_PAYLOAD*)malloc(sizeof(ACU_PAYLOAD));
		 memset(new_payload, 0, sizeof(ACU_PAYLOAD));

		 if (NULL != payloads->next) {  // The next payload in the list is not NULL so it needs to be copied first.
				new_payload->next = clone_audio_video_payloads(payloads->next);
		 }

		 // Copy payload attributes
		 new_payload->payload.audio_video.clock_rate     = payloads->payload.audio_video.clock_rate;
		 new_payload->payload.audio_video.packet_length  = payloads->payload.audio_video. packet_length;
		 if (NULL != payloads->payload.audio_video.payload_specific_options) {
				ACU_CHAR *options;
				options = (ACU_CHAR*)malloc(strlen(payloads->payload.audio_video.payload_specific_options)+1);
				strcpy(options, payloads->payload.audio_video.payload_specific_options);
				new_payload->payload.audio_video.payload_specific_options = options;
		 }

		 if (NULL != payloads->payload.audio_video.rtp_payload_name) {
				ACU_CHAR *payload_name;
				payload_name = (ACU_CHAR*)malloc(strlen(payloads->payload.audio_video.rtp_payload_name) + 1);
				strcpy(payload_name, payloads->payload.audio_video.rtp_payload_name);
				new_payload->payload.audio_video.rtp_payload_name = payload_name;
		 }
		 new_payload->payload.audio_video.rtp_payload_number = payloads->payload.audio_video.rtp_payload_number;

		 return new_payload;
	}

	void MediaOffer::free_audio_video_payloads(ACU_PAYLOAD **payloads) {
		 if (NULL == *payloads) { // There are no payloads to free
				return;
		 }

		 if (NULL != (*payloads)->next) {
				// The next payload in the list is not NULL so needs to be freed first.
				free_audio_video_payloads(&((*payloads)->next));
		 }

		 // Free any dynamically allocated elements of the ACU_PAYLOAD.audio_video structure
		 free((*payloads)->payload.audio_video.rtp_payload_name);
		 (*payloads)->payload.audio_video.rtp_payload_name = NULL;
		 free((*payloads)->payload.audio_video.payload_specific_options);
		 (*payloads)->payload.audio_video.payload_specific_options = NULL;
		 free(*payloads);
		 *payloads = NULL;
	}

	ACU_PAYLOAD *MediaOffer::clone_control_payloads(const ACU_PAYLOAD *payloads) {
		 ACU_PAYLOAD *new_payload = NULL;
	   
		 // Although there may be several payloads this function is recursive
		 // so only enough memory for one payload needs to be allocated here.
		 new_payload = (ACU_PAYLOAD*)malloc(sizeof(ACU_PAYLOAD));
		 memset(new_payload, 0, sizeof(ACU_PAYLOAD));

		 if (NULL != payloads->next) {  // The next payload in the list is not NULL so it needs to be copied first.
				new_payload->next = clone_control_payloads(payloads->next);
		 }

		 // Copy payload attributes
		 new_payload->payload.control.dummy = payloads->payload.control.dummy;
		 return new_payload;
	}

	ACU_CHAR *MediaOffer::convert_payload_number_to_name(const ACU_UINT payload_number)	{
		ACU_CHAR* payload_name = (ACU_CHAR*)malloc(32);
		
		switch (payload_number) {
			case ACU_PCMU_PAYLOAD_NUMBER:  // G.711 mu-law
				strcpy(payload_name, ACU_PCMU);
				break;
			case ACU_G723_PAYLOAD_NUMBER:  // G.723
				strcpy(payload_name, ACU_G723);
				break;
			case ACU_PCMA_PAYLOAD_NUMBER:  // G.711 a-law
				strcpy(payload_name, ACU_PCMA);
				break;
			case ACU_G729_PAYLOAD_NUMBER:  // G.729
				strcpy(payload_name, ACU_G729);
				break;
			default:
				free(payload_name);
				payload_name = NULL;
				break;
		}
		return payload_name;
	}
}