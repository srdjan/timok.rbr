// <fileinfo name="PersonCollection.cs">
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
  /// Represents the <c>Person</c> table.
  /// </summary>
  public class PersonCollection : PersonCollection_Base {
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonCollection"/> class.
    /// </summary>
    /// <param name="db">The database object.</param>
    internal PersonCollection(Rbr_Db db)
      : base(db) {
      // EMPTY
    }

    public static PersonRow Parse(System.Data.DataRow row) {
      return new PersonCollection(null).MapRow(row);
    }

    public PersonRow GetByLogin(string pLogin) {
      string _sqlStr = "SELECT * FROM [dbo].[Person] WHERE " +
        "[" + PersonRow.login_DbName + "]=" + base.Database.CreateSqlParameterName(PersonRow.login_PropName);
      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, PersonRow.login_PropName, pLogin);

      using (IDataReader _reader = _cmd.ExecuteReader()) {
        PersonRow[] _tempArray = MapRecords(_reader);
        return 0 == _tempArray.Length ? null : _tempArray[0];
      }
    }

    public override void Insert(PersonRow value) {
      try {
        string _sqlStr =
          "DECLARE " +
          base.Database.CreateSqlParameterName(PersonRow.person_id_PropName) + " int " +
          "SET " +
          base.Database.CreateSqlParameterName(PersonRow.person_id_PropName) +
          " = COALESCE((SELECT MAX(" + PersonRow.person_id_DbName + ") FROM Person) + 1, " + PersonRow.DefaultId + ") " +

          "INSERT INTO [dbo].[Person] (" +
          "[" + PersonRow.person_id_DbName + "], " +
          "[" + PersonRow.name_DbName + "], " +
          "[" + PersonRow.login_DbName + "], " +
          "[" + PersonRow.password_DbName + "], " +
          "[" + PersonRow.permission_DbName + "], " +
          "[" + PersonRow.is_reseller_DbName + "], " +
          "[" + PersonRow.status_DbName + "], " +
          "[" + PersonRow.registration_status_DbName + "], " +
          "[" + PersonRow.salt_DbName + "], " +
          "[" + PersonRow.partner_id_DbName + "], " +
          "[" + PersonRow.retail_acct_id_DbName + "]," +
          "[" + PersonRow.group_id_DbName + "], " +
          "[" + PersonRow.virtual_switch_id_DbName + "], " +
          "[" + PersonRow.contact_info_id_DbName + "] " +
          ") VALUES (" +
          Database.CreateSqlParameterName(PersonRow.person_id_PropName) + ", " +
          Database.CreateSqlParameterName(PersonRow.name_PropName) + ", " +
          Database.CreateSqlParameterName(PersonRow.login_PropName) + ", " +
          Database.CreateSqlParameterName(PersonRow.password_PropName) + ", " +
          Database.CreateSqlParameterName(PersonRow.permission_PropName) + ", " +
          Database.CreateSqlParameterName(PersonRow.is_reseller_PropName) + ", " +
          Database.CreateSqlParameterName(PersonRow.status_PropName) + ", " +
          Database.CreateSqlParameterName(PersonRow.registration_status_PropName) + ", " +
          Database.CreateSqlParameterName(PersonRow.salt_PropName) + ", " +
          Database.CreateSqlParameterName(PersonRow.partner_id_PropName) + ", " +
          Database.CreateSqlParameterName(PersonRow.retail_acct_id_PropName) + ", " +
          Database.CreateSqlParameterName(PersonRow.group_id_PropName) + ", " +
          Database.CreateSqlParameterName(PersonRow.virtual_switch_id_PropName) + ", " +
          Database.CreateSqlParameterName(PersonRow.contact_info_id_PropName) + ") " +

          " SELECT " + base.Database.CreateSqlParameterName(PersonRow.person_id_PropName) + " ";

        IDbCommand _cmd = Database.CreateCommand(_sqlStr);
        AddParameter(_cmd, PersonRow.name_PropName, value.Name);
        AddParameter(_cmd, PersonRow.login_PropName, value.Login);
        AddParameter(_cmd, PersonRow.password_PropName, value.Password);
        AddParameter(_cmd, PersonRow.permission_PropName, value.Permission);
        AddParameter(_cmd, PersonRow.is_reseller_PropName, value.Is_reseller);
        AddParameter(_cmd, PersonRow.status_PropName, value.Registration_status);
        AddParameter(_cmd, PersonRow.registration_status_PropName, value.Registration_status);
        AddParameter(_cmd, PersonRow.salt_PropName, value.Salt);

        AddParameter(_cmd, PersonRow.partner_id_PropName,
          value.IsPartner_idNull ? DBNull.Value : (object) value.Partner_id);
        AddParameter(_cmd, PersonRow.retail_acct_id_PropName,
          value.IsRetail_acct_idNull ? DBNull.Value : (object) value.Retail_acct_id);
        AddParameter(_cmd, PersonRow.group_id_PropName,
          value.IsGroup_idNull ? DBNull.Value : (object) value.Group_id);
        AddParameter(_cmd, PersonRow.virtual_switch_id_PropName,
          value.IsVirtual_switch_idNull ? DBNull.Value : (object) value.Virtual_switch_id);
        AddParameter(_cmd, PersonRow.contact_info_id_PropName,
          value.IsContact_info_idNull ? DBNull.Value : (object) value.Contact_info_id);

        value.Person_id = (int) _cmd.ExecuteScalar();
      }
      catch (System.Data.SqlClient.SqlException _sqlEx) {
        if (_sqlEx.Message.IndexOf("Cannot insert duplicate key row in object") > -1) {
          throw new AlternateKeyException("Login Name already in use!", _sqlEx);
        }
        throw;// any other ex
      }
    }

    public override bool Update(PersonRow value) {
      try {
        return base.Update(value);
      }
      catch (System.Data.SqlClient.SqlException _sqlEx) {
        if (_sqlEx.Message.IndexOf("Cannot insert duplicate key row in object") > -1) {
          throw new AlternateKeyException("Login Name already in use!", _sqlEx);
        }
        throw;// any other ex
      }
    }

    public bool UpdateStatus(int pPersonId, Status pStatus) {
      string _sqlStr = "UPDATE Person " +
        "SET [" + PersonRow.status_DbName + "] = " + (byte) pStatus + " " +
        "WHERE [" + PersonRow.person_id_DbName + "] = " +
        base.Database.CreateSqlParameterName(PersonRow.person_id_PropName) + " " +
        "SELECT @@ROWCOUNT ";
      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, PersonRow.person_id_PropName, pPersonId);
      int _count = (int) _cmd.ExecuteScalar();
      return _count == 1;
    }

    public bool UpdateRegistrationStatus(int pPersonId, Status pRegistrationStatus) {
      //TODO: create new field for Status, for now registration_status will be used
      string _sqlStr = "UPDATE Person " +
        "SET [" + PersonRow.registration_status_DbName + "] = " + (byte) pRegistrationStatus + " " +
        "WHERE [" + PersonRow.person_id_DbName + "] = " +
        base.Database.CreateSqlParameterName(PersonRow.person_id_PropName) + " " +
        "SELECT @@ROWCOUNT ";
      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, PersonRow.person_id_PropName, pPersonId);
      int _count = (int) _cmd.ExecuteScalar();
      return _count == 1;
    }

    public PersonRow[] GetResellAgents() {
      string _whereSql = "[" + PersonRow.is_reseller_DbName + "] = 3 ";

      IDbCommand _cmd = base.CreateGetCommand(_whereSql, PersonRow.name_DbName);

      using (IDataReader _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }

    public PersonRow[] GetResellAgents(int pPartnerId) {
      string _whereSql = "[" + PersonRow.partner_id_DbName + "]=" + base.Database.CreateSqlParameterName(PersonRow.partner_id_PropName) + 
        " AND " + 
        "[" + PersonRow.is_reseller_DbName + "] = 3 ";

      IDbCommand _cmd = base.CreateGetCommand(_whereSql, PersonRow.name_DbName);
      AddParameter(_cmd, PersonRow.partner_id_PropName, pPartnerId);

      using (IDataReader _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }

    public PersonRow[] GetActiveResellAgents(int pPartnerId) {
      string _whereSql = "[" + PersonRow.partner_id_DbName + "]=" + base.Database.CreateSqlParameterName(PersonRow.partner_id_PropName) +
        " AND " +
        "[" + PersonRow.status_DbName + "] = " + (byte) Status.Active + 
        " AND " + 
        "[" + PersonRow.is_reseller_DbName + "] = 3 " + 
        " AND " + 
        PersonRow.partner_id_DbName + " IN (" + 
        " SELECT " + PartnerRow.partner_id_DbName + " FROM Partner WHERE " + 
        " " + PartnerRow.status_DbName + "=" + (byte) Status.Active + 
        " ) ";

      IDbCommand _cmd = base.CreateGetCommand(_whereSql, PersonRow.name_DbName);
      AddParameter(_cmd, PersonRow.partner_id_PropName, pPartnerId);

      using (IDataReader _reader = _cmd.ExecuteReader()) {
        return MapRecords(_reader);
      }
    }
  } // End of PersonCollection class
} // End of namespace
