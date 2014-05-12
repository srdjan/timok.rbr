// <fileinfo name="TerminationRouteViewCollection.cs">
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
using Timok.Rbr.DAL.Properties;
using Timok.Rbr.DAL.RbrDatabase.Base;

namespace Timok.Rbr.DAL.RbrDatabase {
  /// <summary>
  /// Represents the <c>TerminationRouteView</c> view.
  /// </summary>
  public class TerminationRouteViewCollection : TerminationRouteViewCollection_Base {

    private static readonly string AvailableByCustomerBaseRouteIdSQL = getGetAvailableByCustomerBaseRouteIdSQL();
    private static readonly string ActiveByCustomerDiaCodeSQL = getActiveByCustomerDiaCodeSQL();
    
    /// <summary>
    /// Initializes a new instance of the <see cref="TerminationRouteViewCollection"/> class.
    /// </summary>
    /// <param name="db">The database object.</param>
    internal TerminationRouteViewCollection(Rbr_Db db) : base(db) {
      // EMPTY
    }

    public static TerminationRouteViewRow Parse(DataRow row) {
      return new TerminationRouteViewCollection(null).MapRow(row);
    }

		public TerminationRouteViewRow[] GetAvailableByCustomerBaseRouteId(int pCustomerBaseRouteId, bool pIsLCRMode) {
			var _cmd = Database.CreateCommand(AvailableByCustomerBaseRouteIdSQL);
			Database.AddParameter(_cmd, "CustomerBaseRouteId", DbType.Int32, pCustomerBaseRouteId);
			Database.AddParameter(_cmd, "IsLCRMode", DbType.Byte, (byte)( pIsLCRMode ? 1 : 0 ));

			using (var reader = Database.ExecuteReader(_cmd)) {
				return MapRecords(reader);
			}
		}

    public TerminationRouteViewRow[] GetActiveByCustomerDialCode(long pCustomerDialCode, int pCallingPlanId) {
      if (pCustomerDialCode < 7) {//dial code should never be less than 7
        throw new ArgumentException("Invalid Dial Code: " + pCustomerDialCode);
      }

      var _cmd = Database.CreateCommand(ActiveByCustomerDiaCodeSQL);
      Database.AddParameter(_cmd, "CustomerDialCode", DbType.Int32, pCustomerDialCode);
      Database.AddParameter(_cmd, "CallingPlanId", DbType.Int32, pCallingPlanId);

      using (var reader = Database.ExecuteReader(_cmd)) {
        return MapRecords(reader);
      }
    }

    private static string getGetAvailableByCustomerBaseRouteIdSQL() {
      string _sql;
      try {
				//_sql = Rbr_Db.GetFromResources("RbrDb_TerminationRouteViewCollection_GetAvailableByCustomerBaseRouteId.sql");
				_sql = Resources.TerminationRouteViewCollection_GetAvailableByCustomerBaseRouteId;
			}
      catch (Exception _ex) {
        throw new Exception("Failed to get RbrDb_TerminationRouteViewCollection_GetAvailableByCustomerBaseRouteId.sql RESOURCE.\r\n\r\n" + _ex);
      }
      return _sql;
    }

    private static string getActiveByCustomerDiaCodeSQL() {
      string _sql;
      try {
        //_sql = Rbr_Db.GetFromResources("RbrDb_TerminationRouteViewCollection_GetActiveByCustomerDialCode.sql");
				_sql = Resources.TerminationRouteViewCollection_GetActiveByCustomerDialCode;
      }
      catch (Exception _ex) {
        throw new Exception("Failed to get RbrDb_TerminationRouteViewCollection_GetActiveByCustomerDialCode.sql RESOURCE.\r\n\r\n" + _ex);
      }
      return _sql;
    }

  } // End of TerminationRouteViewCollection class
} // End of namespace
