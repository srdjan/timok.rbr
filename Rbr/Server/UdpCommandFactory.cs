using System;
using Timok.Logger;
using Timok.NetworkLib.Udp;
using Timok.Rbr.Core;
using Timok.Rbr.Core.LogManagement;
using Timok.Rbr.Service;

namespace Timok.Rbr.Server {
	public class UdpCommandFactory : ICommandFactory {
		readonly IRbrDispatcher dispatcher;
		readonly CallStatistics callStatistics;
		public string APIName { get; private set; }

		public UdpCommandFactory(CallStatistics pCallStatistics) {
			callStatistics = pCallStatistics;
			dispatcher = new RbrDispatcher(callStatistics);
			APIName = RbrApi.Name;
		}

		//--------------------------------- Public methods -------------------------------------------------
		public ICommand GetCommand(string pFullRequest) {
			var _commandAndVersion = UdpMessageParser.GetCommandNameAndVersion(pFullRequest);
			ICommand _cmdInstance;
			switch (_commandAndVersion) {
				case RbrApi.OnGetNextSequence:
					_cmdInstance = (ICommand) new OnGetNextSequence(dispatcher);
					break;
				case RbrApi.OnGetCallStats:
					_cmdInstance = (ICommand) new OnGetCallStats(dispatcher);
					break;
				case LoggerAPI.SetLogSeverity:
					_cmdInstance = new SetLogSeverity(TimokLogger.Instance);
					break;
				case LoggerAPI.GetLogSeverity:
					_cmdInstance = new GetLogSeverity(TimokLogger.Instance);
					break;

				default:
					throw ( new ApplicationException("CommandFactory: Unknown command: " + pFullRequest) );
			}

			_cmdInstance.APIName = APIName;
			_cmdInstance.FullRequest = pFullRequest;
			_cmdInstance.NameAndVersion = _commandAndVersion;
			return _cmdInstance;
		}
	}
}