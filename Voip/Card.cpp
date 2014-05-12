//--
#include "card.h"

namespace Timok_IVR {
	Card::Card(int pCard_number, IVRConfiguration^ pIVRConfiguration, IVRLogger^ pLogger) {
		Number = pCard_number;
		IVRConfig = pIVRConfiguration;
		logger = pLogger;
		Shutdown = false;
		Card_info = new CARD_INFORMATION();
		memset(Card_info, 0, sizeof(CARD_INFORMATION));

		String^ _ringFilePath = Path::Combine(Path::Combine(IVRConfig->RbrRoot, PROMPTS_ROOT_FOLDER), RING);
		Ring_string_collection = gcnew StringCollection();	
		Ring_string_collection->Add(_ringFilePath);	

		//-- Get Signaling IP from config
		StringUtilities::StringConvertor _localIPAddress(IVRConfig->LocalIPAddress);
		memset(Card_info->Signaling_IP_address, 0, 20);
		_snprintf(Card_info->Signaling_IP_address, _localIPAddress.ToString()->Length + 1, "%s", _localIPAddress.NativeCharPtr);
		logger->Log(INF, __FILE__, __LINE__, String::Format("Signaling IPAddress: {0}.", gcnew String((char *) Card_info->Signaling_IP_address)));
				
		//-- Get Media IP from config
		StringUtilities::StringConvertor _mediaIPAddress(IVRConfig->MediaIPAddress);
		memset(Card_info->Media_IP_address, 0, 20);
		_snprintf(Card_info->Media_IP_address, _mediaIPAddress.ToString()->Length + 1, "%s", _mediaIPAddress.NativeCharPtr);
		logger->Log(INF, __FILE__, __LINE__, String::Format("Media IPAddress: {0}.", gcnew String((char *) Card_info->Media_IP_address)));
	}

	bool Card::IsSupported(int pCodecType) {
		for(int _i = 0; _i < IVRConfig->SupportedCodecTypes->Length; _i++) {
			if (IVRConfig->SupportedCodecTypes[_i] == pCodecType) {
				return true;
			}
		}
		return false;
	}

	Card::~Card() {
		delete Card_info;
	}
}

