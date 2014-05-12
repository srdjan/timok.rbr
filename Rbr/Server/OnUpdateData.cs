using System;
using Timok.Core.Logger;
using Timok.Rbr.Core;

namespace Timok.Rbr.Server {
	public class OnUpdateData : RbrCommand {
		public OnUpdateData(IRbrDispatcher pDispatcher, ILogger pLogger) : base(pLogger) { 
			dispatcher = pDispatcher;
			nameAndVersion = RbrApi.OnUpdateData;
		}

		protected override void execute() {
			throw new NotImplementedException("OnUpdateData.Execute");
			//try {
				//var _list = new List<IChangedObject>();
				//for (var _i=0; _i<parameters.Length; _i++) {
				//  var _changedObject = Cloner.CloneFromString(parameters[_i]) as IChangedObject;
				//  _list.Add(_changedObject);
				//}
				//dispatcher.RbrSync(_list.ToArray());
			//}
			//catch (Exception _ex) {
				//T.LogRbr(LogSeverity.Critical, "OnUpdateData.Execute", string.Format("OnUpdate Data Exception:\r\n{0}", _ex));
		//}
		}
	}
}
