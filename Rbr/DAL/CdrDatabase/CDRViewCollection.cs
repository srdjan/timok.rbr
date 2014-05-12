// <fileinfo name="CDRViewCollection.cs">
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
using Microsoft.SqlServer.Management.Smo;
using Timok.Core;
using Timok.Rbr.DAL.CdrDatabase.Base;

namespace Timok.Rbr.DAL.CdrDatabase 
{
	/// <summary>
	/// Represents the <c>CDRView</c> view.
	/// </summary>
	public class CDRViewCollection : CDRViewCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="CDRViewCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CDRViewCollection(Cdr_Db db) : base(db) {
			// EMPTY
		}

		public static CDRViewRow Parse(DataRow row) {
			return new CDRViewCollection(null).MapRow(row);
		}

		public override CDRViewRow[] GetAll() {
			throw new NotSupportedException("GetAll() is not supported, use GetBy[Indexes]() methods instead.");
		}

		public override DataTable GetAllAsDataTable() {
			throw new NotSupportedException("GetAllAsDataTable() is not supported, use GetBy[Indexes]AsDataTable() methods instead.");
		}

		#region DataTable Getters

		public DataTable GetByRetailAcctIdAsDataTable(int pTimokDate, int pRetailAcctId) {
			string _where = "[" + CDRViewRow.timok_date_DbName + "]=" + base.Database.CreateSqlParameterName(CDRViewRow.timok_date_PropName) + " AND " + "[" + CDRViewRow.retail_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRViewRow.retail_acct_id_PropName);

			IDbCommand cmd = CreateGetCommand(_where, CDRViewRow.start_DbName + " DESC");
			AddParameter(cmd, CDRViewRow.retail_acct_id_PropName, pRetailAcctId);
			AddParameter(cmd, CDRViewRow.timok_date_PropName, pTimokDate);
			using (IDataReader reader = cmd.ExecuteReader()) {
				return MapRecordsToDataTable(reader);
			}
		}

		public DataTable GetByRetailAcctIdAsDataTable(int pStartTimokDate, int pEndTimokDate, int pRetailAcctId) {
			string _where = "([" + CDRViewRow.timok_date_DbName + "] BETWEEN " + base.Database.CreateSqlParameterName("Start_timok_date") + " AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" + " AND " + "[" + CDRViewRow.retail_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRViewRow.retail_acct_id_PropName);

			IDbCommand cmd = CreateGetCommand(_where, CDRViewRow.start_DbName + " DESC");
			AddParameter(cmd, CDRViewRow.retail_acct_id_PropName, pRetailAcctId);
			base.Database.AddParameter(cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			base.Database.AddParameter(cmd, "End_timok_date", DbType.Int32, pEndTimokDate);
			using (IDataReader reader = cmd.ExecuteReader()) {
				return MapRecordsToDataTable(reader);
			}
		}

		#endregion DataTable Getters

		#region CDRViewRow[] Getters

		public CDRViewRow[] GetByStartRetailAcctId(DateTime pDate, int pRetailAcctId) {
			string _where = CDRViewRow.retail_acct_id_DbName + " = " + Database.CreateSqlParameterName(CDRViewRow.retail_acct_id_PropName) + " AND " + CDRViewRow.start_DbName + " = " + Database.CreateSqlParameterName(CDRViewRow.start_PropName);

			IDbCommand _cmd = CreateGetCommand(_where, CDRViewRow.start_DbName + " DESC");
			AddParameter(_cmd, CDRViewRow.retail_acct_id_PropName, pRetailAcctId);
			AddParameter(_cmd, CDRViewRow.start_PropName, pDate);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CDRViewRow[] GetByRetailAcctId(DateTime pDate, int pRetailAcctId) {
			int _startTimokDate = TimokDate.ParseToShortTimokDate(pDate) * 100;
			int _endTimokDate = TimokDate.ParseToShortTimokDate(pDate) * 100 + 23;
			return GetByRetailAcctId(_startTimokDate, _endTimokDate, pRetailAcctId);
		}

		public CDRViewRow[] GetByRetailAcctId(int pStartTimokDate, int pEndTimokDate, int pRetailAcctId) {
			string _where = "([" + CDRViewRow.timok_date_DbName + "] BETWEEN " + base.Database.CreateSqlParameterName("Start_timok_date") + " AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" + " AND " + "[" + CDRViewRow.retail_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRViewRow.retail_acct_id_PropName);

			IDbCommand _cmd = CreateGetCommand(_where, CDRViewRow.start_DbName + " DESC");
			AddParameter(_cmd, CDRViewRow.retail_acct_id_PropName, pRetailAcctId);
			base.Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			base.Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CDRViewRow[] GetByCustomer_acct_id(int pTimokDate, short customer_acct_id) {
			string _sqlStr = "SELECT * FROM [dbo].[CDRView] WHERE " + "[" + CDRViewRow.timok_date_DbName + "]=" + base.Database.CreateSqlParameterName(CDRViewRow.timok_date_PropName) + " AND " + "[" + CDRViewRow.customer_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRViewRow.customer_acct_id_PropName) + " ORDER BY " + CDRViewRow.start_DbName + " DESC";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRViewRow.customer_acct_id_PropName, customer_acct_id);
			AddParameter(_cmd, CDRViewRow.timok_date_PropName, pTimokDate);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CDRViewRow[] GetByCustomer_acct_id(int pStartTimokDate, int pEndTimokDate, short customer_acct_id) {
			string _sqlStr = "SELECT * FROM [dbo].[CDRView] WHERE " + "([" + CDRViewRow.timok_date_DbName + "] BETWEEN " + base.Database.CreateSqlParameterName("Start_timok_date") + " AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" + " AND " + "[" + CDRViewRow.customer_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRViewRow.customer_acct_id_PropName) + " ORDER BY " + CDRViewRow.start_DbName + " DESC";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRViewRow.customer_acct_id_PropName, customer_acct_id);
			base.Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			base.Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CDRViewRow[] GetByCustomer_acct_idEndpoint_id(int pStartTimokDate, int pEndTimokDate, short customer_acct_id, short endpoint_id) {
			string _where = "([" + CDRViewRow.timok_date_DbName + "] BETWEEN " + base.Database.CreateSqlParameterName("Start_timok_date") + " AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" + " AND " + "[" + CDRViewRow.customer_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRViewRow.customer_acct_id_PropName) + " AND " + "[" + CDRViewRow.orig_end_point_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRViewRow.orig_end_point_id_PropName);
			IDbCommand cmd = CreateGetCommand(_where, CDRViewRow.start_DbName + " DESC");
			AddParameter(cmd, CDRViewRow.customer_acct_id_PropName, customer_acct_id);
			AddParameter(cmd, CDRViewRow.orig_end_point_id_PropName, endpoint_id);
			base.Database.AddParameter(cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			base.Database.AddParameter(cmd, "End_timok_date", DbType.Int32, pEndTimokDate);
			using (IDataReader reader = cmd.ExecuteReader()) {
				return MapRecords(reader);
			}
		}

		public CDRViewRow[] GetByCarrier_acct_id(int pTimokDate, short carrier_acct_id) {
			string _sqlStr = "SELECT * FROM [dbo].[CDRView] WHERE " + "[" + CDRViewRow.timok_date_DbName + "]=" + base.Database.CreateSqlParameterName(CDRViewRow.timok_date_PropName) + " AND " + "[" + CDRViewRow.carrier_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRViewRow.carrier_acct_id_PropName) + " ORDER BY " + CDRViewRow.start_DbName + " DESC";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRViewRow.carrier_acct_id_PropName, carrier_acct_id);
			AddParameter(_cmd, CDRViewRow.timok_date_PropName, pTimokDate);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CDRViewRow[] GetByCarrierAcctId(int pStartTimokDate, int pEndTimokDate, short pCarrierAcctId) {
			string _sqlStr = "SELECT * FROM [dbo].[CDRView] WHERE " + "([" + CDRViewRow.timok_date_DbName + "] BETWEEN " + base.Database.CreateSqlParameterName("Start_timok_date") + " AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" + " AND " + "[" + CDRViewRow.carrier_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRViewRow.carrier_acct_id_PropName) + " ORDER BY " + CDRViewRow.start_DbName + " DESC";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRViewRow.carrier_acct_id_PropName, pCarrierAcctId);
			base.Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			base.Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CDRViewRow[] GetByCarrierAcctIdEndpointId(int pStartTimokDate, int pEndTimokDate, short pCarrierAcctId, short pEndpointId) {
			string _sqlStr = "SELECT * FROM [dbo].[CDRView] WHERE " + "([" + CDRViewRow.timok_date_DbName + "] BETWEEN " + base.Database.CreateSqlParameterName("Start_timok_date") + " AND " + base.Database.CreateSqlParameterName("End_timok_date") + ")" + " AND " + "[" + CDRViewRow.carrier_acct_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRViewRow.carrier_acct_id_PropName) + " AND " + "[" + CDRViewRow.term_end_point_id_DbName + "]=" + base.Database.CreateSqlParameterName(CDRViewRow.term_end_point_id_PropName) + " ORDER BY " + CDRViewRow.start_DbName + " DESC";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);

			_cmd.CommandTimeout = 120;

			AddParameter(_cmd, CDRViewRow.carrier_acct_id_PropName, pCarrierAcctId);
			AddParameter(_cmd, CDRViewRow.term_end_point_id_PropName, pEndpointId);
			base.Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			base.Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		#endregion Array Getters

		#region Paged CDRViewRow[] Getters

		public int GetCountByCustomerAcctIdPaged(int pStartTimokDate, int pEndTimokDate, short pCustomerAcctId) {
			#region sql

			/*
      DECLARE @TimokStartDate INT;
      DECLARE @TimokEndDate INT;
      DECLARE @CustomerAcctId SMALLINT;

      SET @TimokStartDate = 200621400;
      SET @TimokEndDate = 200622223;
      SET @CustomerAcctId = 7100

		      SELECT COUNT(*) FROM CDRView 
			      WHERE 
			      (timok_date BETWEEN @TimokStartDate AND @TimokEndDate)
			      AND customer_acct_id = @CustomerAcctId

      */

			#endregion sql

			//SELECT TOTAL COUNT
			string _sql = "SELECT COUNT(*) FROM CDRView " + " WHERE (" + CDRViewRow.timok_date_DbName + " BETWEEN " + Database.CreateSqlParameterName("StartTimokDate") + " AND " + Database.CreateSqlParameterName("EndTimokDate") + " ) " + " AND " + CDRViewRow.customer_acct_id_DbName + " = " + Database.CreateSqlParameterName(CDRViewRow.customer_acct_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sql);
			AddParameter(_cmd, CDRViewRow.customer_acct_id_PropName, pCustomerAcctId);
			Database.AddParameter(_cmd, "StartTimokDate", DbType.Int32, pStartTimokDate);
			Database.AddParameter(_cmd, "EndTimokDate", DbType.Int32, pEndTimokDate);

			return (int) _cmd.ExecuteScalar();
		}

		public CDRViewRow[] GetByCustomerAcctIdPaged(int pStartTimokDate, int pEndTimokDate, short pCustomerAcctId, int pPageNumber, int pPageSize, out int pTotalCount) {
			#region sql

			/*
      DECLARE @TimokStartDate INT;
      DECLARE @TimokEndDate INT;
      DECLARE @CustomerAcctId SMALLINT;
      DECLARE @PageNum AS INT;
      DECLARE @PageSize AS INT;
      DECLARE @TotalCount INT --OUTPUT;

      SET @TimokStartDate = 200621400;
      SET @TimokEndDate = 200622223;
      SET @CustomerAcctId = 7100
      SET @PageNum = 3;
      SET @PageSize = 20;
      SET @TotalCount = 0;

              WITH CDRViewRN AS (
                  SELECT ROW_NUMBER() OVER(ORDER BY start DESC) AS RowNum
                    ,*
	              FROM CDRView 
	              WHERE (timok_date BETWEEN @TimokStartDate AND @TimokEndDate)
			      AND customer_acct_id = @CustomerAcctId
              )

              SELECT * FROM CDRViewRN WHERE 
              RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 
              AND @PageNum * @PageSize
              ORDER BY start DESC;


		      SELECT COUNT(*) FROM CDRView 
			      WHERE 
			      (timok_date BETWEEN @TimokStartDate AND @TimokEndDate)
			      AND customer_acct_id = @CustomerAcctId

      */

			#endregion sql

			//SELECT TOTAL COUNT
			string _sql = "SELECT COUNT(*) FROM CDRView " + " WHERE (" + CDRViewRow.timok_date_DbName + " BETWEEN " + Database.CreateSqlParameterName("StartTimokDate") + " AND " + Database.CreateSqlParameterName("EndTimokDate") + " ) " + " AND " + CDRViewRow.customer_acct_id_DbName + " = " + Database.CreateSqlParameterName(CDRViewRow.customer_acct_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sql);
			AddParameter(_cmd, CDRViewRow.customer_acct_id_PropName, pCustomerAcctId);
			Database.AddParameter(_cmd, "StartTimokDate", DbType.Int32, pStartTimokDate);
			Database.AddParameter(_cmd, "EndTimokDate", DbType.Int32, pEndTimokDate);

			pTotalCount = (int) _cmd.ExecuteScalar();

			//PAGING
			_sql = "WITH CDRViewRN AS (" + " SELECT ROW_NUMBER() OVER(ORDER BY start DESC) AS RowNum " + ", * " + " FROM CDRView " + " WHERE (" + CDRViewRow.timok_date_DbName + " BETWEEN " + Database.CreateSqlParameterName("StartTimokDate") + " AND " + Database.CreateSqlParameterName("EndTimokDate") + " ) " + " AND " + CDRViewRow.customer_acct_id_DbName + " = " + Database.CreateSqlParameterName(CDRViewRow.customer_acct_id_PropName) + " ) " + " SELECT * FROM CDRViewRN WHERE " + " RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 " + " AND @PageNum * @PageSize" + " ORDER BY " + CDRViewRow.start_DbName + " DESC;";

			_cmd = Database.CreateCommand(_sql);
			AddParameter(_cmd, CDRViewRow.customer_acct_id_PropName, pCustomerAcctId);
			Database.AddParameter(_cmd, "StartTimokDate", DbType.Int32, pStartTimokDate);
			Database.AddParameter(_cmd, "EndTimokDate", DbType.Int32, pEndTimokDate);
			Database.AddParameter(_cmd, "PageNum", DbType.Int32, pPageNumber);
			Database.AddParameter(_cmd, "PageSize", DbType.Int32, pPageSize);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CDRViewRow[] GetByRetailAcctIdPaged(int pStartTimokDate, int pEndTimokDate, int pRetailAcctId, int pPageNumber, int pPageSize, out int pTotalCount) {
			#region sql

			/*
      DECLARE @TimokStartDate INT;
      DECLARE @TimokEndDate INT;
      DECLARE @RetailAcctId INT;
      DECLARE @PageNum AS INT;
      DECLARE @PageSize AS INT;
      DECLARE @TotalCount INT --OUTPUT;

      SET @TimokStartDate = 200621400;
      SET @TimokEndDate = 200622223;
      SET @RetailAcctId = 1000
      SET @PageNum = 3;
      SET @PageSize = 20;
      SET @TotalCount = 0;

              WITH CDRViewRN AS (
                  SELECT ROW_NUMBER() OVER(ORDER BY start DESC) AS RowNum
                    ,*
	              FROM CDRView 
	              WHERE (timok_date BETWEEN @TimokStartDate AND @TimokEndDate)
			      AND retail_acct_id = @RetailAcctId
              )

              SELECT * FROM CDRViewRN WHERE 
              RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 
              AND @PageNum * @PageSize
              ORDER BY start DESC;


		      SELECT COUNT(*) FROM CDRView 
			      WHERE 
			      (timok_date BETWEEN @TimokStartDate AND @TimokEndDate)
			      AND retail_acct_id = @RetailAcctId

      */

			#endregion sql

			//SELECT TOTAL COUNT
			string _sql = "SELECT COUNT(*) FROM CDRView " + " WHERE (" + CDRViewRow.timok_date_DbName + " BETWEEN " + Database.CreateSqlParameterName("StartTimokDate") + " AND " + Database.CreateSqlParameterName("EndTimokDate") + " ) " + " AND " + CDRViewRow.retail_acct_id_DbName + " = " + Database.CreateSqlParameterName(CDRViewRow.retail_acct_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sql);
			AddParameter(_cmd, CDRViewRow.retail_acct_id_PropName, pRetailAcctId);
			Database.AddParameter(_cmd, "StartTimokDate", DbType.Int32, pStartTimokDate);
			Database.AddParameter(_cmd, "EndTimokDate", DbType.Int32, pEndTimokDate);

			pTotalCount = (int) _cmd.ExecuteScalar();

			//PAGING
			_sql = "WITH CDRViewRN AS (" + " SELECT ROW_NUMBER() OVER(ORDER BY start DESC) AS RowNum " + ", * " + " FROM CDRView " + " WHERE (" + CDRViewRow.timok_date_DbName + " BETWEEN " + Database.CreateSqlParameterName("StartTimokDate") + " AND " + Database.CreateSqlParameterName("EndTimokDate") + " ) " + " AND " + CDRViewRow.retail_acct_id_DbName + " = " + Database.CreateSqlParameterName(CDRViewRow.retail_acct_id_PropName) + " ) " + " SELECT * FROM CDRViewRN WHERE " + " RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 " + " AND @PageNum * @PageSize" + " ORDER BY " + CDRViewRow.start_DbName + " DESC;";

			_cmd = Database.CreateCommand(_sql);
			AddParameter(_cmd, CDRViewRow.retail_acct_id_PropName, pRetailAcctId);
			Database.AddParameter(_cmd, "StartTimokDate", DbType.Int32, pStartTimokDate);
			Database.AddParameter(_cmd, "EndTimokDate", DbType.Int32, pEndTimokDate);
			Database.AddParameter(_cmd, "PageNum", DbType.Int32, pPageNumber);
			Database.AddParameter(_cmd, "PageSize", DbType.Int32, pPageSize);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		#endregion Paged CDRViewRow[] Getters

		public CDRViewRow[] Get(int pTimokDate, string pAdditionalWhereFilter) {
			string _sqlStr = "SELECT * FROM [dbo].[CDRView] WHERE ([" + CDRViewRow.timok_date_DbName + "] = " + Database.CreateSqlParameterName("Start_timok_date") + ")";
			if (pAdditionalWhereFilter != null && pAdditionalWhereFilter.Trim().Length > 0) {
				_sqlStr += " AND (" + pAdditionalWhereFilter + ")";
			}
			_sqlStr += " ORDER BY " + CDRViewRow.start_DbName;

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			_cmd.CommandTimeout = 120;

			Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pTimokDate);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public CDRViewRow[] Get(int pStartTimokDate, int pEndTimokDate, string pAdditionalWhereFilter) {
			string _sqlStr = "SELECT * FROM [dbo].[CDRView] WHERE ([" + CDRViewRow.timok_date_DbName + "] BETWEEN " + Database.CreateSqlParameterName("Start_timok_date") + " AND " + Database.CreateSqlParameterName("End_timok_date") + ")";
			if (pAdditionalWhereFilter != null && pAdditionalWhereFilter.Trim().Length > 0) {
				_sqlStr += " AND (" + pAdditionalWhereFilter + ")";
			}
			_sqlStr += " ORDER BY " + CDRViewRow.start_DbName;

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			_cmd.CommandTimeout = 120;

			Database.AddParameter(_cmd, "Start_timok_date", DbType.Int32, pStartTimokDate);
			Database.AddParameter(_cmd, "End_timok_date", DbType.Int32, pEndTimokDate);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}
	}

	// End of CDRViewCollection class
} // End of namespace