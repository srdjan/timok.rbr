using System;
using System.Text.RegularExpressions;

namespace Timok.Rbr.DAL {

	public class ConnectionStringInfo {
		private ConnectionStringInfo() { }

		public static string GetInstanceName (string pConnStr) {
			Regex _parseServer = new Regex (@"(?i:((Data\sSource)|(Server)|(Address)|(Addr)|(Network Address))=(?<server>[^;]+))", RegexOptions.Compiled);
			string _instName = _parseServer.Match (pConnStr).Groups["server"].Value;
			return _instName;
		}

		public static string GetDBName (string pConnStr) {
			Regex _parseDatabase = new Regex (@"(?i:((Initial\sCatalog)|(Database))=(?<database>[^;]+))", RegexOptions.Compiled);
			string _dbName = _parseDatabase.Match (pConnStr).Groups["database"].Value;
			return _dbName;
		}

	}
}
