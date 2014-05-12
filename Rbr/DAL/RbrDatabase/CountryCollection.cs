// <fileinfo name="CountryCollection.cs">
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
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase.Base;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>Country</c> table.
	/// </summary>
	public class CountryCollection : CountryCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="CountryCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CountryCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static CountryRow Parse(DataRow row){
			return new CountryCollection(null).MapRow(row);
		}

		public override void Insert(CountryRow value) {
			var _sqlStr = "DECLARE " + Database.CreateSqlParameterName(CountryRow_Base.country_id_PropName) + " int " + 
				"SET " + Database.CreateSqlParameterName(CountryRow_Base.country_id_PropName) + 
				" = COALESCE((SELECT MAX(" + CountryRow_Base.country_id_DbName + ") FROM Country) + 1, 1) " + 
				
				"INSERT INTO [dbo].[Country] (" +
				"[" + CountryRow_Base.country_id_DbName + "], " +
				"[" + CountryRow_Base.name_DbName + "], " +
				"[" + CountryRow_Base.country_code_DbName + "], " +
				"[" + CountryRow_Base.status_DbName + "], " +
				"[" + CountryRow_Base.version_DbName + "]" +
				") VALUES (" +
				Database.CreateSqlParameterName(CountryRow_Base.country_id_PropName) + ", " +
				Database.CreateSqlParameterName(CountryRow_Base.name_PropName) + ", " +
				Database.CreateSqlParameterName(CountryRow_Base.country_code_PropName) + ", " + 
				Database.CreateSqlParameterName(CountryRow_Base.status_PropName) + ", " +
        Database.CreateSqlParameterName(CountryRow_Base.version_PropName) + ") " +
        " SELECT " + Database.CreateSqlParameterName(CountryRow_Base.country_id_PropName);
			var _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CountryRow_Base.name_PropName, value.Name);
			AddParameter(_cmd, CountryRow_Base.country_code_PropName, value.Country_code);
			AddParameter(_cmd, CountryRow_Base.status_PropName, value.Status);
      AddParameter(_cmd, CountryRow_Base.version_PropName, value.Version);

			value.Country_id = (int) _cmd.ExecuteScalar();
		}

		protected override IDbCommand CreateGetAllCommand() {
			return CreateGetCommand(null, CountryRow_Base.name_DbName);
		}

		/*
			SELECT *
			FROM [RbrDb_267].[dbo].[Country]
			WHERE country_id in ( 
				SELECT country_id 
				FROM [RbrDb_267].[dbo].[Route]
				WHERE [RbrDb_267].[dbo].[Route].[calling_plan_id] = 2
			)  
		 */
		public CountryRow[] GetByCallingPlanId(int pCallingPlanId) {
			var _sqlStr = "SELECT * FROM Country " + 
				"WHERE " + CountryRow_Base.country_id_DbName + " in " + 
				"( SELECT " + RouteRow_Base.country_id_DbName + " FROM Route " + 
			  "WHERE " + RouteRow_Base.calling_plan_id_DbName + "="  + pCallingPlanId + ")";

			var _cmd = Database.CreateCommand(_sqlStr);
	
			using(var _reader = Database.ExecuteReader(_cmd)) {
				return MapRecords(_reader);
			}
		}

		public CountryRow[] GetByRoutingPlanId(int pRoutingPlanId) {
			/*SELECT DISTINCT [dbo].[Country].[country_id]
												,[dbo].[Country].[name]
												,[dbo].[Country].[country_code]
												,[dbo].[Country].[status]
												,[dbo].[Country].[version]
				FROM  [dbo].[Country], [dbo].[Route]
				WHERE [dbo].[Country].country_id = [dbo].[Route].country_id
				AND   [dbo].[Route].route_id in 
					( 
					SELECT [route_id]
					FROM [RbrDb_269].[dbo].[RoutingPlanDetail]
					WHERE routing_plan_id = 1
					)
		 */
			var _sqlStr = "SELECT DISTINCT " +
				"[Country].[" + CountryRow_Base.country_id_DbName + "], " +
				"[Country].[" + CountryRow_Base.name_DbName + "], " +
				"[Country].[" + CountryRow_Base.country_code_DbName + "], " +
				"[Country].[" + CountryRow_Base.status_DbName + "], " +
				"[Country].[" + CountryRow_Base.version_DbName + "]" +
			" FROM [Country], [Route] " +
				"WHERE " + "[Country].[" + CountryRow_Base.country_id_DbName + "] = " + "[Route].[" + RouteRow_Base.country_id_DbName + "]" +
				" and [Route].[" + RouteRow_Base.route_id_DbName + "]" + 
        " in " +
				"( SELECT [RoutingPlanDetail].[" + RoutingPlanDetailRow_Base.route_id_DbName + "] FROM RoutingPlanDetail " +
					"WHERE [RoutingPlanDetail].[" + RoutingPlanDetailRow_Base.routing_plan_id_DbName + "] = " + pRoutingPlanId + ")";

			var _cmd = Database.CreateCommand(_sqlStr);
			using (var _reader = Database.ExecuteReader(_cmd)) {
				return MapRecords(_reader);
			}
		}

		public CountryRow[] Get(short pAccountId, int pCallingPlanId, ViewContext pContext) {
			/*
				SELECT DISTINCT [dbo].[Country].[country_id]
							,[dbo].[Country].[name]
							,[dbo].[Country].[country_code]
							,[dbo].[Country].[status]
							,[dbo].[Country].[version]
					FROM [dbo].[Country], [dbo].[Route]
					where [dbo].[Country].country_id = [dbo].[Route].country_id
					and   [dbo].[Route].calling_plan_id = 2
					and   [dbo].[Route].route_id in 
				( SELECT [WholesaleRoute].[route_id] FROM WholesaleRoute 
					WHERE [WholesaleRoute].[route_id] = [Route].[route_id] 
					and [WholesaleRoute].[service_id] = 10009
			  )
			 
		 */
			var _sqlStr = "SELECT DISTINCT " +
				"[Country].[" + CountryRow_Base.country_id_DbName + "], " +
				"[Country].[" + CountryRow_Base.name_DbName + "], " +
				"[Country].[" + CountryRow_Base.country_code_DbName + "], " +
				"[Country].[" + CountryRow_Base.status_DbName + "], " +
				"[Country].[" + CountryRow_Base.version_DbName + "]" +
			" FROM [Country], [Route] " +
				"WHERE " + "[Country].[" + CountryRow_Base.country_id_DbName + "] = " + "[Route].[" + RouteRow_Base.country_id_DbName + "]" +
				" and [Route].[" + RouteRow_Base.calling_plan_id_DbName + "] = " + pCallingPlanId +
				" and [Route].[" + RouteRow_Base.route_id_DbName + "]";
			
			if (pContext == ViewContext.Carrier) {
				_sqlStr += " in ( SELECT [CarrierRoute].[" + CarrierRouteRow_Base.route_id_DbName + "] FROM CarrierRoute " +
					"WHERE [CarrierRoute].[" + CarrierRouteRow_Base.route_id_DbName + "] = [Route].[" + RouteRow_Base.route_id_DbName + "]" +
					" and [CarrierRoute].[" + CarrierRouteRow_Base.carrier_acct_id_DbName + "] = " + pAccountId + ")";
			}
			else {
				_sqlStr += " in ( SELECT [WholesaleRoute].[" + WholesaleRouteRow_Base.route_id_DbName + "] FROM WholesaleRoute " + 
					"WHERE [WholesaleRoute].[" + WholesaleRouteRow_Base.route_id_DbName + "] = [Route].[" + RouteRow_Base.route_id_DbName + "]" + 
					" and [WholesaleRoute].[" + WholesaleRouteRow_Base.service_id_DbName + "] = " + pAccountId + ")";
			}

			var _cmd = Database.CreateCommand(_sqlStr);
			using (var _reader = Database.ExecuteReader(_cmd)) {
				return MapRecords(_reader);
			}
		}

		public CountryRow GetByName(string pName) {
			var _sqlStr = "SELECT * FROM Country " + 
				"WHERE [" + CountryRow_Base.name_DbName + "]=" + Database.CreateSqlParameterName(CountryRow_Base.name_PropName);

			var _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CountryRow_Base.name_PropName, pName.Trim());
			using(var _reader = Database.ExecuteReader(_cmd)) {
				var _tempArray = MapRecords(_reader);
				return 0 == _tempArray.Length ? null : _tempArray[0];
			}
		}

		public CountryRow[] GetIsCovered(int pCountryCode, int pExcludingCountryId) {
			//SELECT * FROM Country
			//WHERE '73' LIKE CAST([country_code] AS varchar)+'%'

      var _sqlStr = "SELECT * FROM Country " +
        "WHERE " +
        "[" + CountryRow_Base.country_id_DbName + "] <> " + Database.CreateSqlParameterName(CountryRow_Base.country_id_PropName) + " " +
        " AND " +
        Database.CreateSqlParameterName(CountryRow_Base.country_code_PropName) + " LIKE " +
        "CAST([" + CountryRow_Base.country_code_DbName + "] AS varchar) + '%' "; 

			var _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CountryRow_Base.country_code_PropName, pCountryCode);
			AddParameter(_cmd, CountryRow_Base.country_id_PropName, pExcludingCountryId);
			using (var _reader = Database.ExecuteReader(_cmd)) {
				return MapRecords(_reader);
			}
		}

		public CountryRow[] GetWillCover(int pCountryCode, int pExcludingCountryId) {
			//SELECT * FROM Country			
			//WHERE [country_code] LIKE '73%'

      var _sqlStr = "SELECT * FROM Country " +
        "WHERE " +
        "[" + CountryRow_Base.country_id_DbName + "] <> " + Database.CreateSqlParameterName(CountryRow_Base.country_id_PropName) + " " +
        " AND " +
        "[" + CountryRow_Base.country_code_DbName + "] LIKE " +
        "CAST(" + Database.CreateSqlParameterName(CountryRow_Base.country_code_PropName) + " AS varchar) + '%' ";

			var _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CountryRow_Base.country_code_PropName, pCountryCode);
			AddParameter(_cmd, CountryRow_Base.country_id_PropName, pExcludingCountryId);
			using (var _reader = Database.ExecuteReader(_cmd)) {
				return MapRecords(_reader);
			}
		}

		//NOTE: !!! Country Code has to be > 1, in db we store ALL 1+ Countries with country_code=1
		public CountryRow GetByCountryCode(int pCountryCode) {
			if (pCountryCode == 1) {
				throw new Exception("Invalid usage, Country Code has to be > 1.");
			}

			var _sqlStr = "SELECT * FROM Country " +
				"WHERE [" + CountryRow_Base.country_code_DbName + "]=" + Database.CreateSqlParameterName(CountryRow_Base.country_code_PropName);

			var _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, CountryRow_Base.country_code_PropName, pCountryCode);
			using (var _reader = Database.ExecuteReader(_cmd)) {
				var _tempArray = MapRecords(_reader);
				return 0 == _tempArray.Length ? null : _tempArray[0];
			}
		}
		
		//public CountryRow GetByCountryId(int pCountryId) {
		//  var _sqlStr = "SELECT * FROM Country " +
		//    "WHERE [" + CountryRow_Base.country_id_DbName + "]=" + Database.CreateSqlParameterName(CountryRow_Base.country_id_PropName);

		//  var _cmd = Database.CreateCommand(_sqlStr);
		//  AddParameter(_cmd, CountryRow_Base.country_id_PropName, pCountryId);
		//  using (var _reader = Database.ExecuteReader(_cmd)) {
		//    var _tempArray = MapRecords(_reader);
		//    return 0 == _tempArray.Length ? null : _tempArray[0];
		//  }
		//}

		public CountryRow GetByDialedNumber(string pDialedNumber) {
			if (pDialedNumber[0] == '1') {
				return getByDialedNumber1plus(pDialedNumber);
			}
			return getByDialedNumber(pDialedNumber);
		}

		private CountryRow getByDialedNumber1plus(string pDialedNumber) {
			/*
			SELECT * from Country WHERE country_id in 
			(
				SELECT country_id from Route WHERE route_id in 
				(
					SELECT TOP 1 route_id FROM  DialCode
					WHERE @DialedNumber LIKE CAST(dial_code AS varchar) + '%'
					ORDER BY CAST(dial_code AS varchar) DESC 
				)
			)
			*/
			var _sql = "SELECT * from Country WHERE " + 
				CountryRow_Base.country_id_DbName + 
				" in (SELECT " + 
				CountryRow_Base.country_id_DbName +
				" from Route WHERE " + 
				RouteRow_Base.route_id_DbName +
				" in (SELECT TOP 1 " + 
				DialCodeRow_Base.route_id_DbName +
				" FROM  DialCode	WHERE " +
				Database.CreateSqlParameterName("DialedNumber") +
				" LIKE CAST(" +
				DialCodeRow_Base.dial_code_DbName +
				" AS varchar) + '%'	ORDER BY CAST(" +
				DialCodeRow_Base.dial_code_DbName +
				" AS varchar) DESC)	)";

			var _cmd = Database.CreateCommand(_sql);
			Database.AddParameter(_cmd, "DialedNumber", DbType.AnsiString, pDialedNumber);
			using(var _reader = Database.ExecuteReader(_cmd)) {
				var _tempArray = MapRecords(_reader);
				return 0 == _tempArray.Length ? null : _tempArray[0];
			}
		}

		//NOTE: !!! first digit of pDialedNumber has to be != 1
		private CountryRow getByDialedNumber(string pDialedNumber) {
			if (pDialedNumber == null || pDialedNumber.StartsWith("1")) {
				throw new Exception(string.Format("CountryCollection.getByDialedNumber-1: Invalid Dialed Number, DialedNumber={0}", (pDialedNumber ?? "null")));
			}
			if ( ! Timok.Core.Utils.IsNumeric(pDialedNumber) ) {
				throw new Exception(string.Format("CountryCollection.getByDialedNumber-2: Invalid Dialed Number, DialedNumber={0}", (pDialedNumber ?? "null")));
			}

			/*
			SELECT *
			FROM  Country 
			WHERE (@DialedNumber LIKE CAST(country_code AS varchar) + '%')
			*/
			var _where = 
				"(" + Database.CreateSqlParameterName("DialedNumber") + 
				" LIKE CAST([" + CountryRow_Base.country_code_DbName + "] AS varchar) + '%') ";

			//-- ExecuteReader
			var _cmd = base.CreateGetCommand(_where, null);
			Database.AddParameter(_cmd, "DialedNumber", DbType.AnsiString, pDialedNumber);
			using(var _reader = Database.ExecuteReader(_cmd)) {
				var _tempArray = MapRecords(_reader);
				return 0 == _tempArray.Length ? null : _tempArray[0];
			}
		}

		public CountryRow[] GetUnused(int pCallingPlanId) {
			/*
			WHERE country_id NOT IN 
			(
				SELECT country_id FROM Route WHERE calling_plan_id = 1
			)    
			*/
			var _where = CountryRow_Base.country_id_DbName + " NOT IN (" +
				" SELECT " + RouteRow_Base.country_id_DbName + " FROM Route WHERE " +
				RouteRow_Base.calling_plan_id_DbName + "=" + Database.CreateSqlParameterName(RouteRow_Base.calling_plan_id_PropName) +
				" ) ";

			var _cmd = base.CreateGetCommand(_where, CountryRow_Base.name_DbName);
			Database.AddParameter(_cmd, RouteRow_Base.calling_plan_id_PropName, DbType.Int32, pCallingPlanId);
			using (var _reader = Database.ExecuteReader(_cmd)) {
				return MapRecords(_reader);
			}
		}
	} // End of CountryCollection class
} // End of namespace
