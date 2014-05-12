using System;

using Timok.Rbr.Core;

namespace Timok.Rbr.DTO {

	[Serializable]
	public class RetailAccountPaymentDto {
    public const string DateTime_PropName = "DateTime";
    public const string DateTime_DisplayName = "Date Time";

    public const string PreviousAmount_PropName = "PreviousAmount";
    public const string PreviousAmount_DisplayName = "Previous Amount";

    public const string Amount_PropName = "Amount";
    public const string Amount_DisplayName = "Adjust Amount";

    public const string RetailAcctId_PropName = "RetailAcctId";
    public const string RetailAcctId_DisplayName = "Acct ID";

    public const string PersonName_PropName = "PersonName";
    public const string PersonName_DisplayName = "Executed by";

    public const string BalanceAdjustmentReasonDescription_PropName = "BalanceAdjustmentReasonDescription";
    public const string Comments_PropName = "Comments";
    public const string CdrKey_PropName = "CdrKey";

    private DateTime dateTime;
    public DateTime DateTime {
      get { return dateTime; }
      set { dateTime = value; }
    }

    private int retailAcctId;
    public int RetailAcctId {
      get { return retailAcctId; }
      set { retailAcctId = value; }
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

    private short previousbonusMinutes;
    public short PreviousBonusMinutes {
      get { return previousbonusMinutes; }
      set { previousbonusMinutes = value; }
		}

		private short addedBonusMinutes;
    public short AddedBonusMinutes {
      get { return addedBonusMinutes; }
      set { addedBonusMinutes = value; }
		}

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

    private string cdrKey;
    public string CdrKey {
      get { return cdrKey; }
      set { cdrKey = value; }
    }

    private string comments = string.Empty;
    public string Comments {
      get { return comments; }
      set { comments = value; }
    }

		public RetailAccountPaymentDto() { }
  }
}
