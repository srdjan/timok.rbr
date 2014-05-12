using System;
using Timok.Core;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class PersonDto {
		public const string PersonId_PropName = "PersonId";
		public const string Login_PropName = "Login";
		public const string Status_PropName = "Status";
		public const string AccessScope_PropName = "AccessScope";
		public const string Name_PropName = "Name";
		public const string PermissionType_PropName = "PermissionType";
		//public const string MaxAmount_PropName = "MaxAmount";

		int personId;
		public int PersonId { get { return personId; } set { personId = value; } }

		public string Login { get; set; }

		public string Password { get; set; }

		public Status RegistrationStatus { get; set; }

		public Status Status { get; set; }

		public AccessScope AccessScope { get; set; }

		public string Salt { get; set; }

		public int PartnerId { get; set; }

		public int RetailAcctId { get; set; }

		CustomerSupportGroupDto customerSupportGroup;
		public CustomerSupportGroupDto CustomerSupportGroup { get { return customerSupportGroup; } set { customerSupportGroup = value; } }

		public int VendorId { get { return customerSupportGroup != null ? customerSupportGroup.VendorId : 0; } }

		public int GroupId { get { return customerSupportGroup != null ? customerSupportGroup.GroupId : 0; } }

		public VirtualSwitchDto VirtualSwitch { get; set; }

		public int VirtualSwitchId { get { return VirtualSwitch != null ? VirtualSwitch.VirtualSwitchId : 0; } }

		public ContactInfoDto ContactInfo { get; set; }

		public int ContactInfoId { get { return ContactInfo != null ? ContactInfo.ContactInfoId : 0; } }

		public string Name { get; set; }

		public PermissionType PermissionType { get; set; }

		public PersonDto() {
			Login = string.Empty;
			Password = string.Empty;
			Salt = string.Empty;
			Name = string.Empty;
		}

		//NOTE: compare object's values (not refs)
		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			return ObjectComparer.AreEqual(this, obj);
		}

		public override int GetHashCode() {
			//TODO: finish it, get hashes for all fields
			return personId.GetHashCode();
		}
	}
}