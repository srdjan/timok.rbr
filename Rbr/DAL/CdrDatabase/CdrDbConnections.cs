using System;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DAL.CdrDatabase {
	public class CdrDbConnections {
		readonly IConfiguration configuration;

		public CdrDbConnections(IConfiguration pConfiguration) {
			configuration = pConfiguration;
		}

		public static string CurrentDbName { get { return Cdr_Db.GetDbName(DateTime.Today); } }

		public void Validate() {
			try {
				using (var _db = new Cdr_Db(DateTime.Now)) {}
			}
			catch (Exception _ex) {
				throw new Exception(string.Format("CdrDb connection is invalid, exception: {0}", _ex));
			}
		}
	}
}