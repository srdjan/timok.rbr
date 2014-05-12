using System;
using System.Data;

namespace Timok.Rbr.DAL.CdrDatabase {

  public class EndpointAsrDataTable {
    public const string TableName = "EndpointAsr";
		public const string Id_ColumnName = "ep_id";
		public const string Calls_ColumnName = "ep_calls";
		public const string Connected_calls_ColumnName = "ep_connected_calls";
		public const string Asr_ColumnName = "asr";

		public static DataTable CreateNew() {
			DataTable dataTable = new DataTable();
			dataTable.TableName = TableName;

			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add(Id_ColumnName, typeof(short));
			dataColumn.Caption = Id_ColumnName;
			
			dataColumn = dataTable.Columns.Add(Calls_ColumnName, typeof(int));
			dataColumn.Caption = Calls_ColumnName;
			
			dataColumn = dataTable.Columns.Add(Connected_calls_ColumnName, typeof(int));
			dataColumn.Caption = Connected_calls_ColumnName;
			
			dataColumn = dataTable.Columns.Add(Asr_ColumnName, typeof(int));
			dataColumn.Caption = Asr_ColumnName;
			return dataTable;
		}
	}
}
