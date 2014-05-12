using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

using NUnit.Framework;
using Timok.Core;
using Timok.Rbr.Core;
//using Timok.Rbr.BLL;
using Timok.Rbr.DAL.RbrDatabase;

namespace Timok.Rbr.BLL.Test {

	[TestFixture]
	public class TestCloneTypedArray {
		public TestCloneTypedArray() { }
		[SetUp]
		public void Setup() {
			//	T.Init(Folders.LogFolder);
		}

    //TODO: NEW DAL
    //[Test]
    //public void Test_CloneTypedArray() {
    //  TerminationChoiceRow[] _tcs = new TerminationChoiceRow[2];

    //  _tcs[0] = new TerminationChoiceRow();
    //  _tcs[0].Termination_choice_id = 1;
    //  _tcs[0].Carrier_route_id = 2;
    //  _tcs[0].Customer_route_id = 3;
    //  _tcs[0].Priority = 4;

    //  _tcs[1] = new TerminationChoiceRow();
    //  _tcs[1].Termination_choice_id = 2;
    //  _tcs[1].Carrier_route_id = 3;
    //  _tcs[1].Customer_route_id = 4;
    //  _tcs[1].Priority = 5;

    //  ser(_tcs);

    //}

		private void ser(object[] pObj) {

			object _x = Cloner.Clone(pObj);

			using (Stream _s = new FileStream("c:\\test_" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".txt", FileMode.Create)) {
				IFormatter _fmtr = new SoapFormatter();
				_fmtr.Serialize(_s, _x);
				_s.Close();
			}

			System.Diagnostics.Debug.WriteLine(_x.ToString());
		}
		
		[Test]
		public void Test_GetTypedArrayFromFile() {
			string[] _files = Directory.GetFiles("c:\\", "test_*.txt");
			foreach (string _f in _files) {
				TerminationChoiceRow[] _ts = des(_f);
				System.Diagnostics.Debug.WriteLine(_ts.ToString());

				Type _thisType = this.GetType();
				MethodInfo _m = _thisType.GetMethod("Test_TX");
				_m.Invoke(_thisType, new object[1] {_ts});
			}
		}

		private TerminationChoiceRow[] des(string pFile) {
			using (FileStream _fs = new FileStream(pFile, FileMode.Open, FileAccess.Read)) {
				IFormatter _f = new SoapFormatter();
				return (TerminationChoiceRow[]) _f.Deserialize(_fs);
			}
		}

		public static void Test_TX(TerminationChoiceRow[] pTerminationChoiceRows) {
			System.Diagnostics.Debug.WriteLine("Called: " + pTerminationChoiceRows.ToString());
		}

		[TearDown]
		public void Teardown() {
		}
	}
}
