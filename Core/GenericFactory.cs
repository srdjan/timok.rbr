using System;
using System.Collections.Generic;
using System.Globalization;

namespace Timok.Core {
	public static class Activator<TKey, TBaseType> where TBaseType : class {
		static readonly Dictionary<TKey, Type> types = new Dictionary<TKey, Type>();

		static readonly object locker = new object();

		public static void Add(TKey pKey, Type pType) {
			// Some checks on the type argument
			if (pType == null) {
				throw new ArgumentNullException("pType");
			}

			// Check if object is not a class
			if (pType.IsClass == false) {
				throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "{0} is not a reference type.", pType.FullName), "pType");
			}

			// Check if object is abstract
			if (pType.IsAbstract) {
				throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "{0} is an abstract class, which can not be created.", pType.FullName), "pType");
			}

			// Check whether the given type is assignable from
			if (typeof(TBaseType).IsAssignableFrom(pType) == false) {
				throw new ArgumentException(String.Format("The given type {0} should be derivable from {1}.", pType.FullName, typeof(TBaseType).FullName), "pType");
			}

			lock (locker) {
				if (types.ContainsKey(pKey) == false) {
					//NOTE: why this:			TBaseType _t = (TBaseType)Activator.CreateInstance(pType);
					types.Add(pKey, pType);
				}
			}
		}

		public static TBaseType CreateInstance(TKey pKey, object[] pArgs) {
			Type _type;

			lock (locker) {
				types.TryGetValue(pKey, out _type);
			}

			return _type != null ? (TBaseType) Activator.CreateInstance(_type, pArgs) : null;
		}
	}
}