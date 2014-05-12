
namespace Timok.Rbr.Core.Config {
	public class AppConstants {
		public const string RbrRoot = @"c:\timok\rbr\";

		public const string Email = "Email";
		public const string Rbr = "Rbr";
		public const string Cdr = "Cdr";
		public const string Aggr = "Aggr";

		public const int ANILength = 10;
		public const string ANISuffix = ":dialedDigits"; 
		public const string AuditFolderName = "Audit"; 
		public const string CarrierServiceNamePrefix = "CA_S_";
		public const int CarrierRateDecimalDigits = 7;

		public const int RetailRateDecimalDigits = 4;
		public const int RetailAmountDecimalDigits = 2;

		public const string SubRouteSeparator = "-";
		public const string ProperNameSuffix = "Proper";

		public const string DefaultAmountFormat = "0.0000";
		public const string PoundSign = "#";
		public const string Unknown = "?";

		public const string One = "1";
		public const string Zero = "0";
		public const string ZeroZero = "00";
		public const string ZeroOneOne = "011";
		public const string IMT = ":IMT";
		public const string Get_AliasSuffix1 = "323_";

		public const string EmailFolderName = "Email";
		public const string PublishedFilter = "*.published";

		public const string CdrAggrFileExtension = ".Aggr";
		public const string CdrAggrFileFilter = "*.Aggr"; 
		public const string CdrAggrFileNameFormat = "yyyy-MM-dd_HHmm"; 
		public const string CdrAggrPublishingFolderName = "CdrAggrPublishing";
		public const string CdrDateFormat = "yyyyMMdd"; 
		public const string CdrExportFolderName = "CdrExport"; 
		public const string CdrFileExtension  = ".Cdr";
		public const string CdrFileFilter = "*.Cdr"; 
		public const string CdrFileNameFormat = "yyyy-MM-dd_HHmm"; 
		public const string CdrPublishingFolderName = "CdrPublishing"; 
		public const string CdrTimeFormat = "HHmmssf";
		public static readonly string CdrAmountFormat = "0.00000000";
		public const string SerialNumberPrefix = "S#:";

		public const string AliasSuffix = "323_";

		public static readonly string[] Increments = { "60 / 180", "60 / 60", "60 / 6", "30 / 6", "6 / 6", "1 / 1" };
		public static readonly string[] CallCenterIncrements = { "60 / 60", "30 / 6", "6 / 6" };

		public static readonly string CarrierRateAmountFormat = "0." + string.Empty.PadRight(CarrierRateDecimalDigits, '0');
		public static readonly int WholesaleRateDecimalDigits = 7;
		public static readonly string WholesaleRateAmountFormat = "0." + string.Empty.PadRight(WholesaleRateDecimalDigits, '0');
		public static readonly string RetailRateAmountFormat = "0." + string.Empty.PadRight(RetailRateDecimalDigits, '0');
		public static readonly string RetailAmountFormat = "0." + string.Empty.PadRight(RetailAmountDecimalDigits, '0');

		public const short PrefixTypeId_NoPrefixes = 0;

		public const string PublishedExtension = ".published";
		public const string ConsumedExtension = ".consumed"; 
		public const string ConsumedFilter = "*.consumed"; 
		public const string CustomerServiceNamePrefix = "CU_S_"; 
		public const string NumberPortability = "Data"; 
		public const int DefaultCallLimitInSeconds = 10800; 
		public const int DefaultPINLength = 9; 
		public const string DefaultRatesExportImportFileExtension = ".xls"; 

		public const RoutingAlgorithmType DefaultRoutingAlgorithm = RoutingAlgorithmType.Manual; 
		public const int DefaultVirtualSwitchId = -1; 
		public const int DNISLength = 10; 
		public const string DNISPrefix = "DNIS:"; 
		public const string ErrorExtension = ".error"; 
		public const char ImportExport_FieldDelimiter = '|'; 
		public const char ImportExport_RateIncrementsDelimiter = '/'; 
		public const char ImportExport_RateTimePeriodsDelimiter = ','; 
		public const decimal InvalidAvgMinuteCost = 99M; 
		public const string IPv4RegExprPattern = @"\A(25[0-5]|2[0-4]\d|[0-1]?\d?\d)(\.(25[0-5]|2[0-4]\d|[0-1]?\d?\d)){3}\z";  //IPv4  

		public const string LogFileFilter = ".log"; 
		public const string LogFolderName = "Log"; 
		public const int MaxCallLimitInSeconds = 32000; 
		public const int MaxDialCodeLength = 12; 
		public const int MaxPINLength = 18; 
		public const int MaxPrefixLength = 10; 
		public const decimal MaxRateValue = 99.9999999M; 
		public const decimal MaxRetailAcctStartAmount = 9999999.99M; 
		public const int MinEndpointId = 4000; 
		public const int MinIpAddressLength = 7; 
		public const int MinLoginNameLenght = 7; 
		public const int MinPINLength = 7; 
		public const int MinPWDLenght = 7; 
		public const int OnePlusDummyCountryCode = 1000; 
		public const string PendingExtension = ".pending"; 
		public const string PendingFilter = "*.pending"; 
		public const string PermanentEndPointsSectionName = "RasSrv::PermanentEndpoints";
		public const int UdpRbrClientPortRange = 1000;
		public const int UdpRbrServerStartPort = 8080;
		public const int UdpRbrClientStartPort = 9080;
		public static readonly string[] TimeServers = { "time-a.nist.gov", "time-b.nist.gov", "time-a.timefreq.bldrdoc.gov", "time-b.timefreq.bldrdoc.gov", "time-c.timefreq.bldrdoc.gov", "utcnist.colorado.edu", "time.nist.gov", "time-nw.nist.gov", "nist1.datum.com", "nist1.dc.certifiedtime.com", "nist1.nyc.certifiedtime.com", "nist1.sjc.certifiedtime.com" };
	}
}