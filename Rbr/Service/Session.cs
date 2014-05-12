using System;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Service {
	internal class Session : ISession {
		public string Id { get; set; }
		public string CallId { get; set; }
		public ISessionChannel Channel { get; set; }
		public SessionState State { get; set; }
		public string OrigIPAddress { get; set; }
		public string ANI { get; set; }
		public string AccessNumber { get; set; }
		public byte InfoDigits { get; set; }
		public string DestNumber { get; set; }
		public string CardNumber { get; set; }
		public short CustomerAcctId { get; set; }

		public Session(ISessionChannel pSessionChannel) {
			//State = SessionState.Leg1;

			Channel = pSessionChannel;
			CallId = pSessionChannel.CallId;
			OrigIPAddress = pSessionChannel.OrigIPAddress;

			parseAccessNumber(pSessionChannel.AccessNumber);

			getInfoDigits();
			if (InfoDigits > 0) {
				AccessNumber = AccessNumber.Substring(0, 10);
			}

			ANI = pSessionChannel.ANI;
			Id = Guid.NewGuid().ToString("N");
		}

		//----------------------------- Private ------------------------------------------------
		//- AccessNumber can have following components:
		//- pure access number --> retail, or wholesale (if access number is not found in AccessNumber table
		//- accessNumber + '#' + destNumber --> LD script
		//- prefix + '#' + destNumber --> wholesale
		void parseAccessNumber(string pAccessNumberIn) {
			var _dnisAndDestNumber = pAccessNumberIn.Split('#');
			if (_dnisAndDestNumber == null || _dnisAndDestNumber.Length == 0) {
				throw new Exception(string.Format("Invalid DNIS number: {0}", pAccessNumberIn));
			}
			
			//-- it can be either AccessNumber (if retail call) or destination number (if wholesale call)
			if (_dnisAndDestNumber.Length == 1) {
				AccessNumber = _dnisAndDestNumber[0];
				DestNumber = _dnisAndDestNumber[0];
				return;	
			}

			if (_dnisAndDestNumber.Length == 2) {
				if (_dnisAndDestNumber[0].Length == 11) {
					AccessNumber = _dnisAndDestNumber[0];
					DestNumber = _dnisAndDestNumber[1];
				}
				else if (_dnisAndDestNumber[0].Length < 8) {
					AccessNumber = string.Empty;
					DestNumber = pAccessNumberIn;
				}
				else {
					throw new Exception(string.Format("Invalid DNIS number couple: {0}", pAccessNumberIn));
				}
			}
			else {
				throw new Exception(string.Format("Invalid DNIS number combo: {0}", pAccessNumberIn));
			}
		}

		void getInfoDigits() {
			InfoDigits = 0;
			if (AccessNumber.Length == 12) {
				var _infoDigitsStr = AccessNumber.Substring(10, 2);
				InfoDigits = byte.Parse(_infoDigitsStr);
			}
		}
	}
}