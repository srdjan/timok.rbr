using System;
using Timok.Logger;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DOM;
using ScriptLanguage=Timok.Rbr.Core.Config.ScriptLanguage;

namespace Timok.Rbr.BLL.DOM {
	public sealed class AccessNumber {
		readonly AccessNumberListRow accessNumberRow;

		public long Number { get; private set; }

		readonly SurchargeInfo surchargeInfo;
		public SurchargeInfo SurchargeInfo { get { return surchargeInfo; } }

		public ScriptLanguage ScriptLanguage { get { return accessNumberRow.ScriptLanguage; } }
		public ScriptType ScriptType { get { return accessNumberRow.ScriptType; } }

		public AccessNumber(AccessNumberListRow pAccessNumberRow) {
			accessNumberRow = pAccessNumberRow;
			Number = accessNumberRow.Access_number; 
			surchargeInfo = new SurchargeInfo(pAccessNumberRow.Surcharge, pAccessNumberRow.SurchargeType);
		}

		//TODO: bad design, second ctor would not set accessNumberRow!!
		public AccessNumber(string pAccessNumber) {
			if (pAccessNumber == null) {
				throw new Exception("AccessNumber.Ctor: AccessNumber = null");
			}
			if (pAccessNumber.Length == 0) {
				throw new Exception("AccessNumber.Ctor: AccessNumber.Length = 0");
			}

			if (pAccessNumber.Length == 11) {
				if (pAccessNumber[0] != '1') {
					throw new Exception(string.Format("AccessNumber.Ctor: AccessNumber length == 11, but NOT 1+, {0}", pAccessNumber));
				}
				pAccessNumber = pAccessNumber.Substring(1);
			}
			else if (pAccessNumber.Length != 10) {
				throw new Exception(string.Format("AccessNumber.Ctor: AccessNumber length != 10 and != 11, {0}", pAccessNumber));
			}

			if (!Timok.Core.Utils.IsNumeric(pAccessNumber)) {
				throw new Exception(string.Format("AccessNumber.Ctor: AccessNumber invalid, {0}", pAccessNumber));
			}

			Number = parse(pAccessNumber);
		}

		long parse(string pAccessNumber) {
			long _accessNumber;
			long.TryParse(pAccessNumber, out _accessNumber);
			if (_accessNumber == 0) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "AccessNumber.Parse", string.Format("Invalid AccessNumber={0}", pAccessNumber));
			}
			return _accessNumber;
		}
	}
}