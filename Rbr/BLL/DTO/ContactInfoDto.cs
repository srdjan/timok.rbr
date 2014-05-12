using System;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class ContactInfoDto {
		int contactInfoId;
		public int ContactInfoId { get { return contactInfoId; } set { contactInfoId = value; } }

		string address1 = string.Empty;
		public string Address1 { get { return address1; } set { address1 = value; } }

		string address2 = string.Empty;
		public string Address2 { get { return address2; } set { address2 = value; } }

		string city = string.Empty;
		public string City { get { return city; } set { city = value; } }

		string state = string.Empty;
		public string State { get { return state; } set { state = value; } }

		string zip = string.Empty;
		public string Zip { get { return zip; } set { zip = value; } }

		string email = string.Empty;
		public string Email { get { return email; } set { email = value; } }

		long homePhone;
		public long HomePhone { get { return homePhone; } set { homePhone = value; } }

		long cellPhone;
		public long CellPhone { get { return cellPhone; } set { cellPhone = value; } }

		long workPhone;
		public long WorkPhone { get { return workPhone; } set { workPhone = value; } }

		public ContactInfoDto() {}
	}
}