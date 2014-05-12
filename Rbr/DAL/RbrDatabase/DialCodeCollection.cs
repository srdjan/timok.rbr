// <fileinfo name="DialCodeCollection.cs">
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
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>DialCode</c> table.
	/// </summary>
	public class DialCodeCollection : DialCodeCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="DialCodeCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal DialCodeCollection(Rbr_Db db) : base(db) {
			// EMPTY
		}

		public static DialCodeRow Parse(DataRow row){
			return new DialCodeCollection(null).MapRow(row);
		}

    public DialCodeRow[] GetByCallingPlanIdCountryIdPaged(int pCallingPlanId, int pCountryId, int pPageNumber, int pPageSize, out int pTotalCount) {
      /*
      DECLARE @CallingPlanId AS INT;
      DECLARE @CountryId AS INT;
      DECLARE @PageNum AS INT;
      DECLARE @PageSize AS INT;

      SET @CallingPlanId = 1;
      SET @CountryId = 145;
      SET @PageNum = 1;
      SET @PageSize = 20;

      WITH DialCodeRN AS (
          SELECT ROW_NUMBER() OVER(ORDER BY CAST(dial_code AS VARCHAR)) AS RowNum
            ,calling_plan_id
            ,dial_code
            ,route_id
            ,version
          FROM DialCode WITH (INDEX(XIF174DialCode))
	      WHERE 
		      calling_plan_id = @CallingPlanId
		      AND
		      route_id IN (
			      SELECT route_id FROM Route WITH (INDEX(XIF486Route)) 
			      WHERE country_id = @CountryId
		      )
      )

      SELECT * FROM DialCodeRN WHERE 
      RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 
      AND @PageNum * @PageSize
      ORDER BY CAST(dial_code AS VARCHAR(12));
      */

      //SELECT TOTAL COUNT
      var _sql = "SELECT COUNT(*) FROM DialCode " +
        " WHERE " + DialCodeRow_Base.calling_plan_id_DbName + " = " + Database.CreateSqlParameterName(DialCodeRow_Base.calling_plan_id_PropName) +
        " AND " +
        " " + DialCodeRow_Base.route_id_DbName + " IN (" +
        "    SELECT " + RouteRow_Base.route_id_DbName + " FROM Route " +
        "    WHERE " + RouteRow_Base.country_id_DbName + " = " + Database.CreateSqlParameterName(RouteRow_Base.country_id_PropName) +
        "   )";

      var _cmd = Database.CreateCommand(_sql);
      AddParameter(_cmd, DialCodeRow_Base.calling_plan_id_PropName, pCallingPlanId);
      Database.AddParameter(_cmd, RouteRow_Base.country_id_PropName, DbType.Int32, pCountryId);

      pTotalCount = (int) _cmd.ExecuteScalar();

      _sql = "WITH DialCodeRN AS (" +
        " SELECT ROW_NUMBER() OVER(ORDER BY CAST(dial_code AS VARCHAR)) AS RowNum " +
        "," + DialCodeRow_Base.calling_plan_id_DbName +
        "," + DialCodeRow_Base.dial_code_DbName +
        "," + DialCodeRow_Base.route_id_DbName +
        "," + DialCodeRow_Base.version_DbName +
        //NOTE: HARDCODED INDEX NAME
        //TODO: INDEX NAME IS HARDCODED in schema
        " FROM DialCode " +
        " WHERE " + DialCodeRow_Base.calling_plan_id_DbName + " = " + Database.CreateSqlParameterName(DialCodeRow_Base.calling_plan_id_PropName) + 
        " AND " + 
        " " + DialCodeRow_Base.route_id_DbName + " IN (" +
        "    SELECT " + RouteRow_Base.route_id_DbName + " FROM Route  " + 
        "    WHERE " + RouteRow_Base.country_id_DbName + " = " + Database.CreateSqlParameterName(RouteRow_Base.country_id_PropName) + 
        "   )" + 
        " )" + 

        " SELECT * FROM DialCodeRN WHERE " +
        " RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 " +
        " AND @PageNum * @PageSize" +
        " ORDER BY CAST(" + DialCodeRow_Base.dial_code_DbName + " AS VARCHAR(12)) ;";

      _cmd = Database.CreateCommand(_sql);
      AddParameter(_cmd, DialCodeRow_Base.calling_plan_id_PropName, pCallingPlanId);
      Database.AddParameter(_cmd, RouteRow_Base.country_id_PropName, DbType.Int32, pCountryId);
      Database.AddParameter(_cmd, "PageNum", DbType.Int32, pPageNumber);
      Database.AddParameter(_cmd, "PageSize", DbType.Int32, pPageSize);
      using (var _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }

    public DialCodeRow[] GetByRouteIdPaged(int pRouteId, int pPageNumber, int pPageSize, out int pTotalCount) {
      /*
        DECLARE @RouteId AS INT;
        DECLARE @PageNum AS INT;
        DECLARE @PageSize AS INT;

        SET @RouteId = 250;
        SET @PageNum = 1;
        SET @PageSize = 20;

        WITH DialCodeRN AS (
            SELECT ROW_NUMBER() OVER(ORDER BY CAST(dial_code AS VARCHAR)) AS RowNum
              ,calling_plan_id
              ,dial_code
              ,route_id
              ,version
	        FROM DialCode 
	        WHERE route_id = @RouteId
        )

        SELECT * FROM DialCodeRN WHERE 
        RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 
        AND @PageNum * @PageSize
        ORDER BY CAST(dial_code AS VARCHAR(12));
      */
      //SELECT TOTAL COUNT
      var _sql = "SELECT COUNT(*) FROM DialCode " +
        " WHERE " + DialCodeRow_Base.route_id_DbName + " = " + Database.CreateSqlParameterName(DialCodeRow_Base.route_id_PropName);

      var _cmd = Database.CreateCommand(_sql);
      AddParameter(_cmd, DialCodeRow_Base.route_id_PropName, pRouteId);

      pTotalCount = (int) _cmd.ExecuteScalar();

      _sql = "WITH DialCodeRN AS (" +
        " SELECT ROW_NUMBER() OVER(ORDER BY CAST(dial_code AS VARCHAR)) AS RowNum " + 
        "," + DialCodeRow_Base.calling_plan_id_DbName +
        "," + DialCodeRow_Base.dial_code_DbName +
        "," + DialCodeRow_Base.route_id_DbName +
        "," + DialCodeRow_Base.version_DbName +
        " FROM DialCode " +
        " WHERE " + DialCodeRow_Base.route_id_DbName + " = " + Database.CreateSqlParameterName(DialCodeRow_Base.route_id_PropName) +
        " )" + 

        " SELECT * FROM DialCodeRN WHERE " +
        " RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 " +
        " AND @PageNum * @PageSize" + 
        " ORDER BY CAST(" + DialCodeRow_Base.dial_code_DbName + " AS VARCHAR(12)) ;";

      _cmd = Database.CreateCommand(_sql);
      AddParameter(_cmd, DialCodeRow_Base.route_id_PropName, pRouteId);
      Database.AddParameter(_cmd, "PageNum", DbType.Int32, pPageNumber);
      Database.AddParameter(_cmd, "PageSize", DbType.Int32, pPageSize);
      using (var _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }

    public int GetRowNumber(int pCallingPlanId, int pRouteId, long pDialCode) {
      /*
      SELECT COUNT(dial_code) FROM DialCode 
      WHERE 
	      calling_plan_id = 1
	      AND
	      route_id = 250 	
	      AND 
	      CAST(dial_code AS VARCHAR) <= '55113042'
      */
      var _sqlStr = "SELECT COUNT(" + DialCodeRow_Base.dial_code_DbName + ") FROM DialCode " +
        " WHERE " +
        DialCodeRow_Base.calling_plan_id_DbName + " = " + Database.CreateSqlParameterName(DialCodeRow_Base.calling_plan_id_PropName) +
        " AND " +
        DialCodeRow_Base.route_id_DbName + " = " + Database.CreateSqlParameterName(DialCodeRow_Base.route_id_PropName) +
        " AND " +
        " CAST(" + DialCodeRow_Base.dial_code_DbName + " AS VARCHAR(12)) <= '" + pDialCode + "'";

      var _cmd = Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, DialCodeRow_Base.calling_plan_id_PropName, pCallingPlanId);
      AddParameter(_cmd, DialCodeRow_Base.route_id_PropName, pRouteId);
      AddParameter(_cmd, DialCodeRow_Base.dial_code_PropName, pDialCode);

      return (int) _cmd.ExecuteScalar();
    }

    public DialCodeRow GetFirstByCallingPlanIdDialedNumber(int pCallingPlanId, string pDialedNumber) {
      if (!Timok.Core.Utils.IsNumeric(pDialedNumber)) {
				throw new Exception(string.Format("DialCodeCollection.GetFirstByCallingPlanIdDialNumber: Invalid Dialed Number: {0}", pDialedNumber));
      }

      /*
      SELECT TOP 1 *
      FROM  DialCode

      WHERE DialCode.CallingPlanId = X AND (@DialedNumber LIKE CAST(dial_code AS varchar) + '%')
      ORDER BY CAST(dial_code AS varchar) DESC 
      */
      var _sqlStr = "SELECT TOP 1 * FROM [dbo].[DialCode] " +
        "WHERE " +
        "[" + DialCodeRow_Base.calling_plan_id_DbName + "]=" + Database.CreateSqlParameterName(DialCodeRow_Base.calling_plan_id_PropName) +
        " AND " +
        "(" + Database.CreateSqlParameterName("DialedNumber") +
        " LIKE CAST([" + DialCodeRow_Base.dial_code_DbName + "] AS varchar) + '%') " +
        "ORDER BY CAST([" + DialCodeRow_Base.dial_code_DbName + "] AS varchar) DESC ";

      //-- ExecuteReader
      var _cmd = Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, DialCodeRow_Base.calling_plan_id_PropName, pCallingPlanId);
      Database.AddParameter(_cmd, "DialedNumber", DbType.AnsiString, pDialedNumber);
      using (var _reader = _cmd.ExecuteReader()) {
        var _dialCodeRows = MapRecords(_reader);
        return 0 == _dialCodeRows.Length ? null : _dialCodeRows[0];
      }
    }

		public DialCodeRow[] GetByCallingPlanIdRouteId(int pCallingPlanId, int pRouteId) {
			/*
			SELECT * FROM  DialCode WHERE 
			calling_plan_id = 1 
			AND 
			route_id = 10
			ORDER BY CAST(dial_code AS varchar) 
			*/
			var _sqlStr = "SELECT * FROM [dbo].[DialCode] " +
				"WHERE " +
				"[" + DialCodeRow_Base.calling_plan_id_DbName + "]=" + Database.CreateSqlParameterName(DialCodeRow_Base.calling_plan_id_PropName) +
				" AND " +
				"[" + DialCodeRow_Base.route_id_DbName + "]=" + Database.CreateSqlParameterName(DialCodeRow_Base.route_id_PropName) +
				" ORDER BY CAST([" + DialCodeRow_Base.dial_code_DbName + "] AS varchar) ";

			var _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, DialCodeRow_Base.calling_plan_id_PropName, pCallingPlanId);
			AddParameter(_cmd, DialCodeRow_Base.route_id_PropName, pRouteId);
			using (var _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}
		
		public DialCodeRow GetFirstByCallingPlanIdRouteId(int pCallingPlanId, int pRouteId) {
      /*
      SELECT TOP 1 * FROM  DialCode WHERE 
      calling_plan_id = 1 
      AND 
      route_id = 10
      ORDER BY CAST(dial_code AS varchar) 
      */
      var _sqlStr = "SELECT TOP 1 * FROM [dbo].[DialCode] " +
        "WHERE " +
        "[" + DialCodeRow_Base.calling_plan_id_DbName + "]=" + Database.CreateSqlParameterName(DialCodeRow_Base.calling_plan_id_PropName) +
        " AND " +
        "[" + DialCodeRow_Base.route_id_DbName + "]=" + Database.CreateSqlParameterName(DialCodeRow_Base.route_id_PropName) +
        " ORDER BY CAST([" + DialCodeRow_Base.dial_code_DbName + "] AS varchar) ";

      var _cmd = Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, DialCodeRow_Base.calling_plan_id_PropName, pCallingPlanId);
      AddParameter(_cmd, DialCodeRow_Base.route_id_PropName, pRouteId);
      using (var _reader = _cmd.ExecuteReader()) {
        var _dialCodeRows = MapRecords(_reader);
        return 0 == _dialCodeRows.Length ? null : _dialCodeRows[0];
      }
    }

    public bool Exists(int pCallingPlanId, long pDialCode) {
      /*
      IF EXISTS(SELECT dial_code FROM DialCode 
      WHERE 
        calling_plan_id = 1 	
        AND 
        dial_code = 55113042)
      SELECT 1
      ELSE 
      SELECT 0 
      */
      var _sqlStr = "IF EXISTS (SELECT " + DialCodeRow_Base.dial_code_DbName + " FROM DialCode " +
        "WHERE [" + DialCodeRow_Base.calling_plan_id_DbName + "] = " +
        Database.CreateSqlParameterName(DialCodeRow_Base.calling_plan_id_PropName) +
        " AND " +
        " [" + DialCodeRow_Base.dial_code_DbName + "] = " +
        Database.CreateSqlParameterName(DialCodeRow_Base.dial_code_PropName) + 
        ") SELECT 1 " + 
        "ELSE SELECT 0";

      var _cmd = Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, DialCodeRow_Base.calling_plan_id_PropName, pCallingPlanId);
      AddParameter(_cmd, DialCodeRow_Base.dial_code_PropName, pDialCode);

      var _res = (int) _cmd.ExecuteScalar();
      return _res > 0;
    }

    public bool IsCoveredByRouteId(long pDialCode, int pRouteId) {
      /*
      SELECT COUNT(*) FROM  DialCode WHERE 
      '9325777777' 
      LIKE CAST(
      dial_code 
      AS varchar) + '%'
      AND route_id = 321 
      */

      var _sqlStr = "SELECT COUNT(*) FROM  DialCode WHERE " +
        DialCodeRow_Base.route_id_DbName + " = " + Database.CreateSqlParameterName(DialCodeRow_Base.route_id_PropName) + " " +
        " AND " +
        Database.CreateSqlParameterName("DialCodeString") + " " + //'9325777777' 
        " LIKE CAST( " +
        " " + DialCodeRow_Base.dial_code_DbName + " " +
        " AS varchar) + '%' ";

      var _cmd = Database.CreateCommand(_sqlStr);
      Database.AddParameter(_cmd, "DialCodeString", DbType.AnsiString, pDialCode);
      AddParameter(_cmd, DialCodeRow_Base.route_id_PropName, pRouteId);

      //_cmd.CommandTimeout = 120;

      var _res = (int) _cmd.ExecuteScalar();
      return _res > 0;
    }

    public DialCodeRow[] GetIsCovered(int pCallingPlanId, int pRouteId, int pCountryCode, long pDialCode) {
      //SELECT * FROM DialCode
      //WHERE 
      //[calling_plan_id] = 1 
      //AND 
      //[route_id] = 2 
      //AND 
      //55255 LIKE CAST([dial_code] AS varchar)+'%'

      var _sqlStr = "SELECT * FROM DialCode " +
        "WHERE [" + DialCodeRow_Base.calling_plan_id_DbName + "] = " +
        Database.CreateSqlParameterName(DialCodeRow_Base.calling_plan_id_PropName) +
        " AND " +
        " [" + DialCodeRow_Base.route_id_DbName + "] = " +
        Database.CreateSqlParameterName(DialCodeRow_Base.route_id_PropName) +
        " AND " +
        " LEFT([" + DialCodeRow_Base.dial_code_DbName + "], " + pCountryCode.ToString().Length + ") <> " +
        Database.CreateSqlParameterName("CountryCode") +
        " AND " +
        " " + Database.CreateSqlParameterName(DialCodeRow_Base.dial_code_PropName) + " LIKE " +
        "CAST([" + DialCodeRow_Base.dial_code_DbName + "] AS varchar) + '%'";

      var _cmd = Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, DialCodeRow_Base.calling_plan_id_PropName, pCallingPlanId);
      AddParameter(_cmd, DialCodeRow_Base.route_id_PropName, pRouteId);
      Database.AddParameter(_cmd, "CountryCode", DbType.Int32, pCountryCode);
      AddParameter(_cmd, DialCodeRow_Base.dial_code_PropName, pDialCode);
      using (var _reader = Database.ExecuteReader(_cmd)) {
        return MapRecords(_reader);
      }
    }

    public DialCodeRow[] GetWillCover(int pCallingPlanId, int pRouteId, long pDialCode) {
      //SELECT * FROM DialCode
      //WHERE 
      //[Calling_Plan_Id] = 1 
      //AND 
      //[route_id] = 2 
      //AND 
      //[dial_code] LIKE '552%'

      var _sqlStr = "SELECT * FROM DialCode " +
        "WHERE [" + DialCodeRow_Base.calling_plan_id_DbName + "] = " +
        Database.CreateSqlParameterName(DialCodeRow_Base.calling_plan_id_PropName) +
        " AND " +
        " [" + DialCodeRow_Base.route_id_DbName + "] = " +
        Database.CreateSqlParameterName(DialCodeRow_Base.route_id_PropName) +
        " AND " +
        "[" + DialCodeRow_Base.dial_code_DbName + "] LIKE " +
        " CAST(" + Database.CreateSqlParameterName(DialCodeRow_Base.dial_code_PropName) + " AS varchar) + '%'";

      var _cmd = Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, DialCodeRow_Base.calling_plan_id_PropName, pCallingPlanId);
      AddParameter(_cmd, DialCodeRow_Base.route_id_PropName, pRouteId);
      AddParameter(_cmd, DialCodeRow_Base.dial_code_PropName, pDialCode);
      using (var _reader = Database.ExecuteReader(_cmd)) {
        return MapRecords(_reader);
      }
    }

		public DialCodeRow[] GetByServiceTypeDialedNumber(ServiceType pServiceType, string pDialedNumber) {
			if (!Timok.Core.Utils.IsNumeric(pDialedNumber)) {
				throw new Exception(string.Format("DialCodeCollection.GetByServiceTypeDialedNumber: Invalid Dialed Number: {0}", pDialedNumber));
			}

			/*
				DECLARE @DialedNumber varchar(50)
				SET @DialedNumber = '5511600346'

				SELECT DialCode.*
				FROM  DialCode 
				WHERE 
				calling_plan_id IN (
					SELECT calling_plan_id FROM Service WHERE type = 0
				)
				AND 
				(@DialedNumber LIKE CAST(dial_code AS varchar) + '%')
				ORDER BY CAST(dial_code AS varchar) DESC 
			*/
			var _sqlStr = "SELECT DialCode.* FROM [dbo].[DialCode] " +
				" WHERE DialCode." + DialCodeRow_Base.calling_plan_id_DbName + " IN " +
				"   (" +
				"     SELECT Service." + ServiceRow_Base.calling_plan_id_DbName + " FROM Service " +
				"     WHERE Service." + ServiceRow_Base.type_DbName + "=" + Database.CreateSqlParameterName(ServiceRow_Base.type_PropName) +
				"   ) " +
				" AND " +
				"(" + Database.CreateSqlParameterName("DialedNumber") +
				" LIKE CAST([" + DialCodeRow_Base.dial_code_DbName + "] AS varchar) + '%') " +
				"ORDER BY CAST([" + DialCodeRow_Base.dial_code_DbName + "] AS varchar) DESC ";

			//-- ExecuteReader
			var _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, ServiceRow_Base.type_PropName, DbType.Byte, (byte)pServiceType);
			Database.AddParameter(_cmd, "DialedNumber", DbType.AnsiString, pDialedNumber);
			using (var _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public DialCodeRow[] GetByDialedNumberFromCarriers(string pDialedNumber) {
			if (!Timok.Core.Utils.IsNumeric(pDialedNumber)) {
				throw new Exception(string.Format("DialCodeCollection.GetByDialedNumberFromCarriers: Invalid Dialed Number: {0}", pDialedNumber));
			}

			/*
				DECLARE @DialedNumber varchar(50)
				SET @DialedNumber = '5511600346'

				SELECT DialCode.* FROM  DialCode 
				WHERE 
				calling_plan_id IN 
				(
					SELECT distinct calling_plan_id FROM CarrierAcct
				)
				AND @DialedNumber = dial_code
			*/
			var _sqlStr = "SELECT DialCode.* FROM [dbo].[DialCode] " +
			              " WHERE DialCode." + DialCodeRow_Base.calling_plan_id_DbName + " IN " +
			              "   (" +
			              "     SELECT DISTINCT CarrierAcct." + CarrierAcctRow_Base.calling_plan_id_DbName + " FROM CarrierAcct " +
			              "   ) " + " AND " +
										" [" + DialCodeRow_Base.dial_code_DbName + "] = " + Database.CreateSqlParameterName("DialedNumber");

			//-- ExecuteReader
			var _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, "DialedNumber", DbType.AnsiString, pDialedNumber);
			using (var _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public int GetCount(int pRouteId) {
			var _sqlStr = "SELECT COUNT(*) FROM  DialCode WHERE " +
				DialCodeRow_Base.route_id_DbName + " = " + Database.CreateSqlParameterName(DialCodeRow_Base.route_id_PropName);

			var _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, DialCodeRow_Base.route_id_PropName, pRouteId);

			var _res = (int)_cmd.ExecuteScalar();
			return _res;
		}

	} // End of DialCodeCollection class
} // End of namespace
