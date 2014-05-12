using System;
using System.Collections;
using System.Data;

namespace Timok.Core {

	public class ConvertEnum {
		public const string DisplayMemberDefaultName = "name";
		public const string ValueMemberDefaultName = "value";

		private ConvertEnum(){}

		public static Array ToArray(Type EnumType){
			if(EnumType.BaseType != typeof(Enum))
				throw new ArgumentException(
					"Not a valid argument type.\r\n" + 
					"Type of " + EnumType.GetType() + " is not supported by this method." + 
					"Only type of Enum is supported.", "EnumType");
			//Type _valueType = Enum.GetUnderlyingType(EnumType);
			ArrayList _list = new ArrayList();

			foreach (string _name in Enum.GetNames(EnumType) ){
				object _value = Enum.Parse(EnumType, _name);
				_list.Add(_value);
			}
			Array _array = Array.CreateInstance(EnumType, _list.Count);
			_list.CopyTo(_array);
			return _array;

		}

		public static DataView ToDataView(Type EnumType){
			return ToDataView(EnumType, "_");
		}

		public static DataView ToDataView(Type EnumType, string ReplaceUnderscoreWith){
			return ToDataView(EnumType, DisplayMemberDefaultName, ValueMemberDefaultName, ReplaceUnderscoreWith);
		}

		public static DataView ToDataView(Type EnumType, string pDisplayMemberName, string pValueMemberName){
			return ToDataView(EnumType, DisplayMemberDefaultName, ValueMemberDefaultName, "_");
		}

		public static DataView ToDataView(Type EnumType, string pDisplayMemberName, string pValueMemberName, string ReplaceUnderscoreWith){
			if(EnumType.BaseType != typeof(Enum))
				throw new ArgumentException(
					"Not a valid argument type.\r\n" + 
					"Type of " + EnumType.GetType() + " is not supported by this method." + 
					"Only type of Enum is supported.", "EnumType");

			Type _valueType = Enum.GetUnderlyingType(EnumType);;
			DataTable _dt = new DataTable(EnumType.Name);
			_dt.Columns.Add(new DataColumn(pValueMemberName, _valueType));
			_dt.Columns.Add(new DataColumn(pDisplayMemberName, typeof(string)));

			foreach (string _constName in Enum.GetNames(EnumType) ){
				object _value = Convert.ChangeType(Enum.Parse(EnumType, _constName), _valueType);
				string _name = _constName;
				if(ReplaceUnderscoreWith != "_")
					_name = _name.Replace("_", ReplaceUnderscoreWith);
				_dt.Rows.Add(new object[]{_value, _name});
			}
			return _dt.DefaultView;
		}
	}
}
