// <fileinfo name="Cdr_Db.cs">
//		<copyright>
//			Copyright © 2002-2007 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			You can update this source code manually. If the file
//			already exists it will not be rewritten by the generator.
//		</remarks>
//		<generator rewritefile="False" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

//TODO: TIMOK CUSTOM ADDITION
#define SQL_CLIENT

//DB split type
//#define DB_BY_YEAR
#define DB_BY_MONTH
//#define DB_BY_DAY

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Timok.Core;
using Timok.Core.DataProtection;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.CdrDatabase.Base;
using Configuration = Timok.Rbr.Core.Config.Configuration;

namespace Timok.Rbr.DAL.CdrDatabase {
	/// <summary>
	/// Represents a connection to the <c>Cdr_Db</c> database.
	/// </summary>
	/// <remarks>
	/// If the <c>Cdr_Db</c> goes out of scope, the connection to the 
	/// database is not closed automatically. Therefore, you must explicitly close the 
	/// connection by calling the <c>Close</c> or <c>Dispose</c> method.
	/// </remarks>
	/// <example>
	/// using(Cdr_Db db = new Cdr_Db())
	/// {
	///		CDRRow[] rows = db.CDRTable.GetAll();
	/// }
	/// </example>
	public class Cdr_Db : Cdr_Db_Base {
		public const string DB_NAME_PREFIX = "CDRDb_";
		const string INIT_CATALOG_PREFIX = "Initial Catalog=";

		//const string CDRDb_CREATE_EMPTY_DB_TEMPLATE_RESOURCE_NAME = "CDRDb_CREATE_EMPTY_DB_TEMPLATE.sql";
		//const string CDRDb_CREATE_NETSERVICE_DB_USER_TEMPLATE_RESOURCE_NAME = "CREATE_NETSERVICE_DB_USER_TEMPLATE.sql";
		//const string CDRDb_CREATE_FUNCTIONS_RESOURCE_NAME = "CDRDb_CREATE_FUNCTINS.sql";
		//const string CDRDb_CREATE_TABLES_RESOURCE_NAME = "CDRDb_CREATE_TABLES.sql";
		//const string CDRDb_CREATE_VIEWS_TEMPLATE_RESOURCE_NAME = "CDRDb_CREATE_VIEWS_TEMPLATE.sql";
		//const string CDRDb_DROP_CDRView_RESOURCE_NAME = "CDRDb_DROP_CDRView.sql";

#if DB_BY_YEAR
		public const Cdr_Db_DateInterval DbDateInterval = Cdr_Db_DateInterval.Year;
		public const string DBCatalogNameSuffixFormat = "yyyy";
#elif DB_BY_MONTH
		public static readonly DateInterval DbDateInterval = DateInterval.Month;
		public const string DB_CATALOG_NAME_SUFFIX_FORMAT = "yyyyMM";
#elif DB_BY_DAY
		public const Cdr_Db_DateInterval DbDateInterval = Cdr_Db_DateInterval.Day;
		public const string DBCatalogNameSuffixFormat = "yyyyMMdd";
#endif

		const string BASE_CONNECTION_STRING_KEY_NAME = "ivs5";
		static readonly string baseConnectionStringPrefix = string.Format("Data Source={0}\\TRBR;Integrated Security=SSPI;", Configuration.Instance.Main.HostName);

		static string baseConnectionString;
		public static string BaseConnectionString {
			get {
				if (string.IsNullOrEmpty(baseConnectionString)) {
					baseConnectionString = SecuredData.GetFromAppConfig(Configuration.Instance.Main.RbrConfigFile, BASE_CONNECTION_STRING_KEY_NAME);
				}
				return baseConnectionString;
			}
		}

		readonly string dbName = string.Empty;

		public Cdr_Db(DateTime pDateTime) : base(false) {
			//CDRDb_25X_200511
			dbName = GetDbName(pDateTime);
			InitConnection();
		}

		public void CheckConfigConnectionString() {
			if (BaseConnectionString.Trim().Length == 0) {
				baseConnectionString = ConnectionStringExt.GetMachineNameAppInfo() + baseConnectionStringPrefix + /*";Password=" + value +*/ ";";
				SecuredData.SaveToAppConfig(Configuration.Instance.Main.RbrConfigFile, BASE_CONNECTION_STRING_KEY_NAME, baseConnectionString);
			}
			else {
				baseConnectionString = SecuredData.GetFromAppConfig(Configuration.Instance.Main.RbrConfigFile, BASE_CONNECTION_STRING_KEY_NAME);
			}
		}
		
		public static void CheckCdrDependancy() {
			if (Configuration.Instance.Db.RbrDbVersion != Configuration.Instance.Db.CdrDbDependency) {
				UpdateCDRView();
				//T.LogRbr(LogSeverity.Status, "Cdr_Db.CheckCdrDependancy", "Updated CDRViews");

				AppConfig.SetValue(ConfigFileType.CustomAppConfig, "dbSettings", Configuration.Instance.Db.CdrDbDependencyKeyName, Configuration.Instance.Db.RbrDbVersion, Configuration.Instance.Main.RbrConfigFile);
				//T.LogRbr(LogSeverity.Status, "Cdr_Db.CheckCdrDependancy", string.Format("{0} saved, value={1}", Configuration.Instance.Db.CdrDbDependencyKeyName, Configuration.Instance.Db.RbrDbVersion));
			}
		}

		public static string GetDbName(DateTime pDateTime) {
			return DB_NAME_PREFIX + Configuration.Instance.Db.CdrDbVersion + "_" + pDateTime.ToString(DB_CATALOG_NAME_SUFFIX_FORMAT);
		}

		static string getDbNameSuffix(DateTime pDateTime) {
			return Configuration.Instance.Db.CdrDbVersion + "_" + pDateTime.ToString(DB_CATALOG_NAME_SUFFIX_FORMAT);
		}

		static string getFullConnectionString(string pDbName) {
			var _dbconn = /*BaseConnectionString*/baseConnectionStringPrefix + INIT_CATALOG_PREFIX + pDbName + ";";
			return _dbconn;
			//return BaseConnectionString + INIT_CATALOG_PREFIX + pDbName + ";";
		}

		/// <summary>
		/// Creates a new connection to the database.
		/// </summary>
		/// <returns>An <see cref="System.Data.IDbConnection"/> object.</returns>
		protected override IDbConnection CreateConnection() {
			//create connection string from connectionDate:
			string _connString = getFullConnectionString(dbName);
			return new SqlConnection(_connString);
		}

		/// <summary>
		/// Creates a DataTable object for the specified command.
		/// </summary>
		/// <param name="command">The <see cref="System.Data.IDbCommand"/> object.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected internal DataTable CreateDataTable(IDbCommand command) {
			var dataTable = new DataTable();
			new SqlDataAdapter((SqlCommand) command).Fill(dataTable);
			return dataTable;
		}

		/// <summary>
		/// Returns a SQL statement parameter name that is specific for the data provider.
		/// For example it returns ? for OleDb provider, or @paramName for MS SQL provider.
		/// </summary>
		/// <param name="paramName">The data provider neutral SQL parameter name.</param>
		/// <returns>The SQL statement parameter name.</returns>
		protected internal override string CreateSqlParameterName(string paramName) {
			return "@" + paramName;
		}

		/// <summary>
		/// Creates a .Net data provider specific parameter name that is used to
		/// create a parameter object and add it to the parameter collection of
		/// <see cref="System.Data.IDbCommand"/>.
		/// </summary>
		/// <param name="baseParamName">The base name of the parameter.</param>
		/// <returns>The full data provider specific parameter name.</returns>
		protected override string CreateCollectionParameterName(string baseParamName) {
			return "@" + baseParamName;
		}

		#region Check/Create Monthly CDRDb 

		#region public methods

		public static bool Exists(string pDbName) {
			using (var _masterConn = new SqlConnection(getFullConnectionString("master"))) {
				_masterConn.Open();
				var _cmd = new SqlCommand
				           	{
				           		Connection = _masterConn,
				           		CommandType = CommandType.Text,
				           		CommandText = ("SELECT COUNT(name) FROM master.dbo.sysdatabases " + "WHERE name=@DbName")
				           	};

				IDbDataParameter _param = _cmd.CreateParameter();
				_param.ParameterName = "@DbName";
				_param.DbType = DbType.AnsiString;
				_param.Value = pDbName;
				_cmd.Parameters.Add(_param);

				var _count = (int) _cmd.ExecuteScalar();
				return _count == 1;
			}
		}

		public static bool Exists(DateTime pDbDate) {
			var _dbName = GetDbName(pDbDate);
			using (var _masterConn = new SqlConnection(getFullConnectionString("master"))) {
				_masterConn.Open();
				var _cmd = new SqlCommand
				           	{
				           		Connection = _masterConn,
				           		CommandType = CommandType.Text,
				           		CommandText = ("SELECT COUNT(name) FROM master.dbo.sysdatabases " + "WHERE name=@DbName")
				           	};

				IDbDataParameter _param = _cmd.CreateParameter();
				_param.ParameterName = "@DbName";
				_param.DbType = DbType.AnsiString;
				_param.Value = _dbName;
				_cmd.Parameters.Add(_param);

				var _count = (int) _cmd.ExecuteScalar();
				return _count == 1;
			}
		}

		public static void Create(DateTime pTodayDateTime, int pMonthsAhead) {
			for (var _monthCount = 0; _monthCount <= pMonthsAhead; _monthCount++) {
				var _date = pTodayDateTime.AddMonths(_monthCount);
				var _dbName = GetDbName(_date);

				if ( ! Exists(_date)) {
					//T.LogRbr(LogSeverity.Status, "Cdr_Db.Create", string.Format("DB does not exist, START creating CDRDb: {0}", _dbName));

					var _dbConn = new SqlConnection(getFullConnectionString("master"));
					var _server = new Server(new ServerConnection(_dbConn));
					try {
						var _sqlText = getCreateEmptyDbSQL(_date);
						_server.ConnectionContext.ExecuteNonQuery(_sqlText);
						//T.LogRbr(LogSeverity.Status, "Cdr_Db.Create", string.Format("Created of empty CDRDb: {0}", _dbName));
					}
					catch (Exception _ex) {
						//_server.ConnectionContext.RollBackTransaction();
						_server.ConnectionContext.ExecuteNonQuery(getDropCDRDbSQL(_date));
						throw new Exception(string.Format("Failed to create new CDRDb: {0}, exception:\n{1}", _dbName, _ex));
					}

					//-- Create CdrDb objects
					createCDRDbObjects(_dbName, _date);
				}
				else {
					//T.LogRbr(LogSeverity.Status, "Cdr_Db.Create", string.Format("CDRDb: {0} already exists.", _dbName));
				}
			}
		}

		public static string[] GetCDRDbNameList() {
			var _list = new ArrayList();
			/*
					SELECT name FROM master.dbo.sysdatabases 
					WHERE name LIKE 'CDRDb_XXX_%'
					ORDER BY name 
					*/
			using (var _masterConn = new SqlConnection(getFullConnectionString("master"))) {
				_masterConn.Open();
				var _cmd = new SqlCommand
				           	{
				           		Connection = _masterConn,
				           		CommandType = CommandType.Text,
											CommandText = ( "SELECT name FROM master.dbo.sysdatabases " + "WHERE name LIKE '" + DB_NAME_PREFIX + Configuration.Instance.Db.CdrDbVersion + "_%' " + "ORDER BY name" )
				           	};

				using (IDataReader _reader = _cmd.ExecuteReader()) {
					while (_reader.Read()) {
						var _dbName = _reader.GetString(0);
						if (! _list.Contains(_dbName)) {
							_list.Add(_dbName);
						}
					}
				}
			}
			return (string[]) _list.ToArray(typeof (string));
		}

		public static void UpdateCDRView() {
			var _cdrDbNameList = GetCDRDbNameList();
			foreach (var _cdrDbName in _cdrDbNameList) {
				if (_cdrDbName.IndexOf("Template") > 0) {		// skip _template table
					continue;
				}

				var _dbConn = new SqlConnection(getFullConnectionString(_cdrDbName));
				var _server = new Server(new ServerConnection(_dbConn));

				try {
					_server.ConnectionContext.BeginTransaction();

					var _sqlText = getDropCDRViewSQL();
					_server.ConnectionContext.ExecuteNonQuery(_sqlText);
					//T.LogRbr(LogSeverity.Status, "Cdr_Db.UpdateCDRView", "Dropped CDRView: " + _cdrDbName);

					_sqlText = getCreateDbViewsSQL(extractDate(_cdrDbName));
					_server.ConnectionContext.ExecuteNonQuery(_sqlText);
					//T.LogRbr(LogSeverity.Status, "Cdr_Db.UpdateCDRView", "creating CDRDb views: " + _cdrDbName);

					_server.ConnectionContext.CommitTransaction();
				}
				catch (Exception _ex) {
					_server.ConnectionContext.RollBackTransaction();
					throw new Exception(string.Format("Failed to update CDRView, CDRDb Name={0}, Exception:\r\n{1}", _cdrDbName, _ex));
				}
			}
		}

		static DateTime extractDate(string pCdrDbName) {
			//CdrDb_267_200704
			var _index = pCdrDbName.LastIndexOf('_');
			var _date = pCdrDbName.Substring(_index + 1);
			var _year = int.Parse(_date.Substring(0, 4));
			var _month = int.Parse(_date.Substring(4, 2));
			return new DateTime(_year, _month, 1);
		}

		#endregion public methods

		#region private methods
		static void createCDRDbObjects(string pCDRDbName, DateTime pDate) {
			var _dbConn = new SqlConnection(getFullConnectionString(pCDRDbName));
			var _server = new Server(new ServerConnection(_dbConn));
			try {
				var _sqlText = getCreateDbUsersSQL();
				_server.ConnectionContext.ExecuteNonQuery(_sqlText);
				//T.LogRbr(LogSeverity.Status, "Cdr_Db.createCDRDbObjects", "creating CDRDb Users: " + pCDRDbName);

				//-- start transaction
				_server.ConnectionContext.BeginTransaction();

				_sqlText = getCreateDbTablesSQL(pDate);
				_server.ConnectionContext.ExecuteNonQuery(_sqlText);
				//T.LogRbr(LogSeverity.Status, "Cdr_Db.createCDRDbObjects", "creating CDRDb tables: " + pCDRDbName);

				_sqlText = getCreateDbFunctionsSQL(pDate);
				_server.ConnectionContext.ExecuteNonQuery(_sqlText);
				//T.LogRbr(LogSeverity.Status, "Cdr_Db.createCDRDbObjects", "creating CDRDb functions: " + pCDRDbName);

				_sqlText = getCreateDbViewsSQL(pDate);
				_server.ConnectionContext.ExecuteNonQuery(_sqlText);
				//T.LogRbr(LogSeverity.Status, "Cdr_Db.createCDRDbObjects", "creating CDRDb views: " + pCDRDbName);

				//-- end transaction
				_server.ConnectionContext.CommitTransaction();
			}
			catch (Exception _ex) {
				_server.ConnectionContext.RollBackTransaction();
				throw new Exception("Failed to create CDRDb objects\r\nCDRDb Name: " + pCDRDbName + "\r\nException:" + _ex);
			}
		}

		static string getCreateEmptyDbSQL(DateTime pDate) {
			var _dbNameSuffix = getDbNameSuffix(pDate);

			string _sql;
			try {
				_sql = Properties.Resources.CREATE_CDR_DB;
			}
			catch (Exception _ex) {
				throw new Exception("Failed to get CREATE_CDR_DB.\r\n\r\n" + _ex);
			}
			_sql = _sql.Replace("$(DB_NAME_SUFFIX)", _dbNameSuffix);
			_sql = _sql.Replace("$(DB_DIR)", Configuration.Instance.Db.CdrDbPath);
			return _sql;
		}

		static string getDropCDRDbSQL(DateTime pDate) {
			var _dbNameSuffix = getDbNameSuffix(pDate);
			var _dbName = "CdrDb_" + _dbNameSuffix;
			var _sql = "ALTER DATABASE " + _dbName + " SET  SINGLE_USER WITH ROLLBACK IMMEDIATE\r\n";
			_sql += "DROP DATABASE " + _dbName;
			return _sql;
		}

		static string getCreateDbUsersSQL() {
			string _sql;
			try {
				_sql = Properties.Resources.CREATE_NETSERVICE_DB_USER_TEMPLATE;
			}
			catch (Exception _ex) {
				throw new Exception("Failed to get CDRDb_CREATE_NETSERVICE_DB_USER_TEMPLATE_RESOURCE.\r\n\r\n" + _ex);
			}
			return _sql;
		}

		static string getCreateDbFunctionsSQL(DateTime pDate) {
			string _sql;
			try {
				_sql = Properties.Resources.CREATE_CDR_DB_FUNCTIONS;
				var _dbNameSuffix = getDbNameSuffix(pDate);
				_sql = _sql.Replace("$(DB_NAME_SUFFIX)", _dbNameSuffix);
				_sql = _sql.Replace("USE", "--USE");
			}
			catch (Exception _ex) {
				throw new Exception("Failed to get CDRDb_CREATE_FUNCTIONS_RESOURCE.\r\n\r\n" + _ex);
			}
			return _sql;
		}

		static string getCreateDbTablesSQL(DateTime pDate) {
			string _sql;
			try {
				_sql = Properties.Resources.CREATE_CDR_DB_TABLES;
				var _dbNameSuffix = getDbNameSuffix(pDate);
				_sql = _sql.Replace("$(DB_NAME_SUFFIX)", _dbNameSuffix);
			}
			catch (Exception _ex) {
				throw new Exception("Failed to get CDRDb_CREATE_TABLES_RESOURCE.\r\n\r\n" + _ex);
			}
			return _sql;
		}

		static string getCreateDbViewsSQL(DateTime pDate) {
			string _sql;

			try {
				_sql = Properties.Resources.CREATE_CDR_DB_VIEWS;
			}
			catch (Exception _ex) {
				throw new Exception("Failed to get CDRDb_CREATE_VIEWS_TEMPLATE_RESOURCE.\r\n\r\n" + _ex);
			}
			var _dbNameSuffix = getDbNameSuffix(pDate);
			_sql = _sql.Replace("$(DB_NAME_SUFFIX)", _dbNameSuffix);
			_sql = _sql.Replace("$(RBR_DB_VERSION)", Configuration.Instance.Db.RbrDbVersion);
			_sql = _sql.Replace("USE", "--USE");
			return _sql;
		}

		static string getDropCDRViewSQL() {
			string _sql;
			try {
				_sql = Properties.Resources.DROP_CDR_DB_VIEWS;
			}
			catch (Exception _ex) {
				throw new Exception("Failed to get CDRDb_DROP_CDRView_RESOURCE.\r\n\r\n" + _ex);
			}
			return _sql;
		}

		#endregion private methods

		#region internal methods

		#endregion internal methods

		#endregion Check/Create Monthly CDRDb 

		//--------------- Private --------------------------
	} // End of CDRDb class
} // End of namespace