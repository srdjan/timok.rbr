using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Timok.NetworkLib.Tcp {
	public class TcpLibClient : IChannel {
		readonly TimeSpan connTimeout;
		readonly int receiveTimeout;
		readonly IPEndPoint serverIPEndPoint;
		NetworkStream netStream;
		TcpClient tcpClient;

		public TcpLibClient(IPAddress pIP, int pPort, TimeSpan pConnTimeout, int pReceiveTimeout) {
			IsConnected = false;
			serverIPEndPoint = new IPEndPoint(pIP, pPort);
			connTimeout = pConnTimeout;
			receiveTimeout = pReceiveTimeout;
		}

		public bool IsConnected { get; private set; }

		public void Send(string pRequest) {
			//-- try connecting - if not connected (can throw: ConnectionTimeoutException)
			if (! IsConnected) {
				connect();
			}

			//-- encode and send
			var _result = false;
			try {
				var _msg = Encoding.ASCII.GetBytes(pRequest);
				netStream.Write(_msg, 0, _msg.Length);
				_result = true;
			}
			catch (IOException _ioex) {
				if (_ioex.InnerException != null && _ioex.InnerException is SocketException) {
					throw new Exception("TcpLibClient.SetForSending | Socket Exception: " + _ioex.InnerException.Message + ", ErrorCode: " + ((SocketException) _ioex.InnerException).ErrorCode, _ioex);
				}
				throw new Exception("TcpLibClient.SetForSending | IOException: " + _ioex, _ioex);
			}
			catch (Exception _ex) {
				throw new Exception("TcpLibClient.SetForSending | Exception: " + _ex, _ex);
			}
			finally {
				if (! _result) {
					disconnect();
				}
			}
		}

		public string Receive(int pLength) {
			var _response = string.Empty;

			try {
				var _data = new byte[pLength];
				var _bytes = netStream.Read(_data, 0, _data.Length);
				return _response = Encoding.ASCII.GetString(_data, 0, _bytes);
			}
			catch (IOException _ioex) {
				if (_ioex.InnerException != null && _ioex.InnerException is SocketException) {
					throw new Exception("TcpLibClient.Receive | Socket Exception: " + _ioex.InnerException.Message + ", ErrorCode: " + ((SocketException) _ioex.InnerException).ErrorCode, _ioex);
				}
				throw new Exception("TcpLibClient.Receive | IOException: " + _ioex, _ioex);
			}
			catch (Exception _ex) {
				throw new Exception("TcpLibClient.Receive | Exception: " + _ex, _ex);
			}
			finally {
				if (_response.Length == 0) {
					disconnect();
				}
			}
		}

		//-------------------------------Private methods-----------------------------
		void disconnect() {
			try {
				if (netStream != null) netStream.Close();
				if (tcpClient != null) tcpClient.Close();
			}
			finally {
				IsConnected = false;
				tcpClient = null;
			}
		}

		void connect() {
			var _startTime = DateTime.Now;

			while (! IsConnected) {
				if (_startTime.Add(connTimeout) < DateTime.Now) {
					disconnect();
					throw (new ConnectionTimeoutException("TcpLibClient.connect | Connection Timout [host:" + serverIPEndPoint + "]"));
				}

				tcpClient = new TcpClient(serverIPEndPoint.Address.ToString(), serverIPEndPoint.Port);
				tcpClient.ReceiveTimeout = receiveTimeout;
				netStream = tcpClient.GetStream();
				IsConnected = true;
			}
		}
	}
}