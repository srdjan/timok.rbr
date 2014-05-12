// <fileinfo name="EndPointCollection.cs">
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
using Timok.NetworkLib;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>EndPoint</c> table.
	/// </summary>
	public class EndPointCollection : EndPointCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="EndPointCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal EndPointCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static EndPointRow Parse(System.Data.DataRow row){
			return new EndPointCollection(null).MapRow(row);
		}

		#region overridden publics
		public override void Insert(EndPointRow value) {
			validateByIPAddressRange(value);
			validateByAlias(value);

			//SET @end_point_id = COALESCE((SELECT MAX(end_point_id) FROM EndPoint) + 1, 4000)
			string _id_param = base.Database.CreateSqlParameterName(EndPointRow.end_point_id_PropName);
			string _sqlStr = "DECLARE " + _id_param + " smallint " + 
				"SET " + _id_param + " = COALESCE((SELECT MAX(" + EndPointRow.end_point_id_DbName + ") FROM EndPoint) + 1, 7000) " + 

				"INSERT INTO [dbo].[EndPoint] (" +
				"[" + EndPointRow.end_point_id_DbName + "], " +
				"[" + EndPointRow.alias_DbName + "], " +
				"[" + EndPointRow.with_alias_authentication_DbName + "], " +
				"[" + EndPointRow.status_DbName + "], " +
				"[" + EndPointRow.type_DbName + "], " +
				"[" + EndPointRow.protocol_DbName + "], " +
				"[" + EndPointRow.port_DbName + "], " +
				"[" + EndPointRow.registration_DbName + "], " +
				"[" + EndPointRow.is_registered_DbName + "], " +
				"[" + EndPointRow.ip_address_range_DbName + "], " +
				"[" + EndPointRow.max_calls_DbName + "], " +
				"[" + EndPointRow.password_DbName + "], " +
				"[" + EndPointRow.prefix_in_type_id_DbName + "], " +
				"[" + EndPointRow.virtual_switch_id_DbName + "] " +
				") VALUES (" +
				base.Database.CreateSqlParameterName(EndPointRow.end_point_id_PropName) + ", " +
				base.Database.CreateSqlParameterName(EndPointRow.alias_PropName) + ", " +
				base.Database.CreateSqlParameterName(EndPointRow.with_alias_authentication_PropName) + ", " +
				base.Database.CreateSqlParameterName(EndPointRow.status_PropName) + ", " + 
				base.Database.CreateSqlParameterName(EndPointRow.type_PropName) + ", " +
				base.Database.CreateSqlParameterName(EndPointRow.protocol_PropName) + ", " +
				base.Database.CreateSqlParameterName(EndPointRow.port_PropName) + ", " +
				base.Database.CreateSqlParameterName(EndPointRow.registration_PropName) + ", " +
				base.Database.CreateSqlParameterName(EndPointRow.is_registered_PropName) + ", " +
				base.Database.CreateSqlParameterName(EndPointRow.ip_address_range_PropName) + ", " +
				base.Database.CreateSqlParameterName(EndPointRow.max_calls_PropName) + ", " +
				base.Database.CreateSqlParameterName(EndPointRow.password_PropName) + ", " +
				base.Database.CreateSqlParameterName(EndPointRow.prefix_in_type_id_PropName) + ", " +
        base.Database.CreateSqlParameterName(EndPointRow.virtual_switch_id_PropName) + ") " +
        "SELECT " + base.Database.CreateSqlParameterName(EndPointRow.end_point_id_PropName);
			
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			//_db.AddParameter(cmd, end_point_id_PropName, value.end_point_id);
			AddParameter(_cmd, EndPointRow.alias_PropName, value.Alias);
			AddParameter(_cmd, EndPointRow.with_alias_authentication_PropName, value.With_alias_authentication);
			AddParameter(_cmd, EndPointRow.status_PropName, value.Status);
			AddParameter(_cmd, EndPointRow.type_PropName, value.Type);
			AddParameter(_cmd, EndPointRow.protocol_PropName, value.Protocol);
			AddParameter(_cmd, EndPointRow.port_PropName, value.Port);
			AddParameter(_cmd, EndPointRow.registration_PropName, value.Registration);
			AddParameter(_cmd, EndPointRow.is_registered_PropName, 0);
			AddParameter(_cmd, EndPointRow.ip_address_range_PropName, value.Ip_address_range);
			AddParameter(_cmd, EndPointRow.max_calls_PropName, value.Max_calls);
			AddParameter(_cmd, EndPointRow.password_PropName, value.Password);
			AddParameter(_cmd, EndPointRow.prefix_in_type_id_PropName, value.Prefix_in_type_id);
			AddParameter(_cmd, EndPointRow.virtual_switch_id_PropName, value.Virtual_switch_id);

			object _res = _cmd.ExecuteScalar();

			value.End_point_id = (short) _res;
		}
	
		public override bool Update(EndPointRow value) {
			validateByIPAddressRange(value);
			validateByAlias(value);
			return base.Update (value);
		}

		#endregion overridden publics

		#region publics

		//get Unassigned
		public EndPointRow[] GetUnassignedByEndPointProtocol(bool pExcludeMiltiIPAddressEndpoints, EndPointProtocol[] pEndPointProtocols, Status[] pStatuses) {
			#region sql
			/*
			SELECT 
			EndPoint.*

			FROM  EndPoint 
			LEFT OUTER JOIN CarrierAcctEPMap ON 
			EndPoint.end_point_id = CarrierAcctEPMap.end_point_id 
			LEFT OUTER JOIN DialPeer ON 
			EndPoint.end_point_id = DialPeer.end_point_id

			WHERE 
			CHARINDEX('-', EndPoint.ip_address_range) = 0
			AND
			-- exclude Frontend EPs
			EndPoint.prefix_in_type_id <> -1
			AND 
			(DialPeer.end_point_id IS NULL) 
			AND 
			(CarrierAcctEPMap.end_point_id IS NULL)
			 */
			#endregion sql
			
			string _epProtocolFilter = Database.CreateEnumFilter("EndPoint", 
				EndPointRow.protocol_DbName, pEndPointProtocols, typeof(EndPointProtocol));
			string _epStatusFilter = Database.CreateEnumFilter("EndPoint", 
				EndPointRow.status_DbName, pStatuses, typeof(Status));

			string _sqlStr = "SELECT EndPoint.* " + 
				"FROM  EndPoint  " + 
				"LEFT OUTER JOIN CarrierAcctEPMap ON  " + 
				"EndPoint.end_point_id = CarrierAcctEPMap.end_point_id  " + 
				"LEFT OUTER JOIN DialPeer ON  " + 
				"EndPoint.end_point_id = DialPeer.end_point_id " + 
				
				"WHERE "; 
			if (pExcludeMiltiIPAddressEndpoints) {
				//NOTE: get Endpoints with single IP ONLY 
				_sqlStr += "CHARINDEX('-', EndPoint.ip_address_range) = 0 " + 
					" AND ";
			}
      //_sqlStr += " EndPoint." + EndPointRow.prefix_in_type_id_DbName + "<>" + Configuration.Main.PrefixTypeId_Frontend + " " + 
      //  " AND "; 

			_sqlStr += "(DialPeer.end_point_id IS NULL) AND (CarrierAcctEPMap.end_point_id IS NULL) " + 
				" AND (" + _epProtocolFilter + ") " + 
				" AND (" + _epStatusFilter + ") ";
			
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		#region term eps
		public EndPointRow[] GetByCarrierAcctId(short pCarrierAcctId, Status[] pStatuses){
			#region sql
      /*
      SELECT * FROM EndPoint 
      WHERE end_point_id IN ( 
         SELECT end_point_id FROM CarrierAcctEPMap 
         WHERE carrier_acct_id=10000
      )
      AND 
      ( EndPoint.status IN(1,0) )
			*/
      #endregion sql

      string _epStatusFilter = Database.CreateEnumFilter("EndPoint", 
				EndPointRow.status_DbName, pStatuses, typeof(Status));

			string _sqlStr = "SELECT * FROM EndPoint " +
        " WHERE " + EndPointRow.end_point_id_DbName + " IN ( " +
        "   SELECT " + CarrierAcctEPMapRow.end_point_id_DbName + " FROM CarrierAcctEPMap " + 
				"   WHERE " + CarrierAcctEPMapRow.carrier_acct_id_DbName + "=" + base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_acct_id_PropName) + 
        ")" + 
				" AND (" + _epStatusFilter + ") ";

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, CarrierAcctEPMapRow.carrier_acct_id_PropName, DbType.Int16, pCarrierAcctId);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public EndPointRow[] GetByCarrierAcctIdCarrierRouteId(short pCarrierAcctId, int pCarrierRouteId, Status[] pStatuses){
			#region sql
			/*
			SELECT EndPoint.* 
			FROM  EndPoint INNER JOIN CarrierAcctEPMap ON 
			EndPoint.end_point_id = CarrierAcctEPMap.end_point_id
			WHERE 
			(CarrierAcctEPMap.carrier_acct_id = 2000) 
			AND 
			(CarrierAcctEPMap.carrier_route_id = 111)
			AND 
			(EndPoint.status = 1)
			*/
			#endregion sql
						
			string _epStatusFilter = Database.CreateEnumFilter("EndPoint", 
				EndPointRow.status_DbName, pStatuses, typeof(Status));

			string _sqlStr = "SELECT EndPoint.* " + 
				"FROM  EndPoint INNER JOIN CarrierAcctEPMap ON " + 
				"EndPoint.end_point_id = CarrierAcctEPMap.end_point_id " + 
				" WHERE " + 
				"CarrierAcctEPMap." + CarrierAcctEPMapRow.carrier_acct_id_DbName + "=" + base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_acct_id_PropName) + 
				" AND " + 
				"CarrierAcctEPMap." + CarrierAcctEPMapRow.carrier_route_id_DbName + "=" + base.Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_route_id_PropName) + 
				" AND (" + _epStatusFilter + ") ";

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, CarrierAcctEPMapRow.carrier_acct_id_PropName, DbType.Int16, pCarrierAcctId);
			Database.AddParameter(_cmd, CarrierAcctEPMapRow.carrier_route_id_PropName, DbType.Int32, pCarrierRouteId);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

    public EndPointRow[] GetByCarrierAcctIdCountryId(short pCarrierAcctId, int pCountryId, Status[] pStatuses) {
      #region sql
      /*
      SELECT * FROM EndPoint 
      WHERE end_point_id IN (
        SELECT end_point_id FROM CarrierAcctEPMap
        WHERE carrier_acct_id = 77000 
        AND carrier_route_id IN (
          SELECT carrier_route_id FROM CarrierRoute 
          WHERE route_id IN (
            SELECT route_id FROM Route 
            WHERE country_id=202
          )
        )
      )
      NOTE: if performance is bad, try to filter by calling plan id as well
      */
      #endregion sql

      string _epStatusFilter = Database.CreateEnumFilter("EndPoint",
        EndPointRow.status_DbName, pStatuses, typeof(Status));

      string _where =
        EndPointRow.end_point_id_DbName + " IN (" +
        "   SELECT " + CarrierAcctEPMapRow.end_point_id_DbName + " FROM CarrierAcctEPMap " +
        "   WHERE " + CarrierAcctEPMapRow.carrier_acct_id_DbName + "=" + pCarrierAcctId +
        "   AND " + CarrierAcctEPMapRow.carrier_route_id_DbName + " IN (" +
        "       SELECT " + CarrierRouteRow.carrier_route_id_DbName + " FROM CarrierRoute " +
        "       WHERE " + CarrierRouteRow.route_id_DbName + " IN (" +
        "             SELECT " + RouteRow.route_id_DbName + " FROM Route " +
        "             WHERE " + RouteRow.country_id_DbName + "=" + pCountryId +
        "       ) " +
        "   ) " +
        ")" +
        " AND (" + _epStatusFilter + ") ";

      IDbCommand _cmd = CreateGetCommand(_where, EndPointRow.alias_DbName);
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }

		//get avalable by Route
    //NOTE: we were not allowing [IN and OUT] endpoints used by other Partners
    //      NOW we ALLOW that
		public EndPointRow[] GetByEndPointProtocolExcludingSelectedCarrierRoute(short pExcludeCarrierAcctId, int pExcludeCarrierRouteId, EndPointProtocol[] pEndPointProtocols, Status[] pStatuses) {
			#region sql
      /*
      SELECT EndPoint.* FROM EndPoint 
      WHERE  
      (
	      --exclude EPs which are used for selected Carrier, Route
	      EndPoint.end_point_id NOT IN     
	      (
		      SELECT DISTINCT CarrierAcctEPMap.end_point_id 
		      FROM  CarrierAcctEPMap
		      WHERE (
						      CarrierAcctEPMap.carrier_acct_id = 7000 
						      AND 
						      CarrierAcctEPMap.carrier_route_id = 1)
	      )    
      )
      AND 
      -- exclude Frontend EPs
      EndPoint.prefix_in_type_id <> -1 
      AND 
      --single IP only
      CHARINDEX('-', EndPoint.ip_address_range) = 0 
      AND 
      ( EndPoint.protocol IN(0,1) )  
      AND 
      ( EndPoint.status IN(1) ) 
			 */
      #endregion sql

      string _epProtocolFilter = Database.CreateEnumFilter("EndPoint", 
				EndPointRow.protocol_DbName, pEndPointProtocols, typeof(EndPointProtocol));
			string _epStatusFilter = Database.CreateEnumFilter("EndPoint", 
				EndPointRow.status_DbName, pStatuses, typeof(Status));

			string _sqlStr = "SELECT EndPoint.* FROM EndPoint " + 
				"WHERE " + 
				"( " + 
				//"--exclude EPs which are used for selected Carrier, Route " + 
				" EndPoint.end_point_id NOT IN " + 
				"	( " + 
				"		SELECT DISTINCT CarrierAcctEPMap.end_point_id " + 
				"  	FROM  CarrierAcctEPMap " + 
				"  	WHERE ( " + 
				"						CarrierAcctEPMap.carrier_acct_id = " + Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_acct_id_PropName) + " " + 
				"						AND " + 
				"						CarrierAcctEPMap.carrier_route_id = " + Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_route_id_PropName) + ") " + 
				"	) " + 
				") " + 
				" AND " + 
        //" EndPoint." + EndPointRow.prefix_in_type_id_DbName + "<>" + Configuration.Main.PrefixTypeId_Frontend + " " + 
        //"AND " + 
				//NOTE: get Endpoints with single IP ONLY 
				"CHARINDEX('-', EndPoint.ip_address_range) = 0 " + 
				" AND (" + _epProtocolFilter + ") " + 
				" AND (" + _epStatusFilter + ") ";
			
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, CarrierAcctEPMapRow.carrier_acct_id_PropName, DbType.Int16, pExcludeCarrierAcctId);
			Database.AddParameter(_cmd, CarrierAcctEPMapRow.carrier_route_id_PropName, DbType.Int32, pExcludeCarrierRouteId);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

    //get avalable by Country
    //NOTE: we were not allowing [IN and OUT] endpoints used by other Partners
    //      NOW we ALLOW that
    public EndPointRow[] GetByEndPointProtocolExcludingSelectedCarrierCountry(short pExcludeCarrierAcctId, int pExcludeCountryId, EndPointProtocol[] pEndPointProtocols, Status[] pStatuses) {
      #region sql
      /*
      SELECT EndPoint.* FROM EndPoint 
      WHERE  
      (
      --exclude EPs which are used for selected Carrier, Country
      EndPoint.end_point_id NOT IN     
      (
        SELECT DISTINCT CarrierAcctEPMap.end_point_id 
        FROM  CarrierAcctEPMap INNER JOIN CarrierRoute ON        
        CarrierAcctEPMap.carrier_route_id = CarrierRoute.carrier_route_id       
        WHERE (
            CarrierAcctEPMap.carrier_acct_id = 7000 
            AND 
            CarrierRoute.route_id IN (
            SELECT route_id FROM Route WHERE country_id = 1
            )
         )     
      )    
      )
      AND 
      -- exclude Frontend EPs
      EndPoint.prefix_in_type_id <> -1
      AND 
      --single IP only
      CHARINDEX('-', EndPoint.ip_address_range) = 0 
      AND 
      ( EndPoint.protocol IN(0,1) )  
      AND 
      ( EndPoint.status IN(1) ) 
       */
      #endregion sql

      string _epProtocolFilter = Database.CreateEnumFilter("EndPoint",
        EndPointRow.protocol_DbName, pEndPointProtocols, typeof(EndPointProtocol));
      string _epStatusFilter = Database.CreateEnumFilter("EndPoint",
        EndPointRow.status_DbName, pStatuses, typeof(Status));

      string _sqlStr = "SELECT EndPoint.* FROM EndPoint " +
        "WHERE " +
        "( " +
        //"	--exclude EPs which are used for selected Carrier, CarrRoute/CarrCountry " + 
        " EndPoint.end_point_id NOT IN " +
        "	( " +
        //"			--exclude EPs which are used for selected Carrier, Country " + 
        "		SELECT DISTINCT CarrierAcctEPMap.end_point_id " +
        "		FROM  CarrierAcctEPMap INNER JOIN CarrierRoute ON " +
        "		CarrierAcctEPMap.carrier_route_id = CarrierRoute.carrier_route_id " +
        "		WHERE ( " +
        "			CarrierAcctEPMap.carrier_acct_id = " + Database.CreateSqlParameterName(CarrierAcctEPMapRow.carrier_acct_id_PropName) + " " +
        "     AND " + CarrierRouteRow.route_id_DbName + " IN ( " +
        "			  SELECT route_id FROM Route WHERE " +
        "       Route.country_id = " + Database.CreateSqlParameterName(RouteRow.country_id_PropName) +
        "     ) " +
        "   ) " +
        "	) " +
        ") " +
        //" AND " + 
        //" EndPoint." + EndPointRow.prefix_in_type_id_DbName + "<>" + Configuration.Main.PrefixTypeId_Frontend + " " + 

        "AND " +
        //NOTE: get Endpoints with single IP ONLY 
        "CHARINDEX('-', EndPoint.ip_address_range) = 0 " +
        " AND (" + _epProtocolFilter + ") " +
        " AND (" + _epStatusFilter + ") ";

      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      Database.AddParameter(_cmd, CarrierAcctEPMapRow.carrier_acct_id_PropName, DbType.Int16, pExcludeCarrierAcctId);
      Database.AddParameter(_cmd, RouteRow.country_id_PropName, DbType.Int32, pExcludeCountryId);
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }
		#endregion term eps


		//get customer's access list
		public EndPointRow[] GetByCustomerAcctId(short pCustomerAcctId, Status[] pStatuses) {
			#region sql
      /*
      SELECT * FROM  EndPoint 
      WHERE end_point_id IN ( 
         SELECT end_point_id FROM DialPeer 
         WHERE customer_acct_id = 10000
      )  
      AND ( EndPoint.status IN(1,0) )
			 */
      #endregion sql

      string _epStatusFilter = Database.CreateEnumFilter("EndPoint", 
				EndPointRow.status_DbName, pStatuses, typeof(Status));

			string _sqlStr = "SELECT * FROM  EndPoint " + 
        " WHERE " + EndPointRow.end_point_id_DbName + " IN (" + 
        "    SELECT " + DialPeerRow.end_point_id_DbName + " FROM DialPeer " + 
				"    WHERE " + DialPeerRow.customer_acct_id_DbName + " = " + Database.CreateSqlParameterName(DialPeerRow.customer_acct_id_PropName) + 
        ") " + 
				" AND (" + _epStatusFilter + ") ";
			
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, DialPeerRow.customer_acct_id_PropName, DbType.Int16, pCustomerAcctId);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		//get available
    //NOTE: !!! we were not allowng [IN and OUT]endpoints to be used by different Partners
    //      NOW we ALLOW that.
    //TODO: !!! make sure that's correct...
    public EndPointRow[] GetAvailableByPartnerIdExcludeCustomerAcctId(short pExcludeCustomerAcctId, EndPointProtocol[] pEndPointProtocols, Status[] pStatuses) {
			#region sql
      /*
			-- GetAvailableEndPointRowsByPartnerIdExcludeCustomerAcctId
			-- get available
			-- get ALL EPs 
			-- excluding EPs used in DialPeer by this Customer
      DECLARE @ExclCustId int
			SET @ExclCustId = 1002
      SELECT EndPoint.* FROM EndPoint 
      WHERE  
      (
	      (EndPoint.end_point_id NOT IN    
		      (	
			      --excluding EPs used in DialPeer by this Customer
			      SELECT DISTINCT DialPeer.end_point_id    
			      FROM  DialPeer INNER JOIN CustomerAcct ON      
			      DialPeer.customer_acct_id = CustomerAcct.customer_acct_id     
			      WHERE (DialPeer.customer_acct_id = @ExclCustId) 
		      )
	      )
      ) 
      AND 
      (EndPoint.protocol IN(0,1))
      AND 
      (EndPoint.status IN(1,2))
			 */
      #endregion sql

      string _epProtocolFilter = Database.CreateEnumFilter("EndPoint", 
				EndPointRow.protocol_DbName, pEndPointProtocols, typeof(EndPointProtocol));
			string _epStatusFilter = Database.CreateEnumFilter("EndPoint", 
				EndPointRow.status_DbName, pStatuses, typeof(Status));

			string _sqlStr = "SELECT EndPoint.* FROM  EndPoint " + 
				"WHERE " + 
				"( " + 
				" EndPoint.end_point_id NOT IN " + 
				"	( " + 
				//"		--excluding EPs used in DialPeer by this Customer " + 
				"		SELECT DISTINCT DialPeer.end_point_id " + 
				"		FROM  DialPeer INNER JOIN CustomerAcct ON " + 
				"		DialPeer.customer_acct_id = CustomerAcct.customer_acct_id " + 
				"		WHERE (DialPeer.customer_acct_id = " + Database.CreateSqlParameterName(DialPeerRow.customer_acct_id_PropName) + ") " + 
				"	) " + 
				") " + 
        //" AND " + 
        //" EndPoint." + EndPointRow.prefix_in_type_id_DbName + "<>" + Configuration.Main.PrefixTypeId_Frontend + " " + 
				" AND (" + _epProtocolFilter + ") " + 
				" AND (" + _epStatusFilter + ") ";
			
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, DialPeerRow.customer_acct_id_PropName, DbType.Int16, pExcludeCustomerAcctId);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		//get available
		//NOTE: do not show eps from term that are used by diff partner
		public EndPointRow[] GetAvailableFrontendEndPointRowsExcludeCustomerAcctId(short pExcludeCustomerAcctId, Status[] pStatuses) {
			#region sql
			/*
			-- GetAvailableFrontendEndPointRowsExcludeCustomerAcctId
			-- get available

			-- get Frontend EPs (prefix_in_type_id = -1)
			-- excluding EPs used in DialPeer by this Customer
			SELECT EndPoint.* FROM EndPoint 
			WHERE  
			--get Frontend EPs
			(EndPoint.prefix_in_type_id = -1)
			AND 
			(EndPoint.end_point_id NOT IN    
				(	
					--excluding EPs used in DialPeer by this Customer
					SELECT DISTINCT DialPeer.end_point_id    
					FROM  DialPeer INNER JOIN CustomerAcct ON      
					DialPeer.customer_acct_id = CustomerAcct.customer_acct_id     
					WHERE (DialPeer.customer_acct_id = 1000) 
				)
			) 
			AND 
			(EndPoint.status IN(1,2))
			 */
			#endregion sql
			
			string _epStatusFilter = Database.CreateEnumFilter("EndPoint", EndPointRow.status_DbName, pStatuses, typeof(Status));

			string _sqlStr = "SELECT EndPoint.* FROM EndPoint " + 
				" WHERE " + 
				"/* get Frontend EPs */" + 
        //" (EndPoint." + EndPointRow.prefix_in_type_id_DbName + " = " + Configuration.Main.PrefixTypeId_Frontend + ") " + 
        //" AND  " + 
				" (EndPoint.end_point_id NOT IN " + 
				" 	(	 " + 
				"/* excluding EPs used in DialPeer by this Customer */" + 
				" 		SELECT DISTINCT DialPeer.end_point_id " + 
				" 		FROM  DialPeer INNER JOIN CustomerAcct ON " + 
				" 		DialPeer.customer_acct_id = CustomerAcct.customer_acct_id " + 
				" 		WHERE (DialPeer.customer_acct_id = " + Database.CreateSqlParameterName(DialPeerRow.customer_acct_id_PropName) + ") " + 
				" 	) " + 
				" )  " + 
				" AND (" + _epStatusFilter + ") ";
			
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, DialPeerRow.customer_acct_id_PropName, DbType.Int16, pExcludeCustomerAcctId);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public EndPointRow[] GetAllOrigEndPoints(EndPointProtocol[] pEndPointProtocols, EndPointType[] pEndPointTypes) {
			/*
			SELECT EndPoint.*
			FROM  EndPoint INNER JOIN DialPeer ON 
				EndPoint.end_point_id = DialPeer.end_point_id
			.....
			*/
			string _epProtocolFilter = Database.CreateEnumFilter("EndPoint", EndPointRow.protocol_DbName, pEndPointProtocols, typeof(EndPointProtocol));
			string _epTypeFilter = Database.CreateEnumFilter("EndPoint", EndPointRow.type_DbName, pEndPointTypes, typeof(EndPointType));

			string _sqlStr = "SELECT EndPoint.* FROM  EndPoint INNER JOIN DialPeer ON " + 
				"EndPoint." + EndPointRow.end_point_id_DbName + " = DialPeer." + DialPeerRow.end_point_id_DbName + " " + 
				" WHERE " + 
				" (" + _epProtocolFilter + ") " +  
				" AND (" + _epTypeFilter + ") " +  
				" ORDER BY " + EndPointRow.alias_DbName;

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public EndPointRow GetByAlias(string alias) {
			string _sqlStr = "SELECT TOP 1 * FROM [dbo].[EndPoint] WHERE " +
				"[" + EndPointRow.alias_DbName + "]=" + base.Database.CreateSqlParameterName(EndPointRow.alias_PropName);
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, EndPointRow.alias_PropName, alias);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				EndPointRow[] _tempArray = MapRecords(_reader);
				return (_tempArray.Length > 0) ? _tempArray[0] : null;
			}
		}

		public EndPointRow GetByIPAddress(string pDotIPAddress) {
			int _intIPAddress = IPUtil.ToInt32(pDotIPAddress);//convert to int
			return GetByIPAddress(_intIPAddress);
		}

		public EndPointRow GetByIPAddress(int pIPAddress) {
			/*
				SELECT EndPoint.*
				FROM  EndPoint INNER JOIN IPAddress ON 
				EndPoint.end_point_id = IPAddress.end_point_id
				WHERE (IPAddress.IP_address = 111)
			*/
			var _sqlStr = "SELECT * FROM [dbo].[EndPoint]  INNER JOIN IPAddress ON " + 
				"EndPoint." + EndPointRow.end_point_id_DbName + " = IPAddress." + IPAddressRow.end_point_id_DbName + " " + 
				" WHERE " +
				"IPAddress.[" + IPAddressRow.IP_address_DbName + "]=" + Database.CreateSqlParameterName(IPAddressRow.IP_address_PropName);
			
			var _cmd = Database.CreateCommand(_sqlStr);
			Database.AddParameter(_cmd, IPAddressRow.IP_address_PropName, DbType.Int32, pIPAddress);
			using (var _reader = _cmd.ExecuteReader()) {
				var _tempArray = MapRecords(_reader);
				return (_tempArray.Length > 0) ? _tempArray[0] : null;
			}
		}

		public EndPointRow[] GetActiveOriginationsByType_Registration(EndPointType pEndPointType, EPRegistration pRegistration) {
			/*
			SELECT EndPoint.*
			FROM  EndPoint INNER JOIN DialPeer ON 
			EndPoint.end_point_id = DialPeer.end_point_id
			.....
			*/
			string _sqlStr = "SELECT " + 
				"EndPoint." + EndPointRow.end_point_id_DbName + ", " + 
				"EndPoint." + EndPointRow.alias_DbName + ", " + 
				"EndPoint." + EndPointRow.with_alias_authentication_DbName + ", " + 
				"EndPoint." + EndPointRow.status_DbName + ", " + 
				"EndPoint." + EndPointRow.type_DbName + ", " + 
				"EndPoint." + EndPointRow.protocol_DbName + ", " + 
				"EndPoint." + EndPointRow.port_DbName + ", " + 
				"EndPoint." + EndPointRow.registration_DbName + ", " + 
				"EndPoint." + EndPointRow.is_registered_DbName + ", " + 
				"EndPoint." + EndPointRow.ip_address_range_DbName + ", " + 
				"EndPoint." + EndPointRow.max_calls_DbName + ", " + 
				"EndPoint." + EndPointRow.password_DbName + ", " + 
				"EndPoint." + EndPointRow.prefix_in_type_id_DbName + ", " +
        "EndPoint." + EndPointRow.virtual_switch_id_DbName + " " + 

				" FROM  EndPoint INNER JOIN DialPeer ON " + 
				"EndPoint." + EndPointRow.end_point_id_DbName + " = DialPeer." + DialPeerRow.end_point_id_DbName + " " + 

				" WHERE [" + EndPointRow.type_DbName + "]=" + base.Database.CreateSqlParameterName(EndPointRow.type_PropName) + 
				" AND " + 
				"[" + EndPointRow.registration_DbName + "]=" + base.Database.CreateSqlParameterName(EndPointRow.registration_PropName) + 
				" AND " + 
				"[" + EndPointRow.status_DbName + "]=" + base.Database.CreateSqlParameterName(EndPointRow.status_PropName) + 
				
				" GROUP BY " +
				"EndPoint." + EndPointRow.end_point_id_DbName + ", " + 
				"EndPoint." + EndPointRow.alias_DbName + ", " + 
				"EndPoint." + EndPointRow.with_alias_authentication_DbName + ", " + 
				"EndPoint." + EndPointRow.status_DbName + ", " + 
				"EndPoint." + EndPointRow.type_DbName + ", " + 
				"EndPoint." + EndPointRow.protocol_DbName + ", " + 
				"EndPoint." + EndPointRow.port_DbName + ", " + 
				"EndPoint." + EndPointRow.registration_DbName + ", " + 
				"EndPoint." + EndPointRow.is_registered_DbName + ", " + 
				"EndPoint." + EndPointRow.ip_address_range_DbName + ", " + 
				"EndPoint." + EndPointRow.max_calls_DbName + ", " + 
				"EndPoint." + EndPointRow.password_DbName + ", " + 
				"EndPoint." + EndPointRow.prefix_in_type_id_DbName + ", " +
        "EndPoint." + EndPointRow.virtual_switch_id_DbName + " ";

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, EndPointRow.type_PropName, pEndPointType);
			AddParameter(_cmd, EndPointRow.registration_PropName, pRegistration);
			AddParameter(_cmd, EndPointRow.status_PropName, (byte) Status.Active);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public EndPointRow[] GetActiveTerminationsByType_Registration(EndPointType pEndPointType, EPRegistration pRegistration) {
			/*
			SELECT EndPoint.*
			FROM  EndPoint INNER JOIN	CarrierAcctEPMap ON 
			EndPoint.end_point_id = CarrierAcctEPMap.end_point_id
			.......
			*/
			string _sqlStr = "SELECT " +
        "EndPoint." + EndPointRow.end_point_id_DbName + ", " + 
				"EndPoint." + EndPointRow.alias_DbName + ", " + 
				"EndPoint." + EndPointRow.with_alias_authentication_DbName + ", " + 
				"EndPoint." + EndPointRow.status_DbName + ", " + 
				"EndPoint." + EndPointRow.type_DbName + ", " + 
				"EndPoint." + EndPointRow.protocol_DbName + ", " + 
				"EndPoint." + EndPointRow.port_DbName + ", " + 
				"EndPoint." + EndPointRow.registration_DbName + ", " + 
				"EndPoint." + EndPointRow.is_registered_DbName + ", " + 
				"EndPoint." + EndPointRow.ip_address_range_DbName + ", " + 
				"EndPoint." + EndPointRow.max_calls_DbName + ", " + 
				"EndPoint." + EndPointRow.password_DbName + ", " + 
				"EndPoint." + EndPointRow.prefix_in_type_id_DbName + ", " + 
        "EndPoint." + EndPointRow.virtual_switch_id_DbName + " " + 

				" FROM  EndPoint RIGHT OUTER JOIN CarrierAcctEPMap ON " + 
				"EndPoint." + EndPointRow.end_point_id_DbName + " = CarrierAcctEPMap." + CarrierAcctEPMapRow.end_point_id_DbName + " " + 
				" WHERE [" + EndPointRow.type_DbName + "]=" + base.Database.CreateSqlParameterName(EndPointRow.type_PropName) + 
				" AND " + 
				"[" + EndPointRow.registration_DbName + "]=" + base.Database.CreateSqlParameterName(EndPointRow.registration_PropName) + 
				" AND " + 
				"[" + EndPointRow.status_DbName + "]=" + base.Database.CreateSqlParameterName(EndPointRow.status_PropName) + 
							
				" GROUP BY " +
				"EndPoint." + EndPointRow.end_point_id_DbName + ", " + 
				"EndPoint." + EndPointRow.alias_DbName + ", " + 
				"EndPoint." + EndPointRow.with_alias_authentication_DbName + ", " + 
				"EndPoint." + EndPointRow.status_DbName + ", " + 
				"EndPoint." + EndPointRow.type_DbName + ", " + 
				"EndPoint." + EndPointRow.protocol_DbName + ", " + 
				"EndPoint." + EndPointRow.port_DbName + ", " + 
				"EndPoint." + EndPointRow.registration_DbName + ", " + 
				"EndPoint." + EndPointRow.is_registered_DbName + ", " + 
				"EndPoint." + EndPointRow.ip_address_range_DbName + ", " + 
				"EndPoint." + EndPointRow.max_calls_DbName + ", " + 
				"EndPoint." + EndPointRow.password_DbName + ", " + 
				"EndPoint." + EndPointRow.prefix_in_type_id_DbName + ", " +
        "EndPoint." + EndPointRow.virtual_switch_id_DbName + " ";

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, EndPointRow.type_PropName, pEndPointType);
			AddParameter(_cmd, EndPointRow.registration_PropName, pRegistration);
			AddParameter(_cmd, EndPointRow.status_PropName, (byte) Status.Active);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

    public EndPointRow[] GetByStatus(Status[] pStatuses, bool pExcludeWithIPRanges) {
      /*
      SELECT EndPoint.*
      FROM  EndPoint 
      .....
      */
      string _epStatusFilter = Database.CreateEnumFilter("EndPoint",
        EndPointRow.status_DbName, pStatuses, typeof(Status));

      string _sqlStr = "SELECT * FROM  EndPoint " +
        " WHERE (" + _epStatusFilter + ") ";
      if (pExcludeWithIPRanges) {
        _sqlStr += " AND CHARINDEX('-', " + EndPointRow.ip_address_range_DbName + ") = 0 ";
      }


      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      using (IDataReader _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }

		public bool Register(short pEndPointID) {
			string _sqlStr = "UPDATE [dbo].[EndPoint] " + 
				"SET [" + EndPointRow.is_registered_DbName + "] = 1 " + 
				"WHERE [" + EndPointRow.end_point_id_DbName + "] = " + 
				base.Database.CreateSqlParameterName(EndPointRow.end_point_id_PropName) + " " + 
				"SELECT @@ROWCOUNT ";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, EndPointRow.end_point_id_PropName, pEndPointID);
			int _count = (int) _cmd.ExecuteScalar();
			return _count == 1;
		}

		public bool Unregister(short pEndPointID) {
			string _sqlStr = "UPDATE [dbo].[EndPoint] " + 
				"SET [" + EndPointRow.is_registered_DbName + "] = 0 " + 
				"WHERE [" + EndPointRow.end_point_id_DbName + "] = " + 
				base.Database.CreateSqlParameterName(EndPointRow.end_point_id_PropName) + " " + 
				"SELECT @@ROWCOUNT ";
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, EndPointRow.end_point_id_PropName, pEndPointID);
			int _count = (int) _cmd.ExecuteScalar();
			return _count == 1;
		}

		#endregion publics

		#region overridden protected
		protected override IDbCommand CreateGetAllCommand() {
			return CreateGetCommand(null, EndPointRow.alias_DbName);
		}
		#endregion overridden protected

		#region privates
		
		private void validateByIPAddressRange(EndPointRow pEndPointRow) {
			string _ipAddressRange = string.Empty;
			foreach (int _intIP in pEndPointRow.IPAddressRange.IPAddressInt32List) {
				_ipAddressRange += _intIP.ToString() + ",";
			}
			_ipAddressRange = _ipAddressRange.TrimEnd(',');

			string _sqlStr = "SELECT [" + IPAddressRow.end_point_id_DbName + "] FROM [dbo].[IPAddress] WHERE " +
				"[" + IPAddressRow.IP_address_DbName + "] IN(" + _ipAddressRange + ") " + 
				" AND " + 
				"[" + IPAddressRow.end_point_id_DbName + "]<>" + base.Database.CreateSqlParameterName(IPAddressRow.end_point_id_PropName);
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, IPAddressRow.end_point_id_PropName, pEndPointRow.End_point_id);

			object _existingEPID = _cmd.ExecuteScalar();
			short _endPointID = 0;
			if (_existingEPID != null) {
				_endPointID = (short) _existingEPID;
			}

			if (_endPointID > 0) {
				//get Alias;
				EndPointRow _existingEndPoint = GetByPrimaryKey(_endPointID);
				throw new Exception("IP Address in the provided IP Range already in use by other End Point ID=[" + _existingEndPoint.End_point_id + "] Alias=[" + _existingEndPoint.Alias + "] IP Range[" + _existingEndPoint.Ip_address_range + "] .");
			}
		}

		private void validateByAlias(EndPointRow pEndPointRow) {
			if (exists(pEndPointRow.Alias, pEndPointRow.End_point_id)){
				throw new ApplicationException(
					"Alias [" + pEndPointRow.Alias + "] already in use by other End Point ID=[" + pEndPointRow.End_point_id + "] IP Range[" + pEndPointRow.Ip_address_range + "] .");
			}
		}

		private bool exists(string alias, int pExcludeEndPointID) {
			string _sqlStr = "SELECT COUNT(*) FROM [dbo].[EndPoint] WHERE " +
				"LOWER ([" + EndPointRow.alias_DbName + "])=" + base.Database.CreateSqlParameterName(EndPointRow.alias_PropName) + 
				" AND " + 
				"[" + EndPointRow.end_point_id_DbName + "]<>" + base.Database.CreateSqlParameterName(EndPointRow.end_point_id_PropName);
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, EndPointRow.alias_PropName, alias.ToLower());
			AddParameter(_cmd, EndPointRow.end_point_id_PropName, pExcludeEndPointID);
			object _count = _cmd.ExecuteScalar();
			return (_count == null || (int)_count == 0) ? false : true;
		}

		#endregion privates
	} // End of EndPointCollection class
} // End of namespace
