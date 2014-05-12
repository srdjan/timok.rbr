using System;
using System.IO;
using System.Net;

namespace Timok.Core.NetworkLib.Ftp 
{
	public class FTPFile	{
		private String name;

		public FTPFile() {		}

		public String Name {
			get {	return this.name;	}
			set {	this.name = value; }
		}

	  //TODO: Test
    public void UploadBinary(string destinationFile, string localFile, string userName, string password) {
      FtpWebRequest request = FtpWebRequest.Create(destinationFile) as FtpWebRequest;
      request.UseBinary = true;
      request.Method = WebRequestMethods.Ftp.UploadFile;
      request.Credentials = new NetworkCredential(userName, password);
      using (FileStream fileStream = new FileStream(localFile, FileMode.Open))
      using (BinaryReader reader = new BinaryReader(fileStream))
      using (BinaryWriter writer = new BinaryWriter(request.GetRequestStream())) {
        byte[] buffer = new byte[2047];
        int read = 0;
        do {
          read = reader.Read(buffer, 0, buffer.Length);
          writer.Write(buffer, 0, read);

        } while (read != 0);
        writer.Flush();
      }
    }
	}
}
