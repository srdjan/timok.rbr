using NUnit.Framework;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace Timok.Core.Test {
	[TestFixture]
  public class TestEmailValidator {
		[Test]
		public void Test1() {
			const string _email = "";
      var _rc = EmailValidator.Validate(_email, true);
			Assert.AreEqual(_rc, true);
		}

		[Test]
		public void Test2() {
      const string _email = "asdfasdf@sdsdfvfdngf.com.br";
      var _rc = EmailValidator.Validate(_email, true);
      Assert.AreEqual(_rc, true);
    }

    //TODO: check for more cases
    //also check this site: http://regexlib.com/Search.aspx , search for "" in Category: "Email", MinRating: "The Best"
    //or
    //http://www.cambiaresearch.com/cambia3/snippets/csharp/regex/email_regex.aspx

		[Test]
		public void Test3() {
      string _testFilePath = @"C:\Temp\_EmailsToValidate.txt";
      if (!File.Exists(_testFilePath)) {
        Assert.Fail("The test file is not found [" + _testFilePath + "]");
        return;
      }

      var _list = new ArrayList();
      using (var _sr = new StreamReader(_testFilePath)) {
        string _line;
        while ((_line = _sr.ReadLine()) != null) {
          if (_line.Trim().Length > 0) {
            _line = _line.Replace(';', ',');
            _list.Add(_line.Trim().Trim(',').Trim());
          }
        }
      }

      int _lineNumber = 0;
      int _badCount = 0;
      foreach (string  _line in _list) {
        _lineNumber++;
        int _fieldNumber = 0;
        string[] _fields = _line.Split(',');
        foreach (string  _email in _fields) {
          _fieldNumber++;
          bool _good = EmailValidator.Validate(_email.Trim(), false);
          if (_good) {
            Debug.WriteLine(_email.Trim() + "\t\t\tGOOD");// line: [" + _lineNumber + "]  field: [" + _fieldNumber + "]");
          }
          else {
            _badCount++;
            Debug.WriteLine(_email.Trim() + "\t\t\tINVALID line: [" + _lineNumber + "]  field: [" + _fieldNumber + "]");
          }
        }
      }
      Assert.AreEqual(_badCount == 0, true);
    }

		[Test]
		public void Test4() {
      const string _email = "";
      var _rc = EmailValidator.Validate(_email, true);
      Assert.AreEqual(_rc, true);
    }

		[Test]
		public void Test5() {
      const string _email = "";
      var _rc = EmailValidator.Validate(_email, true);
      Assert.AreEqual(_rc, true);
    }
	}
}