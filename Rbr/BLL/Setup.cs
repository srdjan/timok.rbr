using System;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.CdrDatabase;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL {
	public class Setup {
		static readonly string dbInfo = Rbr_Db.DbName + "; " + CdrDbConnections.CurrentDbName;
		public static string DbInfo { get { return dbInfo; } }

		public static void ValidateDb() {
			validateRbrDb();
			validateCdrDb();
		}

		//--------------------------- Private --------------------------
		static void validateRbrDb() {
			//TODO: Rbr_Db.CheckConfigConnectionString();

			var _db = new Rbr_Db();
			if (! _db.Exists(Configuration.Instance.Db.RbrDbNamePrefix + Configuration.Instance.Db.RbrDbVersion)) {
				_db.Create();
			}

			checkRbrDbConnection();
		}

		static void checkRbrDbConnection() {
			try {
				using (var _db = new Rbr_Db()) { }
			}
			catch (Exception _ex) {
				throw new Exception(string.Format("RbrDb connection is invalid, Exception:\r\n{0}", _ex));
			}
		}

		static void checkCdrDbConnection(DateTime pDate) {
			try {
				using (new Cdr_Db(pDate)) { }
			}
			catch (Exception _ex) {
				throw new Exception(string.Format("Cdr_Db.CheckConnection: Exception:\r\n{0}", _ex));
			}
		}
		
		static void validateCdrDb() {
			//TODO: Cdr_Db.CheckConfigConnectionString();

			if (! Cdr_Db.Exists(DateTime.Today)) {
				Cdr_Db.Create(DateTime.Today, 3);
			}

			checkCdrDbConnection(DateTime.Today);
			Cdr_Db.CheckCdrDependancy();
		}
	}
}