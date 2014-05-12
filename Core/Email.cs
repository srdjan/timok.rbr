using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading;

namespace Timok.Core {
	//-----------------------------------------------------------------------------------
	[Serializable]
	class EmailItem {
		public string From;
		public string To;
		public string CC;
		public string BCC;
		public string Server;
		public string Password;
		public string Subject;
		public string Body;

		public EmailItem(string pFrom, string pTo, string pCc, string pBcc, string pServer, string pPassword, string pSubject, string pBody) {
			From = pFrom;
			To = pTo;
			CC = pCc;
			BCC = pBcc;
			Server = pServer;
			Password = pPassword;
			Subject = pSubject;
			Body = pBody;
		}

		public void Serialize(string pFilePath) {
			if (File.Exists(pFilePath)) {
				throw new Exception(string.Format("Email.Serialize: file with same name already exist: {0}", pFilePath));
			}

			pFilePath += ".Email.pending";
			using (Stream _stream = new FileStream(pFilePath, FileMode.Create)) {
				IFormatter _formatter = new SoapFormatter();
				_formatter.Serialize(_stream, this);
				_stream.Close();
			}
		}

		public static	EmailItem Deserialize(string pFilePath) {
			if ( ! File.Exists(pFilePath)) {
				throw new Exception(string.Format("Email.Deserialize: file with that name doesn't exist: {0}", pFilePath));
			}

			EmailItem _emailItem;
			try {
				using (var _fs = new FileStream(pFilePath, FileMode.Open, FileAccess.Read)) {
					IFormatter _formatter = new SoapFormatter();
					_emailItem = (EmailItem)_formatter.Deserialize(_fs);
				}
			}
			catch {
				Thread.Sleep(100);

				using (var _fs = new FileStream(pFilePath, FileMode.Open, FileAccess.Read)) {
					IFormatter _formatter = new SoapFormatter();
					_emailItem = (EmailItem)_formatter.Deserialize(_fs);
				}
			}
			return _emailItem;
		}

		public override string ToString() {
			return string.Format("From={0}, to={1}, CC={2}, BCC={3}, Server={4}, Passwd={5}, Subject={6}, Body={7}", From, To, CC, BCC, Server, Password, Subject, Body);
		}
	}

	//------------------------------------------------------------------------------------
	public class Email {
		public static string SetForSending(string pFilePath, string pFrom, string pTo, string pCc, string pBcc, string pServer, string pPassword, string pSubject, string pBody) {
			var	_emailItem = new EmailItem(pFrom, pTo, pCc, pBcc, pServer, pPassword, pSubject, pBody);
			_emailItem.Serialize(pFilePath);
			return string.Format("Email.SetForSending: {0}\r\n{1}", pFilePath, _emailItem);
		}

		public static bool CheckPending(string pFilePath) {
			var _emailItem = EmailItem.Deserialize(pFilePath);
			var _msg = createMailMessage(_emailItem.From, _emailItem.To, _emailItem.CC, _emailItem.BCC, _emailItem.Server, _emailItem.Password, _emailItem.Subject, _emailItem.Body);
			//pLog(LogSeverity.Status, "Email.CheckPending", string.Format("File: {0}\r\nMessage: {1}", pFilePath, toString(_msg)));

			var _smtpClient = new SmtpClient(_emailItem.Server);
			_smtpClient.Credentials = new NetworkCredential(_msg.From.Address, _emailItem.Password);
			_smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			_smtpClient.Send(_msg);
			return true;
		}

		//----------------------------------- private ----------------------------------------------------
		static MailMessage createMailMessage(string pFrom, string pTo, string pCc, string pBcc, string pServer, string pPassword, string pSubject, string pBody) {
			var _msg = new MailMessage();
			_msg.From = new MailAddress(pFrom);
			_msg.Subject = pSubject;
			_msg.Body = pBody;

			if (pTo == null || pTo.Length < 5) {
				throw new Exception(string.Format("Email.createMailMessage, Error: Missing 'To' field.\r\nServer={0}, Password={1}, From={2}, To={3}, CC={4}", pServer, pPassword, pFrom, pTo, pCc));
			}

			pTo = pTo.Replace(";", ",");
			string[] _addresses = pTo.Split(',');
			foreach (var _address in _addresses) {
				_msg.To.Add(new MailAddress(_address));
			}

			if (pCc != null && pCc.Length > 5) {
				pCc = pCc.Replace(";", ",");
				_addresses = pCc.Split(',');
				foreach (var _address in _addresses) {
					_msg.CC.Add(new MailAddress(_address));
				}
			}

			if (pBcc != null && pBcc.Length > 5) {
				pBcc = pBcc.Replace(";", ",");
				_addresses = pBcc.Split(',');
				foreach (var _address in _addresses) {
					_msg.Bcc.Add(new MailAddress(_address));
				}
			}
			return _msg;
		}

		static string toString(MailMessage pMsg) {
			if (pMsg == null) {
				return "Message is null)";	
			}

			var _strBuilder = new StringBuilder();
			_strBuilder.Append("From: " + pMsg.From);
			_strBuilder.Append(Environment.NewLine);
			_strBuilder.Append("To: " + pMsg.To);
			_strBuilder.Append(Environment.NewLine);
			_strBuilder.Append("Cc: " + pMsg.CC);
			_strBuilder.Append(Environment.NewLine);
			_strBuilder.Append("Bcc: " + pMsg.Bcc);
			_strBuilder.Append(Environment.NewLine);
			_strBuilder.Append("Subject: " + pMsg.Subject);
			_strBuilder.Append(Environment.NewLine);
			_strBuilder.Append("Body: " + pMsg.Body);
			_strBuilder.Append(Environment.NewLine);
			return _strBuilder.ToString();
		}
	}
}