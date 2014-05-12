// <fileinfo name="BatchCollection.cs">
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

namespace Timok.Rbr.DAL.RbrDatabase
{
	/// <summary>
	/// Represents the <c>Batch</c> table.
	/// </summary>
	public class BatchCollection : BatchCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BatchCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal BatchCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static BatchRow Parse(System.Data.DataRow row){
			return new BatchCollection(null).MapRow(row);
		}

    public BatchRow[] GetGeneratedByRequestId(int pRequestId) {
      string _where = BatchRow.status_DbName + " = " + (byte) InventoryStatus.Generated + " " +
        " AND " +
        BatchRow.request_id_DbName + " = " + pRequestId;
      return GetAsArray(_where, null);
    }

    public BatchRow[] GetByInventoryStatusCustomerAcctIdRequestId(InventoryStatus pInventoryStatus, short pCustomerAcctId, int pRequestId) {
      string _where = BatchRow.customer_acct_id_DbName + " = " + pCustomerAcctId + " " +
        " AND " +
        BatchRow.request_id_DbName + " = " + pRequestId + " " + 
        " AND " +
        BatchRow.status_DbName + " = " + (byte) pInventoryStatus;
      return GetAsArray(_where, null);
    }

  } // End of BatchCollection class
} // End of namespace
