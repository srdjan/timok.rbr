using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using MbUnit.Framework;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Test {
	[TestFixture]
	public class TestCarrierAcctMngr {
		[SetUp]
		public void Setup() {
			//	T.Init(Folders.LogFolder);
		}

		[Test]
		public void Test_WithTrans() {
			////-- Enter a new transaction without inheriting from ServicedComponent
			//ServiceConfig config = new ServiceConfig();
			//config.Transaction = TransactionOption.RequiresNew;
			//ServiceDomain.Enter(config);

			////-- Leave Service Domain
			//if (ContextUtil.IsInTransaction) {
			//  ContextUtil.SetAbort();
			//}
			//ServiceDomain.Leave();
		}

		public void Test_DTO_CDR() {
			CdrDto[] _cdrs = CDRController.GetRetailAcctCDRs(200625000, 200625023, 0, 0);
			foreach (CdrDto _cdr in _cdrs) {
				string _xml = serializeCDR(_cdr);
				Debug.WriteLine(_xml.Length);

				CdrDto _xcdr = DeserializeCDR(_xml);
				Debug.WriteLine(_xcdr.ToString());
			}
		}

		String serializeCDR(CdrDto pCDR) {
			try {
				String _xmlizedString = null;
				MemoryStream memoryStream = new MemoryStream();
				XmlSerializer xs = new XmlSerializer(typeof (CdrDto));
				XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
				xs.Serialize(xmlTextWriter, pCDR);
				memoryStream = (MemoryStream) xmlTextWriter.BaseStream;
				_xmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
				return _xmlizedString;
			}
			catch (Exception e) {
				Console.WriteLine(e);
				return null;
			}
		}

		public CdrDto DeserializeCDR(String pXmlizedString) {
			XmlSerializer xs = new XmlSerializer(typeof (CdrDto));
			MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
			XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
			return (CdrDto) xs.Deserialize(memoryStream);
		}

		String UTF8ByteArrayToString(Byte[] characters) {
			UTF8Encoding _encoding = new UTF8Encoding();
			String _constructedString = _encoding.GetString(characters);
			return (_constructedString);
		}

		Byte[] StringToUTF8ByteArray(String pXmlString) {
			UTF8Encoding _encoding = new UTF8Encoding();
			Byte[] _byteArray = _encoding.GetBytes(pXmlString);
			return _byteArray;
		}

		[TearDown]
		public void Teardown() {}
	}
}