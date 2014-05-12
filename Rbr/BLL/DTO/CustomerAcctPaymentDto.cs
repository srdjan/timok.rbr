using System;

namespace Timok.Rbr.DTO {

	[Serializable]
	public class CustomerAcctPaymentDto {
    public const string DateTime_PropName = "DateTime";
    public const string DateTime_DisplayName = "Date Time";

    public const string PreviousAmount_PropName = "PreviousAmount";
    public const string PreviousAmount_DisplayName = "Previous Amount";

    public const string Amount_PropName = "Amount";
    public const string Amount_DisplayName = "Adjust Amount";

    public const string WarningPercentage_PropName = "WarningPercentage";
    public const string WarningPercentage_DisplayName = "Warning %";

    public const string AdjustWarningPercentage_PropName = "AdjustWarningPercentage";
    public const string AdjustWarningPercentage_DisplayName = "Adjust Warning %";

    public const string CustomerAcctName_PropName = "CustomerAcctName";
    public const string CustomerAcctName_DisplayName = "Acct Name";

    public const string PersonName_PropName = "PersonName";
    public const string PersonName_DisplayName = "Executed by";

    public const string BalanceAdjustmentReasonDescription_PropName = "BalanceAdjustmentReasonDescription";
    public const string Comments_PropName = "Comments";

    private DateTime dateTime;
    public DateTime DateTime {
      get { return dateTime; }
      set { dateTime = value; }
    }

    private decimal previousAmount;
    public decimal PreviousAmount {
      get { return previousAmount; }
      set { previousAmount = value; }
    }

		private decimal amount;
		public decimal Amount {
			get { return amount; }
			set { amount = value; }
		}

		private decimal warningLevel;
		public decimal WarningLevel {
			get { return warningLevel; }
			set { warningLevel = value; }
		}

		//private int warningPercentage;
		//public int WarningPercentage {
		//  get { return warningPercentage; }
		//  set { warningPercentage = value; }
		//}

		//private bool adjustWarningPercentage;
		//public bool AdjustWarningPercentage {
		//  get { return adjustWarningPercentage; }
		//  set { adjustWarningPercentage = value; }
		//}

    private BalanceAdjustmentReasonDto balanceAdjustmentReason;
    public BalanceAdjustmentReasonDto BalanceAdjustmentReason {
      get { return balanceAdjustmentReason; }
      set { balanceAdjustmentReason = value; }
    }

    public int BalanceAdjustmentReasonId {
      get { return balanceAdjustmentReason.BalanceAdjustmentReasonId; }
    }

    public string BalanceAdjustmentReasonDescription {
      get { return balanceAdjustmentReason.Description; }
    }

    private short customerAcctId;
    public short CustomerAcctId {
      get { return customerAcctId; }
      set { customerAcctId = value; }
    }

    private string customerAcctName;
    public string CustomerAcctName {
      get { return customerAcctName; }
      set { customerAcctName = value; }
    }

    private PersonDto person;
    public PersonDto Person {
      get { return person; }
      set { person = value; }
    }

    public int PersonId {
      get { return person.PersonId; }
    }

    public string PersonName {
      get { return person.Name; }
    }

    private string comments = string.Empty;
    public string Comments {
      get { return comments; }
      set { comments = value; }
    }

    public CustomerAcctPaymentDto() { }
	}
}
