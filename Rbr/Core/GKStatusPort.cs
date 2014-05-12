using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Timok.Logger;

namespace Timok.Rbr.Core {
	public class GkStatusPort : IDisposable {
		readonly IPEndPoint ipEndPoint;
		TcpClient tcpClient;
		NetworkStream netStream;
		StreamReader streamReader;
		const int SLEEP_TIME = 5000;
		LogDelegate log;
		const int DEFAULT_CONNECTION_TIMEOUT = 5;
		public bool IsConnected { get; set; }

		public GkStatusPort(LogDelegate pLog) {
			log = pLog;
			var _gkTelnetIp = IniFileIO.GetKey("Gatekeeper::Main", "Home", "127.0.0.1");
			var _gkTelnetPort = IniFileIO.GetKey("Gatekeeper::Main", "StatusPort", "7000");

			ipEndPoint = new IPEndPoint(IPAddress.Parse(_gkTelnetIp), int.Parse(_gkTelnetPort));
			connect(true, DEFAULT_CONNECTION_TIMEOUT);
		}

		public GkStatusPort(int pConnectionTimeout) {
			var _gkTelnetIp = IniFileIO.GetKey("Gatekeeper::Main", "Home", "127.0.0.1");
			var _gkTelnetPort = IniFileIO.GetKey("Gatekeeper::Main", "StatusPort", "7000");

			ipEndPoint = new IPEndPoint(IPAddress.Parse(_gkTelnetIp), int.Parse(_gkTelnetPort));
			connect(true, pConnectionTimeout);
		}

		public GkStatusPort(IPAddress pIPAddress, int pPort, int pConnectionTimeout) {
			ipEndPoint = new IPEndPoint(pIPAddress, pPort);
			connect(true, pConnectionTimeout);
		}

		public bool SendCommand(string pCmd, int pReceiveTimeout) {
			string _response;
			return SendCommand(pCmd, pReceiveTimeout, out _response);
		}
		
		public bool SendCommand(string pCmd, int pRecvTimeout, out string pResponse) {
			var _result = false;
			pResponse = string.Empty;
			
			try {
				//-- If not already connected, connect:
				connect(true, DEFAULT_CONNECTION_TIMEOUT);

				var _cmd = ASCIIEncoding.ASCII.GetBytes(pCmd + "\r\n");		
				netStream.Write(_cmd, 0, _cmd.Length);

				//-- wait for response
				var _count = 0;
				while ( ! netStream.DataAvailable && _count++ < pRecvTimeout / SLEEP_TIME) {
					Thread.Sleep(SLEEP_TIME);
				}

				//-- check if there is any response and if yes, read it
				if (netStream.DataAvailable || streamReader.Peek() >= 0) {
					var _response = new StringBuilder(streamReader.ReadLine());
					_response.Append("\r\n");
					while (streamReader.Peek() >= 0) {
						_response.Append(streamReader.ReadLine());
						_response.Append("\r\n");
					}
					pResponse = _response.ToString();
					_result = true;
					Debug.WriteLine("Response: \r\n" + _response);
				}
				else {
					log(LogSeverity.Error, "GkStatusPort.SendCommand", "Timeout !");				
				}
			}
			catch(Exception _ex) {
				log(LogSeverity.Critical, "GkStatusPort.SendCommand", string.Format("Exception:\r\n{0}", _ex));
				throw;
			}
			return _result;
		}

		public void Dispose() {
			if (IsConnected) {
				IsConnected = false;
				try {
					if (streamReader != null) streamReader.Close();
					if (netStream != null) netStream.Close();
					if (tcpClient != null) tcpClient.Close();
				}
				catch (Exception _ex) {
					log(LogSeverity.Critical, "GkStatusPort.Dispose", string.Format("Exception while disposing:\r\n{0}", _ex));
				}
				streamReader = null;
				netStream = null;
				tcpClient = null;
			}
		}

		//-----------------------------Private-----------------------------------------------------------------------------
		private void connect(bool pWaitForResponse, int pConnectionTimeout) {
			if(IsConnected) {
				return;
			}

			//-- set wait counter
			var _connectionTimeoutMiliSec = pConnectionTimeout * 1000;
			var _numberOfRetries = _connectionTimeoutMiliSec / SLEEP_TIME;
			byte _count = 0;

			//-- open connection
			while (tcpClient == null && _count++ < _numberOfRetries) {
				Thread.Sleep(SLEEP_TIME);
				tcpClient = new TcpClient(ipEndPoint.Address.ToString(), ipEndPoint.Port);
			}

			if (tcpClient == null) {
				throw new Exception(string.Format("Tcp Open Connection failed after {0} retries...", _numberOfRetries));	
			}

			//-- receive data
			netStream = tcpClient.GetStream();
			streamReader = new StreamReader(tcpClient.GetStream());
			if (pWaitForResponse) {
				while ( ! netStream.DataAvailable && _count++ < _numberOfRetries) {
					Thread.Sleep(SLEEP_TIME);
				}

				if (netStream.DataAvailable) {
					var _response = new StringBuilder(streamReader.ReadLine());
					while (streamReader.Peek() >= 0) {
						_response.Append(streamReader.ReadLine());
					}
					log(LogSeverity.Status, "GkStatusPort.connect", _response.ToString());
				}
			}

			IsConnected = true;
			return;
		}
	}
}
