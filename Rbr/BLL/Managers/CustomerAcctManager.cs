using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Timok.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Managers {
	internal class CustomerAcctManager {
		CustomerAcctManager() { }

		#region Getters

		internal static int GetAcctUsageCount(Rbr_Db pDb, short pServiceId) { return pDb.CustomerAcctCollection.GetCountByServiceId(pServiceId); }

		internal static CustomerAcctDto GetAcct(Rbr_Db pDb, short pCustomerAcctId) {
			var _customerAcctRow = pDb.CustomerAcctCollection.GetByPrimaryKey(pCustomerAcctId);
			return getAcct(pDb, _customerAcctRow);
		}

		internal static CustomerAcctDto[] GetAccts(Rbr_Db pDb) {
			var _list = new List<CustomerAcctDto>();
			var _customerAcctRows = pDb.CustomerAcctCollection.GetAll();
			foreach (var _customerAcctRow in _customerAcctRows) {
				_list.Add(getAcct(pDb, _customerAcctRow));
			}
			return _list.ToArray();
		}

		internal static CustomerAcctDto[] GetAcctsByServiceId(Rbr_Db pDb, short pServiceId) {
			var _list = new List<CustomerAcctDto>();
			var _customerAcctRows = pDb.CustomerAcctCollection.GetByService_id(pServiceId);
			foreach (var _customerAcctRow in _customerAcctRows) {
				_list.Add(getAcct(pDb, _customerAcctRow));
			}
			return _list.ToArray();
		}

		//-- gets Customer and Resell accounts for a given PartnerId
		internal static CustomerAcctDto[] GetAcctsByPartnerId(Rbr_Db pDb, int pPartnerId) {
			var _list = new List<CustomerAcctDto>();
			var _customerAcctRows = pDb.CustomerAcctCollection.GetByPartner_id(pPartnerId);
			foreach (var _customerAcctRow in _customerAcctRows) {
				_list.Add(getAcct(pDb, _customerAcctRow));
			}
			return _list.ToArray();
		}

		internal static CustomerAcctDto[] GetAllResellAcctsByPartnerId(Rbr_Db pDb, int pPartnerId) {
			var _list = new List<CustomerAcctDto>();
			var _customerAcctRows = pDb.CustomerAcctCollection.GetAllResellAcctsByPartnerId(pPartnerId);
			foreach (var _customerAcctRow in _customerAcctRows) {
				_list.Add(getAcct(pDb, _customerAcctRow));
			}
			return _list.ToArray();
		}

		internal static CustomerAcctDto[] GetAcctsByRoutingPlanId(Rbr_Db pDb, int pRoutingPlanId) {
			var _list = new List<CustomerAcctDto>();
			var _customerAcctRows = pDb.CustomerAcctCollection.GetByRouting_plan_id(pRoutingPlanId);
			foreach (var _customerAcctRow in _customerAcctRows) {
				_list.Add(getAcct(pDb, _customerAcctRow));
			}
			return _list.ToArray();
		}

		internal static CustomerAcctDto[] GetResellAccts(Rbr_Db pDb) {
			var _list = new List<CustomerAcctDto>();
			var _customerAcctRows = pDb.CustomerAcctCollection.GetReselled();
			foreach (var _customerAcctRow in _customerAcctRows) {
				_list.Add(getAcct(pDb, _customerAcctRow));
			}
			return _list.ToArray();
		}

		internal static CustomerAcctDto[] GetResellAcctsByPartnerId(Rbr_Db pDb, int pPartnerId) {
			var _list = new List<CustomerAcctDto>();
			var _customerAcctRows = pDb.CustomerAcctCollection.GetResellAcctsByPartnerId(pPartnerId);
			foreach (var _customerAcctRow in _customerAcctRows) {
				_list.Add(getAcct(pDb, _customerAcctRow));
			}
			return _list.ToArray();
		}

		internal static CustomerAcctDto[] GetResellAcctsByPersonId(Rbr_Db pDb, int pPersonId) {
			var _list = new List<CustomerAcctDto>();
			var _customerAcctRows = pDb.CustomerAcctCollection.GetReselledByPersonId(pPersonId);
			foreach (var _customerAcctRow in _customerAcctRows) {
				_list.Add(getAcct(pDb, _customerAcctRow));
			}
			return _list.ToArray();
		}

		internal static CustomerAcctDto[] GetAcctsByVendorId(Rbr_Db pDb, int pVendorId) {
			var _list = new List<CustomerAcctDto>();
			var _customerAcctRows = pDb.CustomerAcctCollection.GetByCustomerSupportVendor_Vendor_id(pVendorId);
			foreach (var _customerAcctRow in _customerAcctRows) {
				_list.Add(getAcct(pDb, _customerAcctRow));
			}
			return _list.ToArray();
		}

		internal static CustomerAcctDto[] GetUnmapped(Rbr_Db pDb, int pVendorId) {
			var _list = new List<CustomerAcctDto>();
			var _customerAcctRows = pDb.CustomerAcctCollection.GetUnmappedByVendorId(pVendorId);
			foreach (var _customerAcctRow in _customerAcctRows) {
				_list.Add(getAcct(pDb, _customerAcctRow));
			}
			return _list.ToArray();
		}

		internal static DataTable GetAllAsDataTable(Rbr_Db pDb) { return pDb.CustomerAcctCollection.GetAllAsDataTable(); }

		internal static CustomerAcctRow Get(Rbr_Db pDb, short pCustomerAcctId) { return pDb.CustomerAcctCollection.GetByPrimaryKey(pCustomerAcctId); }

		internal static CustomerAcctRow[] GetAll(Rbr_Db pDb) { return pDb.CustomerAcctCollection.GetAll(); }

		internal static CustomerAcctRow[] GetAll(Rbr_Db pDb, short pServiceId) { return pDb.CustomerAcctCollection.GetByService_id(pServiceId); }

		internal static CustomerAcctRow[] GetAll(Rbr_Db pDb, int pPartnerId) { return pDb.CustomerAcctCollection.GetByPartner_id(pPartnerId); }

		internal static CustomerAcctRow[] GetAll(Rbr_Db pDb, int pPartnerId, short pPrefixInTypeId) { return pDb.CustomerAcctCollection.GetByPartner_id_Prefix_in_type_id(pPartnerId, pPrefixInTypeId); }

		internal static CustomerAcctRow[] GetAllByPrefixType(Rbr_Db pDb, short pPrefixInTypeId) { return pDb.CustomerAcctCollection.GetByPrefix_in_type_id(pPrefixInTypeId); }

		internal static bool IsNameInUse(Rbr_Db pDb, string pName, short pCustomerAcctId) { return pDb.CustomerAcctCollection.IsNameInUseByOtherCustomerAcct(pName, pCustomerAcctId); }

		internal static bool IsPrefixInInUse(Rbr_Db pDb, string pPrefixIn, short pCustomerAcctId) { return pDb.CustomerAcctCollection.IsPrefixInInUseByOtherCustomerAcct(pPrefixIn, pCustomerAcctId); }

		internal static bool Exist(Rbr_Db pDb, int pPartnerId) {
			var _customerAcctRows = pDb.CustomerAcctCollection.GetByPartner_id(pPartnerId);
			return _customerAcctRows != null && _customerAcctRows.Length > 0;
		}

		internal static DialPeerViewRow GetDialPeer(Rbr_Db pDb, short pEndPointId, string pPrefix) { return pDb.DialPeerViewCollection.GetByEndPointIDPrefix_in(pEndPointId, pPrefix); }

		internal static DialPeerViewRow[] GetDialPeersViewByEndpointId(Rbr_Db pDb, short pEndpointId) { return pDb.DialPeerViewCollection.GetByEndPointID(pEndpointId); }

		internal static DialPeerViewRow[] GetDialPeersViewByCustomerAcctId(Rbr_Db pDb, short pCustomerAcctId) { return pDb.DialPeerViewCollection.GetByCustomerAcctId(pCustomerAcctId); }

		public static DialPeerRow GetDialPeerRow(Rbr_Db pDb, short pEndpointId, string pPrefix) { return pDb.DialPeerCollection.GetByPrimaryKey(pEndpointId, pPrefix); }

		internal static DialPeerRow[] GetDialPeersByEndpointId(Rbr_Db pDb, short pEndpointId) { return pDb.DialPeerCollection.GetByEnd_point_id(pEndpointId); }

		internal static DialPeerRow[] GetDialPeersByAcctId(Rbr_Db pDb, short pCustomerAcctId) { return pDb.DialPeerCollection.GetByCustomer_acct_id(pCustomerAcctId); }

		internal static int GetDialPeerCountByAcctId(Rbr_Db pDb, short pCustomerAcctId) { return pDb.DialPeerCollection.GetCountByCustomer_acct_id(pCustomerAcctId); }

		internal static int GetDialPeerCountByEndpointId(Rbr_Db pDb, short pEndpointId) { return pDb.DialPeerCollection.GetCountByEndPointID(pEndpointId); }

		internal static OutDialPeerViewRow[] GetDialPeerViewsByEndPointId(Rbr_Db pDb, short pEndPointId) { return pDb.OutDialPeerViewCollection.GetByEndPointID(pEndPointId); }

		internal static bool HasPayments(Rbr_Db pDb, int pPersonId) { return pDb.CustomerAcctPaymentCollection.HasPayments(pPersonId); }

		internal static CustomerAcctPaymentDto[] GetByCustomerAcctId(Rbr_Db pDb, short pCustomerAcctId) {
			var _list = new ArrayList();
			var _customerAcctPaymentRows = pDb.CustomerAcctPaymentCollection.GetByCustomer_acct_id(pCustomerAcctId);
			foreach (var _customerAcctPaymentRow in _customerAcctPaymentRows) {
				var _balanceAdjustmentReason = BalanceAdjustmentReasonManager.Get(pDb, _customerAcctPaymentRow.Balance_adjustment_reason_id);
				var _person = PersonManager.Get(pDb, _customerAcctPaymentRow.Person_id);
				var _customerAcctRow = Get(pDb, _customerAcctPaymentRow.Customer_acct_id);
				_list.Add(mapToCustomerAcctPayment(_customerAcctPaymentRow, _customerAcctRow.Name, _person, _balanceAdjustmentReason));
			}
			if (_list.Count > 1) {
				_list.Sort(new GenericComparer(CustomerAcctPaymentDto.DateTime_PropName, ListSortDirection.Descending));
			}
			return (CustomerAcctPaymentDto[]) _list.ToArray(typeof (CustomerAcctPaymentDto));
		}

		internal static CustomerAcctPaymentDto[] GetByPersonId(Rbr_Db pDb, int pPersonId) {
			var _list = new ArrayList();
			var _customerAcctPaymentRows = pDb.CustomerAcctPaymentCollection.GetByPerson_id(pPersonId);
			foreach (var _customerAcctPaymentRow in _customerAcctPaymentRows) {
				var _balanceAdjustmentReason = BalanceAdjustmentReasonManager.Get(pDb, _customerAcctPaymentRow.Balance_adjustment_reason_id);
				var _person = PersonManager.Get(pDb, _customerAcctPaymentRow.Person_id);
				var _customerAcctRow = Get(pDb, _customerAcctPaymentRow.Customer_acct_id);
				_list.Add(mapToCustomerAcctPayment(_customerAcctPaymentRow, _customerAcctRow.Name, _person, _balanceAdjustmentReason));
			}
			if (_list.Count > 1) {
				_list.Sort(new GenericComparer(CustomerAcctPaymentDto.DateTime_PropName, ListSortDirection.Descending));
			}
			return (CustomerAcctPaymentDto[]) _list.ToArray(typeof (CustomerAcctPaymentDto));
		}

		internal static PrefixInTypeDto[] GetPrefixInTypes(Rbr_Db pDb) { return mapToPrefixInTypes(pDb.PrefixInTypeCollection.GetAll()); }

		internal static PrefixInTypeDto[] GetPrefixInTypes(Rbr_Db pDb, short pExcludePrefixInTypeId) { return mapToPrefixInTypes(pDb.PrefixInTypeCollection.GetAll(pExcludePrefixInTypeId)); }

		internal static PrefixInTypeDto GetPrefixInType(Rbr_Db pDb, short pPrefixInTypeId) { return mapToPrefixInType(pDb.PrefixInTypeCollection.GetByPrimaryKey(pPrefixInTypeId)); }

		internal static string GetPrefixInTypeDescription(Rbr_Db pDb, short pPrefixInTypeId) {
			var _prefixInTypeRow = pDb.PrefixInTypeCollection.GetByPrimaryKey(pPrefixInTypeId);
			return _prefixInTypeRow != null ? _prefixInTypeRow.Description : string.Empty;
		}

		#endregion Getters

		#region Actions

		internal static void Add(Rbr_Db pDb, CustomerAcctRow pCustomerAcctRow) { pDb.CustomerAcctCollection.Insert(pCustomerAcctRow); }

		internal static void AddCustomerAcctsAndRoutes(Rbr_Db Db, CustomerAcctDto pCustomerAcct, int[] pSelectedBaseRouteIds) {
			if (pCustomerAcct.ServiceDto.IsDedicated) {
				pCustomerAcct.ServiceDto.Name = AppConstants.CustomerServiceNamePrefix + pCustomerAcct.Name;
				ServiceManager.AddService(Db, pCustomerAcct.ServiceDto, pSelectedBaseRouteIds);
			}

			var _customerAcctRow = MapToCustomerAcctRow(pCustomerAcct);
			Add(Db, _customerAcctRow);
			pCustomerAcct.CustomerAcctId = _customerAcctRow.Customer_acct_id;

			if (pCustomerAcct.ServiceDto.IsDedicated) {
				ServiceManager.SaveAccessNumbers(Db, pCustomerAcct);
			}

			if (pCustomerAcct.ResellAccount != null) {
				pCustomerAcct.ResellAccount.CustomerAcctId = pCustomerAcct.CustomerAcctId;
				AddResellAccount(Db, pCustomerAcct.ResellAccount);
			}

			#region TODO: for the next rev

			//if (CurrentNode.Instance.BelongsToStandalonePlatform) {
			//  //add LB Map for the actual acct only, not the resell one
			//  LoadBalancingMapManager.Add(_db, CurrentNode.Instance.Id, pCustomerAcct.CustomerAcctId);
			//}

			#endregion for the next rev
		}

		internal static void UpdateAcct(Rbr_Db pDb, CustomerAcctRow pCustomerAcctRow) {
			pDb.CustomerAcctCollection.Update(pCustomerAcctRow);
			//pDb.AddChangedObject(new CustomerAcctKey(TxType.Delete, pCustomerAcctRow.Customer_acct_id));
		}

		internal static void UpdateAcct(Rbr_Db pDb, CustomerAcctDto pCustomerAcct) {
			pDb.CustomerAcctCollection.Update(MapToCustomerAcctRow(pCustomerAcct));
			//pDb.AddChangedObject(new CustomerAcctKey(TxType.Delete, pCustomerAcct.CustomerAcctId));
		}

		internal static void DeleteAcct(Rbr_Db pDb, short pCustomerAcctId) {
			pDb.CustomerAcctCollection.DeleteByCustomerAcctId(pCustomerAcctId);
			//pDb.AddChangedObject(new CustomerAcctKey(TxType.Delete, pCustomerAcctId));
		}

		internal static void Credit(Rbr_Db pDb, CustomerAcctPaymentDto pCustomerAcctPayment) {
			if (!pDb.CustomerAcctCollection.Credit(pCustomerAcctPayment.CustomerAcctId, pCustomerAcctPayment.Amount)) {
				throw new ApplicationException("Failed to Credit Customer Account; customerAcctId: " + pCustomerAcctPayment.CustomerAcctId);
			}
			pDb.CustomerAcctPaymentCollection.Insert(mapToCustomerAcctPaymentRow(pCustomerAcctPayment));

			if (!pDb.CustomerAcctCollection.AdjustWarningAmount(pCustomerAcctPayment.CustomerAcctId, pCustomerAcctPayment.WarningLevel)) {
				throw new ApplicationException("Failed to Set Warning Amount for Customer Account; customerAcctId: " + pCustomerAcctPayment.CustomerAcctId);
			}

			//pDb.AddChangedObject(new CustomerAcctKey(TxType.Delete, pCustomerAcctPayment.CustomerAcctId));
		}

		#region DialPeer  ---------------------------------------

		internal static void AddDialPeer(Rbr_Db pDb, DialPeerRow pDialPeerRow, EndPointRow pEndPointRow) {
			pDb.DialPeerCollection.Insert(pDialPeerRow);
			//pDb.AddChangedObject(new EndpointKey(TxType.Add, pDialPeerRow.End_point_id));
		}

		internal static void DeleteDialPeer(Rbr_Db pDb, short pEndpointId, string pPrefix) {
			var _epRow = EndpointManager.Get(pDb, pEndpointId);
			if (_epRow != null) {
				pDb.DialPeerCollection.DeleteByPrimaryKey(pEndpointId, pPrefix);
			}
			//pDb.AddChangedObject(new CustomerDialPeerKey(TxType.Delete, pEndpointId, pPrefix));
		}

		internal static void DeleteDialPeersByCustomerAcctId(Rbr_Db pDb, short pCustomer_acct_id) {
			var _dialPeerRows = pDb.DialPeerCollection.GetByCustomer_acct_id(pCustomer_acct_id);
			if (_dialPeerRows != null && _dialPeerRows.Length > 0) {
				pDb.DialPeerCollection.DeleteByCustomer_acct_id(pCustomer_acct_id);
				foreach (var _dialPeerRow in _dialPeerRows) {
					var _epRow = EndpointManager.Get(pDb, _dialPeerRow.End_point_id);
					//pDb.AddChangedObject(new CustomerDialPeerKey(TxType.Delete, _dialPeerRow.End_point_id, string.Empty));
				}
			}
		}

		internal static void DeleteDialPeersByEndpointId(Rbr_Db pDb, short pEndpointId) {
			pDb.DialPeerCollection.DeleteByEnd_point_id(pEndpointId);
			//pDb.AddChangedObject(new CustomerDialPeerKey(TxType.Delete, pEndpointId, string.Empty));
		}

		internal static DialPeerRow[] UpdateCustomerDialPeers(Rbr_Db pDb, short pCustomerAcctId, string pNewPrefixIn, string pOriginalPrefixIn) {
			var _dialPeerRows = pDb.DialPeerCollection.GetByCustomer_acct_id(pCustomerAcctId);
			if (_dialPeerRows != null && _dialPeerRows.Length > 0) {
				foreach (var _dialPeerRow in _dialPeerRows) {
					_dialPeerRow.Prefix_in = pNewPrefixIn;
					pDb.DialPeerCollection.Update(pNewPrefixIn, pOriginalPrefixIn, _dialPeerRow);
					//pDb.AddChangedObject(new CustomerDialPeerKey(TxType.Delete, _dialPeerRow.End_point_id, pOriginalPrefixIn));
				}
			}
			return _dialPeerRows;
		}

		#endregion DialPeers

		#region Resell Accounts ------------------------------------------

		internal static void AddResellAccount(Rbr_Db pDb, ResellAccountDto pResellAccount) {
			var _resellAcctRow = mapToResellAcctRow(pResellAccount);
			pDb.ResellAcctCollection.Insert(_resellAcctRow);
		}

		internal static void UpdateResellAccount(Rbr_Db pDb, ResellAccountDto pResellAccount) {
			var _resellAcctRow = mapToResellAcctRow(pResellAccount);
			pDb.ResellAcctCollection.Update(_resellAcctRow);
		}

		internal static void DeleteResellAccount(Rbr_Db pDb, short pResellAccountId) { pDb.ResellAcctCollection.DeleteByPrimaryKey(pResellAccountId); }

		internal static void DeleteResellAccountByCustomerAcctId(Rbr_Db pDb, short pCustomerAcctId) { pDb.ResellAcctCollection.DeleteByCustomer_acct_id(pCustomerAcctId); }

		#endregion Resell Accounts

		#endregion Actions

		#region Mappings

		#region To BLL mappings

		static CustomerAcctDto mapToCustomerAcct(CustomerAcctRow pCustomerAcctRow, PartnerDto pPartner) {
			if (pCustomerAcctRow == null) {
				return null;
			}

			var _customerAcct = new CustomerAcctDto
			                    	{
			                    		CustomerAcctId = pCustomerAcctRow.Customer_acct_id, 
															ServiceId = pCustomerAcctRow.Service_id,
															RoutingPlanId = pCustomerAcctRow.Routing_plan_id,
															AllowRerouting = pCustomerAcctRow.AllowRerouting, 
															DefaultBonusMinutesType = pCustomerAcctRow.DefaultBonusMinutesType, 
															DefaultStartBonusMinutes = pCustomerAcctRow.Default_start_bonus_minutes, 
															IsPrepaid = pCustomerAcctRow.IsPrepaid, 
															ConcurrentUse = pCustomerAcctRow.ConcurrentUse, 
															CurrentAmount = pCustomerAcctRow.Current_amount, 
															LimitAmount = pCustomerAcctRow.Limit_amount, 
															Name = pCustomerAcctRow.Name, 
															PrefixIn = pCustomerAcctRow.Prefix_in, 
															PrefixInTypeId = pCustomerAcctRow.Prefix_in_type_id, 
															PrefixOut = pCustomerAcctRow.Prefix_out, 
															Status = pCustomerAcctRow.AccountStatus, 
															WarningAmount = pCustomerAcctRow.Warning_amount, 
															MaxCallLength = pCustomerAcctRow.Max_call_length, 
															Partner = pPartner, 
															ServiceDto = null, 
															RoutingPlan = null
			                    	};
			return _customerAcct;
		}

		static CustomerAcctPaymentDto mapToCustomerAcctPayment(CustomerAcctPaymentRow_Base pCustomerAcctPaymentRow, string pCustomerAcctName, PersonDto pPerson, BalanceAdjustmentReasonDto pBalanceAdjustmentReason) {
			var _customerAcctPayment = new CustomerAcctPaymentDto
			                           	{
			                           		DateTime = pCustomerAcctPaymentRow.Date_time, 
																		CustomerAcctId = pCustomerAcctPaymentRow.Customer_acct_id, 
																		CustomerAcctName = pCustomerAcctName, 
																		PreviousAmount = pCustomerAcctPaymentRow.Previous_amount, 
																		Amount = pCustomerAcctPaymentRow.Payment_amount, 
																		Comments = pCustomerAcctPaymentRow.Comments, 
																		BalanceAdjustmentReason = pBalanceAdjustmentReason, 
																		Person = pPerson
			                           	};

			return _customerAcctPayment;
		}

		static ResellAcctRow[] mapToResellAcctRows(IEnumerable<ResellAccountDto> pResellAccounts) {
			var _list = new List<ResellAcctRow>();
			if (pResellAccounts != null) {
				foreach (var _resellAccount in pResellAccounts) {
					var _resellAcctRow = mapToResellAcctRow(_resellAccount);
					_list.Add(_resellAcctRow);
				}
			}
			return _list.ToArray();
		}

		static ResellAcctRow mapToResellAcctRow(ResellAccountDto pResellAccount) {
			var _resellAcctRow = new ResellAcctRow
			                     	{
			                     		Resell_acct_id = pResellAccount.ResellAccountId, 
															Customer_acct_id = pResellAccount.CustomerAcctId, 
															Partner_id = pResellAccount.PartnerId, 
															Person_id = pResellAccount.PersonId, 
															CommisionType = pResellAccount.CommisionType, 
															Fee_per_call = pResellAccount.FeePerCall, 
															Fee_per_minute = pResellAccount.FeePerMinute, 
															Markup_dollar = pResellAccount.MarkupDollar, 
															Markup_percent = pResellAccount.MarkupPercent, 
															PerRoute = pResellAccount.PerRoute
			                     	};
			return _resellAcctRow;
		}

		#endregion To BLL mappings

		#region To DAL mappings

		internal static CustomerAcctRow MapToCustomerAcctRow(CustomerAcctDto pCustomerAcct) {
			if (pCustomerAcct == null) {
				return null;
			}

			var _customerAcctRow = new CustomerAcctRow
			                       	{
			                       		Customer_acct_id = pCustomerAcct.CustomerAcctId, 
																AllowRerouting = pCustomerAcct.AllowRerouting, 
																DefaultBonusMinutesType = pCustomerAcct.DefaultBonusMinutesType, 
																Default_start_bonus_minutes = pCustomerAcct.DefaultStartBonusMinutes, 
																IsPrepaid = pCustomerAcct.IsPrepaid, 
																ConcurrentUse = pCustomerAcct.ConcurrentUse, 
																Current_amount = pCustomerAcct.CurrentAmount, 
																Limit_amount = pCustomerAcct.LimitAmount, 
																Name = pCustomerAcct.Name, 
																Prefix_in = pCustomerAcct.PrefixIn, 
																Prefix_in_type_id = pCustomerAcct.PrefixInTypeId, 
																Prefix_out = pCustomerAcct.PrefixOut, 
																AccountStatus = pCustomerAcct.Status, 
																Warning_amount = pCustomerAcct.WarningAmount, 
																Max_call_length = pCustomerAcct.MaxCallLength, 
																Partner_id = pCustomerAcct.PartnerId, 
																Service_id = pCustomerAcct.ServiceDto.ServiceId, 
																Routing_plan_id = pCustomerAcct.RoutingPlanId
			                       	};

			//TODO: NEW DAL
			//if (pCustomerAcct.RetailCallingPlan != null) {
			//  _customerAcctRow.Retail_calling_plan_id = pCustomerAcct.RetailCallingPlanId;
			//}

			return _customerAcctRow;
		}

		static CustomerAcctPaymentRow mapToCustomerAcctPaymentRow(CustomerAcctPaymentDto pCustomerAcctPayment) {
			var _customerAcctPaymentRow = new CustomerAcctPaymentRow
			                              	{
			                              		Date_time = pCustomerAcctPayment.DateTime, 
																				Customer_acct_id = pCustomerAcctPayment.CustomerAcctId, 
																				Previous_amount = pCustomerAcctPayment.PreviousAmount, 
																				Payment_amount = pCustomerAcctPayment.Amount, 
																				Comments = pCustomerAcctPayment.Comments, 
																				Balance_adjustment_reason_id = pCustomerAcctPayment.BalanceAdjustmentReasonId, 
																				Person_id = pCustomerAcctPayment.PersonId
			                              	};

			return _customerAcctPaymentRow;
		}

		#endregion To DAL mappings

		static PrefixInTypeDto[] mapToPrefixInTypes(IEnumerable<PrefixInTypeRow> pPrefixInTypeRows) {
			var _list = new ArrayList();
			if (pPrefixInTypeRows != null) {
				foreach (var _prefixInTypeRow in pPrefixInTypeRows) {
					var _prefixInType = mapToPrefixInType(_prefixInTypeRow);
					_list.Add(_prefixInType);
				}
			}
			return (PrefixInTypeDto[]) _list.ToArray(typeof (PrefixInTypeDto));
		}

		static PrefixInTypeDto mapToPrefixInType(PrefixInTypeRow_Base pPrefixInTypeRow) {
			if (pPrefixInTypeRow == null) {
				return null;
			}
			var _prefixInType = new PrefixInTypeDto
			                    	{
			                    		PrefixInTypeId = pPrefixInTypeRow.Prefix_in_type_id, 
															Description = pPrefixInTypeRow.Description, 
															Length = pPrefixInTypeRow.Length, 
															Delimiter = pPrefixInTypeRow.Delimiter
			                    	};

			return _prefixInType;
		}

		#endregion Mappings

		#region Privates --------------------------------------------------------------

		static CustomerAcctDto getAcct(Rbr_Db pDb, CustomerAcctRow pCustomerAcctRow) {
			if (pCustomerAcctRow == null) {
				return null;
			}
			var _partner = PartnerManager.Get(pDb, pCustomerAcctRow.Partner_id);
			if (_partner == null) {
				return null;
			}

			return mapToCustomerAcct(pCustomerAcctRow, _partner);
		}

		#endregion Privates
	}
}