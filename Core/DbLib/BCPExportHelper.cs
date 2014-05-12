using System;
using System.Diagnostics;
//using System.IO;

namespace Timok.Core.DbLib
{
	public struct BCPExportInfo {
		public string SQL; 
		public string FilePath; 
		public string Delimeter;
		public string Server;
		public string User;
		public string Pwd;
		public bool TrustedConnection;
		public int TimeoutSeconds;
	}

	public struct BCPExportResult {
		public int RecordsExported;
		public long ClockTimeMilliseconds;
		public string ClockTimeMsg;
		public string ErrorDescription;
		public string FilePath;
	}

	public class BCPExportHelper {

		private BCPExportHelper() {}

		public static BCPExportResult Run(BCPExportInfo pBCPExportInfo) {
			string _output = string.Empty;
			try {
				string _exeName = "bcp.exe";

				//_startInfo.FileName = @"C:\TIMOK.DEPLOYMENT\Rbr-v2.0.5.0\Diagrams\205\Init_data\TEST __CDRDb_205_Export_8_Data.bat";
				//bcp "SELECT * FROM CDRDb_205.dbo.CDR WHERE timok_date >= 200421400 AND timok_date < 200424500" queryout "C:\Timok\Rbr\SqlDb\ExportedData\205\CDR_200408_TEST.txt" -c -S(local)\TRBR -T
				string _args = string.Empty;// + //@"SELECT TOP 70000 * FROM CDRDb_205.dbo.CDR WHERE timok_date >= 200421400 AND timok_date < 200424500" + 
				_args += '"' + pBCPExportInfo.SQL + '"';
				_args += " queryout "; //@"C:\Timok\Rbr\SqlDb\ExportedData\205\CDR_200408_TEST.txt" + 
				_args += '"' + pBCPExportInfo.FilePath + '"';
				if (pBCPExportInfo.Delimeter == null || pBCPExportInfo.Delimeter.Trim().Length == 0) {
					//_args += "-t\t";//default
				}
				else {
					_args += " -t" + pBCPExportInfo.Delimeter;
				}

				_args += @" -c ";
				_args += " -S" + pBCPExportInfo.Server; //(local)\TRBR -T";
				if (pBCPExportInfo.TrustedConnection) {
					_args += " -T";
				}
				else {
					_args += " -U" + '"' + pBCPExportInfo.User + '"';// -U"Jane Doe"
					_args += " -P" + '"' + pBCPExportInfo.Pwd + '"';// -P"go dba"
				}

				ProcessPriorityClass _priorityClass = ProcessPriorityClass.BelowNormal;

				_output = CMDHelper.Run(_exeName, _args, _priorityClass, pBCPExportInfo.TimeoutSeconds);
			}
			catch (Exception e) {
				throw new ApplicationException("An error occurred running BCP.", e);
			}

			return processOutput(_output, pBCPExportInfo);
		}

		private static BCPExportResult processOutput(string output, BCPExportInfo pBCPExportInfo) {
			BCPExportResult _res = new BCPExportResult();
			_res.ClockTimeMsg = string.Empty;
			if (output.IndexOf("Error") > 0) {
				_res.ClockTimeMilliseconds = 0;
				_res.RecordsExported = 0;
				_res.ErrorDescription = output;
				return _res;
			}

			string[] _arr = output.Split('\r', '\n');
			foreach (string _line in _arr) {
				if (_line.EndsWith("rows copied.")) {
					try {
						_res.RecordsExported = int.Parse(_line.Replace("rows copied.", ""));
					} 
					catch {
						_res.RecordsExported = -1;
					}
				}

				if (_line.StartsWith("Clock Time (ms.)")) {
					_res.ClockTimeMsg = _line;
					try {
						string _temp = _line.Replace("Clock Time (ms.)", "");

						if (_temp.StartsWith(": total")) { //MSSQL/MSDDE 2000
							_temp = _temp.Replace(": total", "");
						}
						else if (_temp.StartsWith(" Total     :")) { //MSSQL/SSE 2005
							_temp = _temp.Replace(" Total     :", "");
              if (_temp.Contains("Average :")) {
                _temp = _temp.Substring(0, _temp.IndexOf("Average :"));
              }
						}
						_res.ClockTimeMilliseconds = long.Parse(_temp);
					}
					catch {
						_res.ClockTimeMilliseconds = -1;
					}
				}
			}
			_res.FilePath = pBCPExportInfo.FilePath;
			return _res;
		}

		#region good res from BCP MSSQL/MSDE 2000

		/*
		1000 rows successfully bulk-copied to host-file. Total received: 186000
		1000 rows successfully bulk-copied to host-file. Total received: 187000
		1000 rows successfully bulk-copied to host-file. Total received: 188000
		1000 rows successfully bulk-copied to host-file. Total received: 189000
		1000 rows successfully bulk-copied to host-file. Total received: 190000

		190443 rows copied.
		Network packet size (bytes): 4096
		Clock Time (ms.): total    40109
		*/
		#endregion good res from BCP MSSQL/MSDE 2000

		#region good res from BCP MSSQL/SSE 2005

		/*
		1000 rows successfully bulk-copied to host-file. Total received: 6000
		1000 rows successfully bulk-copied to host-file. Total received: 7000
		1000 rows successfully bulk-copied to host-file. Total received: 8000
		1000 rows successfully bulk-copied to host-file. Total received: 9000
		1000 rows successfully bulk-copied to host-file. Total received: 10000

		10000 rows copied.
		Network packet size (bytes): 4096
		Clock Time (ms.) Total     : 1797   Average : (5564.83 rows per sec.)
		*/
    //NOTE !!!!!!!!!!!!!!!!!!!!!!!!!!! "Average... " is OPTIONAL, not always there
		#endregion good res from BCP MSSQL/SSE 2005

			
			
		#region error res

		/*
		SQLState = 37000, NativeError = 170
		Error = [Microsoft][ODBC SQL Server Driver][SQL Server]Line 1: Incorrect syntax near 'timok_date'.
		SQLState = 37000, NativeError = 8180
		Error = [Microsoft][ODBC SQL Server Driver][SQL Server]Statement(s) could not be prepared.
		*/
		#endregion error res

	}
}
