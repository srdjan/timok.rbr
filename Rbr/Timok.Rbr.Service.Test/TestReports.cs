using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using NUnit.Framework;

using Timok.Rbr.Engine;
using Timok.Rbr.Service.EmailReports;

namespace Timok.Rbr.Engine.Test {

  [TestFixture]
  public class TestReports {
    
    [NUnit.Framework.Test]
    public void Test_DailyMinutesReport() {
      DailyMinutesReport _rep = new DailyMinutesReport();
      string _subject = string.Empty;
      string _body = string.Empty;

      _rep.Run(out _subject, out _body);
      Debug.WriteLine(_subject);
      Debug.WriteLine(_body);
    }
  }
}
