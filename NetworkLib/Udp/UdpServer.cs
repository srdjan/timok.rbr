using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Timok.Logger;

namespace Timok.NetworkLib.Udp {
	public class UdpServer {
		readonly ICommandFactory commandFactory;
		readonly Thread udpThread;

		bool started;
		bool stop;
		bool stopped;

		const int BUFFER_SIZE = 8192;
		const int RECEIVE_TIMEOUT = 5000;		//milisecs

		const string UDP_SERVER_DISPATCHER_LABEL = "UdpServer.dispatcher:";
		const string UDP_SERVER_RECEIVE_LABEL = "UdpServer.Receive:";
		const string UDP_SERVER_STOP_LABEL = "UdpServer.Stop:";
		const string UDP_SERVER_START_LABEL = "UdpServer.Start:";

		readonly LocalHost localHost;
		IOCPCustomThreadPool threadPool;
		LogDelegate log;

		public UdpServer(string pIpAddress, int pPort, ICommandFactory pCommandFactory, LogDelegate pLogDelegate) {
			log = pLogDelegate;
			try {
				localHost = new LocalHost(pIpAddress, pPort, RECEIVE_TIMEOUT);
				udpThread = new Thread(receive);
				started = false;
				stopped = false;
				stop = false;

				commandFactory = pCommandFactory;
			}
			catch {
				if (udpThread != null) {
					udpThread.Abort();
				}
				throw;
			}
		}

		public void Start() {
			if (started) {
				throw new Exception("UdpThread Already started.");
			}

			if (udpThread == null) {
				throw new Exception("UdpThread==null.");
			}

			threadPool = new IOCPCustomThreadPool(0, 25, 50, dispatcher);
			udpThread.Start();

			stopped = false;
			stop = false;
			started = true;
		}

		public void Stop() {
			started = false;
			stop = true;
			while (! stopped) {
				Thread.Sleep(500);
			}

			threadPool.Dispose();
		}

		//--------------------------Private methods------------------------------------------------------
		void receive() {
			var _receiveBuffer = new byte[BUFFER_SIZE];
			uint _ctxIndex = 0;

			//-- This value is not realy used. Remote end point is passed as reference, but we only care 
			//-- about the returned value, that represents the end point that sent the datagram/command
			EndPoint _remoteEP = new IPEndPoint(/*IPAddress.Parse("127.0.0.1")*/ IPAddress.Any, 0);

			stopped = false;
			while (! stop) {
				try {
					int _numberOfBytesReceived;
					try {
						_numberOfBytesReceived = localHost.ReceiveFrom(_receiveBuffer, ref _remoteEP);
					}
					catch (SocketException _se) {
						var _errorCode = (SocketError) _se.ErrorCode;
						if (_errorCode != SocketError.TimedOut) {
							log(LogSeverity.Critical, UDP_SERVER_RECEIVE_LABEL, string.Format("Socket error:\r\n{0}", _se));
						}
						continue;
					}

					//-- Convert raw bytes to string.
					var _dataReceived = Encoding.ASCII.GetString(_receiveBuffer, 0, _numberOfBytesReceived);

					_ctxIndex++;
					CmdContextCache.Save(_ctxIndex, new CmdContext(_dataReceived, _remoteEP));
					threadPool.PostEvent(_ctxIndex);
				}
				catch (Exception _ex) {
					log(LogSeverity.Critical, UDP_SERVER_RECEIVE_LABEL, string.Format("Exception:\r\n{0}", _ex));
				}
			}
			stopped = true;
		}

		// we are on our own thread from the IOCP thread pool:
		void dispatcher(uint pIndex) {
			CmdContext _cmdCtx = null;
			try {
				_cmdCtx = CmdContextCache.Get(pIndex);

				var _cmd = commandFactory.GetCommand(_cmdCtx.DataReceived);
				_cmd.Execute();
				_cmd.SendResponse(localHost, _cmdCtx.RmtEndPoint);
			}
			catch (Exception _ex) {
				log(LogSeverity.Critical, UDP_SERVER_DISPATCHER_LABEL, string.Format("Exception:\r\n{0}", _ex));
				if (_cmdCtx != null) {
					log(LogSeverity.Status, UDP_SERVER_DISPATCHER_LABEL, string.Format("Exception:\r\n{0}", _cmdCtx.DataReceived));
				}
			}
			finally {
				CmdContextCache.Remove(pIndex);
			}
		}
	}
}