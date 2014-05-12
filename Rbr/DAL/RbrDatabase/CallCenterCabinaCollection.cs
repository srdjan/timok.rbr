// <fileinfo name="CallCenterCabinaCollection.cs">
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
  /// Represents the <c>CallCenterCabina</c> table.
  /// </summary>
  public class CallCenterCabinaCollection : CallCenterCabinaCollection_Base {
    /// <summary>
    /// Initializes a new instance of the <see cref="CallCenterCabinaCollection"/> class.
    /// </summary>
    /// <param name="db">The database object.</param>
    internal CallCenterCabinaCollection(Rbr_Db db)
      : base(db) {
      // EMPTY
    }

    public static CallCenterCabinaRow Parse(System.Data.DataRow row) {
      return new CallCenterCabinaCollection(null).MapRow(row);
    }

    public bool UpdateLastUsed(short pServiceId, long pSerialNumber) {
      string sqlStr = "UPDATE CallCenterCabina " +
        " SET [" + CallCenterCabinaRow.date_last_used_DbName + "] = " + Database.CreateSqlParameterName(CallCenterCabinaRow.date_last_used_PropName) + " " +
        " WHERE [" + CallCenterCabinaRow.service_id_DbName + "] = " +
        Database.CreateSqlParameterName(CallCenterCabinaRow.service_id_PropName) + " " +
        " AND [" + CallCenterCabinaRow.serial_number_DbName + "] = " +
        Database.CreateSqlParameterName(CallCenterCabinaRow.serial_number_PropName) + " " +
        " SELECT @@ROWCOUNT ";
      IDbCommand cmd = base.Database.CreateCommand(sqlStr);
      AddParameter(cmd, CallCenterCabinaRow.service_id_PropName, pServiceId);
      AddParameter(cmd, CallCenterCabinaRow.serial_number_PropName, pSerialNumber);
      AddParameter(cmd, CallCenterCabinaRow.date_last_used_PropName, DateTime.Today);
      int _count = (int) cmd.ExecuteScalar();
      return _count == 1;
    }

    public bool UpdateFirstUsedAndLastUsed(short pServiceId, long pSerialNumber) {
      string sqlStr = "UPDATE CallCenterCabina " +
        " SET [" + CallCenterCabinaRow.date_first_used_DbName + "] = " + Database.CreateSqlParameterName(CallCenterCabinaRow.date_first_used_PropName) + " " +
        " , " +
        " [" + CallCenterCabinaRow.date_last_used_DbName + "] = " + Database.CreateSqlParameterName(CallCenterCabinaRow.date_last_used_PropName) + " " +
        " WHERE [" + CallCenterCabinaRow.service_id_DbName + "] = " +
        Database.CreateSqlParameterName(CallCenterCabinaRow.service_id_PropName) + " " +
        " AND [" + CallCenterCabinaRow.serial_number_DbName + "] = " +
        Database.CreateSqlParameterName(CallCenterCabinaRow.serial_number_PropName) + " " +
        " SELECT @@ROWCOUNT ";
      IDbCommand cmd = base.Database.CreateCommand(sqlStr);
      AddParameter(cmd, CallCenterCabinaRow.service_id_PropName, pServiceId);
      AddParameter(cmd, CallCenterCabinaRow.serial_number_PropName, pSerialNumber);
      AddParameter(cmd, CallCenterCabinaRow.date_first_used_PropName, DateTime.Today);
      AddParameter(cmd, CallCenterCabinaRow.date_last_used_PropName, DateTime.Today);
      int _count = (int) cmd.ExecuteScalar();
      return _count == 1;
    }

    public bool UpdateStatus(short pServiceId, long pSerialNumber, Status pStatus) {
      string _sqlStr = "UPDATE CallCenterCabina " +
        " SET [" + CallCenterCabinaRow.status_DbName + "] = " + (byte) pStatus + " " +
        " WHERE [" + CallCenterCabinaRow.service_id_DbName + "] = " +
        base.Database.CreateSqlParameterName(CallCenterCabinaRow.service_id_PropName) + " " + 
        " AND " + 
        CallCenterCabinaRow.serial_number_DbName + " = " + 
        Database.CreateSqlParameterName(CallCenterCabinaRow.serial_number_PropName) + " " + 
        " SELECT @@ROWCOUNT ";
      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, CallCenterCabinaRow.service_id_PropName, pServiceId);
      AddParameter(_cmd, CallCenterCabinaRow.serial_number_PropName, pSerialNumber);
      int _count = (int) _cmd.ExecuteScalar();
      return _count == 1;
    }
  } // End of CallCenterCabinaCollection class
} // End of namespace
