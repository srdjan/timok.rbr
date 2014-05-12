using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Timok.Logger;

namespace Timok.NetworkLib.Udp {
	/// <summary> Used to send commands and receive (with timeout) responses</summary>
	public class UdpClient {
		const int BUFFER_SIZE = 8192;
		const int RECEIVE_TIMEOUT = 5000;

		static int sequence;

		readonly int initialPort;
		readonly string ipAddress;
		readonly int portRange;
		readonly EndPoint epServer;
		readonly IPEndPoint server;
		LogDelegate log;

		/// <summary>static constructor</summary>
		static UdpClient() {
			sequence = 0;
		}

		/// <summary>constructor</summary>
		public UdpClient(string pClientIPAddress, int pInitialClientPortNumber, int pClientPortRange, string pServerAddress, int pServerPort, LogDelegate pLog) {
			ipAddress = pClientIPAddress;
			initialPort = pInitialClientPortNumber; //Problem with multiple apps using this client at the same time?
			portRange = pClientPortRange;
			log = pLog;

			//-- Create well known Server EndPoint:
			server = new IPEndPoint(IPAddress.Parse(pServerAddress), pServerPort);
			epServer = server;
		}

		//-------------------Public methods-----------------------------------------------
		public bool SendAndReceive(string pAPIName, string pCmdAndVersion, string[] pParameters, out string pResponse) {
			pResponse = string.Empty;

			Socket _udpSocket = null;
			try {
				//-- prepare cmd message for sending:
				var _sendSequence = Interlocked.Increment(ref sequence);
				var _request = prepare(pAPIName, pCmdAndVersion, _sendSequence, pParameters);

				//-- CreateSocket for each message, that allows for sending multiple messages at the same time.
				_udpSocket = createUDPSocket();

				//-- SetForSending and Receive 
				if (send(_request, _udpSocket, epServer)) {
					pResponse = receive(_udpSocket, epServer);
				}

				//-- check if returned sequence equal to what was sent:
				if (!string.IsNullOrEmpty(pResponse)) {
					var _receiveSequence = UdpMessageParser.GetSequence(pResponse);
					if (_sendSequence != _receiveSequence) {
						log(LogSeverity.Critical, "UdpClient.SendAndReceive", string.Format("Cmd Sequence received={0} != then sent={1}", _receiveSequence.ToString("D6"), _sendSequence.ToString("D6")));
						return false;
					}
					log(LogSeverity.Debug, "UdpClient.SendAndReceive", string.Format("{0}", pResponse));
				}
			}
			catch (SocketException _ex) {
				log(LogSeverity.Critical, "UdpClient.SendAndReceive", string.Format("SocketException:\r\n{0}", _ex));
				return false;
			}
			catch (Exception _ex) {
				log(LogSeverity.Critical, "UdpClient.SendAndReceive", string.Format("Exception:\r\n{0}", _ex));
				return false;
			}
			finally {
				if (_udpSocket != null) {
					_udpSocket.Close();
				}
			}
			return true;
		}

		public bool Send(string pAPIName, string pCmdAndVersion, string[] pParameters) {
			Socket _udpSocket = null;
			try {
				//-- prepare cmd message for sending:
				var _sendSequence = Interlocked.Increment(ref sequence);
				var _request = prepare(pAPIName, pCmdAndVersion, _sendSequence, pParameters);

				//-- CreateSocket for each message, that allows for sending multiple messages at the same time.
				_udpSocket = createUDPSocket();

				//-- SetForSending and Receive 
				if (! send(_request, _udpSocket, epServer)) {
					log(LogSeverity.Critical, "UdpClient.Send", string.Format("Error sending Seq={0}", _sendSequence.ToString("D6")));
				}
			}
			catch (SocketException _ex) {
				log(LogSeverity.Critical, "UdpClient.Send", string.Format("SocketException:\r\n{0}", _ex));
				return false;
			}
			catch (Exception _ex) {
				log(LogSeverity.Critical, "UdpClient.Send", string.Format("Exception:\r\n{0}", _ex));
				return false;
			}
			finally {
				if (_udpSocket != null) {
					_udpSocket.Close();
				}
			}
			return true;
		}

		//----------------- Private ------------------------------------------------------------------------------------------
		bool send(string pRequest, Socket pUdpSocket, EndPoint pServer) {
			//-- convert to byte array:
			var _sendBuffer = Encoding.ASCII.GetBytes(pRequest.ToCharArray());

			try {
				var _numberOfBytes = pUdpSocket.SendTo(_sendBuffer, pRequest.Length, SocketFlags.None, pServer);
				if (_numberOfBytes <= 0) {
					log(LogSeverity.Critical, "UdpClient.send", string.Format("Error: cannot send Packet={0}", _numberOfBytes));
					return false;
				}
			}
			catch (SocketException _se) {
				var _errorCode = (SocketError) _se.ErrorCode;
				switch (_errorCode) {
				case SocketError.TimedOut:
					log(LogSeverity.Critical, "UdpClient.send", "Connection Timeout");
					break;
				case SocketError.ConnectionReset:
					log(LogSeverity.Critical, "UdpClient.send", "Connection Reset by Server");
					break;
				default:
					log(LogSeverity.Critical, "UdpClient.send", string.Format("Socket Exception:\r\n{0}", _se));
					break;
				}
				return false;
			}
			return true;
		}

		string receive(Socket pUDPSocket, EndPoint pServer) {
			int _numberOfBytes;
			var _receiveBuffer = new byte[BUFFER_SIZE];

			try {
				_numberOfBytes = pUDPSocket.ReceiveFrom(_receiveBuffer, ref pServer);
				if (_numberOfBytes <= 0) {
					log(LogSeverity.Critical, "UdpClient.receive", string.Format("Host not Responding={0}", _numberOfBytes));
					return string.Empty;
				}
			}
			catch (SocketException _se) {
				var _errorCode = (SocketError) _se.ErrorCode;
				switch (_errorCode) {
				case SocketError.TimedOut:
					log(LogSeverity.Critical, "UdpClient.receive", "Connection Timeout");
					break;
				case SocketError.ConnectionReset:
					log(LogSeverity.Critical, "UdpClient.receive", "Connection Reset by Server");
					break;
				default:
					log(LogSeverity.Critical, "UdpClient.receive", string.Format("Socket Exception:\r\n{0}", _se));
					break;
				}
				return string.Empty;
			}
			//-- encode response
			return Encoding.ASCII.GetString(_receiveBuffer, 0, _numberOfBytes);
		}

		string prepare(string pAPIAndVersion, string pCmd, int pSequence, string[] pParameters) {
			var _msg = new StringBuilder("cmd.");

			try {
				_msg.Append(pSequence.ToString("D6"));
				_msg.Append(':');
				_msg.Append(pAPIAndVersion);
				_msg.Append('.');
				_msg.Append(pCmd); //name.version
				_msg.Append('(');
				for (var _i = 0; _i < pParameters.Length; _i++) {
					_msg.Append(pParameters[_i]);

					//don't add IN_DELIMETER for the last field:
					if (_i == (pParameters.Length - 1)) {
						break;
					}
					_msg.Append(UdpMessageParser.IN_DELIMETER);
				}
				_msg.Append(')');
				log(LogSeverity.Debug, "UdpClient.prepare", _msg.ToString());
			}
			catch (Exception _ex) {
				log(LogSeverity.Critical, "UdpClient.prepare", string.Format("Exception:\r\n{0}", _ex));
				_msg.Remove(0, _msg.Length);
			}
			return _msg.ToString();
		}

		Socket createUDPSocket() {
			var _port = initialPort + sequence % portRange;
			var _ipEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), _port);
			EndPoint _endPoint = _ipEndPoint;

			var _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			_socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, RECEIVE_TIMEOUT);
			_socket.Bind(_endPoint);
			//_socket.Connect(epServer);
			return _socket;
		}
	}
}