using System;
using Timok.Core;
using Timok.Rbr.BLL.DTO;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO 
{
	[Serializable]
	public class ServiceDto {
		public const string DisplayName_PropName = "DisplayName";
		public const string DisplayName_DisplayName = "Name";

    public const string Name_PropName = "Name";
    public const string Name_DisplayName = "Name";
    public const string ServiceId_PropName = "ServiceId";
    public const string ServiceId_DisplayName = "ServiceId";
    public const string ComboDisplayName_PropName = "ComboDisplayName";
    public const string DefaultRoutingPlanName_PropName = "DefaultRoutingPlanName";
    public const string ServiceType_PropName = "ServiceType";
    public const string RetailType_PropName = "RetailType";

		short serviceId;
		public short ServiceId { get { return serviceId; } set { serviceId = value; } }

		public string ServiceText {
			get {
				string _serviceText;
				if (IsShared) {
					_serviceText = "Shared Service [" + Name + "]";
				}
				else {
					_serviceText = ServiceType + ": " + DisplayName;
				}
				return _serviceText;
			}
		}

		public string DisplayName { get { return IsShared ? Name : Name.Replace(AppConstants.CustomerServiceNamePrefix, string.Empty); } }

    public string ComboDisplayName {
      get { return ServiceType == ServiceType.Retail ? string.Format("{0}  [ {1} :: {2} ]", DisplayName, ServiceType, RetailType) : string.Format("{0}  [ {1} ]", DisplayName, ServiceType); }
    }

		public string Name { get; set; }

		public Status Status { get; set; }

		public ServiceType ServiceType { get; set; }

		public RetailType RetailType { get; set; }

		public bool IsShared { get; set; }

		public bool IsDedicated { get { return !IsShared; } }

		public RatingType RatingType { get; set; }

		public bool IsRatingEnabled { get { return RatingType != RatingType.Disabled; } }

		public int PinLength { get; set; }

		public int PayphoneSurchargeId { get { return PayphoneSurcharge != null ? PayphoneSurcharge.PayphoneSurchargeId : 0; } }

		public PayphoneSurchargeDto PayphoneSurcharge { get; set; }

		public int CallingPlanId {  get { return DefaultRoutingPlan != null ? DefaultRoutingPlan.CallingPlanId : 0; } }

    public int DefaultRoutingPlanId {  get { return DefaultRoutingPlan != null ? DefaultRoutingPlan.RoutingPlanId : 0; } }

    public string DefaultRoutingPlanName {  get { return DefaultRoutingPlan != null ? DefaultRoutingPlan.Name : string.Empty; } }

		public RoutingPlanDto DefaultRoutingPlan { get; set; }

		public int SweepScheduleId { get; set; }

		public decimal SweepFee { get; set; }

		public int SweepRule { get; set; }

		public int VirtualSwitchId { get; set; }

		public AccessNumberDto[] AccessNumbers { get; set; }

		public RatedRouteDto DefaultRoute { get; set; }

		public RatingInfoDto DefaultRatingInfo { get; set; }

		public string CurrencyFormat {
			get {
				string _currencyFormat;
				switch (ServiceType) {
					case ServiceType.Wholesale:
					case ServiceType.CallCenter:
						_currencyFormat = AppConstants.WholesaleRateAmountFormat;
						break;
					case ServiceType.Retail:
						switch (RetailType) {
							case RetailType.PhoneCard:
							case RetailType.Residential:
								_currencyFormat = AppConstants.RetailRateAmountFormat;
								break;
							case RetailType.None:
							default:
								_currencyFormat = Configuration.Instance.Main.AmountFormat;
								break;
						}
						break;
					default:
						_currencyFormat = Configuration.Instance.Main.AmountFormat;
						break;
				}
				return _currencyFormat;
			}
		}

		public BalancePromptType BalancePromptType { get; set; }

		public decimal BalancePromptPerUnit { get; set; }

		public ServiceDto() {
			Status = Status.Active;
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
			return serviceId.GetHashCode();
		}
	}
}