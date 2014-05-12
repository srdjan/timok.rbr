using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;

namespace Timok.Core {
	public class Cloner {
		private Cloner() {}

		// Create a "deep" clone of an object. That is, copy not only the object and its pointers
		// to other objects, but create copies of all the subsidiary objects as well. This code even 
		// handles recursive relationships.
		public static object Clone(object pObj) {
			if (pObj == null) {
				return null;
			}
			object _objResult;
			using (var _ms = new MemoryStream()) {
				var _bf = new BinaryFormatter();
				_bf.Serialize(_ms, pObj);

				// Rewind back to the beginning of the memory stream. Deserialize the data, and return cloned object.
				_ms.Position = 0;
				_objResult = _bf.Deserialize(_ms);
			}
			return _objResult;
		}

		public static string CloneToString(object pObj) {
			if (pObj == null) {
				return string.Empty;
			}

			using (var _ms = new MemoryStream()) {
				var _f = new SoapFormatter();
				_f.Serialize(_ms, pObj);
				return Encoding.ASCII.GetString(_ms.ToArray());
			}
		}

		public static object CloneFromString(string pString) {
			if (pString == null || pString.Length == 0) {
				return null;
			}

			using (var _ms = new MemoryStream(Encoding.ASCII.GetBytes(pString))) {
				var _f = new SoapFormatter();
				_ms.Position = 0;
				return _f.Deserialize(_ms);
			}
		}
	}
}