namespace Timok.Rbr.Core {
	public enum RbrResult : short {
		Success = 0,
		Continue = 2,
		//
		ExceptionThrown = 1000,
		Unknown = 1001,
		//
		OrigEP_NotFound = 1100,
		OrigEP_InvalidCache = 1101,
		OrigEP_NotActive = 1102,
		OrigEP_UnknownType = 1103,
		OrigEP_UnknownUsage = 1104,
		OrigEP_NotFoundInCache = 1105,
		OrigEP_AliasNotMatching = 1106,
		OrigEP_NotRegistered = 1107,
		OrigEP_IPInvalid = 1118,
		OrigEP_ProtocolNotMatching = 1109,
		//
		Customer_NotFound = 1200,
		Customer_LimitReached = 1201,
		Customer_NotActive = 1202,
		Customer_RateNotFound = 1203,
		Customer_BalanceInvalid = 1204,
		Customer_NoRetailRating = 1205,
		Customer_RouteNotSupported = 1206,
		Customer_AcctTypeInvalid = 1207,
		Customer_NoRetail = 1208,
		Customer_SubAcctNotFound = 1219,
		Customer_RetailTypeInvalid = 1210,
		Customer_RouteNotFound = 1211,
		Customer_RouteBlocked = 1212,
		Customer_DialCodeNotFound = 1213,
		Customer_DialPeerNotFound = 1214,
		Customer_MaxCallsReached = 1215,
		//
		User_NotFound = 1300,
		User_RateNotFound = 1301,
		User_LimitReached = 1302,
		User_NotActive = 1303,
		User_BalanceInvalid = 1304,
		User_StateInvalid = 1305,
		User_InvalidCardNumber = 1306,
		User_InvalidAuthToken = 1307,
		//
		TermEP_NotFound = 1400,
		TermEP_NotActive = 1401,
		TermChoice_NotFound = 1402,
		//
		Routing_AlgorithmUnknown = 1500,
		Routing_LimitReached = 1501,
		Routing_NoTerminationFound = 1502,
		Routing_InvalidPriority = 1503,
		Routing_NoCountry = 1504,
		Routing_NoBreakout = 1505,
		Routing_NoCountryNoBreakout = 1506,
		Routing_RouteNotFound = 1507,
		Routing_TypeUnknown = 1508,
		Routing_TooManyRetries = 1509,
		Routing_InvalidAccessNumber = 1510,
		//
		DialedNumber_Invalid = 1600,
		DialedNumber_Error_Rewriting = 1601,
		//
		Route_NotFound = 1700,
		Route_NotInDialPlan = 1701,
		Route_Blocked = 1702,
		Route_ExceptionThrown = 1703,
		//
		DNIS_NotInUse = 1800,
		DNIS_NotFound = 1801,
		DNIS_Invalid = 1802,
		//
		DialPeer_NotFound = 1900,
		//
		Batch_NotFound = 2000,
		Batch_NotOrdered = 2001,
		//
		Service_NotFound = 2100,
		Service_NotActive = 2101,
		//
		Rate_NotFound = 2200,
		Rate_TypeUnknown = 2201,
		//
		Imdb_NotSupported = 2300,
		//
		Carrier_NotFound = 2400,
		Carrier_NotActive = 2401,
		Carrier_LimitReached = 2402,
		//
		Carrier_RouteNotInDialPlan = 2451,
		Carrier_RouteNotFound = 2452,
		Carrier_RouteNotActive = 2453,
		Carrier_NoRoutesFound = 2454,
		Carrier_NoTermEPsFound = 2455,

		//
		Retail_AcctNotFound = 2500,
		Retail_RouteNotSupported = 2501,
		Retail_NoSubAccount = 2502,
		Retail_NoBalance = 2503,
		Retail_UnknownType = 2505,
		Retail_NoBalanceForRoute = 2506,
		Retail_AcctNotActive = 2507,
		Retail_SubAcctNotActive = 2508,
		Retail_CardNumberInvalid = 2509,
		Retail_AuthenticationFailed = 2510,
		Retail_AuthenticationTypeUnknown = 2511,
		Retail_AcctExpired = 2512,
		//
		SaleChannelInvalid = 2600,
		//
		SIP_NotRegistered = 2700,
		SIP_AuthFailed = 2701,
		//
		ANI_Invalid = 2800,
		//
		Alias_Invalid = 2900,
		//
		PromptPlayer_Failed = 3000,
		//
		MakeCall_Failed = 3100,
		MakeCall_MaxRings = 3101,
		//
		GetDestNumberDTMF_error = 3200,
		GetCardNumberDTMF_error = 3201,
		//
		CallEnd_Failed = 3300,
		WaitForCallEnd_Failed = 3301,
		//
		Router_NotFound = 3400,
		//
		IVR_Leg1Disconnect = 3500,
		IVR_Leg2Disconnect = 3501,
		IVR_CollectDTMF_MAXRetries = 3502,
		IVR_MakeCallOut = 3503,
		IVR_Collect_DTMF_Shutdown = 3504,
		IVR_GetChoice_Error = 3505,
		IVR_InvalidPromptFiles = 3506,
		IVR_PromptFileNotPresent = 3507,
		IVR_Init_Error = 3508,
		IVR_ConfigResources = 3509,
		IVR_NoAnswer = 3510,
		IVR_SIPOutFailed = 3511,
		IVR_OriginationDisconnect = 3512,
		IVR_OriginationIdle = 3513,
		IVR_SIPDetailsOutboundException = 3514,
		IVR_OutboundBadMediaPort = 3515,
		IVR_DestinationDisconnect = 3516,
		IVR_DestinationIdle = 3517,
		IVR_DestinationMediaReject = 3518,
		IVR_DestinationMediaRejectRequest = 3519,
		IVR_Shutdown = 3520,
		IVR_CheckOutboundEventTimeout = 3521,

		//
		CPS_GlobalLimitReached = 3600,
		CPS_EndpointLimitReached = 3601,
		//
		Prefix_NotValid = 3700,
		Prefix_TypeNotValid = 3701,
		//
		Cdr_Unexpected = 3800,
		Cdr_NotFound = 3801,
		//
		POSA_Exception = 4000,
		POSA_AcctNotFound = 4001,
		POSA_InvalidAcctStatus = 4002,
		POSA_InvalidCallHistoryScope = 4003
	}
}