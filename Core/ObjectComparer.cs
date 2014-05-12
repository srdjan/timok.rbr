using System;
using System.Collections;
using System.Reflection;

namespace Timok.Core {
	[Serializable]
	public class ObjectComparer : IComparer {
		#region methods
		/// <summary>
		/// Compares two objects and returns a value indicating whether one is less than, equal to or greater than the other.
		/// </summary>
		/// <param name="x">First object to compare.</param>
		/// <param name="y">Second object to compare.</param>
		/// <returns></returns>
		public int Compare(object x, object y) {
			//Get types of the objects
			Type type_of_x = x.GetType();
			Type type_of_y = y.GetType();

			for (int i = 0; i < Fields.Length; i++) {
				//Get each property by name
				PropertyInfo pi_of_x = type_of_x.GetProperty(Fields[i]);
				PropertyInfo pi_of_y = type_of_y.GetProperty(Fields[i]);

				//Get the value of the property for each object
				object pval_of_x = null;
				object pval_of_y = null;
				
				pval_of_x = pi_of_x.GetValue(x, null);
				pval_of_y = pi_of_y.GetValue(y, null);

				int iResult = 0;
				//Compare values, using IComparable interface of the property's type
				if (pval_of_x == null && pval_of_y == null) {
					//x(null) and y(null) are equal
					iResult = 0;
				}
				else if (pval_of_x == null && pval_of_y != null) {
					//null is always less then anything else
					iResult = -1;
				}
				else {
					//Compare actual values
					if (pval_of_x is IComparable) {
						iResult = (pval_of_x as IComparable).CompareTo(pval_of_y);
					}
					else {
						string _temp = pval_of_x.ToString();
						iResult = _temp.CompareTo(pval_of_y.ToString());
					}
				}

				if (iResult != 0) {
					//Return if not equal
					if (Descending[i]) {
						//Invert order
						return -iResult;
					}
					else {
						return iResult;
					}
				}
			}
			//Objects have the same sort order
			return 0;
		}
		#endregion

		#region constructors
		/// <summary>
		/// Create a comparer for objects of arbitrary types having using the specified properties
		/// </summary>
		/// <param name="fields">Properties to sort objects by</param>
		public ObjectComparer(params string[] fields)
			: this(fields, new bool[fields.Length]) {}


		/// <summary>
		/// Create a comparer for objects of arbitrary types having using the specified propertie and sort order
		/// </summary>
		/// <param name="field">Propertie to sort objects by</param>
		/// <param name="descending">To sort in descending order</param>
		public ObjectComparer(string field, bool descending)
			: this(new string[]{field}, new bool[]{descending})  {
		}

		/// <summary>
		/// Create a comparer for objects of arbitrary types having using the specified properties and sort order
		/// </summary>
		/// <param name="fields">Properties to sort objects by</param>
		/// <param name="descending">Properties to sort in descending order</param>
		public ObjectComparer(string[] fields, bool[] descending) {
			Fields = fields;
			Descending = descending;
		}

		#endregion

		#region protected fields
		/// <summary>
		/// Properties to sort objects by
		/// </summary>
		protected string[] Fields;

		/// <summary>
		/// Properties to sort in descending order
		/// </summary>
		protected bool[] Descending;
		#endregion
	
		public static bool AreEqual(object obj1, object obj2) {
			if (obj1 == null && obj2 == null)
				return true;

			if (obj1 == null || obj2 == null)
				return false;

			if (! obj1.GetType().Equals(obj2.GetType())) {
				return false;
			}

			if (obj1.GetType().IsValueType) {
				return obj1.Equals(obj2);
			}

			PropertyInfo[] props = obj1.GetType().GetProperties();
			ArrayList _propNames = new ArrayList();
			foreach (PropertyInfo _pi in props) {
				_propNames.Add(_pi.Name);
			}

			if (_propNames.Count > 0) {
				string[] _fields = new string[_propNames.Count];
				_propNames.CopyTo(_fields);
				return (new ObjectComparer(_fields).Compare(obj1, obj2) == 0);
			}

			return Equals(obj1, obj2);
		}
	}
}
