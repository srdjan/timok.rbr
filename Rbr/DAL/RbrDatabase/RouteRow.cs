// <fileinfo name="RouteRow.cs">
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
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Timok.Rbr.DAL.RbrDatabase {
	/// <summary>
	/// Represents a record in the <c>Route</c> table.
	/// </summary>
	[Serializable]
	public class RouteRow : RouteRow_Base {
		public const string IsProper_PropName = "IsProper";
		public const string IsProper_DisplayName = "Proper";

		public const string BreakoutName_PropName = "BreakoutName";
		public const string BreakoutName_DisplayName = "Breakout Name";

		public const string CountryName_PropName = "CountryName";
		public const string CountryName_DisplayName = "Country Name";

		public const string RouteStatus_PropName = "RouteStatus";
		public const string RouteStatus_DisplayName = "Status";

		[XmlIgnore]
		public Status RouteStatus {
			get { return (Status) this.Status; }
			set { this.Status = (byte) value; }
		}

		[XmlIgnore]
		public bool IsProper {
			get { return this.BreakoutName.ToUpper() == AppConstants.ProperNameSuffix.ToUpper(); }
		}

		[XmlIgnore]
		public string CountryName {
			get { 
				if (this.Name != null) {
					return this.Name.Split(AppConstants.SubRouteSeparator.ToCharArray())[0];
				}
				else {
					return string.Empty;
				}
			}
		}

		[XmlIgnore]
		public string BreakoutName {
			get { 
				if (this.Name != null && this.Name.IndexOf(AppConstants.SubRouteSeparator) > 0) {
					return this.Name.Split(AppConstants.SubRouteSeparator.ToCharArray())[1];
				}
				else {
					return string.Empty;
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RouteRow"/> class.
		/// </summary>
		public RouteRow(){
			// EMPTY
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

	} // End of RouteRow class
} // End of namespace
