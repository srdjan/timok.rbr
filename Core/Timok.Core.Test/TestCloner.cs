using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Timok.Core.Test 
{
	/// <summary>
	/// Summary description for TestCloner.
	/// </summary>
	[TestFixture]
	public class TestCloner {
		[SetUp]
		public void Setup() {
			//	T.Init(Folders.LogFolder);
		}

		[Test]
		public void TestCloneOne() {
			ObjX _orig = new ObjX();
			_orig.Bool = true;
			_orig.Int = 11;
			_orig.Object = "OBJECT";
			_orig.String = "ObjX 11 true";

			object _clone = Cloner.Clone(_orig);

			Debug.WriteLine(_clone.ToString());
		}

		[Test]
		public void TestCloneArray() {
			ObjX[] _origList = new ObjX[5];
			for (int _i = 0; _i < 5; _i++) {
				_origList[_i] = new ObjX();
				_origList[_i].Bool = true;
				_origList[_i].Int = 11;
				_origList[_i].Object = "OBJECT";
				_origList[_i].String = "ObjX 11 true";
			}
			try {
				object _clone = Cloner.Clone(_origList);
				Debug.WriteLine(_clone.ToString());
			}
			catch (Exception _ex) {
				Debug.WriteLine(_ex);
			}
		}

		[Test]
		public void TestCloneArrayList() {
			ObjX[] _origList = new ObjX[5];
			for (int _i = 0; _i < 5; _i++) {
				ObjX _o = new ObjX();
				_o.Bool = true;
				_o.Int = 11;
				_o.Object = "OBJECT";
				_o.String = "ObjX 11 true";
				_origList[_i] = _o;
			}
			try {
				object _clone = Cloner.Clone(_origList);
				Debug.WriteLine(_clone.ToString());
			}
			catch (Exception _ex) {
				Debug.WriteLine(_ex);
			}
		}
	}

	[Serializable]
	public class ObjX {
		public const string ConstStr = "this is a Constant field";
		public readonly string ReadOnlyStr = "this is a Readonly field";
		public int Int;
		public string String;
		public bool Bool;
		public object Object;

		string publicGetOnly = "this is a Public Get only Property ";
		public string PublicGetOnly { get { return publicGetOnly; } }

		public override string ToString() {
			return Int + "\r\n" + String + "\r\n" + Bool + "\r\n" + Object + "\r\n" + ConstStr + "\r\n" + ReadOnlyStr + "\r\n" + PublicGetOnly;
		}
	}
}