using System;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.BLL.DOM {
	public interface IRetailAccount {
		int Id { get; }

		Status AcctStatus { get; set; }
		//Status SubAcctStatus { get; set; }

		short ServiceId { get; }
		short CustomerAcctId { get; }

		long SerialNumber { get; }

		bool WithBonusMinutes { get; }

		decimal StartingBalance { get; }
		decimal CurrentBalance { get; }

		short CurrentBonusBalance { get; }

		void UpdateUsage();

		bool IsPrepaid { get; }
		bool NeverUsed { get; }
		DateTime DateToExpire { get; }

		int Rate(CustomerRoute pCustomerRoute, ref Cdr pCdr);
		void Debit(decimal pAmount, short pBonusMinutes);
	}
}