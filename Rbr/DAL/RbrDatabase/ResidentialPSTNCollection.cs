// <fileinfo name="ResidentialPSTNCollection.cs">
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
using System.Data.SqlClient;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase.Base;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents the <c>ResidentialPSTN</c> table.
	/// </summary>
	public class ResidentialPSTNCollection : ResidentialPSTNCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="ResidentialPSTNCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal ResidentialPSTNCollection(Rbr_Db db) : base(db) {
			// EMPTY
		}

		public static ResidentialPSTNRow Parse(DataRow row) {
			return new ResidentialPSTNCollection(null).MapRow(row);
		}

		public override void Insert(ResidentialPSTNRow value) {
			try {
				base.Insert(value);
			}
			catch (SqlException _sqlex) {
				if (_sqlex.Message.IndexOf("Violation of PRIMARY KEY constraint") > -1 || _sqlex.Message.IndexOf("Cannot insert duplicate key row in object") > -1) {
					return; //return and continue, PIN already loaded
				}
				throw new Exception("Unexpected Exception:\r\n", _sqlex);
			}
		}

		public bool UpdateStatus(short pServiceId, long pANI, Status pStatus) {
			string _sqlStr = "UPDATE ResidentialPSTN " + "SET [" + ResidentialPSTNRow.status_DbName + "] = " + (byte) pStatus + " " + "WHERE [" + ResidentialPSTNRow.service_id_DbName + "] = " + Database.CreateSqlParameterName(ResidentialPSTNRow.service_id_PropName) + " " + "AND [" + ResidentialPSTNRow.ANI_DbName + "] = " + Database.CreateSqlParameterName(ResidentialPSTNRow.ANI_PropName) + " " + "SELECT @@ROWCOUNT ";
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, ResidentialPSTNRow.service_id_PropName, pServiceId);
			AddParameter(_cmd, ResidentialPSTNRow.ANI_PropName, pANI);
			int _count = (int) _cmd.ExecuteScalar();
			return _count == 1;
		}

		public bool UpdateLastUsed(short pServiceId, long pANI) {
			string _sqlStr = "UPDATE ResidentialPSTN " + " SET [" + ResidentialPSTNRow.date_last_used_DbName + "] = " + Database.CreateSqlParameterName(ResidentialPSTNRow.date_last_used_PropName) + " " + " WHERE [" + ResidentialPSTNRow.service_id_DbName + "] = " + Database.CreateSqlParameterName(ResidentialPSTNRow.service_id_PropName) + " " + " AND [" + ResidentialPSTNRow.ANI_DbName + "] = " + Database.CreateSqlParameterName(ResidentialPSTNRow.ANI_PropName) + " " + " SELECT @@ROWCOUNT ";
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, ResidentialPSTNRow.service_id_PropName, pServiceId);
			AddParameter(_cmd, ResidentialPSTNRow.ANI_PropName, pANI);
			AddParameter(_cmd, ResidentialPSTNRow.date_last_used_PropName, DateTime.Today);
			int _count = (int) _cmd.ExecuteScalar();
			return _count == 1;
		}

		public bool UpdateFirstUsedAndLastUsed(short pServiceId, long pANI) {
			string _sqlStr = "UPDATE ResidentialPSTN " + " SET [" + ResidentialPSTNRow.date_first_used_DbName + "] = " + Database.CreateSqlParameterName(ResidentialPSTNRow.date_first_used_PropName) + " " + " , " + " [" + ResidentialPSTNRow.date_last_used_DbName + "] = " + Database.CreateSqlParameterName(ResidentialPSTNRow.date_last_used_PropName) + " " + " WHERE [" + ResidentialPSTNRow.service_id_DbName + "] = " + Database.CreateSqlParameterName(ResidentialPSTNRow.service_id_PropName) + " " + " AND [" + ResidentialPSTNRow.ANI_DbName + "] = " + Database.CreateSqlParameterName(ResidentialPSTNRow.ANI_PropName) + " " + " SELECT @@ROWCOUNT ";
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, ResidentialPSTNRow.service_id_PropName, pServiceId);
			AddParameter(_cmd, ResidentialPSTNRow.ANI_PropName, pANI);
			AddParameter(_cmd, ResidentialPSTNRow.date_first_used_PropName, DateTime.Today);
			AddParameter(_cmd, ResidentialPSTNRow.date_last_used_PropName, DateTime.Today);
			int _count = (int) _cmd.ExecuteScalar();
			return _count == 1;
		}

		public int GetCountByServiceID(short pServiceId) {
			string _sqlStr = "SELECT COUNT([" + ResidentialPSTNRow.service_id_DbName + "]) FROM ResidentialPSTN WHERE " + "[" + ResidentialPSTNRow.service_id_DbName + "]=" + base.Database.CreateSqlParameterName(ResidentialPSTNRow.service_id_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, ResidentialPSTNRow.service_id_PropName, pServiceId);
			return ((int) _cmd.ExecuteScalar());
		}

		public bool Exists(short pServiceId, long pANI) {
			string _sqlStr = "SELECT COUNT(*) FROM ResidentialPSTN WHERE " + "[" + ResidentialPSTNRow.service_id_DbName + "]=" + base.Database.CreateSqlParameterName(ResidentialPSTNRow.service_id_PropName) + " AND " + "[" + ResidentialPSTNRow.ANI_DbName + "]=" + base.Database.CreateSqlParameterName(ResidentialPSTNRow.ANI_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, ResidentialPSTNRow.service_id_PropName, pServiceId);
			AddParameter(_cmd, ResidentialPSTNRow.ANI_PropName, pANI);
			int _res = (int) _cmd.ExecuteScalar();
			return _res > 0;
		}
	} // End of ResidentialPSTNCollection class
} // End of namespace