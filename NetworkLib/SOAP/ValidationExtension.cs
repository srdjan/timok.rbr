using System;
using System.Web;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using T = Timok.Core.Logging.TimokLogger;

namespace Timok.Core.NetworkLib.SOAP 
{
	//----------------------------------- Soap Extension Attribute ----------------------------------------
	//-----------------------------------------------------------------------------------------------------
	[AttributeUsage(AttributeTargets.Method)]
	public class ValidationAttribute : SoapExtensionAttribute 
	{
		public string Namespace;
		public string SchemaLocation;
		private int priority;

		public ValidationAttribute() {}
		public ValidationAttribute(string ns, string schemaLoc) {
			this.Namespace = ns;
			this.SchemaLocation = schemaLoc;
		}

		public override Type ExtensionType {
			get { return typeof(ValidationExtension); }
		}

		public override int Priority {
			get { return priority; }
			set { priority = value; }
		}
	}

	//------------------------------------ Soap Extension -------------------------------------------------
	//-----------------------------------------------------------------------------------------------------
	public class ValidationExtension : SoapExtension 
	{
		private ValidationEventHandler validateEventHandler = null;
		private XmlSchemaCollection schemaCache = null;

		public ValidationExtension() {
			this.validateEventHandler = new ValidationEventHandler(OnValidate);
		}

		//-- The SOAP extension was configured to run using attribute. 
		//-- GetInitializer is called once, the first time the class is used the returned object is then cached and passed to initialize each
		//-- time a new instance is created and used. 
		public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute) {
			ValidationAttribute _va = attribute as ValidationAttribute;
			if (_va == null) {
				throw new Exception("Unsupported initializer type");
			}
			
			if (_va.Namespace.Equals("")) {
				throw new Exception("Namespace not specified");
			}
			
			if (_va.SchemaLocation.Equals("")) {
				throw new Exception("Schema location not specified");
			}

			schemaCache.Add(_va.Namespace, HttpContext.Current.Server.MapPath(_va.SchemaLocation));
			schemaCache.Add("http://schemas.xmlsoap.org/soap/envelope/", HttpContext.Current.Server.MapPath("soapenvelope.xml"));
			schemaCache.Add("http://schemas.xmlsoap.org/soap/encoding/", HttpContext.Current.Server.MapPath("soapencoding.xml"));
			return schemaCache;
		}

		//-- The SOAP extension was configured to run using a configuration file
		public override object GetInitializer(Type serviceType) {
			//-- not usable for this extension: USE ATTRIBUTE config/init instead xml.config file!!
			return null;
		}

		public override void Initialize(object initializer) {
			//-- get the cached SchemaCollection so we don't have to do the initialization again
			schemaCache = (XmlSchemaCollection) initializer;
		}

		public override void ProcessMessage(SoapMessage message) {
			switch (message.Stage) {
				case SoapMessageStage.AfterSerialize:
					try {
						//-- this is where we need to perform schema validation
						XmlValidatingReader vr = new XmlValidatingReader(new XmlTextReader(message.Stream));
						vr.Schemas.Add(this.schemaCache);
						vr.ValidationEventHandler += this.validateEventHandler;
						while (vr.Read()) ; // do nothing
					}
					finally {
						//-- reposition the stream so remaining deserialization isn't affected
						message.Stream.Position = 0;
					}
					break;
				default:
					break;
			}
		}

		private void OnValidate(object source, ValidationEventArgs args) {
			throw new SoapException(String.Format("Validation error: {0}", args.Exception.Message), SoapException.ClientFaultCode);
		}
	}
}
