// <fileinfo name="GenerationRequestCollection.cs">
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
	/// Represents the <c>GenerationRequest</c> table.
	/// </summary>
	public class GenerationRequestCollection : GenerationRequestCollection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GenerationRequestCollection"/> class.
		/// </summary>
		/// <param name="db">The database object.</param>
		internal GenerationRequestCollection(Rbr_Db db)
				: base(db)
		{
			// EMPTY
		}

		public static GenerationRequestRow Parse(System.Data.DataRow row){
			return new GenerationRequestCollection(null).MapRow(row);
		}

    public override void Insert(GenerationRequestRow value) {
      string _sqlStr = "DECLARE " + base.Database.CreateSqlParameterName(GenerationRequestRow.request_id_PropName) + " int " +
        "SET " + base.Database.CreateSqlParameterName(GenerationRequestRow.request_id_PropName) +
        " = COALESCE((SELECT MAX(" + GenerationRequestRow.request_id_DbName + ") FROM GenerationRequest) + 1, 1000) " +

      " INSERT INTO [dbo].[GenerationRequest] (" +
      "[" + GenerationRequestRow.request_id_DbName + "], " +
      "[" + GenerationRequestRow.date_requested_DbName + "], " +
      "[" + GenerationRequestRow.date_to_process_DbName + "], " +
      "[" + GenerationRequestRow.date_completed_DbName + "], " +
      "[" + GenerationRequestRow.number_of_batches_DbName + "], " +
      "[" + GenerationRequestRow.batch_size_DbName + "], " +
      "[" + GenerationRequestRow.lot_id_DbName + "]" +
      ") VALUES (" +
      Database.CreateSqlParameterName(GenerationRequestRow.request_id_PropName) + ", " +
      Database.CreateSqlParameterName(GenerationRequestRow.date_requested_PropName) + ", " +
      Database.CreateSqlParameterName(GenerationRequestRow.date_to_process_PropName) + ", " +
      Database.CreateSqlParameterName(GenerationRequestRow.date_completed_PropName) + ", " +
      Database.CreateSqlParameterName(GenerationRequestRow.number_of_batches_PropName) + ", " +
      Database.CreateSqlParameterName(GenerationRequestRow.batch_size_PropName) + ", " +
      Database.CreateSqlParameterName(GenerationRequestRow.lot_id_PropName) + ")" +
      " SELECT " + Database.CreateSqlParameterName(GenerationRequestRow.request_id_PropName);


      IDbCommand _cmd = Database.CreateCommand(_sqlStr);
      //AddParameter(cmd, "Request_id", value.Request_id);
      AddParameter(_cmd, GenerationRequestRow.date_requested_PropName, value.Date_requested);
      AddParameter(_cmd, GenerationRequestRow.date_to_process_PropName, value.Date_to_process);
      AddParameter(_cmd, GenerationRequestRow.date_completed_PropName,
        value.IsDate_completedNull ? DBNull.Value : (object) value.Date_completed);
      AddParameter(_cmd, GenerationRequestRow.number_of_batches_PropName, value.Number_of_batches);
      AddParameter(_cmd, GenerationRequestRow.batch_size_PropName, value.Batch_size);
      AddParameter(_cmd, GenerationRequestRow.lot_id_PropName, value.Lot_id);
      object _res = _cmd.ExecuteScalar();

      value.Request_id = (int) _res;
    }
  } // End of GenerationRequestCollection class
} // End of namespace
