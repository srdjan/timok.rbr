/* 
 * (C) Copyright 2002, 2005 - Lorne Brinkman - All Rights Reserved.
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
using System.Diagnostics;

namespace Timok.Logger {
	/// <summary>
	/// EventLogLogger writes log entries to the Windows Event Log
	/// </summary>
	public class EventLogLogger : Logger {
		/// <summary>
		/// The Windows event log.
		/// </summary>
		EventLog eventLog;

		/// <summary>
		/// Gets and Sets the Windows event log.
		/// </summary>
		//protected EventLog EventLog 
		//{
		//  get { return eventLog; }
		//  set { eventLog = value; }
		//}
		/// <summary>
		/// Write pLogEntry information to the Windows event log
		/// </summary>
		/// <param name="pLogEntry">The LogEntry being logged</param>
		/// <returns>true upon success, false upon failure.</returns>
		protected internal override bool DoLog(LogEntry pLogEntry) {
			//Timok:
			//EventLog should always get only high severity logs: Critical, Fatal
			if (pLogEntry.Severity < LogSeverity.Critical) {
				return true;
			}
			//EndTimok

			// convert the log entry's severity to an appropriate type for the event log
			EventLogEntryType t = (pLogEntry.Severity < LogSeverity.Warning) ? EventLogEntryType.Information : (pLogEntry.Severity == LogSeverity.Warning ? EventLogEntryType.Warning : EventLogEntryType.Error);

			try {
				eventLog.WriteEntry(Formatter.AsString(pLogEntry), t);
			}
			catch {
				return false;
			}

			return true;
		}

		public EventLogLogger() : base() {
			eventLog = new EventLog();
			eventLog.Source = AppDomain.CurrentDomain.FriendlyName;
		}

		/// <summary>
		/// Create a new instance of EventLogLogger with a given Eventog
		/// </summary>
		/// <param name="anEventLog">An EventLog to which logs will be written by this logger</param>
		public EventLogLogger(EventLog anEventLog) : base() {
			eventLog = anEventLog;
		}

		/// <summary>
		/// EventLogLogger uses LogEntryMessageOnlyFormatter by default.
		/// </summary>
		/// <returns></returns>
		protected override LogEntryFormatter GetDefaultFormatter() {
			return new LogEntryMessageOnlyFormatter();
		}
	}
}