using System;
using Timok.Logger;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Server {
	public class OnGetCallStats : UdpCommand {

		CallStatsType callStatsType;
		short key;

		public OnGetCallStats(IRbrDispatcher pDispatcher) : base() {
			Dispatcher = pDispatcher;
			nameAndVersion = RbrApi.OnGetCallStats;
		}

		protected override void execute() {
			try {
				int _type;
				int.TryParse(parameters[0], out _type);
				callStatsType = (CallStatsType) _type;
				
				short.TryParse(parameters[1], out key);

				CallStats _callStats;
				if (callStatsType == CallStatsType.Total) {
					_callStats = Dispatcher.GetCallStats();
				}
				else if (callStatsType == CallStatsType.Customer) {
					_callStats = Dispatcher.GetCustomerCallStats(key);
				}
				else if (callStatsType == CallStatsType.Carrier) {
					_callStats = Dispatcher.GetCarrierCallStats(key);
				}
				else {
					throw new ArgumentException("Unknown CallStats Type: " + callStatsType);
				}
				
				AddResponseField("true", false);
				AddResponseField(_callStats.Startup, false);
				AddResponseField(_callStats.Running, false);

				AddResponseField(_callStats.TotalCalls.ToString(), false);
				AddResponseField(_callStats.TotalSuccessfullCalls.ToString(), false);

				AddResponseField(_callStats.OpenCalls.ToString(), false);
				AddResponseField(_callStats.ConnectingCalls.ToString(), false);
				AddResponseField(_callStats.ConnectedCalls.ToString(), false);
				AddResponseField(_callStats.TotalCurrentCalls.ToString(), true);
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "OnGetCallStats.Execute:", string.Format("Exception:\r\n{0}", _ex));
				SetResponseToException((int) RbrResult.ExceptionThrown);
			}
		}
		
		//------------------------------------------  Private  ----------------------------------------
		/// <summary> Call Setup </summary>
		private void SetResponseToException(int pErrorCode) {
			AddResponseField(pErrorCode.ToString(), false);
			AddResponseField("", false);	
			AddResponseField("", false);	
			AddResponseField("0", false);	
			AddResponseField("0", false);	
			AddResponseField("0", false);	
			AddResponseField("0", true);	
		}
	}
}
