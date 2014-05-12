/* 
 * (C) Copyright 2002 - Lorne Brinkman - All Rights Reserved.
 * http://www.TheObjectGuy.com
 *
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 * 
 *  - Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 * 
 *  - Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 * 
 *  - Neither the name "Lorne Brinkman", "The Object Guy", nor the name "Bit Factory"
 *    may be used to endorse or promote products derived from this software without
 *    specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY
 * OF SUCH DAMAGE.
 * 
 */

using System;
using System.IO;
using System.Net.Mail;

namespace Timok.Logger {
	public delegate void SetForSendingDelegate(string pTargetFolder, string pFrom, string pTo, string pCC, string pBCC, string pToServer, string pPassword, string pSubject, string pLogEntry);
	/// <summary>
	/// An EmailLogger sends log information via an email message.
	/// </summary>
	/// <remarks>
	/// If the subject attribute is not explicitly set, then it will automatically be filled
	/// with the logEntry's application, category, and severity.
	/// </remarks>
	public class EmailLogger : Logger {
		readonly string fileFolder;
		readonly string subjectHeader;
		readonly string from;
		readonly string to;
		readonly string toServer;
		readonly string password;
		SetForSendingDelegate setForSendingDelegate;

		/// <summary>
		/// Create an instance of EmailLogger.
		/// </summary>
		/// <param name="pFileFolder">The full path to the serilization file.</param>
		/// <param name="pFromServer">The name of the SMTP server.</param>
		/// <param name="pToServer">The name of the SMTP server.</param>
		/// <param name="pPassword">The "from" for the emails that get sent.</param>
		/// <param name="pFrom">The "from" for the emails that get sent.</param>
		/// <param name="pTo">The "to" for the emails that get sent.</param>
		public EmailLogger(string pFileFolder, string pFromServer, string pToServer, string pPassword, string pFrom, String pTo, SetForSendingDelegate pSetForSendingDelegate) {
			fileFolder = pFileFolder;
			subjectHeader = "From> " + pFromServer + " - ";
			toServer = pToServer;
			password = pPassword;
			from = pFrom;
			to = pTo;
			setForSendingDelegate = pSetForSendingDelegate;
		}

		/// <summary>
		/// SetForSending the email representing pLogEntry.
		/// </summary>
		/// <param name="pLogEntry">The LogEntry.</param>
		/// <returns>true upon success, false upon failure.</returns>
		protected internal override bool DoLog(LogEntry pLogEntry) {
			//EmailLog should processes only high severity logs: Critical, Fatal
			if(pLogEntry.Severity < LogSeverity.Critical) {
				return true;
			}

			//-- see if the Subject was embedeed
			string _subject;
			if (pLogEntry.Category != null) {
				_subject = subjectHeader + pLogEntry.Category;
			}
			else {
				_subject = subjectHeader + "Critical ERROR !";
			}

			setForSendingDelegate(Path.Combine(fileFolder, Guid.NewGuid().ToString("N")), from, to, string.Empty, string.Empty, toServer, password, _subject, Formatter.AsString(pLogEntry));
			return true;
		}
	}
}