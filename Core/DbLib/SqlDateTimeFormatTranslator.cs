using System;

namespace Timok.Core.DbLib 
{
	public class SqlDateTimeFormatTranslator {

		public const string DefaultDate =				"Default Date: YYYYMMDD";
		public const string DefaultTime =				"Default Time: HHmmss0";
		//public const string DateTimeDefault = "";
		public const string StandardDate =			"Standard Date: YYYY-MM-DD";
		public const string StandardTime =			"Standard Time: HH:mm:ss";
		public const string StandardDateTime =	"Standard Date and Time: YYYY-MM-DD HH:mm:ss";

		private SqlDateTimeFormatTranslator() { }

		public static string GetDateTimeFormatString(string pFieldName, string pFormatType) {
			return GetFormatTemplate(pFormatType).Replace("%fied_name%", pFieldName);
		}

		public static string GetFormatTemplate(string pFormatType) {
			switch (pFormatType) {
				case DefaultDate:
					return "CAST(YEAR([%fied_name%]) AS char(4))+RIGHT('0'+RTRIM(CAST(MONTH([%fied_name%]) AS char(2))), 2)+RIGHT('0'+RTRIM(CAST(DAY([%fied_name%]) AS char(2))),2)";
				case DefaultTime:
					return "RIGHT('0'+RTRIM(CAST(DATEPART(hh,[%fied_name%]) AS char(2))),2)+RIGHT('0'+RTRIM(CAST(DATEPART(mi,[%fied_name%]) AS char(2))),2)+RIGHT('0'+RTRIM(CAST(DATEPART(ss,[%fied_name%]) AS char(2))),2)+'0'";
				case StandardDate:
					return "CAST(YEAR([%fied_name%]) AS char(4))+ '-' +RIGHT('0'+RTRIM(CAST(MONTH([%fied_name%]) AS char(2))), 2)+'-'+RIGHT('0'+RTRIM(CAST(DAY([%fied_name%]) AS char(2))),2)";
				case StandardTime:
					return "RIGHT('0'+RTRIM(CAST(DATEPART(hh,[%fied_name%]) AS char(2))),2)+':'+RIGHT('0'+RTRIM(CAST(DATEPART(mi,[%fied_name%]) AS char(2))),2)+':'+RIGHT('0'+RTRIM(CAST(DATEPART(ss,[%fied_name%]) AS char(2))),2)";
				case StandardDateTime:
					return "CAST(YEAR([%fied_name%]) AS char(4))+ '-' +RIGHT('0'+RTRIM(CAST(MONTH([%fied_name%]) AS char(2))), 2)+'-'+RIGHT('0'+RTRIM(CAST(DAY([%fied_name%]) AS char(2))),2)" + 
						"+' '+" + 
						"RIGHT('0'+RTRIM(CAST(DATEPART(hh,[%fied_name%]) AS char(2))),2)+':'+RIGHT('0'+RTRIM(CAST(DATEPART(mi,[%fied_name%]) AS char(2))),2)+':'+RIGHT('0'+RTRIM(CAST(DATEPART(ss,[%fied_name%]) AS char(2))),2)";
				default:
					throw new Exception("Unknown format: " + pFormatType);
			}
		}
	}
}
