using System;
using System.Net;
using System.Net.Sockets;

namespace Timok.NetworkLib.Udp {
	public class LocalHost {
		readonly string ip;
		readonly int port;

		readonly Socket udpSocket;

		public LocalHost(string pIpAddress, int pPort, int pReceiveTimeout) {
			ip = pIpAddress;
			port = pPort;

			var _ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
			udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			udpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, pReceiveTimeout);

			var IOC_IN = 0x80000000;
			var IOC_VENDOR = 0x18000000;
			var SIO_UDP_CONNRESET = (uint) (IOC_IN | IOC_VENDOR | 12);
			udpSocket.IOControl((int) SIO_UDP_CONNRESET, new byte[] {Convert.ToByte(false)}, null);

			udpSocket.Bind(_ipEndPoint);
		}

		public int ReceiveFrom(byte[] pBuffer, ref EndPoint pRemoteEndPoint) {
			return udpSocket.ReceiveFrom(pBuffer, ref pRemoteEndPoint);
		}

		public void SendTo(byte[] pBuffer, EndPoint pRemoteEndPoint) {
			udpSocket.SendTo(pBuffer, pRemoteEndPoint);
		}
	}
}