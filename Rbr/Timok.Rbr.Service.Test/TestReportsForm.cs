using System;
using System.Diagnostics;
using NUnit.Framework;
using Timok.Rbr.Service.EmailReports;

namespace Timok.Rbr.Service.Test {
	[TestFixture]
	public class TestReportsForm {

	[Test]
	public void TestReport() {
			var _rep = new DailyMinutesReport();
			string _subject;
			string _body;

			_rep.Run(DateTime.Today, out _subject, out _body);
			Debug.WriteLine(_subject);
			Debug.WriteLine(_body);
		}
	}
}