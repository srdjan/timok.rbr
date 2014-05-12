using System;
using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class CdrExportMapDto {
		int mapId;
		public int MapId { get { return mapId; } set { mapId = value; } }

		string name;
		public string Name { get { return name; } set { name = value; } }

		CdrExportDelimeter cdrExportDelimeter;
		public CdrExportDelimeter CdrExportDelimeter { get { return cdrExportDelimeter; } set { cdrExportDelimeter = value; } }

		public CdrExportMapDetailDto[] CdrExportMapDetails { get; set; }

		//NOTE: not in use for now
		//string targetDestFolder;

		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			return mapId.ToString().GetHashCode();
		}

		public override string ToString() {
			return name + "  [" + cdrExportDelimeter + " - delimited]";
		}
	}
}