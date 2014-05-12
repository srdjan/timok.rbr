using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Timok.Core;
using Timok.Logger;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.Core {
	[Serializable]
	public class TransactionMsg {
		[NonSerialized] static readonly object padlock = new object();
		[NonSerialized] static Assembly assembly;
		[NonSerialized] readonly long instanceSequence;

		//-- Public fields, for serialization
		public string AssemblyFullName;
		public string TypeFullName;
		public string MethodFullName;
		public object[] Arguments;
		string auditFolder;

		static TransactionMsg() {
			assembly = null;
		}

		//-- for serialization
		public TransactionMsg() {}

		public TransactionMsg(CallingMethodInfo pCallingMethodInfo, string pAuditFolder) {
			instanceSequence = RbrClient.Instance.GetNextSequence();

			AssemblyFullName = pCallingMethodInfo.AssemblyFullName;
			TypeFullName = pCallingMethodInfo.TypeFullName;
			MethodFullName = pCallingMethodInfo.FullName;
			Arguments = pCallingMethodInfo.MethodParameters;

			auditFolder = pAuditFolder;
		}

		public void Serialize() {
			lock (padlock) {
				//-- (1) Create a file name
				var _fileName = string.Format("{0}{1}.{2}{3}", DateTime.Now.ToString("yyyy-MM-dd."), instanceSequence.ToString("D7"), AppConstants.Rbr, AppConstants.PendingExtension);
				var _filePath = Path.Combine(auditFolder, _fileName);

				//-- (2) Serialize and save:
				using (Stream _stream = new FileStream(_filePath, FileMode.Create)) {
					IFormatter _formatter = new BinaryFormatter();
					_formatter.Serialize(_stream, this);
					_stream.Close();
				}
			}
		}

		public static TransactionMsg Deserialize(string pFilePath) {
			TransactionMsg _txMsg;
			try {
				_txMsg = deserializeFromFile(pFilePath);
			}
			catch {
				Thread.Sleep(100);
				_txMsg = deserializeFromFile(pFilePath);
			}
			return _txMsg;
		}

		public void Execute() {
			//T.LogRbr(LogSeverity.Debug, "TransactionMsg.Execute", string.Format("Type={0}, Method={1}", TypeFullName, MethodFullName));
			if (assembly == null) {
				getAssembly(AssemblyFullName);
				if (assembly == null) {
					throw new Exception("MESSAGE: Assembly: " + AssemblyFullName + " NOT FOUND");
				}
			}

			var _type = assembly.GetType(TypeFullName, true);
			if (_type == null) {
				throw new Exception("MESSAGE: Type: " + TypeFullName + " NOT FOUND");
			}

			var _method = _type.GetMethod(MethodFullName);
			if (_method == null) {
				throw new Exception("MESSAGE: Method: " + MethodFullName + " NOT FOUND");
			}

			//T.LogRbr(LogSeverity.Debug, "TransactionMsg.Execute", string.Format("Invoke Type={0}, Args={1}", _type, toString(Arguments)));
			_method.Invoke(_type, Arguments);
		}

		static string toString(object[] pArguments) {
			var _strBuilder = new StringBuilder();
			foreach (var _object in pArguments) {
				_strBuilder.Append(_object.ToString());
			}
			return _strBuilder.ToString();
		}

		//------------------------- Private Methods --------------------------------------------------------
		static void getAssembly(string pFullName) {
			var _assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (var _asm in _assemblies) {
				if (_asm.FullName == pFullName) {
					assembly = _asm;
					return;
				}
			}
			assembly = Assembly.Load(pFullName);
		}

		static TransactionMsg deserializeFromFile(string pFilePath) {
			using (var _fs = new FileStream(pFilePath, FileMode.Open, FileAccess.Read)) {
				IFormatter _formatter = new BinaryFormatter();
				return (TransactionMsg) _formatter.Deserialize(_fs);
			}
		}
	}
}