using System;
using System.Xml.Serialization;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase.Base;

namespace Timok.Rbr.DAL.RbrDatabase {
	[Serializable]
	public class PersonRow : PersonRow_Base {
		public const int DefaultId = 77777;

		[XmlIgnore]
		public AccessScope AccessScope { get { return (AccessScope) Is_reseller; } set { Is_reseller = (byte) value; } }

		[XmlIgnore]
		public PermissionType PermissionType { get { return (PermissionType) Permission; } set { Permission = (byte) value; } }

		[XmlIgnore]
		public Status RegistrationStatus { get { return (Status) Registration_status; } set { Registration_status = (byte) value; } }

		[XmlIgnore]
		public Status PersonStatus { get { return (Status) Status; } set { Status = (byte) value; } }

		public void MigrateIs_Reseller_2_AccessScope() {
			if ( ! IsVirtual_switch_idNull) {
				if (Virtual_switch_id == AppConstants.DefaultVirtualSwitchId) {
					AccessScope = AccessScope.Switch;
				}
				else {
					AccessScope = AccessScope.VirtualSwitch;
				}
				return;
			}

			if ( ! IsPartner_idNull && Partner_id > 0) {
				if (Is_reseller == 1) {
					AccessScope = AccessScope.ResellAgent;
				}
				else {
					AccessScope = AccessScope.Partner;
				}
				return;
			}

			if ( ! IsGroup_idNull && Group_id > 0) {
				AccessScope = AccessScope.CustomerSupport;
				return;
			}

			if ( ! IsRetail_acct_idNull && Retail_acct_id > 0) {
				AccessScope = AccessScope.Consumer;
				return;
			}

			AccessScope = AccessScope.None;
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}
	} // End of PersonRow class
} // End of namespace