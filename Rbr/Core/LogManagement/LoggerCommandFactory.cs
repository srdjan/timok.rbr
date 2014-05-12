using System;
using Timok.Logger;
using Timok.NetworkLib.Udp;

namespace Timok.Rbr.Core.LogManagement {
	public class LoggerCommandFactory : ICommandFactory {
		protected string apiName;
		public string APIName { get { return apiName; } set { apiName = value; } }
		readonly ILogger logger;

		public LoggerCommandFactory(ILogger pLogger) {
			logger = pLogger;
			apiName = LoggerAPI.Name;
		}

		public ICommand GetCommand(string pFullRequest) {
			string _commandAndVersion = UdpMessageParser.GetCommandNameAndVersion(pFullRequest);
			ICommand _cmdInstance;	
			switch (_commandAndVersion) {
			case LoggerAPI.SetLogSeverity:
				_cmdInstance = new SetLogSeverity(logger);
				break;
			case LoggerAPI.GetLogSeverity:
				_cmdInstance = new GetLogSeverity(logger);
				break;
			default:
				throw ( new ApplicationException("Unknown command: " + _commandAndVersion) );
			}

			_cmdInstance.APIName = apiName;
			_cmdInstance.FullRequest = pFullRequest;
			_cmdInstance.NameAndVersion = _commandAndVersion;
			return _cmdInstance;
		}
	}
}