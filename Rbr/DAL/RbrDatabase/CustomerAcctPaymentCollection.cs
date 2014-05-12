// <fileinfo name="CustomerAcctPaymentCollection.cs">
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

namespace Timok.Rbr.DAL.RbrDatabase
{
	/// <summary>
	/// Represents the <c>CustomerAcctPayment</c> table.
	/// </summary>
	public class CustomerAcctPaymentCollection : CustomerAcctPaymentCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerAcctPaymentCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal CustomerAcctPaymentCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static CustomerAcctPaymentRow Parse(System.Data.DataRow row){
			return new CustomerAcctPaymentCollection(null).MapRow(row);
		}

    public bool HasPayments(int pPersonId) {
      string _sqlStr = "SELECT COUNT(*) FROM [dbo].[CustomerAcctPayment] WHERE " +
        "[" + CustomerAcctPaymentRow.person_id_DbName + "]=" + base.Database.CreateSqlParameterName(CustomerAcctPaymentRow.person_id_PropName);

      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, CustomerAcctPaymentRow.person_id_PropName, pPersonId);
      object _count = _cmd.ExecuteScalar();
      return (_count == null || (int) _count == 0) ? false : true;
    }
  } // End of CustomerAcctPaymentCollection class
} // End of namespace
