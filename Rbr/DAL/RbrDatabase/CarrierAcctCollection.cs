// <fileinfo name="CarrierAcctCollection.cs">
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
	/// Represents the <c>CarrierAcct</c> table.
	/// </summary>
	public class CarrierAcctCollection : CarrierAcctCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="CarrierAcctCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CarrierAcctCollection(Rbr_Db db)
			: base(db) {
			// EMPTY
		}

		public static CarrierAcctRow Parse(System.Data.DataRow row){
			return new CarrierAcctCollection(null).MapRow(row);
		}

		public override void Insert(CarrierAcctRow value) {
			string _sqlStr = "DECLARE " + base.Database.CreateSqlParameterName(CarrierAcctRow.carrier_acct_id_PropName) + " smallint " + 
				"SET " + base.Database.CreateSqlParameterName(CarrierAcctRow.carrier_acct_id_PropName) +
        " = COALESCE((SELECT MAX(" + CarrierAcctRow.carrier_acct_id_DbName + ") FROM CarrierAcct) + 1, 10000) " + 
				
				"INSERT INTO [dbo].[CarrierAcct] (" + 
				"[" + CarrierAcctRow.carrier_acct_id_DbName + "], " +
				"[" + CarrierAcctRow.name_DbName + "], " +
				"[" + CarrierAcctRow.status_DbName + "], " +
				"[" + CarrierAcctRow.rating_type_DbName + "], " +
				"[" + CarrierAcctRow.prefix_out_DbName + "], " +
				"[" + CarrierAcctRow.max_call_length_DbName + "], " +
				"[" + CarrierAcctRow.strip_1plus_DbName + "], " +
				"[" + CarrierAcctRow.intl_dial_code_DbName + "], " +
				"[" + CarrierAcctRow.partner_id_DbName + "], " +
				"[" + CarrierAcctRow.calling_plan_id_DbName + "] " +
				") VALUES (" +
				//"@Carrier_id, " +
				Database.CreateSqlParameterName(CarrierAcctRow.carrier_acct_id_PropName) + ", " +
				Database.CreateSqlParameterName(CarrierAcctRow.name_PropName) + ", " +
				Database.CreateSqlParameterName(CarrierAcctRow.status_PropName) + ", " + 
				Database.CreateSqlParameterName(CarrierAcctRow.rating_type_PropName) + ", " + 
				Database.CreateSqlParameterName(CarrierAcctRow.prefix_out_PropName) + ", " +
				Database.CreateSqlParameterName(CarrierAcctRow.max_call_length_PropName) + ", " +
				Database.CreateSqlParameterName(CarrierAcctRow.strip_1plus_PropName) + ", " +
				Database.CreateSqlParameterName(CarrierAcctRow.intl_dial_code_PropName) + ", " + 
				Database.CreateSqlParameterName(CarrierAcctRow.partner_id_PropName) + ", " +
        Database.CreateSqlParameterName(CarrierAcctRow.calling_plan_id_PropName) + ") " + 
				"SELECT " + Database.CreateSqlParameterName(CarrierAcctRow.carrier_acct_id_PropName);
			
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			//AddParameter(cmd, "Carrier_id", DbType.Int32, value.Carrier_id);
			AddParameter(_cmd, CarrierAcctRow.name_PropName, value.Name);
			AddParameter(_cmd, CarrierAcctRow.status_PropName, value.Status);
			AddParameter(_cmd, CarrierAcctRow.rating_type_PropName, value.Rating_type);
			AddParameter(_cmd, CarrierAcctRow.prefix_out_PropName, value.Prefix_out);
			AddParameter(_cmd, CarrierAcctRow.max_call_length_PropName, value.Max_call_length);
			AddParameter(_cmd, CarrierAcctRow.strip_1plus_PropName, value.Strip_1plus);
			AddParameter(_cmd, CarrierAcctRow.intl_dial_code_PropName, value.Intl_dial_code);
			AddParameter(_cmd, CarrierAcctRow.partner_id_PropName, value.Partner_id);
      AddParameter(_cmd, CarrierAcctRow.calling_plan_id_PropName, value.Calling_plan_id);

			object _res = _cmd.ExecuteScalar();
			value.Carrier_acct_id = (short) _res;
		}
	
		protected override IDbCommand CreateGetByPartner_idCommand(int partner_id) {
			string _whereSql = "[" + CarrierAcctRow.partner_id_DbName + "]=" + Database.CreateSqlParameterName(CarrierAcctRow.partner_id_PropName);

			IDbCommand _cmd = CreateGetCommand(_whereSql, CarrierAcctRow.name_DbName);
			AddParameter(_cmd, CarrierAcctRow.partner_id_PropName, partner_id);
			return _cmd;
		}

		public CarrierAcctRow[] GetByPartner_id(int pPartnerId, Status[] pStatuses) {
			IDbCommand cmd = this.CreateGetByPartner_idCommand(pPartnerId, pStatuses);
			using(IDataReader reader = cmd.ExecuteReader()) {
				return MapRecords(reader);
			}
		}

		public CarrierAcctRow[] GetByStatus(Status[] pStatuses) {
			IDbCommand cmd = this.CreateGetByStatusCommand(pStatuses);
			using(IDataReader reader = cmd.ExecuteReader()) {
				return MapRecords(reader);
			}
		}

    public CarrierAcctRow GetByCallingPlanId(int pCallingPlanId) {
      string _where = CarrierAcctRow.calling_plan_id_DbName + "=" + 
				Database.CreateSqlParameterName(CarrierAcctRow.calling_plan_id_PropName);

			IDbCommand _cmd = this.CreateGetCommand(_where, null);
      AddParameter(_cmd, CarrierAcctRow.calling_plan_id_PropName, pCallingPlanId);

			using (IDataReader _reader = _cmd.ExecuteReader()) {
				CarrierAcctRow[] _temp = MapRecords(_reader);
				return 0 == _temp.Length ? null : _temp[0];
			}
		}
		
		protected IDbCommand CreateGetByPartner_idCommand(int pPartnerId, Status[] pStatuses) {
			string _where = CarrierAcctRow.partner_id_DbName + "=" + pPartnerId + 
				" AND " + base.Database.CreateStatusFilter(pStatuses);
			string _order = CarrierAcctRow.name_DbName;
			return this.CreateGetCommand(_where, _order);
		}

		protected IDbCommand CreateGetByStatusCommand(Status[] pStatuses) {
			string _where = base.Database.CreateStatusFilter(pStatuses);
			string _order = CarrierAcctRow.name_DbName;
			return this.CreateGetCommand(_where, _order);
		}

		protected override IDbCommand CreateGetAllCommand() {
			return CreateGetCommand(null, CarrierAcctRow.name_DbName);
		}

		public bool IsNameInUseByOtherCarrierAcct(string name, short carrier_acct_id){
			string sqlStr = "SELECT COUNT(*) FROM [dbo].[CarrierAcct] WHERE " +
				"[" + CarrierAcctRow.name_DbName + "]=" + base.Database.CreateSqlParameterName(CarrierAcctRow.name_PropName) + 
				" AND " + 
				"[" + CarrierAcctRow.carrier_acct_id_DbName + "]<>" + base.Database.CreateSqlParameterName(CarrierAcctRow.carrier_acct_id_PropName);

			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(cmd, CarrierAcctRow.name_PropName, name);
			AddParameter(cmd, CarrierAcctRow.carrier_acct_id_PropName, carrier_acct_id);

			int _count = (int)cmd.ExecuteScalar();
			return _count > 0 ? true : false;
		}

    public int GetCountByCallingPlanId(int pCallingPlanId) {
			string _sqlStr = "SELECT COUNT(*) FROM [CarrierAcct] WHERE " + 
				"[" + CarrierAcctRow.calling_plan_id_DbName + "]=" + base.Database.CreateSqlParameterName(CarrierAcctRow.calling_plan_id_PropName);

			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, CarrierAcctRow.calling_plan_id_PropName, pCallingPlanId);
			return (int) _cmd.ExecuteScalar();
		}

	} // End of CarrierAcctCollection class
} // End of namespace
