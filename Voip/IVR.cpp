//--
#include "ivr.h"

namespace Timok_IVR {
	//---------------------  Global method TiNG log ---------------------------------
	int ting_log(const char* format, va_list arglist) {
		static int _initialized = 0;
		static FILE* _fp;
		static ACU_OS_CRIT_SEC *_cs;

		if ( ! _initialized) {
			// Open file
			char _file_name[256];
			strcpy(_file_name, "c:\\ting_trace.txt");
			_fp = fopen(_file_name, "w+");

			// Create critical section
			_cs = acu_os_create_critical_section();
			_initialized = 1;
		}

		acu_os_lock_critical_section(_cs);
		vfprintf(_fp, format, arglist);
		fflush(_fp);
		acu_os_unlock_critical_section(_cs);

		return 0;
	}

	//------------------   IVR Class  ----------------------------------------------
	IVR::IVR() {
		totalCards = 0;
		totalModules = 0;
		totalPorts = 0;
		numberOfChannels = 0;

		//NOTE: no need to set TiNGTrace, using system environment variable instead
		TiNG_showtrace = &ting_log;
	}

	IVR::~IVR() { }

	// --------------------------- Private ---------------------------------------------------------
	//bool IVR::init_hardware() {
	//	int	 iResult = 0;

	//	try {
	//		logger->Log(INF, __FILE__, __LINE__, "Attempting to initialize hardware.");
	//		
	//		ACU_SNAPSHOT_PARMS acuSnapshot_parms;
	//		INIT_ACU_STRUCT( &acuSnapshot_parms);
	//		if ((iResult = acu_get_system_snapshot(&acuSnapshot_parms)) != 0)	{
	//			logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, acu_get_system_snapshot failed.", iResult));
	//			return false;
	//		}
	//		totalCards = acuSnapshot_parms.count;
	//		logger->Log(INF, __FILE__, __LINE__, String::Format("sm_open_module - iTotal_cards[{0}].", totalCards));

	//		if (totalCards == 0) {
	//			logger->Log(ERR, __FILE__, __LINE__, "ERROR: Card not found.");
	//			return false;
	//		}

	//		if (totalCards > 1) {
	//			logger->Log(ERR, __FILE__, __LINE__, "ERROR: More then 1 card installed, NOT Supported.");
	//			return false;
	//		}

	//		card = gcnew Card(0, ivrConfig);
	//		logger->Log(INF, __FILE__, __LINE__, String::Format("Serial number[{0}].", gcnew String((char *)acuSnapshot_parms.serial_no[0])));

	//		ACU_OPEN_CARD_PARMS	acuOpen_card_parms;
	//		INIT_ACU_STRUCT( &acuOpen_card_parms);
	//		strcpy(acuOpen_card_parms.serial_no, acuSnapshot_parms.serial_no[0]);
	//		strcpy(card->Card_info->serial, acuSnapshot_parms.serial_no[0]);
	//		if ((iResult = acu_open_card(&acuOpen_card_parms)) != 0) {
	//			logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, acu_open_card failed.", iResult));
	//			return false;
	//		}
	//			
	//		ACU_CARD_INFO_PARMS	acuCard_info;
	//		INIT_ACU_STRUCT( &acuCard_info);
	//		acuCard_info.card_id = acuOpen_card_parms.card_id;
	//		if ((iResult = acu_get_card_info(&acuCard_info)) != 0) {
	//			logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, acu_get_card_info().", iResult));
	//			return false;
	//		}

	//		if (ivrConfig->IsProsodyX) {
	//			strcpy(card->Card_info->Signaling_IP_address, acuCard_info.ip_address);
	//		}
	//		else {
	//			StringUtilities::StringConvertor _localIPAddress(ivrConfig->LocalIPAddress);
	//			memset(card->Card_info->Signaling_IP_address, 0, 20);
	//			_snprintf(card->Card_info->Signaling_IP_address, _localIPAddress.ToString()->Length + 1, "%s", _localIPAddress.NativeCharPtr);
	//		}
	//		logger->Log(INF, __FILE__, __LINE__, String::Format("Card IPAddress: {0}.", gcnew String((char *) card->Card_info->Signaling_IP_address)));
	//	
	//		if (acuCard_info.resources_available & ACU_RESOURCE_SWITCH) {
	//			ACU_OPEN_SWITCH_PARMS	strOpen_switch_parms;
	//			INIT_ACU_STRUCT( &strOpen_switch_parms);

	//			strOpen_switch_parms.card_id = acuCard_info.card_id;
	//			if ((iResult = acu_open_switch(&strOpen_switch_parms)) != 0) {
	//				logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, acu_open_switch().", iResult));
	//				return false;
	//			}
	//			logger->Log(INF, __FILE__, __LINE__, "acu_open_switch() success.");
	//		}

	//		//-- ACU_RESOURCE_SPEECH
	//		if (acuCard_info.resources_available & ACU_RESOURCE_SPEECH)	{
	//			ACU_OPEN_PROSODY_PARMS acuOpen_prosody_parms;
	//			INIT_ACU_STRUCT( &acuOpen_prosody_parms);

	//			acuOpen_prosody_parms.card_id = acuCard_info.card_id;
	//			if ((iResult = acu_open_prosody(&acuOpen_prosody_parms)) != 0) {
	//				logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, acu_open_prosody().", iResult));
	//				return false;
	//			}
	//			logger->Log(INF, __FILE__, __LINE__, "acu_open_prosody() success.");

	//			SM_CARD_INFO_PARMS smCard_info;
	//			INIT_ACU_SM_STRUCT( &smCard_info);
	//			smCard_info.card = acuCard_info.card_id;
	//			if ((iResult = sm_get_card_info(&smCard_info)) != 0) {
	//				logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sm_get_card_info().", iResult));
	//				return false;
	//			}
	//			logger->Log(INF, __FILE__, __LINE__, "sm_get_card_info() success.");

	//			totalModules += smCard_info.module_count;
	//			card->Card_info->num_of_modules = smCard_info.module_count;

	//			int _number_of_modules = ivrConfig->NumberOfModules <= smCard_info.module_count ? ivrConfig->NumberOfModules : smCard_info.module_count;
	//			
	//			SM_OPEN_MODULE_PARMS smModule_parms;
	//			for (int _countModules = 0; _countModules < _number_of_modules; _countModules++) {
	//				INIT_ACU_SM_STRUCT( &smModule_parms);
	//				smModule_parms.module_ix = _countModules;
	//				smModule_parms.card_id = acuCard_info.card_id;
	//				if ((iResult = sm_open_module(&smModule_parms)) != 0) {
	//					logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sm_open_module, has to do with adding new DSP without having all the DSP's on the board.", iResult));
	//					return false;
	//				}
	//				card->Card_info->module_id[_countModules] = smModule_parms.module_id;
	//				logger->Log(INF, __FILE__, __LINE__, String::Format("sm_open_module() success, module: {0}.", _countModules));
	//			}
	//		}

	//		//-- ACU_RESOURCE_CALL
	//		if (acuCard_info.resources_available & ACU_RESOURCE_CALL)	{
	//			ACU_OPEN_CALL_PARMS	acuOpen_call_parms;
	//			INIT_ACU_STRUCT( &acuOpen_call_parms);

	//			acuOpen_call_parms.card_id = acuOpen_card_parms.card_id;
	//			card->Card_info->card_id = acuOpen_card_parms.card_id;
	//			if ((iResult = acu_open_call(&acuOpen_call_parms)) != 0) {
	//				logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, acu_open_call().", iResult));
	//				return false;
	//			}
	//			logger->Log(INF, __FILE__, __LINE__, "acu_open_call() success.");

	//			//Speach driver goes here.//	
	//			CARD_INFO_PARMS	strCard_info_parms;
	//			INIT_ACU_STRUCT( &strCard_info_parms);
	//			strCard_info_parms.card_id = acuOpen_card_parms.card_id;
	//			if ((iResult = call_get_card_info(&strCard_info_parms)) != 0)	{
	//				logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, call_get_card_info().", iResult));
	//				return false;
	//			}
	//			logger->Log(INF, __FILE__, __LINE__, "call_get_card_info() success.");
	//							
	//			if (acuCard_info.resources_available & ACU_RESOURCE_IP_TELEPHONY ) {
	//				logger->Log(DBG, __FILE__, __LINE__, "ACU_RESOURCE_IP_TELEPHONY.");
	//				card->Card_info->num_of_ports = (strCard_info_parms.ports -1);
	//				totalPorts = (strCard_info_parms.ports - 1);
	//			}
	//			else {
	//				logger->Log(DBG, __FILE__, __LINE__, " Not ACU_RESOURCE_IP_TELEPHONY.");
	//				card->Card_info->num_of_ports = strCard_info_parms.ports;
	//				totalPorts = strCard_info_parms.ports;
	//			}
	//			logger->Log(DBG, __FILE__, __LINE__, String::Format("Total ports: {0}.", totalPorts));

	//			PORT_INFO_PARMS	strPort_info;
	//			INIT_ACU_STRUCT( &strPort_info);
	//			if (ivrConfig->IsProsodyX) {
	//				OPEN_PORT_PARMS	strOpen_port;
	//				INIT_ACU_STRUCT( &strOpen_port);

	//				for (UINT _countPorts = 0; _countPorts < card->Card_info->num_of_ports; _countPorts++)	{
	//					strOpen_port.port_ix = _countPorts;
	//					strOpen_port.card_id = acuOpen_card_parms.card_id;
	//					if ((iResult = call_open_port( &strOpen_port )) != 0)	{
	//						logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, call_open_port().", iResult));
	//						return false;
	//					}
	//					//acuPort_id_from_call_open_port[iPort_count] = strOpen_port.port_id;
	//					logger->Log(DBG, __FILE__, __LINE__, String::Format("call_open_port() port[{0}] portId[{1}].", _countPorts, strOpen_port.port_id));

	//					strPort_info.port_id = strOpen_port.port_id;
	//					card->Card_info->port_id[_countPorts] = strOpen_port.port_id;
	//					//May not need this function
	//					if ((iResult = call_port_info( &strPort_info )) != 0)	{
	//						logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, call_port_info().", iResult));
	//						return false;
	//					}
	//					logger->Log(INF, __FILE__, __LINE__, String::Format("TotalChannels on port[{0}] is [{1}].", _countPorts, strPort_info.channel_count));
	//				}
	//			}
	//		}

	//		ACU_PORT_ID _sipPortId = 0;
	//		for (ACU_INT iSIP_port_count = 0; iSIP_port_count < card->Card_info->num_of_modules; iSIP_port_count++) {
	//			//SRDJAN-04-17-2008
	//			//if (acuCard_info.resources_available & ACU_RESOURCE_IP_TELEPHONY ) {
	//				if ((iResult = sip_open_port(&_sipPortId)) != 0) {
	//					logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sip_open_port.", iResult));
	//					return false;
	//				}
	//				logger->Log(INF, __FILE__, __LINE__, String::Format("sip_open_port, port_id {0}.", _sipPortId));
	//				card->Card_info->sip_port_id[iSIP_port_count] = _sipPortId;
	//			//SRDJAN-04-17-2008 }
	//		}

	//		memcpy(&card->Card_info->acuCard_info, &acuCard_info, sizeof(acuCard_info));

	//		//-- Initialize credentials
	//		if (ivrConfig->CredentialsRealm->Length > 0) {
	//			initialize_credentials();
	//		}
	//		else {
	//			logger->Log(INF, __FILE__, __LINE__, "No credentials, will not use SIP authentication.");
	//		}
	//	}
	//	catch(Exception^ _ex) {
	//		logger->Log(ERR, __FILE__, __LINE__, String::Format("Init_hardware Exception - [{0}].", _ex));
	//		return false;
	//	}
	//	logger->Log(INF, __FILE__, __LINE__, "Hardware initialized successfully.");
	//	return true;
	//}

	bool IVR::init_hardware() {
		int	 iResult = 0;
		
		ACU_OPEN_CARD_PARMS	acuOpen_card_parms;
		INIT_ACU_STRUCT( &acuOpen_card_parms);

		ACU_CARD_INFO_PARMS	acuCard_info;
		INIT_ACU_STRUCT( &acuCard_info);

		ACU_OPEN_CALL_PARMS	acuOpen_call_parms;
		INIT_ACU_STRUCT( &acuOpen_call_parms);

		CARD_INFO_PARMS	strCard_info_parms;
		INIT_ACU_STRUCT( &strCard_info_parms);

		OPEN_PORT_PARMS	strOpen_port;
		INIT_ACU_STRUCT( &strOpen_port);

		PORT_INFO_PARMS	strPort_info;
		INIT_ACU_STRUCT( &strPort_info);

		ACU_OPEN_SWITCH_PARMS	strOpen_switch_parms;
		INIT_ACU_STRUCT( &strOpen_switch_parms);

		ACU_OPEN_PROSODY_PARMS acuOpen_prosody_parms;
		INIT_ACU_STRUCT( &acuOpen_prosody_parms);

		//SM_CARD_INFO_PARMS smCard_info;
		//INIT_ACU_SM_STRUCT( &smCard_info);

		SM_OPEN_MODULE_PARMS smModule_parms;
		INIT_ACU_SM_STRUCT( &smModule_parms);

		try {
			logger->Log(INF, __FILE__, __LINE__, "Attempting to initialize hardware.");
			
			ACU_SNAPSHOT_PARMS acuSnapshot_parms;
			int _i = 0;
			while (totalCards == 0 && _i++ < 8) {
				if (_i > 0) {
					Sleep(1000);
				}
				INIT_ACU_STRUCT( &acuSnapshot_parms);
				if ((iResult = acu_get_system_snapshot(&acuSnapshot_parms)) != 0)	{
					logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, acu_get_system_snapshot failed.", iResult));
					return false;
				}
				totalCards = acuSnapshot_parms.count;
			}
			logger->Log(INF, __FILE__, __LINE__, String::Format("sm_open_module - iTotal_cards[{0}].", totalCards));

			if (totalCards == 0) {
				logger->Log(ERR, __FILE__, __LINE__, "ERROR: Card not found.");
				return false;
			}

			if (totalCards > 1) {
				logger->Log(ERR, __FILE__, __LINE__, "ERROR: More then 1 card installed, NOT Supported.");
				return false;
			}

			cards = gcnew array<Card^>(totalCards); 
			for (int _countCards = 0; _countCards < totalCards; _countCards++)	{
				cards[_countCards] = gcnew Card(_countCards, ivrConfig, logger);
				logger->Log(INF, __FILE__, __LINE__, String::Format("sm_open_module - _countCards[{0}].", _countCards));
				logger->Log(INF, __FILE__, __LINE__, String::Format("Serial number[{0}].", gcnew String((char *)acuSnapshot_parms.serial_no[_countCards])));

				strcpy(acuOpen_card_parms.serial_no, acuSnapshot_parms.serial_no[_countCards]);
				strcpy(cards[_countCards]->Card_info->serial, acuSnapshot_parms.serial_no[_countCards]);
				if ((iResult = acu_open_card(&acuOpen_card_parms)) != 0) {
					logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, acu_open_card failed.", iResult));
					return false;
				}
				
				acuCard_info.card_id = acuOpen_card_parms.card_id;
				if ((iResult = acu_get_card_info(&acuCard_info)) != 0) {
					logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, acu_get_card_info().", iResult));
					return false;
				}

				//TODO: ask Chris how to get both IP addresses from Card API, otherwise configured in the config file, and set in Card.cpp
				//if (ivrConfig->IsProsodyX) {
				//	strcpy(cards[_countCards]->Card_info->Signaling_IP_address, acuCard_info.ip_address);
				//}
				//else {
				//	StringUtilities::StringConvertor _localIPAddress(ivrConfig->LocalIPAddress);
				//	memset(cards[_countCards]->Card_info->Signaling_IP_address, 0, 20);
				//	_snprintf(cards[_countCards]->Card_info->Signaling_IP_address, _localIPAddress.ToString()->Length + 1, "%s", _localIPAddress.NativeCharPtr);
				//	logger->Log(INF, __FILE__, __LINE__, String::Format("Signaling IPAddress: {0}.", gcnew String((char *) cards[_countCards]->Card_info->Signaling_IP_address)));
				//}

				if (acuCard_info.resources_available & ACU_RESOURCE_SWITCH) {
					strOpen_switch_parms.card_id = acuCard_info.card_id;
					if ((iResult = acu_open_switch(&strOpen_switch_parms)) != 0) {
						logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, acu_open_switch().", iResult));
						return false;
					}
					logger->Log(INF, __FILE__, __LINE__, "acu_open_switch() success.");
				}

				//-- ACU_RESOURCE_SPEECH
				if (acuCard_info.resources_available & ACU_RESOURCE_SPEECH)	{
					acuOpen_prosody_parms.card_id = acuCard_info.card_id;
					if ((iResult = acu_open_prosody(&acuOpen_prosody_parms)) != 0) {
						logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, acu_open_prosody().", iResult));
						return false;
					}
					logger->Log(INF, __FILE__, __LINE__, "acu_open_prosody() success.");

					SM_CARD_INFO_PARMS smCard_info;
					INIT_ACU_SM_STRUCT( &smCard_info);
					smCard_info.card = acuCard_info.card_id;
					if ((iResult = sm_get_card_info(&smCard_info)) != 0) {
						logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sm_get_card_info().", iResult));
						return false;
					}
					logger->Log(INF, __FILE__, __LINE__, "sm_get_card_info() success.");

					totalModules += smCard_info.module_count;
					cards[_countCards]->Card_info->num_of_modules = smCard_info.module_count;

					int _number_of_modules = ivrConfig->NumberOfModules <= smCard_info.module_count ? ivrConfig->NumberOfModules : smCard_info.module_count;
					for (int _countModules = 0; _countModules < _number_of_modules; _countModules++) {
						smModule_parms.module_ix = _countModules;
						smModule_parms.card_id = acuCard_info.card_id;
						if ((iResult = sm_open_module(&smModule_parms)) != 0) {
							logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sm_open_module, has to do with adding new DSP without having all the DSP's on the board.", iResult));
							return false;
						}
						cards[_countCards]->Card_info->module_id[_countModules] = smModule_parms.module_id;
						logger->Log(INF, __FILE__, __LINE__, String::Format("sm_open_module() success, module: {0}.", _countModules));
					}
				}

				//-- ACU_RESOURCE_CALL
				if (acuCard_info.resources_available & ACU_RESOURCE_CALL)	{
					acuOpen_call_parms.card_id = acuOpen_card_parms.card_id;
					cards[_countCards]->Card_info->card_id = acuOpen_card_parms.card_id;
					if ((iResult = acu_open_call(&acuOpen_call_parms)) != 0) {
						logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, acu_open_call().", iResult));
						return false;
					}
					logger->Log(INF, __FILE__, __LINE__, "acu_open_call() success.");

					//Speach driver goes here.//	
					strCard_info_parms.card_id = acuOpen_card_parms.card_id;
					if ((iResult = call_get_card_info(&strCard_info_parms)) != 0)	{
						logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, call_get_card_info().", iResult));
						return false;
					}
					logger->Log(INF, __FILE__, __LINE__, "call_get_card_info() success.");
									
					if (acuCard_info.resources_available & ACU_RESOURCE_IP_TELEPHONY ) {
						logger->Log(DBG, __FILE__, __LINE__, "ACU_RESOURCE_IP_TELEPHONY.");
						cards[_countCards]->Card_info->num_of_ports = (strCard_info_parms.ports -1);
						totalPorts = (strCard_info_parms.ports - 1);
					}
					else {
						logger->Log(DBG, __FILE__, __LINE__, " Not ACU_RESOURCE_IP_TELEPHONY.");
						cards[_countCards]->Card_info->num_of_ports = strCard_info_parms.ports;
						totalPorts = strCard_info_parms.ports;
					}
					logger->Log(DBG, __FILE__, __LINE__, String::Format("Total ports: {0}.", totalPorts));

					if (ivrConfig->IsProsodyX) {
						for (UINT _countPorts = 0; _countPorts < cards[_countCards]->Card_info->num_of_ports; _countPorts++)	{
							strOpen_port.port_ix = _countPorts;
							strOpen_port.card_id = acuOpen_card_parms.card_id;
							if ((iResult = call_open_port( &strOpen_port )) != 0)	{
								logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, call_open_port().", iResult));
								return false;
							}
							//acuPort_id_from_call_open_port[iPort_count] = strOpen_port.port_id;
							logger->Log(DBG, __FILE__, __LINE__, String::Format("call_open_port() port[{0}] portId[{1}].", _countPorts, strOpen_port.port_id));

							strPort_info.port_id = strOpen_port.port_id;
							cards[_countCards]->Card_info->port_id[_countPorts] = strOpen_port.port_id;

							//May not need this function
							if ((iResult = call_port_info( &strPort_info )) != 0)	{
								logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, call_port_info().", iResult));
								return false;
							}
							logger->Log(INF, __FILE__, __LINE__, String::Format("TotalChannels on port[{0}] is [{1}].", _countPorts, strPort_info.channel_count));
						}
					}
				}

				ACU_PORT_ID _sipPortId = 0;
				for (ACU_INT iSIP_port_count = 0; iSIP_port_count < cards[_countCards]->Card_info->num_of_modules; iSIP_port_count++) {
					//SRDJAN-04-17-2008
					//if (acuCard_info.resources_available & ACU_RESOURCE_IP_TELEPHONY ) {
						if ((iResult = sip_open_port(&_sipPortId)) != 0) {
							logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sip_open_port.", iResult));
							return false;
						}
						logger->Log(INF, __FILE__, __LINE__, String::Format("sip_open_port, port_id {0}.", _sipPortId));
						cards[_countCards]->Card_info->sip_port_id[iSIP_port_count] = _sipPortId;
					//SRDJAN-04-17-2008 }
				}

				memcpy(&cards[_countCards]->Card_info->acuCard_info, &acuCard_info, sizeof(acuCard_info));
			}

			//-- Initialize credentials
			if (ivrConfig->CredentialsRealm->Length > 0) {
				initialize_credentials();
			}
			else {
				logger->Log(INF, __FILE__, __LINE__, "No credentials, will not use SIP authentication.");
			}
		}
		catch(Exception^ _ex) {
			logger->Log(ERR, __FILE__, __LINE__, String::Format("Init_hardware Exception - [{0}].", _ex));
			return false;
		}
		logger->Log(INF, __FILE__, __LINE__, "Hardware initialized successfully.");
		return true;
	}

	void IVR::initialize_credentials() {
		SIP_ANSWER_CHALLENGE_CREDENTIALS_PARMS credentials; 
		INIT_ACU_STRUCT(&credentials); 

		//TODO: load from db, realm(domain), username(alias), passwd
		//this may not require db change, username(stored in ailas) can be in the form: srdjan@timok.com  
		char _realm[32] = {0};
		_snprintf(_realm, 32, "%s", ivrConfig->CredentialsRealm);
		credentials.realm = _realm;
		
		char _user[32] = {0};
		_snprintf(_user, 32, "%s", ivrConfig->CredentialsUser);
		credentials.user = _user;

		char _password[32] = {0};
		_snprintf(_password, 32, "%s", ivrConfig->CredentialsPassword);
		credentials.password = _password;

		ACU_ERR _result = sip_remove_answer_challenge_credentials(&credentials);
		_result = sip_add_answer_challenge_credentials(&credentials);
		if (_result != 0) {
			logger->Log(ERR, __FILE__, __LINE__, String::Format("ERROR: {0}, sip_add_answer_challenge_credentials.", gcnew String(error_2_string(_result))));
		}
		else {
			logger->Log(INF, __FILE__, __LINE__, "sip_add_answer_challenge_credentials set Ok");
		}
	}

	//bool IVR::cleanup_hardware() {
	//	int iResult	= 0;
	//
	//	try {
	//		int _number_of_modules = ivrConfig->NumberOfModules <= card->Card_info->num_of_modules ? ivrConfig->NumberOfModules : card->Card_info->num_of_modules;
	//		for (int iModule_count = 0; iModule_count < _number_of_modules; iModule_count++)	{
	//			logger->Log(INF, __FILE__, __LINE__, String::Format("Card [{0}] Module [{1}].", 1, (long) card->Card_info->module_id[iModule_count]));

	//			SM_CLOSE_MODULE_PARMS smClose_module_parms;
	//			INIT_ACU_SM_STRUCT(&smClose_module_parms);
	//			smClose_module_parms.module_id  = card->Card_info->module_id[iModule_count];
	//			if ((iResult = sm_close_module(&smClose_module_parms)) != 0)	{
	//				logger->Log(ERR, __FILE__, __LINE__, String::Format("sm_close_module() error [{0}].", iResult));
	//			}
	//			else {
	//				logger->Log(INF, __FILE__, __LINE__, String::Format("sm_close_module: done on module id[{0}].", (long) card->Card_info->module_id[iModule_count]));
	//			}
	//		}

	//		UINT iCount_ports;
	//		CLOSE_PORT_PARMS strClose_port;
	//		for (iCount_ports = 0; iCount_ports < card->Card_info->num_of_ports; iCount_ports++) {
	//			logger->Log(INF, __FILE__, __LINE__, String::Format("call_close_port port id[{0}].", card->Card_info->port_id[iCount_ports]));

	//			INIT_ACU_STRUCT(&strClose_port);
	//			strClose_port.port_id = card->Card_info->port_id[iCount_ports];
	//			if ((iResult = call_close_port(&strClose_port)) != 0) {
	//				if (iResult == -510) {
	//					Sleep(5000);//Wait for call to end, so port is closed.//
	//					if ((iResult = call_close_port(&strClose_port)) != 0) {
	//						logger->Log(ERR, __FILE__, __LINE__, String::Format("call_close_port() error [{0}].", iResult));
	//					}
	//				}
	//			}
	//		}

	//		if (card->Card_info->acuCard_info.resources_available & ACU_RESOURCE_SWITCH) {
	//			ACU_CLOSE_SWITCH_PARMS acuClose_switch;
	//			INIT_ACU_STRUCT(&acuClose_switch);
	//			acuClose_switch.card_id = card->Card_info->card_id;
	//			logger->Log(INF, __FILE__, __LINE__, String::Format("Cleanup_and_close - call_close_port close switch, card id[{0}].", card->Card_info->card_id));
	//			if ((iResult = acu_close_switch(&acuClose_switch)) != 0)	{
	//				logger->Log(ERR, __FILE__, __LINE__, String::Format("acu_close_switch() error [{0}].", iResult));
	//			}
	//		}

	//		if (card->Card_info->acuCard_info.resources_available & ACU_RESOURCE_CALL) {
	//			logger->Log(INF, __FILE__, __LINE__, String::Format("Cleanup_and_close - call_close_port close call, card id[{0}].", card->Card_info->card_id));

	//			ACU_CLOSE_CALL_PARMS acuClose_call;
	//			INIT_ACU_STRUCT(&acuClose_call);
	//			acuClose_call.card_id = card->Card_info->card_id;
	//			if ((iResult = acu_close_call(&acuClose_call)) != 0)	{
	//				logger->Log(ERR, __FILE__, __LINE__, String::Format("call_close_call() error [{0}].", iResult));
	//			}
	//		}

	//		if (card->Card_info->acuCard_info.resources_available & ACU_RESOURCE_SPEECH) {
	//			logger->Log(INF, __FILE__, __LINE__, String::Format("Cleanup_and_close - close prosod,: card id[{0}].", card->Card_info->card_id));

	//			ACU_CLOSE_PROSODY_PARMS acuClose_prosody_parms;
	//			INIT_ACU_SM_STRUCT(&acuClose_prosody_parms);
	//			acuClose_prosody_parms.card_id  = card->Card_info->card_id;
	//			if ((iResult = acu_close_prosody(&acuClose_prosody_parms)) != 0)	{
	//				logger->Log(ERR, __FILE__, __LINE__, String::Format("acu_close_prosody() error [{0}].", iResult));
	//			}
	//		}

	//		logger->Log(INF, __FILE__, __LINE__, String::Format("Cleanup_and_close - close card: [{0}].", card->Card_info->card_id));
	//		ACU_CLOSE_CARD_PARMS acuClose_card;
	//		INIT_ACU_STRUCT(&acuClose_card);
	//		acuClose_card.card_id  = card->Card_info->card_id;
	//		if ((iResult = acu_close_card(&acuClose_card)) != 0) {
	//		logger->Log(ERR, __FILE__, __LINE__, String::Format("acuClose_card() error [{0}].", iResult));
	//		}

	//		Sleep(0);	
	//		logger->Log(INF, __FILE__, __LINE__, "Cleanup_and_close - ** Complete **.");
	//	}
	//	catch(Exception^ e) {
	//		logger->Log(ERR, __FILE__, __LINE__, String::Format("Cleanup_and_close - exception[{0}].", e->ToString()));
	//	}
	//	return true;
	//}
	bool IVR::cleanup_hardware() {
		int iResult	= 0;
		
		CLOSE_PORT_PARMS strClose_port;
		INIT_ACU_STRUCT(&strClose_port);

		ACU_SNAPSHOT_PARMS acuSnapshot_parms;
		INIT_ACU_STRUCT(&acuSnapshot_parms);

		CAUSE_XPARMS strCause_xparms;
		INIT_ACU_STRUCT(&strCause_xparms);

		ACU_CLOSE_CALL_PARMS acuClose_call;
		INIT_ACU_STRUCT(&acuClose_call);

		ACU_CLOSE_CARD_PARMS acuClose_card;
		INIT_ACU_STRUCT(&acuClose_card);

		ACU_CLOSE_SWITCH_PARMS acuClose_switch;
		INIT_ACU_STRUCT(&acuClose_switch);

		SM_CLOSE_MODULE_PARMS smClose_module_parms;
		INIT_ACU_SM_STRUCT(&smClose_module_parms);

		ACU_CLOSE_PROSODY_PARMS acuClose_prosody_parms;
		INIT_ACU_SM_STRUCT(&acuClose_prosody_parms);
		
		try {
			//-- Deallocate resources
			for (int _countCards = 0; _countCards < totalCards; _countCards++) {
				int _number_of_modules = ivrConfig->NumberOfModules <= cards[_countCards]->Card_info->num_of_modules ? ivrConfig->NumberOfModules : cards[_countCards]->Card_info->num_of_modules;
				for (int iModule_count = 0; iModule_count < _number_of_modules; iModule_count++)	{
					logger->Log(INF, __FILE__, __LINE__, String::Format("Card [{0}] Module [{1}].", _countCards, (long) cards[_countCards]->Card_info->module_id[iModule_count]));

					smClose_module_parms.module_id  = cards[_countCards]->Card_info->module_id[iModule_count];
					if ((iResult = sm_close_module(&smClose_module_parms)) != 0)	{
						logger->Log(ERR, __FILE__, __LINE__, String::Format("sm_close_module() error [{0}].", iResult));
					}
					else {
						logger->Log(INF, __FILE__, __LINE__, String::Format("sm_close_module: done on module id[{0}].", (long) cards[_countCards]->Card_info->module_id[iModule_count]));
					}
				}

				UINT iCount_ports;
				for (iCount_ports = 0; iCount_ports < cards[_countCards]->Card_info->num_of_ports; iCount_ports++) {
					logger->Log(INF, __FILE__, __LINE__, String::Format("call_close_port port id[{0}].", cards[_countCards]->Card_info->port_id[iCount_ports]));
					strClose_port.port_id = cards[_countCards]->Card_info->port_id[iCount_ports];
					if ((iResult = call_close_port(&strClose_port)) != 0) {
						if (iResult == -510) {
							Sleep(5000);//Wait for call to end, so port is closed.//
							if ((iResult = call_close_port(&strClose_port)) != 0) {
								logger->Log(ERR, __FILE__, __LINE__, String::Format("call_close_port() error [{0}].", iResult));
							}
						}
					}
				}

				if (cards[_countCards]->Card_info->acuCard_info.resources_available & ACU_RESOURCE_SWITCH) {
					acuClose_switch.card_id = cards[_countCards]->Card_info->card_id;
					logger->Log(INF, __FILE__, __LINE__, String::Format("Cleanup_and_close - call_close_port close switch, card id[{0}].", cards[_countCards]->Card_info->card_id));
					if ((iResult = acu_close_switch(&acuClose_switch)) != 0)	{
						logger->Log(ERR, __FILE__, __LINE__, String::Format("acu_close_switch() error [{0}].", iResult));
					}
				}

				if (cards[_countCards]->Card_info->acuCard_info.resources_available & ACU_RESOURCE_CALL) {
					logger->Log(INF, __FILE__, __LINE__, String::Format("Cleanup_and_close - call_close_port close call, card id[{0}].", cards[_countCards]->Card_info->card_id));
					acuClose_call.card_id = cards[_countCards]->Card_info->card_id;
					if ((iResult = acu_close_call(&acuClose_call)) != 0)	{
						logger->Log(ERR, __FILE__, __LINE__, String::Format("call_close_call() error [{0}].", iResult));
					}
				}

				if (cards[_countCards]->Card_info->acuCard_info.resources_available & ACU_RESOURCE_SPEECH) {
					logger->Log(INF, __FILE__, __LINE__, String::Format("Cleanup_and_close - close prosod,: card id[{0}].", cards[_countCards]->Card_info->card_id));
					acuClose_prosody_parms.card_id  = cards[_countCards]->Card_info->card_id;
					if ((iResult = acu_close_prosody(&acuClose_prosody_parms)) != 0)	{
						logger->Log(ERR, __FILE__, __LINE__, String::Format("acu_close_prosody() error [{0}].", iResult));
					}
				}

				logger->Log(INF, __FILE__, __LINE__, String::Format("Cleanup_and_close - close card: [{0}].", cards[_countCards]->Card_info->card_id));
				acuClose_card.card_id  = cards[_countCards]->Card_info->card_id;
				if ((iResult = acu_close_card(&acuClose_card)) != 0) {
					logger->Log(ERR, __FILE__, __LINE__, String::Format("acuClose_card() error [{0}].", iResult));
				}
				Sleep(0);	
			}
			logger->Log(INF, __FILE__, __LINE__, "Cleanup_and_close - ** Complete **.");
		}
		catch(Exception^ e) {
			logger->Log(ERR, __FILE__, __LINE__, String::Format("Cleanup_and_close - exception[{0}].", e->ToString()));
		}
		return true;
	}
}