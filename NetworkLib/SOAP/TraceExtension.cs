using System;
using System.IO;
using System.Web.Services.Protocols;
using T = Timok.Core.Logging.TimokLogger;

namespace Timok.Core.NetworkLib.SOAP 
{
	//----------------------------------- Soap Extension Attribute ----------------------------------------
	//-----------------------------------------------------------------------------------------------------
	[AttributeUsage(AttributeTargets.Method)]
	public class TraceExtensionAttribute : SoapExtensionAttribute 
	{
		private int priority;
		private int group;

		public override Type ExtensionType {
			get { return typeof(TraceExtension); }
		}

		public override int Priority {
			get { return priority; }
			set { priority = value; }
		}
		public int Group {
			get { return group; }
			set { group = value; }
		}
	}

	//------------------------------------ Soap Extension -------------------------------------------------
	//-----------------------------------------------------------------------------------------------------
	public class TraceExtension : SoapExtension 
	{
		Stream oldStream;
		Stream newStream;

		//-- Save the Stream representing the SOAP request or SOAP response into a local memory buffer.
		public override Stream ChainStream( Stream stream ) {
			oldStream = stream;
			newStream = new MemoryStream();
			return newStream;
		}

		//-- The SOAP extension was configured to run using attribute. 
		//-- When the SOAP extension is accessed for the first time, the XML Web service method it is 
		//-- applied to is accessed to store the file name passed in, using the corresponding SoapExtensionAttribute.    
		public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute) {
			return null;
		}

		//-- The SOAP extension was configured to run using a configuration file
		public override object GetInitializer(Type WebServiceType) {
			return null;    
		}

		//-- Receive the variable stored by GetInitializer and store it in a member variable for this specific instance.
		public override void Initialize(object initializer) {		}

		//-- If the SoapMessageStage is such that the SoapRequest or SoapResponse is still in the SOAP format to 
		//-- be sent or received, save it out to a file.
		public override void ProcessMessage(SoapMessage pMessage) {
			switch (pMessage.Stage) {
				case SoapMessageStage.BeforeSerialize:
					break;
				case SoapMessageStage.AfterSerialize:
					LogOutput(pMessage);
					break;
				case SoapMessageStage.BeforeDeserialize:
					LogInput(pMessage);
					break;
				case SoapMessageStage.AfterDeserialize:
					break;
				default:
					throw new Exception("invalid stage");
			}
		}

		public void LogOutput(SoapMessage pMessage) {
			try {
				//-- First reset newStream position
				newStream.Position = 0;

				StringWriter _sw = new StringWriter();
				copy(newStream, _sw);
				string _soapString = (pMessage is SoapServerMessage) ? "SoapResponse:" : "SoapRequest:";
				T.LogDebug(" " + _soapString + "\r\n" + _sw.GetStringBuilder().ToString());

				//-- Last, reset newStreamPosition and copy to oldStream
				newStream.Position = 0;
				copy(newStream, oldStream);
			}
			catch (Exception ex) {
				T.LogCritical(string.Format("Exception\r\n{0}", ex));
			}
		}

		public void LogInput(SoapMessage pMessage) {
			try {
				//-- First:
				copy(oldStream, newStream);

				StringWriter _sw = new StringWriter();
				newStream.Position = 0;
				copy(newStream, _sw);

				string _soapString = (pMessage is SoapServerMessage) ? "SoapRequest:" : "SoapResponse:";
				T.LogDebug(" " + _soapString + "\r\n" + _sw.GetStringBuilder());

				//-- Last
				newStream.Position = 0;
			}
			catch (Exception ex) {
				T.LogCritical(string.Format("Exception\r\n{0}", ex));
			}
		}


		//------------------Private methods ----------------------------------------------------------------
		private void copy(Stream pFrom, Stream pTo) {
			TextReader _reader = new StreamReader(pFrom);
			TextWriter _writer = new StreamWriter(pTo);
			_writer.WriteLine(_reader.ReadToEnd());
			_writer.Flush();
		}

		private void copy(Stream pFrom, StringWriter pTo) {
			TextReader _reader = new StreamReader(pFrom);
			pTo.WriteLine(_reader.ReadToEnd());
			pTo.Flush();
		}
	}
}