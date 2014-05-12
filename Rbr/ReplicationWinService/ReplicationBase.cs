using System.IO;
using Indy.Sockets;
using Timok.Rbr.BLL.Entities;

namespace Timok.Rbr.Replication {
	public class ReplicationBase {
		public static bool Send(Node pTargetNode, string pFilePath) {
			var _fileName = Path.GetFileName(pFilePath);

			using (var _ftp = new FTP()) {
				_ftp.Host = pTargetNode.IPAddressString;
				_ftp.Username = pTargetNode.UserName;
				_ftp.Password = pTargetNode.Password;
				_ftp.ConnectTimeout = 30000;
				_ftp.ReadTimeout = 30000;
				_ftp.TransferTimeout = 30000;
				_ftp.Passive = false;

				var _fileNameTemp = _fileName + ".temp";

				_ftp.Connect();
				_ftp.Put(pFilePath, _fileNameTemp, false);
				_ftp.Rename(_fileNameTemp, _fileName);
				//				try { _ftp.Disconnect(true); } catch {}
			}
			return true;
		}
	}
}