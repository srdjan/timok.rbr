using System;
using Timok.Core;
using Timok.Rbr.BLL.Controllers;
using Timok.Rbr.Core.Config;

namespace Timok.Rbr.DTO {
	[Serializable]
	public class CustomerAcctDto {
		public const string CustomerAcctId_PropName = "CustomerAcctId";
		public const string Name_PropName = "Name";
		public const string Status_PropName = "Status";
		public const string PartnerId_PropName = "PartnerId";
		public const string PartnerName_PropName = "PartnerName";
		public const string ServiceType_PropName = "ServiceType";
		public const string RetailType_PropName = "RetailType";
		public const string IsPrepaid_PropName = "IsPrepaid";
		public const string RoutingPlanName_PropName = "RoutingPlanName";
		public const string ServiceName_PropName = "ServiceName";
		public const string ServiceDefaultRoutingPlanName_PropName = "ServiceDefaultRoutingPlanName";

		public short CustomerAcctId { get; set; }
		public short ServiceId { get; set; }
		public string Name { get; set; }
		public Status Status { get; set; }
		public BonusMinutesType DefaultBonusMinutesType { get; set; }
		public short DefaultStartBonusMinutes { get; set; }
		public bool IsPrepaid { get; set; }
		public decimal CurrentAmount { get; set; }
		public decimal LimitAmount { get; set; }
		public decimal WarningAmount { get; set; }
		public bool AllowRerouting { get; set; }
		public bool ConcurrentUse { get; set; }
		public short PrefixInTypeId { get; set; }
		public string PrefixIn { get; set; }
		public string PrefixOut { get; set; }
		public int PartnerId { get { return Partner != null ? Partner.PartnerId : 0; } }
		public string PartnerName { get { return Partner != null ? Partner.Name : string.Empty; } }
		public PartnerDto Partner { get; set; }

		ServiceDto serviceDto;
		public ServiceDto ServiceDto {
			get {
				if (serviceDto == null) {
					serviceDto = ServiceController.Get(ServiceId);
				}
				return serviceDto;
			}
			set { serviceDto = value; }
		}

		public string ServiceName { get { return ServiceDto != null ? ServiceDto.DisplayName : string.Empty; } }

		public string ServiceDefaultRoutingPlanName {
			get {
				if (ServiceDto != null && ServiceDto.DefaultRoutingPlan != null) {
					return ServiceDto.DefaultRoutingPlan.Name;
				}
				return string.Empty;
			}
		}

		public ServiceType ServiceType { get { return ServiceDto.ServiceType; } }

		public RetailType RetailType { get { return ServiceDto.RetailType; } }


		RoutingPlanDto routingPlan;
		public RoutingPlanDto RoutingPlan {
			get {
				if (routingPlan == null) {
					routingPlan = RoutingControllerFactory.Create().GetRoutingPlan(RoutingPlanId);
				}
				return routingPlan;
			}
			set { routingPlan = value; }
		}

		//public int RoutingPlanId { get { return routingPlan != null ? routingPlan.RoutingPlanId : 0; } }
		public int RoutingPlanId { get; set; } 

		public string RoutingPlanName {
			get {
				if (routingPlan != null) {
					return routingPlan.Name;
				}
				return string.Empty;
			}
		}

		public bool WithPrefixes { get { return PrefixInTypeId != 0; } } // Configuration.Instance.Main.PrefixTypeId_NoPrefixes; } }

		public short MaxCallLength { get; set; }

		public ResellAccountDto ResellAccount { get; set; }

		public override string ToString() {
			if (WithPrefixes) {
				return Name + "  [" + PrefixIn + "]";
			}
			return Name;
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
			return CustomerAcctId.GetHashCode();
		}
	}
}