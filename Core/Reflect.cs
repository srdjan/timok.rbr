using System;
using System.Reflection;

namespace Timok.Core
{
	public class Reflect {
		public static void Invoke(Assembly pAssembly, string pTypeName, string pMethodName, object[] pParameters) {
			Type _reflectedClass = null;

			try {
				_reflectedClass = pAssembly.GetType(pTypeName, true);

				if (_reflectedClass == null) {
					throw new ApplicationException("MESSAGE: Type: " + pTypeName + " NOT FOUND");
				}

				MethodInfo _methodToInvoke = _reflectedClass.GetMethod(pMethodName);
				if (_methodToInvoke == null) {
					throw new ApplicationException("MESSAGE: Method: " + pMethodName + " NOT FOUND");
				}
				
				object _result = _methodToInvoke.Invoke(_reflectedClass, pParameters);
			}
			catch (Exception _ex) {
				throw new ApplicationException(Utils.GetFullExceptionInfo(_ex));
			}
		}
	}
}
