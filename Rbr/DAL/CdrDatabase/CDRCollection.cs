// <fileinfo name="CDRCollection.cs">
//		<copyright>
//			Copyright © 2002-2007 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			You can update this source code manually. If the file
//			already exists it will not be rewritten by the generator.
//		</remarks>
//		<generator rewritefile="False" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using System.Data;

using Timok.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.CdrDatabase.Base;

namespace Timok.Rbr.DAL.CdrDatabase {
	/// <summary>
	/// Represents the <c>CDR</c> table.
	/// </summary>
	public class CDRCollection : CDRCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="CDRCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CDRCollection(Cdr_Db db)
			: base(db) {
			// EMPTY
		}

		public static CDRRow Parse(System.Data.DataRow row) {
			return new CDRCollection(null).MapRow(row);
		}

		public override CDRRow[] GetAll() {
			throw new NotSupportedException();
		}

		public override DataTable GetAllAsDataTable() {
			throw new NotSupportedException();
		}

		public void Update(CDRRow value) {
			string _sqlStr = "UPDATE [dbo].[CDR] SET " +
				"[date_logged]=" + Database.CreateSqlParameterName("Date_logged") + ", " +
				"[timok_date]=" + Database.CreateSqlParameterName("Timok_date") + ", " +
				"[start]=" + Database.CreateSqlParameterName("Start") + ", " +
				"[duration]=" + Database.CreateSqlParameterName("Duration") + ", " +
				"[ccode]=" + Database.CreateSqlParameterName("CCode") + ", " +
				"[local_number]=" + Database.CreateSqlParameterName("Local_number") + ", " +
				"[carrier_route_id]=" + Database.CreateSqlParameterName("Carrier_route_id") + ", " +
				"[price]=" + Database.CreateSqlParameterName("Price") + ", " +
				"[cost]=" + Database.CreateSqlParameterName("Cost") + ", " +
				"[orig_IP_address]=" + Database.CreateSqlParameterName("Orig_IP_address") + ", " +
				"[orig_end_point_id]=" + Database.CreateSqlParameterName("Orig_end_point_id") + ", " +
				"[term_end_point_id]=" + Database.CreateSqlParameterName("Term_end_point_id") + ", " +
				"[customer_acct_id]=" + Database.CreateSqlParameterName("Customer_acct_id") + ", " +
				"[disconnect_cause]=" + Database.CreateSqlParameterName("Disconnect_cause") + ", " +
				"[disconnect_source]=" + Database.CreateSqlParameterName("Disconnect_source") + ", " +
				"[rbr_result]=" + Database.CreateSqlParameterName("Rbr_result") + ", " +
				"[prefix_in]=" + Database.CreateSqlParameterName("Prefix_in") + ", " +
				"[prefix_out]=" + Database.CreateSqlParameterName("Prefix_out") + ", " +
				"[DNIS]=" + Database.CreateSqlParameterName("DNIS") + ", " +
				"[ANI]=" + Database.CreateSqlParameterName("ANI") + ", " +
				"[serial_number]=" + Database.CreateSqlParameterName("Serial_number") + ", " +
				"[end_user_price]=" + Database.CreateSqlParameterName("End_user_price") + ", " +
				"[used_bonus_minutes]=" + Database.CreateSqlParameterName("Used_bonus_minutes") + ", " +
				"[node_id]=" + Database.CreateSqlParameterName("Node_id") + ", " +
				"[customer_route_id]=" + Database.CreateSqlParameterName("Customer_route_id") + ", " +
				"[mapped_disconnect_cause]=" + Database.CreateSqlParameterName("Mapped_disconnect_cause") + ", " +
				"[carrier_acct_id]=" + Database.CreateSqlParameterName("Carrier_acct_id") + ", " +
				"[customer_duration]=" + Database.CreateSqlParameterName("Customer_duration") + ", " +
				"[retail_acct_id]=" + Database.CreateSqlParameterName("Retail_acct_id") + ", " +
				"[reseller_price]=" + Database.CreateSqlParameterName("Reseller_price") + ", " +
				"[carrier_duration]=" + Database.CreateSqlParameterName("Carrier_duration") + ", " +
				"[retail_duration]=" + Database.CreateSqlParameterName("Retail_duration") + ", " +
				"[info_digits]=" + Database.CreateSqlParameterName("Info_digits") + 
				" WHERE [id]=" + Database.CreateSqlParameterName("Id");
				
			IDbCommand cmd = Database.CreateCommand(_sqlStr);
			AddParameter(cmd, "Date_logged", value.Date_logged);
			AddParameter(cmd, "Timok_date", value.Timok_date);
			AddParameter(cmd, "Start", value.Start);
			AddParameter(cmd, "Duration", value.Duration);
			AddParameter(cmd, "Ccode", value.Ccode);
			AddParameter(cmd, "Local_number", value.Local_number);
			AddParameter(cmd, "Carrier_route_id", value.Carrier_route_id);
			AddParameter(cmd, "Price", value.Price);
			AddParameter(cmd, "Cost", value.Cost);
			AddParameter(cmd, "Orig_IP_address", value.Orig_IP_address);
			AddParameter(cmd, "Orig_end_point_id", value.Orig_end_point_id);
			AddParameter(cmd, "Term_end_point_id", value.Term_end_point_id);
			AddParameter(cmd, "Customer_acct_id", value.Customer_acct_id);
			AddParameter(cmd, "Disconnect_cause", value.Disconnect_cause);
			AddParameter(cmd, "Disconnect_source", value.Disconnect_source);
			AddParameter(cmd, "Rbr_result", value.Rbr_result);
			AddParameter(cmd, "Prefix_in", value.Prefix_in);
			AddParameter(cmd, "Prefix_out", value.Prefix_out);
			AddParameter(cmd, "DNIS", value.DNIS);
			AddParameter(cmd, "ANI", value.ANI);
			AddParameter(cmd, "Serial_number", value.Serial_number);
			AddParameter(cmd, "End_user_price", value.End_user_price);
			AddParameter(cmd, "Used_bonus_minutes", value.Used_bonus_minutes);
			AddParameter(cmd, "Node_id", value.Node_id);
			AddParameter(cmd, "Customer_route_id", value.Customer_route_id);
			AddParameter(cmd, "Mapped_disconnect_cause", value.Mapped_disconnect_cause);
			AddParameter(cmd, "Carrier_acct_id", value.Carrier_acct_id);
			AddParameter(cmd, "Customer_duration", value.Customer_duration);
			AddParameter(cmd, "Retail_acct_id", value.Retail_acct_id);
			AddParameter(cmd, "Reseller_price", value.Reseller_price);
			AddParameter(cmd, "Carrier_duration", value.Carrier_duration);
			AddParameter(cmd, "Retail_duration", value.Retail_duration);
			AddParameter(cmd, "Info_digits", value.Info_digits);
			AddParameter(cmd, "Id", value.Id);
			cmd.ExecuteNonQuery();
		}

		#region CDRRow[] Array Getters
		public CDRRow[] GetByTimeSpan(DateTime pEndDateTime, int pMinutesBack) {
			DateTime _startDateTime = pEndDateTime.Subtract(TimeSpan.FromMinutes(pMinutesBack));

			string _sqlStr = "SELECT * FROM [dbo].[CDR] WHERE " +
				"([" + CDRRow.start_DbName + "] BETWEEN " + base.Database.CreateSqlParameterName("Start_date_time") +
				" AND " + base.Database.CreateSqlParameterName("End_date_time") + ")";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);

			_cmd.CommandTimeout = 120;

			base.Database.AddParameter(_cmd, "Start_date_time", DbType.DateTime, _startDateTime);
			base.Database.AddParameter(_cmd, "End_date_time", DbType.DateTime, pEndDateTime);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		//used by CdrExporter
		public CDRRow[] GetByDateLoggedRange(DateTime pStartDateTime, DateTime pEndDateTime) {
			string _sqlStr = "SELECT * FROM [dbo].[CDR] WHERE " +
				"([" + CDRRow.date_logged_DbName + "] BETWEEN " + base.Database.CreateSqlParameterName("Start_date_time") +
				" AND " + base.Database.CreateSqlParameterName("End_date_time") + ")";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);

			_cmd.CommandTimeout = 120;

			base.Database.AddParameter(_cmd, "Start_date_time", DbType.DateTime, pStartDateTime);
			base.Database.AddParameter(_cmd, "End_date_time", DbType.DateTime, pEndDateTime);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CDRRow[] GetByDateRange(int pStartTimokDate, int pEndTimokDate) {
			string _sqlStr = "SELECT * FROM [dbo].[CDR] WHERE " +
				"([" + CDRRow.timok_date_DbName + "] BETWEEN " + base.Database.CreateSqlParameterName("Start_timok_date") +
				" AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);

			_cmd.CommandTimeout = 120;

			base.Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			base.Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CDRRow[] GetByCustomer_acct_id(int pTimokDate, short customer_acct_id) {
			string _sqlStr = "SELECT * FROM [dbo].[CDR] WHERE " +
				"[" + CDRRow.timok_date_DbName + "]=" + base.Database.CreateSqlParameterName(CDRRow.timok_date_PropName) +
				" AND " +
				"[" + CDRRow.customer_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRRow.customer_acct_id_PropName) +
				" ORDER BY " + CDRRow.start_DbName + " DESC";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRRow.customer_acct_id_PropName, customer_acct_id);
			AddParameter(_cmd, CDRRow.timok_date_PropName, pTimokDate);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CDRRow[] GetByCustomer_acct_id(int pStartTimokDate, int pEndTimokDate, short customer_acct_id) {
			string _sqlStr = "SELECT * FROM [dbo].[CDR] WHERE " +
				"([" + CDRRow.timok_date_DbName + "] BETWEEN " + base.Database.CreateSqlParameterName("Start_timok_date") +
				" AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" +
				" AND " +
				"[" + CDRRow.customer_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRRow.customer_acct_id_PropName) +
				" ORDER BY " + CDRRow.start_DbName + " DESC";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRRow.customer_acct_id_PropName, customer_acct_id);
			base.Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			base.Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CDRRow[] GetBySerial(long pSerial) {
			string _sqlStr = "SELECT * FROM [dbo].[CDR] WHERE " +
				"[" + CDRRow.serial_number_DbName + "]=" + base.Database.CreateSqlParameterName(CDRRow.serial_number_PropName) +
				" ORDER BY " + CDRRow.start_DbName + " DESC" +
				"";

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRRow.serial_number_PropName, pSerial);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CDRRow[] GetByRetailAcctId(int pTimokDate, int pRetailAcctId) {
			string _sqlStr = "SELECT * FROM [dbo].[CDR] WHERE " +
				"[" + CDRRow.timok_date_DbName + "]=" + base.Database.CreateSqlParameterName(CDRRow.timok_date_PropName) +
				" AND " +
				"[" + CDRRow.retail_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRRow.retail_acct_id_PropName) +
				" ORDER BY " + CDRRow.start_DbName + " DESC" +
				"";

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRRow.retail_acct_id_PropName, pRetailAcctId);
			AddParameter(_cmd, CDRRow.timok_date_PropName, pTimokDate);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CDRRow[] GetByRetailAcctId(int pStartTimokDate, int pEndTimokDate, int pRetailAcctId) {
			string _sqlStr = "SELECT * FROM [dbo].[CDR] WHERE " +
				"([" + CDRRow.timok_date_DbName + "] BETWEEN " + base.Database.CreateSqlParameterName("Start_timok_date") +
				" AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" +
				" AND " +
				"[" + CDRRow.retail_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRRow.retail_acct_id_PropName) +
				" ORDER BY " + CDRRow.start_DbName + " DESC";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRRow.retail_acct_id_PropName, pRetailAcctId);
			base.Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			base.Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		#endregion CDRRow[] Array Getters

		#region Count Getters

		public int GetCountByCarrierRouteId(int pStartTimokDate, int pEndTimokDate, int pCarrierRouteId) {
			string _sql = "SELECT COUNT([" + CDRRow.carrier_route_id_DbName + "]) FROM CDR " +
				"WHERE ([" + CDRRow.timok_date_DbName + "] BETWEEN " +
				base.Database.CreateSqlParameterName("Start_timok_date") +
				" AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" +
				" AND " +
				"[" + CDRRow.carrier_route_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRRow.carrier_route_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sql);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRRow.carrier_route_id_PropName, pCarrierRouteId);
			Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);

			object _count = _cmd.ExecuteScalar();
			if (_count != null) {
				return (int)_count;
			}
			else {
				return 0;
			}
		}

		public int GetCountByCustomerRouteId(int pStartTimokDate, int pEndTimokDate, int pCustomerRouteId) {
			string _sql = "SELECT COUNT([" + CDRRow.customer_route_id_DbName + "]) FROM CDR " +
				"WHERE ([" + CDRRow.timok_date_DbName + "] BETWEEN " +
				base.Database.CreateSqlParameterName("Start_timok_date") +
				" AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" +
				" AND " +
				"[" + CDRRow.customer_route_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRRow.customer_route_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sql);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRRow.customer_route_id_PropName, pCustomerRouteId);
			Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);

			object _count = _cmd.ExecuteScalar();
			if (_count != null) {
				return (int)_count;
			}
			else {
				return 0;
			}
		}

		public int GetCountByCustomerAcctId(int pStartTimokDate, int pEndTimokDate, short pCustomerAcctId) {
			string _sql = "SELECT COUNT([" + CDRRow.customer_acct_id_DbName + "]) FROM CDR " +
				"WHERE ([" + CDRRow.timok_date_DbName + "] BETWEEN " +
				base.Database.CreateSqlParameterName("Start_timok_date") +
				" AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" +
				" AND " +
				"[" + CDRRow.customer_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRRow.customer_acct_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sql);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRRow.customer_acct_id_PropName, pCustomerAcctId);
			Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);

			object _count = _cmd.ExecuteScalar();
			if (_count != null) {
				return (int)_count;
			}
			else {
				return 0;
			}
		}

		public int GetCountByCarrierAcctId(int pStartTimokDate, int pEndTimokDate, short pCarrierAcctId) {
			string _sql = "SELECT COUNT([" + CDRRow.carrier_acct_id_DbName + "]) FROM CDR " +
				"WHERE ([" + CDRRow.timok_date_DbName + "] BETWEEN " +
				base.Database.CreateSqlParameterName("Start_timok_date") +
				" AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" +
				" AND " +
				"[" + CDRRow.carrier_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRRow.carrier_acct_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sql);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRRow.carrier_acct_id_PropName, pCarrierAcctId);
			Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);

			object _count = _cmd.ExecuteScalar();
			if (_count != null) {
				return (int)_count;
			}
			else {
				return 0;
			}
		}

		public int GetCountByTermEndPointId(int pStartTimokDate, int pEndTimokDate, short pTermEndPointId) {
			string _sql = "SELECT COUNT([" + CDRRow.term_end_point_id_DbName + "]) FROM CDR " +
				"WHERE ([" + CDRRow.timok_date_DbName + "] BETWEEN " +
				base.Database.CreateSqlParameterName("Start_timok_date") +
				" AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" +
				" AND " +
				"[" + CDRRow.term_end_point_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRRow.term_end_point_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sql);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRRow.term_end_point_id_PropName, pTermEndPointId);
			Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);

			object _count = _cmd.ExecuteScalar();
			if (_count != null) {
				return (int)_count;
			}
			else {
				return 0;
			}
		}

		public int GetCountByOrigEndPointId(int pStartTimokDate, int pEndTimokDate, short pOrigEndPointId) {
			string _sql = "SELECT COUNT([" + CDRRow.orig_end_point_id_DbName + "]) FROM CDR " +
				"WHERE ([" + CDRRow.timok_date_DbName + "] BETWEEN " +
				base.Database.CreateSqlParameterName("Start_timok_date") +
				" AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" +
				" AND " +
				"[" + CDRRow.orig_end_point_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRRow.orig_end_point_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sql);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRRow.orig_end_point_id_PropName, pOrigEndPointId);
			Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);

			object _count = _cmd.ExecuteScalar();
			if (_count != null) {
				return (int)_count;
			}
			else {
				return 0;
			}
		}

		public int GetCountByRetailAcctId(int pStartTimokDate, int pEndTimokDate, int pRetailAcctId) {
			string _sql = "SELECT COUNT([" + CDRRow.retail_acct_id_DbName + "]) FROM CDR " +
				"WHERE ([" + CDRRow.timok_date_DbName + "] BETWEEN " +
				base.Database.CreateSqlParameterName("Start_timok_date") +
				" AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" +
				" AND " +
				"[" + CDRRow.retail_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRRow.retail_acct_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sql);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRRow.retail_acct_id_PropName, pRetailAcctId);
			Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);

			object _count = _cmd.ExecuteScalar();
			if (_count != null) {
				return (int)_count;
			}
			else {
				return 0;
			}
		}
		#endregion Count Getters

		#region used by ReportEngine

		/// <summary>
		/// GetTermEpASRByTimokDate return number of calls completed and 
		/// number of total calls for termination TrunkGroups per day
		/// </summary>
		/// <param name="pTimokDate">TimokDate [yyyyjjj] or [yyyyjjjhh]</param>
		/// <returns></returns>
		public DataTable GetTermEpAsr(int pTimokDate) {
			return GetTermEpAsr(pTimokDate, pTimokDate);
		}
		/// <summary>
		/// GetTermEpASRByTimokDateRange return number of calls completed and 
		/// number of total calls for termination TrunkGroups for date range
		/// </summary>
		/// <param name="pStartTimokDate">TimokDate [yyyyjjj] or [yyyyjjjhh]</param>
		/// <param name="pEndTimokDate">TimokDate [yyyyjjj] or [yyyyjjjhh]</param>
		/// <returns></returns>
		public DataTable GetTermEpAsr(int pStartTimokDate, int pEndTimokDate) {
			if (TimokDate.IsShortTimokDate(pStartTimokDate)) {
				pStartTimokDate = pStartTimokDate * 100;//add 00 hour
			}
			if (TimokDate.IsShortTimokDate(pEndTimokDate)) {
				pEndTimokDate = pEndTimokDate * 100 + 23;//add 23rd hour
			}

			string sqlStr = "DECLARE  @T TABLE (term_ep_id smallint, term_ep_calls int, term_ep_connected_calls int) " +
				//DECLARE @Start_timok_date int, @End_timok_date int
				//SET @Start_timok_date = 200333000
				//SET @End_timok_date = 200333023
				"INSERT @T " +
				"SELECT CAST(" + CDRRow.term_end_point_id_DbName + " as smallint), CAST(count(*) as int), CAST(0 as int) " +
				"FROM [CDR] WHERE " + CDRRow.timok_date_DbName + " BETWEEN " +
				base.Database.CreateSqlParameterName("Start_timok_date") +
				" AND " + base.Database.CreateSqlParameterName("End_timok_date") +
				" GROUP BY " + CDRRow.term_end_point_id_DbName + " " +

				"UNION " +

				"SELECT CAST(" + CDRRow.term_end_point_id_DbName + " as smallint), CAST(0 as int), CAST(count(*) as int) " +
				"FROM [CDR] WHERE " + CDRRow.duration_DbName + " > 0 " +
				"AND " + CDRRow.timok_date_DbName + " BETWEEN " +
				base.Database.CreateSqlParameterName("Start_timok_date") +
				" AND " + base.Database.CreateSqlParameterName("End_timok_date") +
				" GROUP BY " + CDRRow.term_end_point_id_DbName + " " +

				"SELECT term_ep_id AS " + EndpointAsrDataTable.Id_ColumnName + ", SUM(term_ep_calls) AS " + EndpointAsrDataTable.Calls_ColumnName + ", SUM(term_ep_connected_calls) AS " + EndpointAsrDataTable.Connected_calls_ColumnName + " " +
				", asr = " +
				"CASE WHEN SUM(term_ep_calls) > 0 THEN SUM(term_ep_connected_calls) * 100 / SUM(term_ep_calls) " +
				"ELSE 0 END " +
				"FROM @T GROUP BY term_ep_id " +
				"ORDER BY term_ep_id";

			IDbCommand cmd = base.Database.CreateCommand(sqlStr);

			cmd.CommandTimeout = 600;

			base.Database.AddParameter(cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			base.Database.AddParameter(cmd, "End_timok_date", DbType.Int32, pEndTimokDate);
			using (IDataReader reader = cmd.ExecuteReader()) {
				return MapRecordsToDataTable(reader, EndpointAsrDataTable.CreateNew());
			}
		}

		public DataTable GetCustomerRoutesTotalMinutes(int pTimokDate) {
			return GetCustomerRoutesTotalMinutes(pTimokDate, pTimokDate);
		}

		public DataTable GetCustomerRoutesTotalMinutes(int pStartTimokDate, int pEndTimokDate) {
			if (TimokDate.IsShortTimokDate(pStartTimokDate)) {
				pStartTimokDate = pStartTimokDate * 100;//add 00 hour
			}
			if (TimokDate.IsShortTimokDate(pEndTimokDate)) {
				pEndTimokDate = pEndTimokDate * 100 + 23;//add 23rd hour
			}

			string sqlStr = "SELECT " + CDRRow.customer_route_id_DbName + " AS " + CustomerRoutesTotalMinutesDataTable.Id_ColumnName +
				", SUM(" +
				"CAST(" + CDRRow.duration_DbName + " / 6 + (CASE WHEN " + CDRRow.duration_DbName + " % 6 > 0 THEN 1 ELSE 0 END) AS decimal(9, 1)) / 10 " +
				//CDRRow.minutes_DbName + 
				") AS " + CustomerRoutesTotalMinutesDataTable.Minutes_ColumnName + " " +
				"FROM CDR " +
				"WHERE (" + CDRRow.timok_date_DbName + " BETWEEN " +
				base.Database.CreateSqlParameterName("Start_timok_date") +
				" AND " + base.Database.CreateSqlParameterName("End_timok_date") +
				") GROUP BY " + CDRRow.customer_route_id_DbName;

			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			base.Database.AddParameter(cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			base.Database.AddParameter(cmd, "End_timok_date", DbType.Int32, pEndTimokDate);

			cmd.CommandTimeout = 600;

			using (IDataReader reader = cmd.ExecuteReader()) {
				return MapRecordsToDataTable(reader, CustomerRoutesTotalMinutesDataTable.CreateNew());
			}
		}

		private DataTable MapRecordsToDataTable(IDataReader reader, DataTable pDataTable) {
			pDataTable.BeginLoadData();
			int columnCount = reader.FieldCount;
			object[] values = new object[columnCount];

			while (reader.Read()) {
				reader.GetValues(values);
				pDataTable.LoadDataRow(values, true);
			}
			pDataTable.EndLoadData();
			return pDataTable;
		}


		#endregion used by ReportEngine
	}
	 // End of CDRCollection class
} // End of namespace
