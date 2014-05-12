#pragma once

#include "ivrlogger.h"
#include "common.h"

namespace Timok_IVR {
	ref class Card {
	private:
		IVRLogger^ logger;

	public:
		Card(int pCardNumber, IVRConfiguration^ pIVRConfiguration, IVRLogger^ pLogger);
		~Card();

		int Number;
		bool Shutdown;
		CARD_INFORMATION* Card_info;
		StringCollection^ Ring_string_collection;
		IVRConfiguration^ IVRConfig;
		bool IsSupported(int pCodecType);
	};
}
