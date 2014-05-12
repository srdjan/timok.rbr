#pragma once

#include "ivrlogger.h"
#include "callleg.h"
#include "common.h"

namespace Timok_IVR {
	ref class MediaOffer {
	private:
		Card^ card;
		int	module_number;
		int	call_number;
		IVRLogger^ logger;

		int clone_media_offer(const ACU_MEDIA_OFFER_ANSWER* offer);
		ACU_MEDIA_DESCRIPTION* clone_media_descriptions(ACU_MEDIA_DESCRIPTION *media_descriptions);
		void free_media_descriptions(ACU_MEDIA_DESCRIPTION **media_descriptions);

		ACU_PAYLOAD* clone_payloads(const ACU_PAYLOAD *payloads, const ACU_MEDIA_TYPES media_type);
		void free_payloads(ACU_PAYLOAD **payloads, const ACU_MEDIA_TYPES media_type);

		ACU_PAYLOAD* clone_audio_video_payloads(const ACU_PAYLOAD *payloads);
		void free_audio_video_payloads(ACU_PAYLOAD **payloads);

		ACU_MISCELLANEOUS_MEDIA_ATTRIBUTE* clone_misc_attributes(ACU_MISCELLANEOUS_MEDIA_ATTRIBUTE *misc_attributes);
		void free_misc_attributes(ACU_MISCELLANEOUS_MEDIA_ATTRIBUTE **misc_attributes);

		ACU_CHAR* convert_payload_number_to_name(const ACU_UINT payload_number);
		ACU_PAYLOAD* clone_control_payloads(const ACU_PAYLOAD *payloads);

	public:
		MediaOffer(Card^ pCard, int pModuleNumber, int pChannelNumber, IVRLogger^ pLogger);
		~MediaOffer();

		ACU_MEDIA_OFFER_ANSWER* Current;
		int	Populate(const ACU_MEDIA_OFFER_ANSWER* pOffer, char* pRTPAddress, ACU_SHORT pRTP_port);
		void Update(CallLeg^ pCallLeg);
		void Free();
		void Display();
	};
}