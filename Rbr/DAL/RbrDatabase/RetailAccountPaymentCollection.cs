// <fileinfo name="RetailAccountPaymentCollection.cs">
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
	/// Represents the <c>RetailAccountPayment</c> table.
	/// </summary>
	public class RetailAccountPaymentCollection : RetailAccountPaymentCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RetailAccountPaymentCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal RetailAccountPaymentCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static RetailAccountPaymentRow Parse(System.Data.DataRow row){
			return new RetailAccountPaymentCollection(null).MapRow(row);
		}

    public bool HasPayments(int pPersonId) {
      string _sqlStr = "SELECT COUNT(*) FROM [dbo].[RetailAccountPayment] WHERE " +
        "[" + RetailAccountPaymentRow.person_id_DbName + "]=" + base.Database.CreateSqlParameterName(RetailAccountPaymentRow.person_id_PropName);

      IDbCommand _cmd = base.Database.CreateCommand(_sqlStr);
      AddParameter(_cmd, RetailAccountPaymentRow.person_id_PropName, pPersonId);
      object _count = _cmd.ExecuteScalar();
      return (_count == null || (int) _count == 0) ? false : true;
    }
	} // End of RetailAccountPaymentCollection class
} // End of namespace
