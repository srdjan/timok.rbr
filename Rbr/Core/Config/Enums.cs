using System;
namespace Timok.Rbr.Core.Config {
	public enum SessionState {
		Leg1,
		Leg2Signaling,
		Leg2Redirecting,
		Leg2
	}

	[Flags]
  public enum RouteState {
    Valid = 0,
    NoDialCodes = 1,
    NotInRoutingPlan = 2,
    NoTerminations = 4,
    NoActiveEndpoints = 8,
    NoRates = 16,
  }

  public enum RouteType {
    Carrier,
    Wholesale,
    Retail
  }

  public enum CommisionType : byte {
    MarkupPercent,
    MarkupDollar,
    FeePerCall,
    FeePerMinute
  }

  public enum DefaultTerminationMode : byte {
    none,
    NoPrefix,
    WithPrefix
  }

  public enum BalanceAdjustmentReasonType : byte {
    Wholesale,
    Retail
  }

  public enum CallStatsType {
    Total,
    Customer,
    Carrier
  }
	
	public enum CallState {
		Initializing,
		Started,
		Connecting,
		Connected
	}

  public enum InventoryCommand {
    Generate,
    Load,
    Activate,
    Deactivate,
    Archive, //unload cards from db ?
  	Export
  }

  public enum InventoryStatus {
    Generated,
    Loaded,
    Activated,
    Deactivated,
    Archived //cards unloaded from db ?
  }

	//should match datatype in DB
  public enum SurchargeType : byte {
		None,
		PerCall, 
		PerMinute
	}

	public enum AccessScope {
    None = -1,
    Switch,
		VirtualSwitch,
		Partner,
    ResellAgent, 
		Consumer, 
    CustomerSupport
	}

  public enum CustomerSupportGroupRole {
    Agent = 0,
    Manager = 1,
    Supervisor = 2
  }

  public enum PermissionType : byte {
		Read, 
		Write
	}

	public enum TransportType : byte {
		None, 
		Ftp
	}

	public enum TxType {
		None,
		//Add, 
		Delete
	}
	
	public enum CdrExportFormat {
		Default,
		IncludeOrigPrefix
	}
	
	public enum CdrExportDelimeter: byte {
		Comma = 44,
		Tab = 9,
		Pipe = 124
	}

	//should match datatype in DB
	public enum ScheduleType: byte {
		DayOfWeek,
		DayOfMonth
	}

	//should match datatype in DB
	public enum RoutingAlgorithmType : byte {
		Unknown = 0,
		Manual = 1,
		LoadBalance = 2,
		LCR = 3,
		Invalid = 10
	}

	//should match datatype in DB
	public enum Status: byte {
		Pending,
		Active,
		Archived,
		Blocked,
		InUse
	}

	//should match datatype in DB
	public enum RatingType : byte {
		Disabled = 0,
		TimeBased = 1,
		PerCall = 2,
		NotSet
	}

	//should match datatype in DB
	public enum TypeOfDayChoice : byte {
		RegularDay,
		Weekend,
		Holiday
	}

	//should match datatype in DB
	public enum TimeOfDayPolicy {
		Flat,
		PeakOffPeak,
		NightDayEve
	}

	//should match datatype in DB
	public enum TimeOfDay {
		BlockedFlat,				//0 Color.Gray
		Flat,								//1 Color.White
		BlockedPeakOffPeak,	//2 Color.Gray
		Peak,								//3 Color.White
    OffPeak,						//4 Color.LightGreen
		BlockedNightDayEve,	//5 Color.Gray
		Night,							//6 Color.White
    Day,								//7 Color.LightGreen
		Eve									//8 Color.Red
	}

//	//should match datatype in DB
//	public enum PeakOffPeakType : byte {
//		Peak = 1, 
//		OffPeak
//	}
//
//	//should match datatype in DB
//	public enum NightDayEveType : byte {
//		Night = 3, 
//		Day, 
//		Eve
//	}

	//should match datatype in DB
	public enum EndPointProtocol: byte {
		H323,
		SIP
	}

	public enum EndPointDirection {
		IN,
		OUT,
		BOTH
	}

	public enum EndPointUsage {
		Unused,
		Dedicated,
		Shared
	}

	public enum EndPointType: byte {
		//H323:
		Gateway = 1,
		Gatekeeper = 2,
		Terminal = 3
//		MCU = 4

//		//SIP:
//		UserAgent = 11,
//		Registrar = 12,
//		RedirectServer = 13,
//		ProxyServer = 14,
//		LocationServer = 15
	}

	public enum EPChangeType {
		Add,
		Update,
		Delete,
		None
	}

	//should match datatype in DB
	public enum EPRegistration: byte {
		Required = 1,
		Permanent = 2
	}

  //should match datatype in DB
  public enum ServiceType : byte {
    //Carrier = 0,
    Wholesale = 1,
		Retail = 2,
    CallCenter = 3,
  	None = 99
  }

  //should match datatype in DB
	public enum RetailType {
		None = 0, //for Carrier and Wholesale Services
		PhoneCard = 1,
		Residential = 2
	}

	//should match datatype in DB
	public enum ScriptLanguage : byte {
		English,
		Spanish
	}

	public enum ScriptType {
		PhoneCard,
		Residential,
		LD,
		NumberRouting,
		Wholesale
	}

	public enum ScriptAuthenticationType {
		None,
		Card,
		ANI,
		IP
	}

	public enum BalancePromptType : byte {
		Money,
		Minutes,
		Units,
		None
	}

	//should match datatype in DB
	public enum BonusMinutesType : byte {
		None,
		One_Time,
		Re_Occuring
	}

	//should match datatype in DB
	public enum MarkupType : byte {
		None,
		Percent,
		Dollar,
    Manual
	}

	public enum RbrNodeTransportType{
		Local,
		FTP
	}

	public enum ArchiveDeleteMessageBoxDefaultButton{
		Cancel,
		Archive,
		Delete
	}

	public enum ArchiveDeleteDialogResult {
		Cancel,
		Archive,
		Delete
	}

	public enum GkDisconnectCause {
		//Q.931 codes
		Unknown                      = -1,
		UnknownCauseIE               =  0,
		UnallocatedNumber            =  1,
		NoRouteToNetwork             =  2,
		NoRouteToDestination         =  3,
		SendSpecialTone              =  4,
		MisdialledTrunkPrefix        =  5,
		ChannelUnacceptable          =  6,
		NormalCallClearing           = 16,
		UserBusy                     = 17,
		NoResponse                   = 18,
		NoAnswer                     = 19,
		SubscriberAbsent             = 20,
		CallRejected                 = 21,
		NumberChanged                = 22,
		Redirection                  = 23,
		ExchangeRoutingError         = 25,
		NonSelectedUserClearing      = 26,
		DestinationOutOfOrder        = 27,
		InvalidNumberFormat          = 28,
		FacilityRejected             = 29,
		StatusEnquiryResponse        = 30,
		NormalUnspecified            = 31,
		NoCircuitChannelAvailable    = 34,
		NetworkOutOfOrder            = 38,
		TemporaryFailure             = 41,
		Congestion                   = 42,
		RequestedCircuitNotAvailable = 44,
		ResourceUnavailable          = 47,
		ServiceOptionNotAvailable    = 63,
		InvalidCallReference         = 81,
		ClearedRequestedCallIdentity = 86,
		IncompatibleDestination      = 88,
		IENonExistantOrNotImplemented= 99,
		TimerExpiry                  = 102,
		ProtocolErrorUnspecified     = 111,
		InterworkingUnspecified      = 127,
		ErrorInCauseIE               = 256,

		//ISDN Event Cause Code, http://www.netopia.com/en-us/support/technotes/hardware/NIR_044.html?print=yes
		BearerCapabilityNotImplemented = 65,
	}

	public enum GkDisconnectSource : byte {
		Origination		= 0,
		Destination	= 1,
		GK				= 2,
		Unknown		= byte.MaxValue
	}

	public enum PlatformConfig {
		Standalone,															// all on one server
		Multi,																	// mulptiple ssw, one used as GuiHost
		Unknown = int.MaxValue
	}

	//public enum NodeRole {
	//  Standalone,
	//  Admin,
	//  Server
	//}
	
	public enum NodeRole {
		Admin = 1,		//to make sure that the values are the same as in enum it replaces, see above. zero is invalid value
		H323,
		SIP,
		Unknown = int.MaxValue
	}
	
	public enum ExceptionType {
		None, 
		CurrentCarrierRoute, 
		OtherCarrierRoutes
	}
	
	public enum ViewContext {
		None,
		CdrExport,
		CallingPlan,

		Carrier,
		Customer,
		Service,
		OrigEndpoint,
		TermEndpoint
	}

	public enum IVRDisconnectCause {
		LC_NORMAL = 0,
		LC_NUMBER_BUSY = 1,
		LC_NO_ANSWER = 2,
		LC_NUMBER_UNOBTAINABLE = 3,
		LC_NUMBER_CHANGED = 4,
		LC_OUT_OF_ORDER = 5,
		LC_INCOMING_CALLS_BARRED = 6,
		LC_CALL_REJECTED = 7,
		LC_CALL_FAILED = 8,
		LC_CHANNEL_BUSY = 9,
		LC_NO_CHANNELS = 10,
		LC_CONGESTION = 11,
		LC_UNKNOWN = 12
	}

	public enum IVRDisconnectSource : byte {
		Origination = 0,
		Destination = 1,
		IVR = 2,
		Unknown = byte.MaxValue
	}

	public enum CallStatus : byte {
		Connecting,
		Connected,
		Completed
	}
	
	public enum CallPaymentStatus : byte {
		NotPayed,
		Payed
	}

	public enum PartnerListFilter {
		CustomerAccounts,
		CarrierAccounts,
		ResellAgents
	}

	public enum NumberPortabilityRequestStatus {
		Upsert = 0,
		Delete
	}
}
