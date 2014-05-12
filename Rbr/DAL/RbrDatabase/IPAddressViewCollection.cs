// <fileinfo name="IPAddressViewCollection.cs">
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
using System.Collections;
using System.Data;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>IPAddressView</c> view.
	/// </summary>
	public class IPAddressViewCollection : IPAddressViewCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="IPAddressViewCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal IPAddressViewCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static IPAddressViewRow Parse(System.Data.DataRow row){
			return new IPAddressViewCollection(null).MapRow(row);
		}

		public IPAddressViewRow GetByIPAddress(int pIPAddress) {
			string _sqlStr = "SELECT * FROM [dbo].[IPAddressView] WHERE " +
				IPAddressViewRow.IP_address_DbName + "=" + base.Database.CreateSqlParameterName(IPAddressViewRow.IP_address_PropName);
			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, IPAddressViewRow.IP_address_PropName, pIPAddress);
			using(IDataReader _reader = _cmd.ExecuteReader()) {
				IPAddressViewRow[] _tempArray = MapRecords(_reader);
				return 0 == _tempArray.Length ? null : _tempArray[0];
			}
		}

		public IPAddressViewRow[] GetByEndPointID(short pEndPointID) {
			string _sqlStr = "SELECT * FROM [dbo].[IPAddressView] WHERE " +
				IPAddressViewRow.end_point_id_DbName + "=" + base.Database.CreateSqlParameterName(IPAddressViewRow.end_point_id_PropName) + 
				" ORDER BY " + IPAddressViewRow.dot_IP_address_DbName;

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, IPAddressViewRow.end_point_id_PropName, pEndPointID);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		public IPAddressViewRow[] GetAll(params Status[] pStatuses) {
			string _where = base.Database.CreateStatusFilter(pStatuses);
			IDbCommand cmd = CreateGetCommand(_where, IPAddressViewRow.dot_IP_address_DbName);
			using (IDataReader reader = cmd.ExecuteReader()) {
				return MapRecords(reader);
			}
		} 

		//used by GkINIFileHelper
		public IPAddressViewRow[] GetAllOriginationIPAddressViews(Status[] pStatuses) {
			/*
			SELECT 
			IPAddressView.IP_address, 
			IPAddressView.dot_IP_address, 
			IPAddressView.end_point_id, 
			IPAddressView.alias, 
			IPAddressView.with_alias_authentication, 
			IPAddressView.status, 
			IPAddressView.type, 
			IPAddressView.protocol, 
			IPAddressView.port, 
			IPAddressView.registration, 
			IPAddressView.is_registered, 
			IPAddressView.IP_address_range, 
			IPAddressView.max_calls, 
			IPAddressView.password, 
			IPAddressView.prefix_in_type_id, 
			IPAddressView.prefix_type_descr, 
			IPAddressView.prefix_length, 
			IPAddressView.prefix_delimiter

			FROM  IPAddressView INNER JOIN DialPeer ON 
			IPAddressView.end_point_id = DialPeer.end_point_id

			GROUP BY 
			IPAddressView.IP_address, 
			IPAddressView.dot_IP_address, 
			IPAddressView.end_point_id, 
			IPAddressView.alias, 
			IPAddressView.with_alias_authentication, 
			IPAddressView.status, 
			IPAddressView.type, 
			IPAddressView.protocol, 
			IPAddressView.port, 
			IPAddressView.registration, 
			IPAddressView.is_registered, 
			IPAddressView.IP_address_range, 
			IPAddressView.max_calls, 
			IPAddressView.password, 
			IPAddressView.prefix_in_type_id, 
			IPAddressView.prefix_type_descr, 
			IPAddressView.prefix_length, 
			IPAddressView.prefix_delimiter

			ORDER BY IPAddressView.dot_IP_address
			*/
			string _epStatusFilter = base.Database.CreateStatusFilter(pStatuses);
			string _sqlStr = 
							"SELECT " + 
			"IPAddressView.IP_address,  " + 
			"IPAddressView.dot_IP_address,  " + 
			"IPAddressView.end_point_id,  " + 
			"IPAddressView.alias,  " + 
			"IPAddressView.with_alias_authentication,  " + 
			"IPAddressView.status,  " + 
			"IPAddressView.type,  " + 
			"IPAddressView.protocol,  " + 
			"IPAddressView.port,  " + 
			"IPAddressView.registration,  " + 
			"IPAddressView.is_registered,  " + 
			"IPAddressView.IP_address_range,  " + 
			"IPAddressView.max_calls,  " + 
			"IPAddressView.password,  " + 
			"IPAddressView.prefix_in_type_id,  " + 
			"IPAddressView.prefix_type_descr,  " + 
			"IPAddressView.prefix_length,  " + 
			"IPAddressView.prefix_delimiter " + 

			"FROM  IPAddressView INNER JOIN DialPeer ON  " + 
			"IPAddressView.end_point_id = DialPeer.end_point_id " + 

			"WHERE (" + _epStatusFilter + ") " + 

			"GROUP BY  " + 
			"IPAddressView.IP_address,  " + 
			"IPAddressView.dot_IP_address,  " + 
			"IPAddressView.end_point_id,  " + 
			"IPAddressView.alias,  " + 
			"IPAddressView.with_alias_authentication,  " + 
			"IPAddressView.status,  " + 
			"IPAddressView.type,  " + 
			"IPAddressView.protocol,  " + 
			"IPAddressView.port,  " + 
			"IPAddressView.registration,  " + 
			"IPAddressView.is_registered,  " + 
			"IPAddressView.IP_address_range,  " + 
			"IPAddressView.max_calls,  " + 
			"IPAddressView.password,  " + 
			"IPAddressView.prefix_in_type_id,  " + 
			"IPAddressView.prefix_type_descr,  " + 
			"IPAddressView.prefix_length,  " + 
			"IPAddressView.prefix_delimiter " + 

			"ORDER BY IPAddressView.dot_IP_address "; 


			ArrayList _list = new ArrayList();
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				_list.AddRange( MapRecords(_reader) );
			}
			return (IPAddressViewRow[]) _list.ToArray(typeof(IPAddressViewRow));
		}

		//used by GkINIFileHelper
		public IPAddressViewRow[] GetAllTerminationIPAddressViews(Status[] pStatuses) {
			/*
			SELECT 
			IPAddressView.IP_address, 
			IPAddressView.dot_IP_address, 
			IPAddressView.end_point_id, 
			IPAddressView.alias, 
			IPAddressView.with_alias_authentication, 
			IPAddressView.status, 
			IPAddressView.type, 
			IPAddressView.protocol, 
			IPAddressView.port, 
			IPAddressView.registration, 
			IPAddressView.is_registered, 
			IPAddressView.IP_address_range, 
			IPAddressView.max_calls, 
			IPAddressView.password, 
			IPAddressView.prefix_in_type_id, 
			IPAddressView.prefix_type_descr, 
			IPAddressView.prefix_length, 
			IPAddressView.prefix_delimiter

			FROM  IPAddressView INNER JOIN CarrierAcctEPMap ON 
			IPAddressView.end_point_id = DialPeer.end_point_id

			GROUP BY 
			IPAddressView.IP_address, 
			IPAddressView.dot_IP_address, 
			IPAddressView.end_point_id, 
			IPAddressView.alias, 
			IPAddressView.with_alias_authentication, 
			IPAddressView.status, 
			IPAddressView.type, 
			IPAddressView.protocol, 
			IPAddressView.port, 
			IPAddressView.registration, 
			IPAddressView.is_registered, 
			IPAddressView.IP_address_range, 
			IPAddressView.max_calls, 
			IPAddressView.password, 
			IPAddressView.prefix_in_type_id, 
			IPAddressView.prefix_type_descr, 
			IPAddressView.prefix_length, 
			IPAddressView.prefix_delimiter

			ORDER BY IPAddressView.dot_IP_address
			*/
			string _epStatusFilter = base.Database.CreateStatusFilter(pStatuses);
			string _sqlStr = 
				"SELECT " + 
				"IPAddressView.IP_address,  " + 
				"IPAddressView.dot_IP_address,  " + 
				"IPAddressView.end_point_id,  " + 
				"IPAddressView.alias,  " + 
				"IPAddressView.with_alias_authentication,  " + 
				"IPAddressView.status,  " + 
				"IPAddressView.type,  " + 
				"IPAddressView.protocol,  " + 
				"IPAddressView.port,  " + 
				"IPAddressView.registration,  " + 
				"IPAddressView.is_registered,  " + 
				"IPAddressView.IP_address_range,  " + 
				"IPAddressView.max_calls,  " + 
				"IPAddressView.password,  " + 
				"IPAddressView.prefix_in_type_id,  " + 
				"IPAddressView.prefix_type_descr,  " + 
				"IPAddressView.prefix_length,  " + 
				"IPAddressView.prefix_delimiter " + 

				"FROM  IPAddressView INNER JOIN CarrierAcctEPMap ON " + 
				"IPAddressView.end_point_id = CarrierAcctEPMap.end_point_id " + 

				"WHERE (" + _epStatusFilter + ") " + 

				"GROUP BY  " + 
				"IPAddressView.IP_address,  " + 
				"IPAddressView.dot_IP_address,  " + 
				"IPAddressView.end_point_id,  " + 
				"IPAddressView.alias,  " + 
				"IPAddressView.with_alias_authentication,  " + 
				"IPAddressView.status,  " + 
				"IPAddressView.type,  " + 
				"IPAddressView.protocol,  " + 
				"IPAddressView.port,  " + 
				"IPAddressView.registration,  " + 
				"IPAddressView.is_registered,  " + 
				"IPAddressView.IP_address_range,  " + 
				"IPAddressView.max_calls,  " + 
				"IPAddressView.password,  " + 
				"IPAddressView.prefix_in_type_id,  " + 
				"IPAddressView.prefix_type_descr,  " + 
				"IPAddressView.prefix_length,  " + 
				"IPAddressView.prefix_delimiter " + 

				"ORDER BY IPAddressView.dot_IP_address "; 

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			using (IDataReader _reader = _cmd.ExecuteReader()) {
				return MapRecords(_reader);
			}
		}

		protected override IDbCommand CreateGetAllCommand() {
			return CreateGetCommand(null, IPAddressViewRow.dot_IP_address_DbName);
		}
	} // End of IPAddressViewCollection class
} // End of namespace
