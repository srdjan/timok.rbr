using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Timok.Rbr.Core {
	public class GkStatusPortEx : IDisposable {
		readonly IPEndPoint ipEndPoint;
		TcpClient tcpClient;
		NetworkStream netStream;
		StreamReader streamReader;

		readonly int connectSleepTimeCounter;
		readonly int receiveSleepTimeCounter;

		const int SLEEP_TIME_SLICE = 1000;

		/// <summary>
		/// opens telnet connection
		/// </summary>
		/// <param name="pConnectTimeout">seconds</param>
		/// <param name="pReceiveTimeout">seconds</param>
		public GkStatusPortEx(int pConnectTimeout, int pReceiveTimeout) {
			var _gkTelnetIp = IniFileIO.GetKey("Gatekeeper::Main", "Home", "127.0.0.1");
			var _gkTelnetPort = IniFileIO.GetKey("Gatekeeper::Main", "StatusPort", "7000");
			ipEndPoint = new IPEndPoint(IPAddress.Parse(_gkTelnetIp), int.Parse(_gkTelnetPort));

			connectSleepTimeCounter = (pConnectTimeout * 1000) / SLEEP_TIME_SLICE;
			receiveSleepTimeCounter = (pReceiveTimeout * 1000) / SLEEP_TIME_SLICE;

			connect();
		}

		public void SendCommand(string pCmd) {
			SendCommand(pCmd, string.Empty);
		}

		/// <summary>
		/// sends a command and if pExpectedResponse != null expects a one line response,
		/// that will be comared to pExpectedResponse
		/// if pExpectedResponse.Length == 0 response will NOT be checked against it, but buffer will be red
		/// </summary>
		/// <param name="pCmd"></param>
		/// <param name="pExpectedResponse"></param>
		/// <returns></returns>
		public void SendCommand(string pCmd, string pExpectedResponse) {
			if (pExpectedResponse == null) {
				throw new Exception(string.Format("GkStatusPortEx.SendCommand[{0}:{1}] | Argument pExpectedResponse is required", ipEndPoint.Address, ipEndPoint.Port));
			}

			var _cmd = Encoding.ASCII.GetBytes(pCmd + "\r\n");
			netStream.Write(_cmd, 0, _cmd.Length);

			//-- wait for response
			var _count = 0;
			while (! netStream.DataAvailable) {
				if (_count++ < receiveSleepTimeCounter) {
					Thread.Sleep(SLEEP_TIME_SLICE);
					continue;
				}
				break;
			}
			if (! netStream.DataAvailable) {
				throw new Exception(string.Format("GKStatusPortEx.SendCommand[{0}:{1}] | Timeout receiving!", ipEndPoint.Address, ipEndPoint.Port));
			}
			var _response = streamReader.ReadLine();

			if (pExpectedResponse.Length > 0) {
				if (pExpectedResponse.CompareTo(_response) != 0) {
					throw new Exception(string.Format("GKStatusPortEx.SendCommand[{0}:{1}] | Unexpected response", ipEndPoint.Address, ipEndPoint.Port));
				}
			}
		}

		public void Dispose() {
			try {
				if (streamReader != null) {
					streamReader.Close();
				}
				if (netStream != null) {
					netStream.Close();
				}
				if (tcpClient != null) {
					tcpClient.Close();
				}
			}
			catch {}
			streamReader = null;
			netStream = null;
			tcpClient = null;
		}

		//-----------------------------Private-----------------------------------------------------------------------------
		void connect() {
			tcpClient = new TcpClient(ipEndPoint.Address.ToString(), ipEndPoint.Port);
			netStream = tcpClient.GetStream();
			streamReader = new StreamReader(tcpClient.GetStream());

			var _count = 0;
			while (! netStream.DataAvailable) {
				if (_count++ < connectSleepTimeCounter) {
					Thread.Sleep(SLEEP_TIME_SLICE);
					continue;
				}
				break;
			}

			if (! netStream.DataAvailable) {
				throw new Exception(string.Format("GKStatusPortEx.connect[{0}:{1}] | Timeout connecting", ipEndPoint.Address, ipEndPoint.Port));
			}

			var _response = new StringBuilder(streamReader.ReadLine());
			while (streamReader.Peek() >= 0) {
				_response.Append(streamReader.ReadLine());
			}
		}
	}
}