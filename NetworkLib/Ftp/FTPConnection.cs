using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Timok.Core.NetworkLib.Ftp {
	/// <summary>
	/// Summary description for FTPConnection.
	/// </summary>
	public class FTPConnection {
		static int BLOCK_SIZE = 512;
		static int DATA_PORT_RANGE_FROM = 1500;
		static int DATA_PORT_RANGE_TO = 65000;
		static int DEFAULT_REMOTE_PORT = 21;
		int activeConnectionsCount;
		bool logMessages;
		ArrayList messageList = new ArrayList();
		FTPMode mode;
		string remoteHost;
		TcpClient tcpClient;

		public ArrayList MessageList {
			get { return messageList; }
		}

		public bool LogMessages {
			get { return logMessages; }

			set {
				if (!value) {
					messageList = new ArrayList();
				}

				logMessages = value;
			}
		}

		public FTPConnection() {
			activeConnectionsCount = 0;
			mode = FTPMode.Active;
			logMessages = false;
		}

		public virtual void Open(string remoteHost, string user, string password) {
			Open(remoteHost, DEFAULT_REMOTE_PORT, user, password, FTPMode.Active);
		}

		public virtual void Open(string remoteHost, string user, string password, FTPMode mode) {
			Open(remoteHost, DEFAULT_REMOTE_PORT, user, password, mode);
		}

		public virtual void Open(string remoteHost, int remotePort, string user, string password) {
			Open(remoteHost, remotePort, user, password, FTPMode.Active);
		}

		public virtual void Open(string remoteHost, int remotePort, string user, string password, FTPMode pMode) {
			ArrayList tempMessageList = new ArrayList();
			int returnValue;

			mode = pMode;

			tcpClient = new TcpClient();
			this.remoteHost = remoteHost;

			// As we cannot detect the local address from the TCPClient class, convert "127.0.0.1" and "localhost" to
			// the DNS record of this machine; this will ensure that the connection address and the PORT command address
			// are identical.  This fixes bug 854919.
			if (remoteHost == "localhost" || remoteHost == "127.0.0.1") {
				remoteHost = GetLocalAddressList()[0].ToString();
			}

			//CONNECT
			try {
				tcpClient.Connect(remoteHost, remotePort);
			}
			catch (Exception) {
				throw new IOException("Couldn't connect to remote server");
			}
			tempMessageList = Read();
			returnValue = GetMessageReturnValue((string) tempMessageList[0]);
			if (returnValue != 220) {
				Close();
				throw new Exception((string) tempMessageList[0]);
			}

			//SEND USER
			tempMessageList = SendCommand("USER " + user);
			returnValue = GetMessageReturnValue((string) tempMessageList[0]);
			if (!(returnValue == 331 || returnValue == 202)) {
				Close();
				throw new Exception((string) tempMessageList[0]);
			}

			//SEND PASSWORD
			if (returnValue == 331) {
				tempMessageList = SendCommand("PASS " + password);
				returnValue = GetMessageReturnValue((string) tempMessageList[0]);
				if (!(returnValue == 230 || returnValue == 202)) {
					Close();
					throw new Exception((string) tempMessageList[0]);
				}
			}
		}

		public virtual void Close() {
			ArrayList messageList = new ArrayList();

			if (tcpClient != null) {
				messageList = SendCommand("QUIT");
				tcpClient.Close();
			}
		}

		public ArrayList Dir(String mask) {
			ArrayList tmpList = Dir();

			DataTable table = new DataTable();
			table.Columns.Add("Name");
			for (int i = 0; i < tmpList.Count; i++) {
				DataRow row = table.NewRow();
				row["Name"] = (string) tmpList[i];
				table.Rows.Add(row);
			}

			DataRow[] rowList = table.Select("Name LIKE '" + mask + "'", "", DataViewRowState.CurrentRows);
			tmpList = new ArrayList();
			for (int i = 0; i < rowList.Length; i++) {
				tmpList.Add((string) rowList[i]["Name"]);
			}

			return tmpList;
		}

		public ArrayList Dir() {
			LockTcpClient();
			TcpListener listner = null;
			TcpClient client = null;
			NetworkStream networkStream = null;
			ArrayList tempMessageList = new ArrayList();
			int returnValue = 0;
			string returnValueMessage = "";
			ArrayList fileList = new ArrayList();

			SetTransferType(FTPFileTransferType.ASCII);

			if (mode == FTPMode.Active) {
				listner = CreateDataListner();
				listner.Start();
			}
			else {
				client = CreateDataClient();
			}

			tempMessageList = new ArrayList();
			tempMessageList = SendCommand("NLST");
			returnValue = GetMessageReturnValue((string) tempMessageList[0]);
			if (!(returnValue == 150 || returnValue == 125 || returnValue == 550)) {
				throw new Exception((string) tempMessageList[0]);
			}

			if (returnValue == 550) {
				//No files found
				return fileList;
			}

			if (mode == FTPMode.Active) {
				client = listner.AcceptTcpClient();
			}
			networkStream = client.GetStream();

			fileList = ReadLines(networkStream);

			if (tempMessageList.Count == 1) {
				tempMessageList = Read();
				returnValue = GetMessageReturnValue((string) tempMessageList[0]);
				returnValueMessage = (string) tempMessageList[0];
			}
			else {
				returnValue = GetMessageReturnValue((string) tempMessageList[1]);
				returnValueMessage = (string) tempMessageList[1];
			}

			if (!(returnValue == 226)) {
				throw new Exception(returnValueMessage);
			}

			networkStream.Close();
			client.Close();

			if (mode == FTPMode.Active) {
				listner.Stop();
			}
			UnlockTcpClient();
			return fileList;
		}

		public void SendStream(Stream stream, string remoteFileName, FTPFileTransferType type) {
			LockTcpClient();
			TcpListener listner = null;
			TcpClient client = null;
			NetworkStream networkStream = null;
			ArrayList tempMessageList = new ArrayList();
			int returnValue = 0;
			string returnValueMessage = "";
			tempMessageList = new ArrayList();

			SetTransferType(type);

			if (mode == FTPMode.Active) {
				listner = CreateDataListner();
				listner.Start();
			}
			else {
				client = CreateDataClient();
			}

			tempMessageList = SendCommand("STOR " + remoteFileName);
			returnValue = GetMessageReturnValue((string) tempMessageList[0]);
			if (!(returnValue == 150 || returnValue == 125)) {
				throw new Exception((string) tempMessageList[0]);
			}

			if (mode == FTPMode.Active) {
				client = listner.AcceptTcpClient();
			}

			networkStream = client.GetStream();

			Byte[] buffer = new Byte[BLOCK_SIZE];
			int bytes = 0;
			int totalBytes = 0;

			while (totalBytes < stream.Length) {
				bytes = (int) stream.Read(buffer, 0, BLOCK_SIZE);
				totalBytes = totalBytes + bytes;
				networkStream.Write(buffer, 0, bytes);
			}

			networkStream.Close();
			client.Close();

			if (mode == FTPMode.Active) {
				listner.Stop();
			}

			if (tempMessageList.Count == 1) {
				tempMessageList = Read();
				returnValue = GetMessageReturnValue((string) tempMessageList[0]);
				returnValueMessage = (string) tempMessageList[0];
			}
			else {
				returnValue = GetMessageReturnValue((string) tempMessageList[1]);
				returnValueMessage = (string) tempMessageList[1];
			}

			if (!(returnValue == 226)) {
				throw new Exception(returnValueMessage);
			}
			UnlockTcpClient();
		}

		public virtual void SendFile(string localFileName, FTPFileTransferType type) {
			SendFile(localFileName, Path.GetFileName(localFileName), type);
		}

		public virtual void SendFile(string localFileName, string remoteFileName, FTPFileTransferType type) {
			FileStream fs = new FileStream(localFileName, FileMode.Open);
			SendStream(fs, remoteFileName, type);
			fs.Close();
		}

		public void GetStream(string remoteFileName, Stream stream, FTPFileTransferType type) {
			TcpListener listner = null;
			TcpClient client = null;
			NetworkStream networkStream = null;
			ArrayList tempMessageList = new ArrayList();
			int returnValue = 0;
			string returnValueMessage = "";

			LockTcpClient();

			SetTransferType(type);

			if (mode == FTPMode.Active) {
				listner = CreateDataListner();
				listner.Start();
			}
			else {
				client = CreateDataClient();
			}

			tempMessageList = new ArrayList();
			tempMessageList = SendCommand("RETR " + remoteFileName);
			returnValue = GetMessageReturnValue((string) tempMessageList[0]);
			if (!(returnValue == 150 || returnValue == 125)) {
				throw new Exception((string) tempMessageList[0]);
			}

			if (mode == FTPMode.Active) {
				client = listner.AcceptTcpClient();
			}

			networkStream = client.GetStream();

			Byte[] buffer = new Byte[BLOCK_SIZE];
			int bytes = 0;

			bool read = true;
			while (read) {
				bytes = (int) networkStream.Read(buffer, 0, buffer.Length);
				stream.Write(buffer, 0, bytes);
				if (bytes == 0) {
					read = false;
				}
			}

			networkStream.Close();
			client.Close();

			if (mode == FTPMode.Active) {
				listner.Stop();
			}

			if (tempMessageList.Count == 1) {
				tempMessageList = Read();
				returnValue = GetMessageReturnValue((string) tempMessageList[0]);
				returnValueMessage = (string) tempMessageList[0];
			}
			else {
				returnValue = GetMessageReturnValue((string) tempMessageList[1]);
				returnValueMessage = (string) tempMessageList[1];
			}

			if (!(returnValue == 226)) {
				throw new Exception(returnValueMessage);
			}

			UnlockTcpClient();
		}

		public virtual void GetFile(string remoteFileName, FTPFileTransferType type) {
			GetFile(remoteFileName, Path.GetFileName(remoteFileName), type);
		}

		public virtual void GetFile(string remoteFileName, string localFileName, FTPFileTransferType type) {
			FileStream fs = new FileStream(localFileName, FileMode.Create);
			GetStream(remoteFileName, fs, type);
			fs.Close();
		}

		public virtual void DeleteFile(String remoteFileName) {
			Monitor.Enter(tcpClient);
			ArrayList tempMessageList = new ArrayList();
			int returnValue = 0;
			tempMessageList = SendCommand("DELE " + remoteFileName);
			returnValue = GetMessageReturnValue((string) tempMessageList[0]);
			if (returnValue != 250) {
				throw new Exception((string) tempMessageList[0]);
			}
			Monitor.Exit(tcpClient);
		}

		public virtual void MoveFile(string remoteFileName, string toRemotePath) {
			if (toRemotePath.Length > 0 && toRemotePath.Substring(toRemotePath.Length - 1, 1) != "/") {
				toRemotePath = toRemotePath + "/";
			}

			RenameFile(remoteFileName, toRemotePath + remoteFileName);
		}

		public virtual void RenameFile(string fromRemoteFileName, string toRemoteFileName) {
			Monitor.Enter(tcpClient);
			ArrayList tempMessageList = new ArrayList();
			int returnValue = 0;
			tempMessageList = SendCommand("RNFR " + fromRemoteFileName);
			returnValue = GetMessageReturnValue((string) tempMessageList[0]);
			if (returnValue != 350) {
				throw new Exception((string) tempMessageList[0]);
			}
			tempMessageList = SendCommand("RNTO " + toRemoteFileName);
			returnValue = GetMessageReturnValue((string) tempMessageList[0]);
			if (returnValue != 250) {
				throw new Exception((string) tempMessageList[0]);
			}
			Monitor.Exit(tcpClient);
		}

		public virtual void SetCurrentDirectory(String remotePath) {
			LockTcpClient();

			ArrayList tempMessageList = new ArrayList();
			int returnValue = 0;
			tempMessageList = SendCommand("CWD " + remotePath);
			returnValue = GetMessageReturnValue((string) tempMessageList[0]);
			if (returnValue != 250) {
				throw new Exception((string) tempMessageList[0]);
			}
			UnlockTcpClient();
		}

		void SetTransferType(FTPFileTransferType type) {
			switch (type) {
			case FTPFileTransferType.ASCII:
				SetMode("TYPE A");
				break;
			case FTPFileTransferType.Binary:
				SetMode("TYPE I");
				break;
			default:
				throw new Exception("Invalid File Transfer Type");
			}
		}

		void SetMode(string mode) {
			LockTcpClient();

			ArrayList tempMessageList = new ArrayList();
			int returnValue = 0;
			tempMessageList = SendCommand(mode);
			returnValue = GetMessageReturnValue((string) tempMessageList[0]);
			if (returnValue != 200) {
				throw new Exception((string) tempMessageList[0]);
			}
			UnlockTcpClient();
		}

		TcpListener CreateDataListner() {
			int port = GetPortNumber();
			SetDataPort(port);

			//TODO: CreateDataListner(): should listen on a ip address from config file!!!
			IPAddress _ip = IPAddress.Parse(GetLocalAddressList()[0].ToString());
			TcpListener listner = new TcpListener(_ip, port);
			return listner;
		}

		TcpClient CreateDataClient() {
			int port = GetPortNumber();

			//IPEndPoint ep = new IPEndPoint(GetLocalAddressList()[0], port);

			TcpClient client = new TcpClient();

			//client.Connect(ep);
			client.Connect(remoteHost, port);

			return client;
		}

		void SetDataPort(int portNumber) {
			LockTcpClient();

			ArrayList tempMessageList = new ArrayList();
			int returnValue = 0;
			int portHigh = portNumber >> 8;
			int portLow = portNumber & 255;

			tempMessageList = SendCommand("PORT " + GetLocalAddressList()[0].ToString().Replace(".", ",") + "," + portHigh.ToString() + "," + portLow);

			returnValue = GetMessageReturnValue((string) tempMessageList[0]);
			if (returnValue != 200) {
				throw new Exception((string) tempMessageList[0]);
			}
			UnlockTcpClient();
		}

		public virtual void MakeDir(string directoryName) {
			LockTcpClient();

			ArrayList tempMessageList = new ArrayList();
			int returnValue = 0;

			tempMessageList = SendCommand("MKD " + directoryName);
			returnValue = GetMessageReturnValue((string) tempMessageList[0]);
			if (returnValue != 257) {
				throw new Exception((string) tempMessageList[0]);
			}

			UnlockTcpClient();
		}

		public virtual void RemoveDir(string directoryName) {
			LockTcpClient();

			ArrayList tempMessageList = new ArrayList();
			int returnValue = 0;

			tempMessageList = SendCommand("RMD " + directoryName);
			returnValue = GetMessageReturnValue((string) tempMessageList[0]);
			if (returnValue != 250) {
				throw new Exception((string) tempMessageList[0]);
			}

			UnlockTcpClient();
		}

		public ArrayList SendCommand(String command) {
			activeConnectionsCount++;

			Byte[] cmdBytes = Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());
			NetworkStream stream = tcpClient.GetStream();
			stream.Write(cmdBytes, 0, cmdBytes.Length);

			activeConnectionsCount--;

			//TEMP - START
			if (command == "QUIT") {
				ArrayList _al = new ArrayList();
				_al.Add("221 Bye...");
				return _al;
			}
			else {
				return Read();
			}
			//TEMP - END

			//			return Read();
		}

		ArrayList Read() {
			NetworkStream stream = tcpClient.GetStream();
			ArrayList messageList = new ArrayList();

			ArrayList tempMessage = ReadLines(stream);

			int tryCount = 0;
			while (tempMessage.Count == 0) {
				if (tryCount == 10) {
					throw new Exception("Server does not return message to the message");
				}

				Thread.Sleep(1000);
				tryCount++;
				tempMessage = ReadLines(stream);
			}

			while (((string) tempMessage[tempMessage.Count - 1]).Substring(3, 1) == "-") {
				messageList.AddRange(tempMessage);
				tempMessage = ReadLines(stream);
			}
			messageList.AddRange(tempMessage);

			AddMessagesToMessageList(messageList);

			return messageList;
		}

		ArrayList ReadLines(NetworkStream stream) {
			ArrayList messageList = new ArrayList();
			char[] seperator = {'\n'};
			char[] toRemove = {'\r'};
			Byte[] buffer = new Byte[BLOCK_SIZE];
			int bytes = 0;
			string tmpMes = "";

			while (stream.DataAvailable) {
				bytes = stream.Read(buffer, 0, buffer.Length);
				tmpMes += Encoding.ASCII.GetString(buffer, 0, bytes);
			}
			string[] mess = tmpMes.Split(seperator);
			for (int i = 0; i < mess.Length; i++) {
				if (mess[i].Length > 0) {
					messageList.Add(mess[i].Trim(toRemove));
				}
			}

			return messageList;
		}

		int GetMessageReturnValue(string message) {
			return int.Parse(message.Substring(0, 3));
		}

		int GetPortNumber() {
			LockTcpClient();
			int port = 0;
			switch (mode) {
			case FTPMode.Active:
				Random rnd = new Random((int) DateTime.Now.Ticks);
				port = DATA_PORT_RANGE_FROM + rnd.Next(DATA_PORT_RANGE_TO - DATA_PORT_RANGE_FROM);
				break;
			case FTPMode.Passive:
				ArrayList tempMessageList = new ArrayList();
				int returnValue = 0;
				tempMessageList = SendCommand("PASV");
				returnValue = GetMessageReturnValue((string) tempMessageList[0]);
				if (returnValue != 227) {
					if (((string) tempMessageList[0]).Length > 4) {
						throw new Exception((string) tempMessageList[0]);
					}
					else {
						throw new Exception((string) tempMessageList[0] + " Passive Mode not implemented");
					}
				}
				string message = (string) tempMessageList[0];
				int index1 = message.IndexOf(",", 0);
				int index2 = message.IndexOf(",", index1 + 1);
				int index3 = message.IndexOf(",", index2 + 1);
				int index4 = message.IndexOf(",", index3 + 1);
				int index5 = message.IndexOf(",", index4 + 1);
				int index6 = message.IndexOf(")", index5 + 1);
				port = 256*int.Parse(message.Substring(index4 + 1, index5 - index4 - 1)) + int.Parse(message.Substring(index5 + 1, index6 - index5 - 1));
				break;
			}
			UnlockTcpClient();
			return port;
		}

		void AddMessagesToMessageList(ArrayList messages) {
			if (logMessages) {
				messageList.AddRange(messages);
			}
		}

		IPAddress[] GetLocalAddressList() {
			return Dns.GetHostEntry(Dns.GetHostName()).AddressList;
		}

		void LockTcpClient() {
			Monitor.Enter(tcpClient);
		}

		void UnlockTcpClient() {
			Monitor.Exit(tcpClient);
		}
	}
}