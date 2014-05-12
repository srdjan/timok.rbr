using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Timok.Logger;

namespace Timok.Rbr.Service.EmailReports {
	internal class ActivationReport {
		public void Run(DateTime pActivatedOn) {
			SortedList _activatedPins = null;
			short _customerAcctId = 0;
			var _activePinsFilePath = string.Empty;
			var _ftpFolder = string.Empty;
			var _connectionString = string.Empty;

			try {
				if (!activationReportEnabled()) {
					TimokLogger.Instance.LogRbr(LogSeverity.Status, "ActivationReport.Run", "Activation Report is disabled. Skipping run");
					return;
				}

				loadAppConfigurations(ref _customerAcctId, ref _activePinsFilePath, ref _ftpFolder, ref _connectionString);
				checkPaths(_activePinsFilePath, _ftpFolder);

				TimokLogger.Instance.LogRbr(LogSeverity.Debug, "ActivationReport.Run", "Executing Activation Report");

				_activatedPins = getActiveted(pActivatedOn, _customerAcctId, _activePinsFilePath, _connectionString);
				appendActivetedPINsToActivePINsFile(_activePinsFilePath, _activatedPins);
				createActivetedPINsReportFile(pActivatedOn, _activePinsFilePath, _ftpFolder);
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "ActivationReport.Run", string.Format("Exception:\r\n{0}", _ex));
			}
			if (_activatedPins != null) {
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "ActivationReport.Run", string.Format("Activated on {0}: {1}", pActivatedOn.ToShortDateString(), _activatedPins.Count));
			}
		}

		//---------------------------------- Private --------------------------------------------------------
		static bool activationReportEnabled() {
			var _value = ConfigurationManager.AppSettings["ActivationReportEnabled"];
			bool _activationReportEnabled;
			bool.TryParse(_value, out _activationReportEnabled);
			return _activationReportEnabled;
		}

		void loadAppConfigurations(ref short pCustomerAcctId, ref string pActivePinsFilePath, ref string pFtpFolder, ref string pConnectionString) {
			TimokLogger.Instance.LogRbr(LogSeverity.Debug, "ActivationReport.loadAppConfigurations", "Loading Configurations for Activation Report");

			pConnectionString = ConfigurationManager.AppSettings["ConnectionString"] ?? string.Empty;
			if (pConnectionString.Length == 0) {
				throw new Exception("ConnectionString value is missing in AppSettings!");
			}
			pCustomerAcctId = (ConfigurationManager.AppSettings["CingularAcctId"] != null) ? short.Parse(ConfigurationManager.AppSettings["CingularAcctId"]) : (short) 0;
			if (pCustomerAcctId <= 0) {
				throw new Exception("CingularAcctId value is missing in AppSettings!");
			}
			pActivePinsFilePath = ConfigurationManager.AppSettings["ActivePINsFilePath"] ?? string.Empty;
			if (pActivePinsFilePath.Length == 0) {
				throw new Exception("ActivePINsFilePath value is missing in AppSettings!");
			}

			pFtpFolder = ConfigurationManager.AppSettings["FtpFolder"] ?? string.Empty;
			if (pFtpFolder.Length == 0) {
				throw new Exception("FtpFolder value is missing in AppSettings!");
			}
		}

		void checkPaths(string pActivePinsFilePath, string pFtpFolder) {
			TimokLogger.Instance.LogRbr(LogSeverity.Debug, "ActivationReport.checkPaths", "Checking paths for Activation Report");

			if (!Directory.Exists(Path.GetDirectoryName(pFtpFolder))) {
				Directory.CreateDirectory(Path.GetDirectoryName(pFtpFolder));
			}

			if (!Directory.Exists(Path.GetDirectoryName(pActivePinsFilePath))) {
				Directory.CreateDirectory(Path.GetDirectoryName(pActivePinsFilePath));
			}

			if (!File.Exists(pActivePinsFilePath)) {
				File.Create(pActivePinsFilePath);
			}
		}

		SortedList getActiveted(DateTime pActivatedOn, short pCustomerAcctId, string pActivePinsFilePath, string pConnectionString) {
			/*						
			 SET @sql = N'
			 CREATE TABLE #ActivePINs (active_pin bigint PRIMARY KEY, start_balance decimal(9,2), ani bigint, timok_day_active int);
			 BULK INSERT #ActivePINs
			 FROM ''' + @ActivePINsFilePath + N''';
 
			 SELECT PH.pin, RA.start_balance, CDR.ANI 
			 FROM  RbrDb_266.dbo.PhoneCard AS PH 
			 INNER JOIN 
							 RbrDb_266.dbo.RetailAccount AS RA ON 
			 PH.retail_acct_id = RA.retail_acct_id 
			 INNER JOIN  
							 ' + @CdrDbName + N'.dbo.CDR AS CDR ON PH.serial_number = CDR.serial_number 
				
			 WHERE PH.serial_number IN 
			 (
			 SELECT serial_number FROM ' + @CdrDbName + N'.dbo.CDR
			 GROUP BY serial_number, customer_acct_id
			 HAVING MIN(timok_date / 100) = ' + @tday + N' AND customer_acct_id = ' + @customer_acct_id_char + N' 
			 ) 
			 AND PH.pin NOT IN 
			 (
				 SELECT active_pin FROM #ActivePINs
			 )
			 ORDER BY PH.pin ';
		 */
			var _tday = (pActivatedOn.Year * 1000) + pActivatedOn.DayOfYear;
			var _cdrDbName = "CDRDb_" + Core.Config.Configuration.Instance.Db.CdrDbVersion + "_" + pActivatedOn.Year + pActivatedOn.Month.ToString("00");
			var _sql = " CREATE TABLE #ActivePINs (active_pin bigint PRIMARY KEY, start_balance decimal(9,2), ani bigint, timok_day_active int); " + "BULK INSERT #ActivePINs FROM '" +
										pActivePinsFilePath + "'; SELECT PH.pin, RA.start_balance, CDR.ANI  " + " FROM  RbrDb_" + Core.Config.Configuration.Instance.Db.RbrDbVersion + ".dbo.PhoneCard AS PH  INNER JOIN  " +
										" RbrDb_" + Core.Config.Configuration.Instance.Db.RbrDbVersion + ".dbo.RetailAccount AS RA ON  PH.retail_acct_id = RA.retail_acct_id  INNER JOIN " + " " + _cdrDbName +
			              ".dbo.CDR AS CDR ON PH.serial_number = CDR.serial_number  WHERE PH.pin NOT IN  ( SELECT active_pin FROM #ActivePINs ) AND PH.serial_number IN ( SELECT serial_number FROM " +
			              _cdrDbName + ".dbo.CDR " + " GROUP BY serial_number, customer_acct_id HAVING MIN(timok_date / 100) = " + _tday + " AND customer_acct_id = " + pCustomerAcctId +
			              " )  ORDER BY PH.pin, CDR.start; ";

			using (var _conn = new SqlConnection(pConnectionString)) {
				_conn.Open();
				var _cmd = new SqlCommand {Connection = _conn, CommandType = CommandType.Text, CommandText = _sql};

				//IDbDataParameter _activatedOnParam = _cmd.CreateParameter();
				//_activatedOnParam.ParameterName = "@ActivatedOn";//@CustomerAcctId @ActivePINsFilePath
				//_activatedOnParam.DbType = DbType.Date;
				//_activatedOnParam.Value = pActivatedOn;
				//_cmd.Parameters.Add(_activatedOnParam);

				//IDbDataParameter _customerAcctIdParam = _cmd.CreateParameter();
				//_customerAcctIdParam.ParameterName = "@CustomerAcctId";
				//_customerAcctIdParam.DbType = DbType.Int32;
				//_customerAcctIdParam.Value = pCustomerAcctId;
				//_cmd.Parameters.Add(_customerAcctIdParam);

				//IDbDataParameter _activePINsFilePathParam = _cmd.CreateParameter();
				//_activePINsFilePathParam.ParameterName = "@ActivePINsFilePath";
				//_activePINsFilePathParam.DbType = DbType.AnsiString;
				//_activePINsFilePathParam.Value = pActivatedOn;
				//_cmd.Parameters.Add(_activePINsFilePathParam);

				var _list = new SortedList();
				using (var _reader = _cmd.ExecuteReader()) {
					if (_reader != null) {
						while (_reader.Read()) {
							var _phoneCardRecord = new PhoneCardRecord(_reader.GetInt64(0),
							                                           //pin
							                                           _reader.GetDecimal(1),
							                                           //start amnt
							                                           _reader.GetInt64(2),
							                                           //ani
							                                           pActivatedOn);
							if (!_list.Contains(_phoneCardRecord.Pin)) {
								_list.Add(_phoneCardRecord.Pin, _phoneCardRecord);
							}
						}
					}
				}

				return _list;
			}
		}

		static void appendActivetedPINsToActivePINsFile(string pActivePinsFilePath, IDictionary pActivatedPins) {
			using (var _sw = new StreamWriter(pActivePinsFilePath, true)) {
				foreach (PhoneCardRecord _pin in pActivatedPins.Values) {
					_sw.WriteLine(_pin.ToStorageFileString());
				}
			}
		}

		static void createActivetedPINsReportFile(DateTime pActivatedOn, string pActivePinsFilePath, string pFtpFolder) {
			var _reportFileName = "Cingular_Active_PINS_" + pActivatedOn.ToString("yyyy_MM_dd") + ".pins";
			var _reportFilePath = Path.Combine(Path.GetDirectoryName(pActivePinsFilePath), _reportFileName);
			var _tdate = (pActivatedOn.Year * 1000) + pActivatedOn.DayOfYear;
			using (var _sr = new StreamReader(pActivePinsFilePath)) {
				using (var _sw = new StreamWriter(_reportFilePath, false)) {
					string _line;
					while ((_line = _sr.ReadLine()) != null) {
						var _phoneCardRecord = PhoneCardRecord.Parse(_line);
						if (_phoneCardRecord.TimokActivatedOn == _tdate) {
							_sw.WriteLine(_phoneCardRecord.ToReportFileString());
						}
					}
				}
			}

			if (File.Exists(Path.Combine(pFtpFolder, _reportFileName))) {
				//backup existing file
				var _activePinsFolder = Path.GetDirectoryName(pActivePinsFilePath);
				File.Copy(Path.Combine(pFtpFolder, _reportFileName), Path.Combine(_activePinsFolder, _reportFileName) + ".existed_backup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
			}

			File.Copy(_reportFilePath, Path.Combine(pFtpFolder, _reportFileName), true);
		}
	}

	internal class PhoneCardRecord {
		const string DELIMITER = "\t";
		DateTime activatedOn;
		long ani;
		long pin;
		decimal startBalance;

		public PhoneCardRecord(long pPin, decimal pStartAmount, long pAni, DateTime pActivatedOn) {
			pin = pPin;
			ani = pAni;
			startBalance = pStartAmount;
			activatedOn = pActivatedOn;
		}

		public long Pin { get { return pin; } set { pin = value; } }

		public long Ani { get { return ani; } set { ani = value; } }

		public decimal StartBalance { get { return startBalance; } set { startBalance = value; } }

		public DateTime ActivatedOn { get { return activatedOn; } set { activatedOn = value; } }

		public int TimokActivatedOn { get { return (activatedOn.Year * 1000) + activatedOn.DayOfYear; } }

		public string ToReportFileString() {
			var _sb = new StringBuilder();
			_sb.Append(pin);
			_sb.Append(DELIMITER);
			_sb.Append(startBalance.ToString("0.00"));
			_sb.Append(DELIMITER);
			_sb.Append(ani);
			return _sb.ToString();
		}

		public string ToStorageFileString() {
			var _sb = new StringBuilder();
			_sb.Append(pin);
			_sb.Append(DELIMITER);
			_sb.Append(startBalance.ToString("0.00"));
			_sb.Append(DELIMITER);
			_sb.Append(ani);
			_sb.Append(DELIMITER);
			_sb.Append(TimokActivatedOn);
			return _sb.ToString();
		}

		public static PhoneCardRecord Parse(string pFileLine) {
			var _fields = pFileLine.Split('\t');
			var _pin = long.Parse(_fields[0]);
			var _startAmount = decimal.Parse(_fields[1]);
			var _ani = long.Parse(_fields[2]);
			var _timokActivatedOn = int.Parse(_fields[3]);

			var _date = new DateTime(_timokActivatedOn / 1000, 1, 1);
			_date = _date.AddDays(_timokActivatedOn % 1000 - 1);
			return new PhoneCardRecord(_pin, _startAmount, _ani, _date);
		}
	}
}