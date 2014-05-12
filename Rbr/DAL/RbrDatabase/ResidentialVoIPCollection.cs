// <fileinfo name="ResidentialVoIPCollection.cs">
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
	/// Represents the <c>ResidentialVoIP</c> table.
	/// </summary>
	public class ResidentialVoIPCollection : ResidentialVoIPCollection_Base {
		/// <summary>
		/// Initializes a new instance of the <see cref="ResidentialVoIPCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal ResidentialVoIPCollection(Rbr_Db db) : base(db) {
			// EMPTY
		}

		public static ResidentialVoIPRow Parse(DataRow row) {
			return new ResidentialVoIPCollection(null).MapRow(row);
		}

		public override void Insert(ResidentialVoIPRow value) {
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

		public bool UpdateLastUsed(string pUserId) {
			string _sqlStr = "UPDATE ResidentialVoIP " + " SET [" + ResidentialVoIPRow.date_last_used_DbName + "] = " + Database.CreateSqlParameterName(ResidentialVoIPRow.date_last_used_PropName) + " " + " WHERE [" + ResidentialVoIPRow.user_id_DbName + "] = " + Database.CreateSqlParameterName(ResidentialVoIPRow.user_id_PropName) + " " + " SELECT @@ROWCOUNT ";
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, ResidentialVoIPRow.user_id_PropName, pUserId);
			AddParameter(_cmd, ResidentialVoIPRow.date_last_used_PropName, DateTime.Today);
			int _count = (int) _cmd.ExecuteScalar();
			return _count == 1;
		}

		public bool UpdateFirstUsedAndLastUsed(string pUserId) {
			string _sqlStr = "UPDATE ResidentialVoIP " + " SET [" + ResidentialVoIPRow.date_first_used_DbName + "] = " + Database.CreateSqlParameterName(ResidentialVoIPRow.date_first_used_PropName) + " " + " , " + " [" + ResidentialVoIPRow.date_last_used_DbName + "] = " + Database.CreateSqlParameterName(ResidentialVoIPRow.date_last_used_PropName) + " " + " WHERE [" + ResidentialVoIPRow.user_id_DbName + "] = " + Database.CreateSqlParameterName(ResidentialVoIPRow.user_id_PropName) + " " + " SELECT @@ROWCOUNT ";
			IDbCommand _cmd = Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, ResidentialVoIPRow.user_id_PropName, pUserId);
			AddParameter(_cmd, ResidentialVoIPRow.date_first_used_PropName, DateTime.Today);
			AddParameter(_cmd, ResidentialVoIPRow.date_last_used_PropName, DateTime.Today);
			int _count = (int) _cmd.ExecuteScalar();
			return _count == 1;
		}

		public bool UpdateStatus(string pUserId, Status pStatus) {
			string sqlStr = "UPDATE ResidentialVoIP " + "SET [" + ResidentialVoIPRow.status_DbName + "] = " + (byte) pStatus + " " + "WHERE [" + ResidentialVoIPRow.user_id_DbName + "] = " + base.Database.CreateSqlParameterName(ResidentialVoIPRow.user_id_PropName) + " " + "SELECT @@ROWCOUNT ";
			IDbCommand cmd = base.Database.CreateCommand(sqlStr);
			AddParameter(cmd, ResidentialVoIPRow.user_id_PropName, pUserId);
			int _count = (int) cmd.ExecuteScalar();
			return _count == 1;
		}

		public bool Exists(string pUserId) {
			string _sqlStr = "SELECT COUNT([" + ResidentialVoIPRow.user_id_DbName + "]) FROM ResidentialVoIP WHERE " + "[" + ResidentialVoIPRow.user_id_DbName + "]=" + base.Database.CreateSqlParameterName(ResidentialVoIPRow.user_id_PropName);

			IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
			AddParameter(_cmd, ResidentialVoIPRow.user_id_PropName, pUserId);
			int _res = (int) _cmd.ExecuteScalar();
			return _res > 0;
		}
	} // End of ResidentialVoIPCollection class
} // End of namespace