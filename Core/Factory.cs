using System;
using System.Collections.Generic;

namespace Timok.Core {
	public class GenericFactory<TKey, TBaseType> where TBaseType : class {
		public delegate TBaseType BaseTypeInvoker();

		readonly Dictionary<TKey, BaseTypeInvoker> methods = new Dictionary<TKey, BaseTypeInvoker>();

		public void Add(TKey pKey, BaseTypeInvoker pInstantiatorMethod) {
			if ( ! methods.ContainsKey(pKey)) {
				methods.Add(pKey, pInstantiatorMethod);
			}
		}

		public void CreateInstance(TKey key) {
			if (methods.ContainsKey(key)) {
				methods[key]();
			}
			else { //what would you like to do in this case?
				throw new ArgumentException(string.Format("{0} not found", key));
			}
		}
	}

	//NOTE: this one doesn't work!
	/*
	 public static class Factory1<TKey, TBaseType> where TBaseType : class {
		static readonly object locker = new object();

		// Declare the delegate
		delegate TBaseType BaseTypeInvoker();

		// The HashTable that caches the delegates
		static readonly Dictionary<TKey, BaseTypeInvoker> delegates = new Dictionary<TKey, BaseTypeInvoker>();

		public static TBaseType Create(TKey pKey) {
			BaseTypeInvoker _invoke;

			lock (locker) {
				delegates.TryGetValue(pKey, out _invoke);
			}

			return _invoke != null ? _invoke() : null;
		}

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
				throw new ArgumentException(String.Format("The given type {0} should be derivable from {1}.", pType.FullName, typeof (TBaseType).FullName), "pType");
			}

			lock (locker) {
				if (delegates.ContainsKey(pKey) == false) {
					try {
						// Create the delegate for the type
						BaseTypeInvoker _invoke = createInvoker(pType);

						// Try to invoke function (extra error check, so the delegate is not added on error)
						_invoke();

						// The invoker executed correctly (no exceptions) so let's add it to the dictionary
						delegates.Add(pKey, _invoke);
					}
					catch (InvalidCastException) {
						throw new InvalidCastException(String.Format(CultureInfo.InvariantCulture, "{0} couldn't be casted to {1}.", pType.FullName, typeof(TBaseType).FullName));
					}
				}
			}
		}

		//----------------------------- private -------------------------------------
		// Create a new delegate that returns a new object of Type t.
		static BaseTypeInvoker createInvoker(Type pType) {
			// Get the Default constructor.
			ConstructorInfo _ctor = pType.GetConstructor(new Type[0]);

			// Check if the constructor exists.
			if (_ctor == null) {
				throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "{0} doesn't have a public default constructor.", pType.FullName));
			}

			// Create a new method.
			DynamicMethod _dm = new DynamicMethod(pType.Name + "Ctor", pType, new Type[0], typeof (TBaseType).Module);

			// Generate the intermediate language.
			ILGenerator _lgen = _dm.GetILGenerator();
			_lgen.Emit(OpCodes.Newobj, _ctor);
			_lgen.Emit(OpCodes.Ret);

			// Finish the method and create new delegate pointing at it.
			return (BaseTypeInvoker) _dm.CreateDelegate(typeof (BaseTypeInvoker));
		}
	}
	*/
}