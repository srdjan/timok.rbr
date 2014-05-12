#pragma once

#define _WIN32_WINNT 0x0500

#include <stdio.h>
#include <stdlib.h>
#include <stdarg.h>
#include <process.h>
#include "acu_type.h"
#include "smclib.h"
#include "res_lib.h"
#include "cl_lib.h"
#include "sw_lib.h"
#include "smhlib.h"
#include "smrtp.h"
#include "prosrtpapi.h"
#include "acu_bin.h"
#include "acu_os_port.h"
#include "smbesp.h"			// Basic Speech Processing functionallity
#include "smwavlib.h"		// Wav file replay/record funtions

#include "stringconvertor.h"
#include "AutoPtr.h"

#undef CreateDirectory 
#using <System.Configuration.dll>
#using <System.Configuration.Install.dll>
#using <System.ServiceProcess.dll>
#using <System.Management.dll>

using namespace System;
using namespace System::Text;
using namespace System::IO;
using namespace System::Configuration;
using namespace System::Configuration::Install;
using namespace System::Diagnostics;
using namespace System::Management;
using namespace System::Threading;
using namespace System::Reflection;
using namespace System::Collections;
using namespace System::Collections::Generic;
using namespace System::Collections::Specialized;
using namespace System::Runtime::InteropServices;
using namespace System::ServiceProcess;
using namespace System::ComponentModel;

//#define TESTING

#if _DEBUG
#define ASSERT(expr) System::Diagnostics::Debug::Assert(expr)
#else
#define ASSERT(expr) ((void)0)
#endif

#using <Timok.Core.dll>
#using <Timok.Rbr.Core.dll>
#using <Timok.IVR.Scripting.dll>

using namespace Timok::Rbr::Core;
using namespace Timok::Rbr::Core::Config;
using namespace Timok::IVR::Scripting;

#define	VMP_RX	0	
#define	VMP_TX	1

#define Leg1		"Leg1:"
#define Leg2		"Leg2:"

#define		PROMPTS_ROOT_FOLDER		"IVR\\PrepaidPrompts\\"
#define		RING									"ring.raw"

#define		SIP_ADDR_PREFIX				"sip:"
#define		SIP_ADDR_SUFIX_HEADER "@"
#define		SIP_PORT_DELIMETER		":"
#define		SIP_MESSAGE_DELIMETER	";"

#define		URL_ENCODED_POUND_SIGN	"%23"
#define		POUND_SIGN							"#"

#define		POLL_DELAY	1000
#define		RING_DELAY	3500
#define		MAX_NUMBER_OF_RINGS	8
#define		DTMF_WAIT_TIMEOUT	6000

#define	MAX_CARDS									1
#define MAX_MODULES								4
#define MAX_CALLS_PER_MODULE			375	

#define RUN														1
#define	STOP													0

#define RET_FAIL												-1
#define RET_SUCCESS											101
#define RET_NO_AVAILABLE_RESOURCES			121
#define RET_CHANNELS_OUT_OF_ORDER				131
#define RET_NO_EVENT_AVAILABLE					201
#define RET_ALL_NOT_PLAYED							701
#define RET_FILE_NOT_FOUND							702
#define RET_SIP_CURRENT									720
#define RET_SIP_NEXT										721
#define RET_CALL_TYPE_SIP								752
#define RET_SYSTEM_SHUTDOWN							801
#define RET_PLAY_STOPPED_ON_DISCONNECT	803
#define RET_PLAY_ABORTED								804
#define RET_MAX_RINGS										900
#define RET_REMOTE_DISCONNECT						911

//-- Logger
#define SPC_1							-1
#define SPC_2							-2

#define DBG								0
#define INF								1
#define WRN								2
#define ERR								3
#define CRT								4
#define SYSTEM						0
#define CHANNEL						1

#define LOCAL_RTP_PORT						1024
#define EMPTY_DISPLAY_NAME				""

#define LC_UNKNOWN			-1

#define MIN_THREAD_STACK_SIZE			131072

#define	ORIGINATION		0
#define DESTINATION		1
#define SOURCE_IVR		2


enum VoiceFileFormat {
	Invalid = 0,
	Raw = 1,
	Wav = 2
};

typedef struct _card_data {
	ACU_CARD_ID			card_id;
	const ACU_CHAR*	serial;
	ACU_UINT				num_of_modules;
	ACU_UINT				num_of_ports;
	ACU_QQ_NODE			node;
	ACU_QQ_HDR			PORTS;
	ACU_QQ_HDR			MODULES;
	ACU_INT					call_open;
	ACU_INT					switch_open;
	ACU_INT					speech_open;
} CARD_DATA;

typedef struct _card_information {
	ACU_CARD_ID			card_id;
	ACU_PORT_ID			port_id[4];
	ACU_PORT_ID     sip_port_id[16];
	char						serial[20];
	char						Signaling_IP_address[20];
	char						Media_IP_address[20];
	ACU_INT					num_of_modules;
	tSMModuleId			module_id[4];
	ACU_UINT				num_of_ports;
	ACU_CARD_INFO_PARMS	acuCard_info;
} CARD_INFORMATION;

static enum kSMCodecType Convert_payload_number_to_ting( ACU_INT payload_number ) {
	switch (payload_number) {
		case ACU_PCMU_PAYLOAD_NUMBER: 
			return kSMCodecTypeMulaw;
		case ACU_PCMA_PAYLOAD_NUMBER: 
			return kSMCodecTypeAlaw;
		case ACU_G729_PAYLOAD_NUMBER: 
			return kSMCodecTypeG729AB;
		default: 
			return (kSMCodecType)-1;
	}
};

