using System;
using System.Data;

namespace Timok.Rbr.DAL.CdrDatabase {

  public class CustomerRoutesTotalMinutesDataTable {
    public const string TableName = "CustomerRoutesTotalMinutesDataTable";
		public const string Id_ColumnName = "customer_route_id";
		public const string Minutes_ColumnName = "route_minutes";

		public static DataTable CreateNew() {
			DataTable dataTable = new DataTable();
			dataTable.TableName = TableName;

			DataColumn dataColumn;

			dataColumn = dataTable.Columns.Add(Id_ColumnName, typeof(int));
			dataColumn.Caption = Id_ColumnName;

			dataColumn = dataTable.Columns.Add(Minutes_ColumnName, typeof(decimal));
			dataColumn.Caption = Minutes_ColumnName;
			return dataTable;
		}
	}
}
