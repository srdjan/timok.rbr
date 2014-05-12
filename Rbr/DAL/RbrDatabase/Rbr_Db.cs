// <fileinfo name="Rbr_Db.cs">
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.Properties;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Configuration = Timok.Rbr.Core.Config.Configuration;

namespace Timok.Rbr.DAL.RbrDatabase {
	public class Rbr_Db : Rbr_Db_Base, IConnection {
		const string INIT_CATALOG_PREFIX = "Initial Catalog=";
		//const string baseConnectionStringKeyName = "ivs3";
		static string dbName;
		public static string DbName { get { return dbName; } }
		static string baseConnectionStringPrefix;
		string fullConnectionString;

		//static string baseConnectionString = SecuredRbrConfigSettings.GetValue(baseConnectionStringKeyName);
		//static string baseConnectionString;
		const string MASTER_CONNECTION_STRING = @"Data Source=.\TRBR;Integrated Security=SSPI;Initial Catalog=master;";

		//public static void CheckConfigConnectionString() {
		//  if (baseConnectionString.Trim().Length == 0) {
		//    baseConnectionString = ConnectionStringExt.GetMachineNameAppInfo() + baseConnectionStringPrefix + /*";Password=" + value +*/ ";";
		//    SecuredRbrConfigSettings.SetValue(baseConnectionStringKeyName, baseConnectionString);
		//  }
		//  else {
		//    baseConnectionString = SecuredRbrConfigSettings.GetValue(baseConnectionStringKeyName);
		//  }
		//}

		public Rbr_Db() : base() {}

		public bool Exists(string pDbName) {
		  using (var _masterConn = new SqlConnection(MASTER_CONNECTION_STRING)) {
		    _masterConn.Open();
		    var _cmd = new SqlCommand
		                {
		                  Connection = _masterConn, 
		                  CommandType = CommandType.Text, 
		                  CommandText = ("SELECT COUNT(name) FROM master.dbo.sysdatabases " + "WHERE name=@pDbName")
		                };

				IDbDataParameter _param = _cmd.CreateParameter();
				_param.ParameterName = "@pDbName";
				_param.DbType = DbType.AnsiString;
				_param.Value = pDbName;
				_cmd.Parameters.Add(_param);

				var _count = (int) _cmd.ExecuteScalar();
				return _count == 1;
			}
		}

		public void Create() {
			try {
				//NOTE: SqlPath and BackupPath are hardcoded in the script !!!
				if (!Directory.Exists(Path.Combine(Configuration.Instance.Folders.SqlDbFolder, "Backups"))) {
					Directory.CreateDirectory(Path.Combine(Configuration.Instance.Folders.SqlDbFolder, "Backups"));
				}

				var _masterDbConn = new SqlConnection(MASTER_CONNECTION_STRING);
				var _server = new Server(new ServerConnection(_masterDbConn));

				//-- Backup 
				var _sqlText = getBackupDbSQL();
				_server.ConnectionContext.ExecuteNonQuery(_sqlText);
				//T.LogRbr(LogSeverity.Debug, "Rbr_Db.Create", string.Format("Backup for {0} done", dbName));

				//-- Create all db objects
				_sqlText = getCreateFullDbSQL();
				_server.ConnectionContext.ExecuteNonQuery(_sqlText);
				//T.LogRbr(LogSeverity.Debug, "Rbr_Db.Create", string.Format("CreateFull for {0} done", dbName));

				//-- Load initial data
				_sqlText = getLoadDbSQL();
				_server.ConnectionContext.ExecuteNonQuery(_sqlText);
				//T.LogRbr(LogSeverity.Debug, "Rbr_Db.Create", string.Format("Load for {0} done", dbName));
			}
			catch (Exception _ex) {
				throw new Exception(string.Format("Failed to create new RbrDb: {0}, Exception:\r\n{1}", dbName, _ex));
			}
		}

		string getBackupDbSQL() {
			string _sql;
			try {
				_sql = Resources.BACKUP_RBR_DB;
			}
			catch (Exception _ex) {
				throw new Exception("Failed to get BACKUP_DB_RESOURCE.\r\n\r\n" + _ex);
			}

			_sql = _sql.Replace("$(DB_VERSION)", Configuration.Instance.Db.RbrDbVersion);
			_sql = _sql.Replace("$(BATCH_MODE)", "0");
			//TODO: replace hardcoded paths in the script
			return _sql;			
		}

		string getCreateFullDbSQL() {
			string _sql;
			try {
				_sql = Resources.CREATE_RBR_DB;
			}
			catch (Exception _ex) {
				throw new Exception("Failed to get CREATE_RBR_DB_RESOURCE.\r\n\r\n" + _ex);
			}

			_sql = _sql.Replace("$(DB_VERSION)", Configuration.Instance.Db.RbrDbVersion);
			return _sql;
		}

		string getLoadDbSQL() {
			string _sql;
			try {
				_sql = Resources.LOAD_INIT_DATA;
			}
			catch (Exception _ex) {
				throw new Exception("Failed to get LOAD_INIT_DATA.\r\n\r\n" + _ex);
			}

			_sql = _sql.Replace("$(DB_VERSION)", Configuration.Instance.Db.RbrDbVersion);
			_sql = _sql.Replace("$(IMPORT_FILES_PATH)", Configuration.Instance.Folders.ImportFilesPath);
			return _sql;
		}

		/// <summary>
		/// Creates a new connection to the database.
		/// </summary>
		/// <returns>An <see cref="System.Data.IDbConnection"/> object.</returns>
		protected override IDbConnection CreateConnection() {
			baseConnectionStringPrefix = string.Format("Data Source={0}\\TRBR;Integrated Security=SSPI;", Configuration.Instance.Main.HostName);
			dbName = Configuration.Instance.Db.RbrDbNamePrefix + Configuration.Instance.Db.RbrDbVersion;
			fullConnectionString = baseConnectionStringPrefix + INIT_CATALOG_PREFIX + dbName + ";";

			//baseConnectionStringPrefix = @"Data Source=TIMOK-LPT\TRBR;Integrated Security=SSPI;";
			//dbName = "RbrDb_269";
			//fullConnectionString = baseConnectionStringPrefix + INIT_CATALOG_PREFIX + dbName + ";";
			
			return new SqlConnection(fullConnectionString);
		}

		/// <summary>
		/// Creates a DataTable object for the specified pCommand.
		/// </summary>
		/// <param name="pCommand">The <see cref="System.Data.IDbCommand"/> object.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected internal DataTable CreateDataTable(IDbCommand pCommand) {
			var _dataTable = new DataTable();
			new SqlDataAdapter((SqlCommand) pCommand).Fill(_dataTable);
			return _dataTable;
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

		protected internal string CreateStatusFilter(Status[] pStatuses) {
			var _statusFilter = string.Empty;
			if (pStatuses == null || pStatuses.Length == 0) {
				//get with any status
				_statusFilter = (byte) Status.Active + ",";
				_statusFilter += (byte) Status.Pending + ",";
				_statusFilter += (byte) Status.Blocked + ",";
				_statusFilter += (byte) Status.Archived;
			}
			else {
				//get with provided status(es)
				foreach (byte _status in pStatuses) {
					_statusFilter += "," + _status;
				}
				_statusFilter = _statusFilter.TrimStart(',');
			}
			return " status IN(" + _statusFilter + ") ";
		}

		protected internal string CreateEnumFilter(string pDbTableName, string pDbFieldName, Array pEnumMembers, Type pEnumType) {
			var _values = string.Empty;
			if (pEnumMembers == null || pEnumMembers.Length == 0) {
				//get all
				return CreateEnumFilter(pDbTableName, pDbFieldName, Enum.GetValues(pEnumType), pEnumType);
			}
			//get with provided values
			foreach (var _obj in pEnumMembers) {
				_values += "," + Convert.ChangeType(_obj, Enum.GetUnderlyingType(pEnumType));
			}
			_values = _values.TrimStart(',');
			return " " + pDbTableName + "." + pDbFieldName + " IN(" + _values + ") ";
		}

		public override void Dispose() {
			base.Dispose();
		}
	} // End of Rbr_Db class
} // End of namespace