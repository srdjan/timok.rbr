// <fileinfo name="AccessNumberListRow.cs">
//		<copyright>
//			Copyright © 2002-2007 Timok ES LLC. All rights reserved.
//		</copyright>
//		<remarks>
//			You can update this source code manually. If the file
//			already exists it will not be rewritten by the generator.
//		</remarks>
//		<generator rewritefile="False" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase.Base;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>AccessNumberList</c> table.
	/// </summary>
	[Serializable]
	public class AccessNumberListRow : AccessNumberListRow_Base {
		public const string ScriptType_PropName = "ScriptType";
		public const string ScriptType_DisplayName = "Script Type";
		public const string ScriptLanguage_PropName = "ScriptLanguage";
		public const string ScriptLanguage_DisplayName = "Language";
		public const string SurchargeType_PropName = "SurchargeType";
		public const string SurchargeType_DisplayName = "Surcharge Type";

		public ScriptType ScriptType { get { return (ScriptType) Script_type; } set { Script_type = (int) value; } }
		public ScriptLanguage ScriptLanguage { get { return (ScriptLanguage) Language; } set { Language = (byte) value; } }
		public SurchargeType SurchargeType { get { return (SurchargeType) Surcharge_type; } set { Surcharge_type = (byte) value; } }

		public new decimal Surcharge {
			get { return base.Surcharge; }
			set {
				if (value > 99.9999999M) {
					base.Surcharge = decimal.Zero;
				}
				else if (value < decimal.Zero) {
					base.Surcharge = decimal.Zero;
				}
				else {
					base.Surcharge = value;
				}
			}
		}
	} // End of AccessNumberListRow class
} // End of namespace