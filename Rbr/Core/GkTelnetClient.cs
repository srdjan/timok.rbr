using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Timok.Logger;

namespace Timok.Rbr.Core {
	public delegate void StatusDelegate(string pMsg);

	public delegate void DataReceivedDelegate(string pData);

	public delegate void ConnectedDelegate();

	public delegate void DisconnectedDelegate();

	public class GkTelnetClient {
		TcpClient tcpClient;
		NetworkStream netStream;
		StreamReader readStream;
		bool doReceive;

		readonly IPEndPoint gkIPEndPoint;
		TimeSpan connTimeout;
		bool isConnected = false;

		public event StatusDelegate Status;
		public event DataReceivedDelegate DataReceived;
		public event ConnectedDelegate Connected;
		public event DisconnectedDelegate Disconnected;
		LogDelegate log;

		public GkTelnetClient(IPAddress pIP, int pPort, TimeSpan pConnTimeout, LogDelegate pLog) {
			gkIPEndPoint = new IPEndPoint(pIP, pPort);
			connTimeout = pConnTimeout;
			log = pLog;
		}

		void onStatus(string pMsg) {
			Debug.WriteLine("GkTelnetClient.onStatus | " + pMsg);
			if (Status != null) {
				try {
					Status(pMsg);
				}
				catch {}
			}
		}

		void onDataReceived(string pData) {
			Debug.WriteLine("GkTelnetClient.onDataReceived | " + pData);
			if (DataReceived != null) {
				try {
					DataReceived(pData);
				}
				catch {}
			}
		}

		void onConnected() {
			Debug.WriteLine("GkTelnetClient.onConnected | Connected.");
			if (Connected != null) {
				try {
					Connected();
				}
				catch {}
			}
		}

		void onDisconnected() {
			Debug.WriteLine("GkTelnetClient.onDisconnected | Disconnected.");
			if (Disconnected != null) {
				try {
					Disconnected();
				}
				catch {}
			}
		}

		public void OpenSession(string pAfterConnectCommand) {
			try {
				log(LogSeverity.Status, "GkTelnetClient.OpenSession", "Opening...");

				var _startTime = DateTime.Now;
				onStatus("attempting to connect to Gk [" + gkIPEndPoint + "] for " + connTimeout.Seconds + " seconds ...");
				while (! connect()) {
					if (_startTime.Add(connTimeout) < DateTime.Now) {
						var msg = "Unable to connection to host [" + gkIPEndPoint + "]; timeout [" +
						          connTimeout.Seconds + " seconds].\r\n";
						onStatus(msg);
						throw new ApplicationException(msg);
					}
				}
				doReceive = true;
				//sendCommand("trace 2");
				if (pAfterConnectCommand != null && pAfterConnectCommand.Trim().Length > 0) {
					log(LogSeverity.Status, "GkTelnetClient.OpenSession", string.Format("Sending: {0} command.", pAfterConnectCommand));
					sendCommand(pAfterConnectCommand);
				}
				receive();
			}
			catch (ThreadAbortException _tex) {
				log(LogSeverity.Error, "GktelnetClient.OpenSession", string.Format("GkTelnetClient.OpenSession, Exception1:\r\n{0}", _tex));
			}
			catch (Exception _ex) {
				log(LogSeverity.Error, "GktelnetClient.OpenSession", string.Format("GkTelnetClient.OpenSession, Exception2:\r\n{0}", _ex));
			}
			finally {
				if (netStream != null) {
					netStream.Close();
				}
				if (readStream != null) {
					readStream.Close();
				}
				if (tcpClient != null) {
					tcpClient.Close();
				}
				netStream = null;
				readStream = null;
				tcpClient = null;
				onDisconnected();
			}
		}

		public void CloseSession() {
			try {
				log(LogSeverity.Status, "GkTelnetClient.CloseSession", "Closing...");
				doReceive = false;
			}
			catch (Exception _ex) {
				log(LogSeverity.Error, "GktelnetClient.loseSession", string.Format("GkTelnetClient.CloseSession, Exception:\r\n{0}", _ex));
			}
		}

		public bool SendCommand(string pCommand) {
			if (isConnected && netStream.CanWrite) {
				var _cmdBytes = ASCIIEncoding.ASCII.GetBytes(pCommand + "\r\n\r\n"); //double \r\n for securing response from Gk (gk bug?) 
				netStream.Write(_cmdBytes, 0, _cmdBytes.Length);
				return true;
			}
			return false;
		}

		bool connect() {
			try {
				tcpClient = new TcpClient(gkIPEndPoint.Address.ToString(), gkIPEndPoint.Port);
				netStream = tcpClient.GetStream();
				readStream = new StreamReader(tcpClient.GetStream());
				isConnected = true;
				onStatus(string.Format("Connected to Gk:  [{0}] .", gkIPEndPoint));
				onConnected();
			}
			catch (Exception _ex) {
				log(LogSeverity.Error, "GktelnetClient.connect", string.Format("GkTelnetClient.connect, Exception:\r\n{0}", _ex));
			}
			return isConnected;
		}

		void sendCommand(string pCmd) {
			if (!isConnected) {
				log(LogSeverity.Error, "GktelnetClient.sendCommand", "Error: Not connected");
				return;
			}
			var cmd = ASCIIEncoding.ASCII.GetBytes(pCmd + "\r\n\r\n"); //double \r\n for securing response from Gk (gk bug?) 
			netStream.Write(cmd, 0, cmd.Length);
		}

		void disconnect() {
			if (isConnected) {
				isConnected = false;
				try {
					if (readStream != null) {
						readStream.Close();
					}
					if (netStream != null) {
						netStream.Close();
					}
					if (tcpClient != null) {
						tcpClient.Close();
					}
				}
				catch {}
				readStream = null;
				netStream = null;
				tcpClient = null;
				onStatus("Disconnected from Gk:  [" + gkIPEndPoint + "] .");
			}
			onDisconnected();
		}

		void receive() {
			try {
				while (doReceive) {
					if (! isConnected || ! netStream.CanRead) {
						break;
					}

					string _data;
					_data = readStream.ReadLine();
					//_data = readStream.ReadToEnd();
					if (_data == null) {
						log(LogSeverity.Status, "GkTelnetClient.receive", "The host dropped the connection");
						doReceive = false;
						continue;
						//throw new ApplicationException("The host dropped the connection.");
					}

					//char[] _charArray = _data.ToCharArray();
					char[] _zeroChar = {'\0'};
					_data.Trim(_zeroChar);
					_data.Replace("\r", "\r\n");

					onDataReceived(_data);
				}
			}
			catch (IOException) {
				log(LogSeverity.Status, "GkTelnetClient.receive", "Stream Closed...");
			}
			catch (Exception _ex) {
				log(LogSeverity.Status, "GkTelnetClient.receive", string.Format("Exception:\r\n{0}", _ex));
			}
			finally {
				log(LogSeverity.Status, "GkTelnetClient.receive", "GkTelnetClient Stopped Receiving");
				disconnect();
			}
		}
	}
}