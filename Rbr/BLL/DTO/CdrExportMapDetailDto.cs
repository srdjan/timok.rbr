using System;
using Timok.Core;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class CdrExportMapDetailDto {
		int mapDetailId;
		public int MapDetailId { get { return mapDetailId; } set { mapDetailId = value; } }

		int mapId;
		public int MapId { get { return mapId; } set { mapId = value; } }

		int sequence;
		public int Sequence { get { return sequence; } set { sequence = value; } }

		string fieldName;
		public string FieldName { get { return fieldName; } set { fieldName = value; } }

		string formatType;
		public string FormatType { get { return formatType; } set { formatType = value; } }

		// DefaultDate =			"Default Date: YYYYMMDD";
		// DefaultTime =			"Default Time: HHmmss0";
		// DateTimeDefault =  "";
		// StandardDate =			"Standard Date: YYYY-MM-DD";
		// StandardTime =			"Standard Time: HH:mm:ss";
		// StandardDateTime =	"Standard Date and Time: YYYY-MM-DD HH:mm:ss";
		public string DateTimeFormat {
			get {
				if (formatType.Contains("Default Date:")) {
					return "yyyyMMdd";
				}
				if (formatType.Contains("Default Time:")) {
					return "HHmmss0";
				}
				if (formatType.Contains("Standard Date:")) {
					return "yyyy-MM-dd";
				}
				if (formatType.Contains("Standard Time:")) {
					return "HH:mm:ss";
				}
				if (formatType.Contains("Standard Date and Time:")) {
					return "yyyy-MM-dd HH:mm:ss";
				}
				throw new Exception("Bad Usage");
			}
		}

		public bool IsDateTimeField {
			get {
				if (fieldName == "start" || fieldName == "date_logged") {
					return true;
				}
				return false;
			}
		}

		public CdrExportMapDetailDto() { }

		public override string ToString() {
			if (formatType != null && formatType.Trim().Length > 0) {
				return fieldName + "  [" + formatType + "]";
			}
			else {
				return fieldName;
			}
		}

		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			return string.Concat(mapDetailId, "_", mapId).GetHashCode();
		}
	}
}