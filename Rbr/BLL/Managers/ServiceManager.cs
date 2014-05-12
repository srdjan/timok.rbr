using System;
using System.Collections.Generic;
using Timok.Rbr.BLL.DTO;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Managers {
	public class ServiceManager {
		ServiceManager() { }

		#region Getters

		internal static ServiceDto[] GetServices(Rbr_Db pDb) {
			var _list = new List<ServiceDto>();
			ServiceRow[] _serviceRows = pDb.ServiceCollection.GetAll();

			if (_serviceRows != null) {
				foreach (ServiceRow _serviceRow in _serviceRows) {
					_list.Add(getService(pDb, _serviceRow, 0));
				}
			}
			return _list.ToArray();
		}

		internal static ServiceDto[] GetServices(Rbr_Db pDb, ServiceType pServiceType) {
			var _list = new List<ServiceDto>();
			ServiceRow[] _serviceRows = pDb.ServiceCollection.GetByType(pServiceType);

			if (_serviceRows != null) {
				foreach (ServiceRow _serviceRow in _serviceRows) {
					_list.Add(getService(pDb, _serviceRow, 0));
				}
			}
			return _list.ToArray();
		}

		internal static ServiceDto[] GetSharedServices(Rbr_Db pDb) {
			var _list = new List<ServiceDto>();
			ServiceRow[] _serviceRows = pDb.ServiceCollection.GetAllShared();

			if (_serviceRows != null) {
				foreach (ServiceRow _serviceRow in _serviceRows) {
					_list.Add(getService(pDb, _serviceRow, 0));
				}
			}
			return _list.ToArray();
		}

		internal static ServiceDto[] GetSharedServices(Rbr_Db pDb, int pRoutingPlanId) {
			var _list = new List<ServiceDto>();
			ServiceRow[] _serviceRows = pDb.ServiceCollection.GetSharedByRoutingPlanId(pRoutingPlanId);

			if (_serviceRows != null) {
				foreach (ServiceRow _serviceRow in _serviceRows) {
					_list.Add(getService(pDb, _serviceRow, 0));
				}
			}
			return _list.ToArray();
		}

		internal static ServiceDto[] GetSharedServices(Rbr_Db pDb, RetailType pRetailType) {
			var _list = new List<ServiceDto>();
			ServiceRow[] _serviceRows = pDb.ServiceCollection.GetSharedByRetailType(pRetailType);

			if (_serviceRows != null) {
				foreach (ServiceRow _serviceRow in _serviceRows) {
					_list.Add(getService(pDb, _serviceRow, 0));
				}
			}
			return _list.ToArray();
		}

		internal static ServiceDto GetService(Rbr_Db pDb, short pServiceId) {
			ServiceRow _serviceRow = pDb.ServiceCollection.GetByPrimaryKey(pServiceId);
			if (_serviceRow == null) {
				return null;
			}
			return getService(pDb, _serviceRow, 0);
		}

		internal static ServiceDto GetService(Rbr_Db pDb, short pServiceId, short pCustomerAcctId) {
			ServiceRow _serviceRow = pDb.ServiceCollection.GetByPrimaryKey(pServiceId);
			if (_serviceRow == null) {
				return null;
			}
			return getService(pDb, _serviceRow, pCustomerAcctId);
		}

		internal static ServiceRow[] GetAll(Rbr_Db pDb) { return pDb.ServiceCollection.GetAll(); }

		internal static ServiceRow[] GetAll(Rbr_Db pDb, ServiceType pServiceType) { return pDb.ServiceCollection.GetByType(pServiceType); }

		internal static ServiceRow[] GetAllShared(Rbr_Db pDb) { return pDb.ServiceCollection.GetAllShared(); }

		internal static ServiceRow[] GetAllShared(Rbr_Db pDb, RetailType pRetailType) { return pDb.ServiceCollection.GetSharedByRetailType(pRetailType); }

		internal static ServiceRow Get(Rbr_Db pDb, short pServiceId) { return pDb.ServiceCollection.GetByPrimaryKey(pServiceId); }

		public static int GetSharedServiceCount(Rbr_Db pDb, int pRoutingPlanId) { return pDb.ServiceCollection.GetCountByDefaultRoutingPlanId(pRoutingPlanId); }

		#endregion Getters

		#region Actions

		internal static void Add(Rbr_Db pDb, ServiceRow pServiceRow /*, AccessNumberListRow[] pAccessNumberRows*/) {
			try {
				pDb.ServiceCollection.Insert(pServiceRow);
			}
			catch {
				if (pServiceRow != null) {
					pServiceRow.Service_id = 0;
				}
				throw;
			}
		}

		internal static void Update(Rbr_Db pDb, ServiceRow pServiceRow) { pDb.ServiceCollection.Update(pServiceRow); }

		internal static void Delete(Rbr_Db pDb, short pServiceId) { pDb.ServiceCollection.DeleteByPrimaryKey(pServiceId); }

		internal static void AddService(Rbr_Db pDb, ServiceDto pService, int[] pSelectedBaseRouteIds) {
			//TODO: NEW DAL - VirtualSwitch
			pService.VirtualSwitchId = AppConstants.DefaultVirtualSwitchId;

			if (pService.RetailType == RetailType.PhoneCard) {
				if (pService.PayphoneSurcharge != null) {
					pService.PayphoneSurcharge.PayphoneSurchargeId = RetailAccountManager.AddPayphoneSurcharge(pDb, pService.PayphoneSurcharge);
				}
			}
			else {
				pService.PayphoneSurcharge = null;
			}

			ServiceRow _serviceRow = mapToServiceRow(pService);
			Add(pDb, _serviceRow);
			pService.ServiceId = _serviceRow.Service_id;

			//Create Default WholesaleRoute
			int _defaultWholesaleRouteId;
			CustomerRouteManager.AddDefault(pDb, pService, out _defaultWholesaleRouteId);
			pService.DefaultRoute.RatedRouteId = _defaultWholesaleRouteId;

			CustomerRouteManager.Add(pDb, pService.ServiceId, pSelectedBaseRouteIds);
		}

		internal static void UpdateService(Rbr_Db pDb, ServiceDto pService) {
			if (pService.IsRatingEnabled) {
				//Update Service's Default RatingInfo
				//NOTE: DefaultRatingInfo is always created on Add, no metter is RatingEnabled or not
				RatingManager.UpdateRatingInfo(pDb, pService.DefaultRatingInfo);

				//check RatingInfo for all others WholesaleRoutes, if they have no RatingInfo - create it
				//if WholesaleRoute already have Rates, just leave it as is
				WholesaleRouteRow[] _wholesaleRouteRows = pDb.WholesaleRouteCollection.GetByService_id(pService.ServiceId);
				foreach (WholesaleRouteRow _wholesaleRouteRow in _wholesaleRouteRows) {
					WholesaleRateHistoryRow[] _wholesaleRateHistoryRows = pDb.WholesaleRateHistoryCollection.GetByWholesale_route_id(_wholesaleRouteRow.Wholesale_route_id);
					if (_wholesaleRateHistoryRows.Length == 0) {
						//route has no rates, create them using Default
						RatingManager.AddDefaultRatingInfo(pDb, _wholesaleRouteRow.Wholesale_route_id, pService.DefaultRatingInfo, RouteType.Wholesale);
					}
				}

				if (pService.PayphoneSurcharge != null) {
					if (pService.PayphoneSurcharge.PayphoneSurchargeId == 0) {
						pService.PayphoneSurcharge.PayphoneSurchargeId = RetailAccountManager.AddPayphoneSurcharge(pDb, pService.PayphoneSurcharge);
					}
					else {
						RetailAccountManager.UpdatePayphoneSurcharge(pDb, pService);
					}
				}
			}
			CustomerRouteManager.Update(pDb, pService.DefaultRoute);

			ServiceRow _serviceRow = mapToServiceRow(pService);
			Update(pDb, _serviceRow);
			//pDb.AddChangedObject(new ServiceKey(TxType.Delete, pService.ServiceId));

			ServiceRow _originalServiceRow = Get(pDb, pService.ServiceId);
			if (pService.PayphoneSurcharge == null && !_originalServiceRow.IsPayphone_surcharge_idNull) {
				pDb.PayphoneSurchargeCollection.DeleteByPrimaryKey(_originalServiceRow.Payphone_surcharge_id);
			}
		}

		internal static void DeleteService(Rbr_Db pDb, ServiceDto pService) {
			if (pService.ServiceType == ServiceType.Retail) {
				InventoryLotRow[] _inventoryLotRows = pDb.InventoryLotCollection.GetByService_id(pService.ServiceId);
				if (_inventoryLotRows.Length > 0) {
					throw new Exception("Inventory exists for Service [" + pService.DisplayName + "];  Cannot delete.");
				}
			}

			CustomerRouteManager.Delete(pDb, pService.DefaultRoute);
			Delete(pDb, pService.ServiceId);
			//pDb.AddChangedObject(new ServiceKey(TxType.Delete, pService.ServiceId));

			if (pService.PayphoneSurcharge != null) {
				pDb.PayphoneSurchargeCollection.DeleteByPrimaryKey(pService.PayphoneSurchargeId);
			}
			pDb.ScheduleCollection.DeleteByPrimaryKey(pService.SweepScheduleId);
		}

		public static AccessNumberListRow[] GetAccessNumbers(Rbr_Db pDb, short pCustomerAcctId) { return pDb.AccessNumberListCollection.GetByCustomer_acct_id(pCustomerAcctId); }

		public static void SaveAccessNumbers(Rbr_Db pDb, CustomerAcctDto pCustomerAcct) {
			if (pCustomerAcct.ServiceDto.RetailType == RetailType.PhoneCard || pCustomerAcct.ServiceDto.RetailType == RetailType.Residential) {
				if (pCustomerAcct.ServiceDto.AccessNumbers != null) {
					var _accessNumberListRowsFromView = mapToAccessNumberRows(pCustomerAcct.ServiceDto.AccessNumbers);
					var _accessNumberRowsFromDb = pDb.AccessNumberListCollection.GetByCustomer_acct_id(pCustomerAcct.CustomerAcctId);

					detachAccessNumbers(pDb, pCustomerAcct, _accessNumberListRowsFromView, _accessNumberRowsFromDb);
					attachAccessNumbers(pDb, pCustomerAcct, _accessNumberListRowsFromView);

					//if (pCustomerAcct.ServiceDto.ServiceId != 0) {
						//var _serviceRow = mapToServiceRow(pCustomerAcct.ServiceDto);
						//pDb.AddChangedObject(new ServiceKey(TxType.Delete, _serviceRow.Service_id));
					//}
				}
			}
		}

		#endregion Actions

		#region privates

		static ServiceDto getService(Rbr_Db pDb, ServiceRow pServiceRow, short pCustomerAcctId) {
			if (pServiceRow == null) {
				return null;
			}
			AccessNumberListRow[] _accessNumberRows;
			if (pCustomerAcctId > 0) {
				_accessNumberRows = pDb.AccessNumberListCollection.GetByCustomer_acct_id(pCustomerAcctId);
			}
			else {
				_accessNumberRows = pDb.AccessNumberListCollection.GetByService_id(pServiceRow.Service_id);
			}

			var _defaultRoutingPlan = RoutingManager.GetRoutingPlan(pDb, pServiceRow.Default_routing_plan_id);

			PayphoneSurchargeRow _payphoneSurchargeRow = null;
			if (!pServiceRow.IsPayphone_surcharge_idNull) {
				_payphoneSurchargeRow = pDb.PayphoneSurchargeCollection.GetByPrimaryKey(pServiceRow.Payphone_surcharge_id);
			}

			//if (pServiceRow.IsRatingEnabled) {
			//NOTE: DefaultRatingInfo is always created no metter what
			//and it should be loaded as well no metter what
			var _defaultRatingInfo = getDefaultServiceRatingInfo(pDb, pServiceRow.Service_id);
			//}

			var _service = mapToService(pServiceRow, _defaultRoutingPlan, _payphoneSurchargeRow, _accessNumberRows, _defaultRatingInfo);

			//NOTE: DefaultServiceRoute's ID = [negative] -ServiceId
			var _defaultWholesaleRouteRow = pDb.WholesaleRouteCollection.GetByPrimaryKey(-pServiceRow.Service_id);
			_service.DefaultRoute = CustomerRouteManager.Get(pDb, _service, _service.DefaultRoutingPlanId, _defaultWholesaleRouteRow);
			return _service;
		}

		static RatingInfoDto getDefaultServiceRatingInfo(Rbr_Db pDb, short pServiceId) {
			var _defaultWholesaleRouteRow = pDb.WholesaleRouteCollection.GetByPrimaryKey(-pServiceId);
			var _wholesaleRateHistoryRows = pDb.WholesaleRateHistoryCollection.GetByWholesale_route_id(_defaultWholesaleRouteRow.Wholesale_route_id);
			if (_wholesaleRateHistoryRows.Length != 1) {
				throw new Exception("Unexpected: _wholesaleRateHistoryRows.Length = " + _wholesaleRateHistoryRows.Length);
			}
			return RatingManager.GetRatingInfo(pDb, _wholesaleRateHistoryRows[0].Rate_info_id, false);
		}

		static ServiceDto mapToService(ServiceRow pServiceRow, RoutingPlanDto pDefaultRoutingPlan, PayphoneSurchargeRow pPayphoneSurchargeRow, ICollection<AccessNumberListRow> pAccessNumberListRows, RatingInfoDto pDefaultRatingInfo) {
			if (pServiceRow == null) {
				return null;
			}

			var _service = new ServiceDto
			               	{
			               		ServiceId = pServiceRow.Service_id, 
												Name = pServiceRow.Name, 
												Status = ((Status) pServiceRow.Status), 
												ServiceType = pServiceRow.ServiceType, 
												RetailType = pServiceRow.RetailType, 
												IsShared = pServiceRow.IsShared, 
												RatingType = pServiceRow.RatingType, 
												PinLength = pServiceRow.Pin_length, 
												DefaultRatingInfo = pDefaultRatingInfo, 
												DefaultRoutingPlan = pDefaultRoutingPlan, 
												AccessNumbers = mapToAccessNumbers(pAccessNumberListRows), 
												PayphoneSurcharge = RetailAccountManager.MapToPayphoneSurcharge(pPayphoneSurchargeRow), 
												SweepScheduleId = pServiceRow.Sweep_schedule_id, 
												SweepFee = pServiceRow.Sweep_fee, 
												SweepRule = pServiceRow.Sweep_rule, 
												BalancePromptType = pServiceRow.BalancePromptType, 
												BalancePromptPerUnit = pServiceRow.Balance_prompt_per_unit, 
												VirtualSwitchId = pServiceRow.Virtual_switch_id
			               	};
			return _service;
		}

		static ServiceRow mapToServiceRow(ServiceDto pService) {
			if (pService == null) {
				return null;
			}

			var _serviceRow = new ServiceRow
			                  	{
			                  		Service_id = pService.ServiceId, 
														Name = pService.Name, 
														Status = ((byte) pService.Status), 
														ServiceType = pService.ServiceType, 
														RetailType = pService.RetailType, 
														Calling_plan_id = pService.CallingPlanId, 
														Default_routing_plan_id = pService.DefaultRoutingPlanId, 
														IsShared = pService.IsShared, 
														RatingType = pService.RatingType, 
														Pin_length = pService.PinLength, 
														Sweep_fee = pService.SweepFee, 
														Sweep_rule = pService.SweepRule, 
														BalancePromptType = pService.BalancePromptType, 
														Balance_prompt_per_unit = pService.BalancePromptPerUnit, 
														Virtual_switch_id = pService.VirtualSwitchId
			                  	};

			if (pService.PayphoneSurcharge != null) {
				_serviceRow.Payphone_surcharge_id = pService.PayphoneSurchargeId;
			}
			if (pService.SweepScheduleId > 0) {
				_serviceRow.Sweep_schedule_id = pService.SweepScheduleId;
			}
			return _serviceRow;
		}

		static void detachAccessNumbers(Rbr_Db pDb, CustomerAcctDto pCustomerAcct, ICollection<AccessNumberListRow> pAccessNumberRowsFromView, IEnumerable<AccessNumberListRow> pAccessNumberRowsFromDb) {
			foreach (var _accessNumberRowFromDb in pAccessNumberRowsFromDb) {
				if (deleted(_accessNumberRowFromDb, pAccessNumberRowsFromView)) {
					_accessNumberRowFromDb.IsCustomer_acct_idNull = true;
					_accessNumberRowFromDb.IsService_idNull = true;
					pDb.AccessNumberListCollection.DeleteByPrimaryKey(_accessNumberRowFromDb.Access_number);
					//pDb.AddChangedObject(new AccessNumberKey(TxType.Delete, _accessNumberRowFromDb.Access_number));

					//-- Remove retail_dial_peers
					var _dialPeerRows = CustomerAcctManager.GetDialPeersByAcctId(pDb, pCustomerAcct.CustomerAcctId);
					foreach (var _dialPeerRow in _dialPeerRows) {
						if (_dialPeerRow.Prefix_in == _accessNumberRowFromDb.Access_number.ToString()) {
							CustomerAcctManager.DeleteDialPeer(pDb, _dialPeerRow.End_point_id, _dialPeerRow.Prefix_in);
						}
					}
					//pDb.AddChangedObject(new CustomerAcctKey(TxType.Delete, pCustomerAcct.CustomerAcctId));
				}
			}
		}

		static bool deleted(AccessNumberListRow_Base pAccessNumberRowFromDb, ICollection<AccessNumberListRow> pAccessNumberRowsFromView) {
			if (pAccessNumberRowsFromView == null || pAccessNumberRowsFromView.Count == 0) {
				return true;
			}
			foreach (var _accessNumberRowFromView in pAccessNumberRowsFromView) {
				if (pAccessNumberRowFromDb.Access_number == _accessNumberRowFromView.Access_number) {
					return false;
				}
			}
			return true;
		}

		static void attachAccessNumbers(Rbr_Db pDb, CustomerAcctDto pCustomerAcct, IEnumerable<AccessNumberListRow> pAccessNumberRowsFromView) {
			foreach (var _accessNumberRowFromView in pAccessNumberRowsFromView) {
				_accessNumberRowFromView.Customer_acct_id = pCustomerAcct.CustomerAcctId;
				_accessNumberRowFromView.Service_id = pCustomerAcct.ServiceDto.ServiceId;
				var _accessNumberRowFromDb = pDb.AccessNumberListCollection.GetByPrimaryKey(_accessNumberRowFromView.Access_number);
				if (_accessNumberRowFromDb == null || _accessNumberRowFromDb.IsCustomer_acct_idNull) {
					pDb.AccessNumberListCollection.Insert(_accessNumberRowFromView);

					//-- Add retail_dial_peers
					var _endpointRows = pDb.EndPointCollection.GetByCustomerAcctId(pCustomerAcct.CustomerAcctId, new[] {Status.Pending, Status.Active, Status.Blocked, Status.Archived});
					foreach (var _endpointRow in _endpointRows) {
						var _dialPeerRow = new DialPeerRow
						                   	{
						                   		End_point_id = _endpointRow.End_point_id, 
																	Prefix_in = _accessNumberRowFromView.Access_number.ToString(), 
																	Customer_acct_id = pCustomerAcct.CustomerAcctId
						                   	};
						CustomerAcctManager.AddDialPeer(pDb, _dialPeerRow, _endpointRow);
					}
					continue;
				}

				if (_accessNumberRowFromDb.Customer_acct_id != pCustomerAcct.CustomerAcctId) {
					var _otherCustomerAcctRow = pDb.CustomerAcctCollection.GetByPrimaryKey(_accessNumberRowFromDb.Customer_acct_id);
					throw new Exception(string.Format("Access Number={0} already in use by other Customer Account={1}", _accessNumberRowFromView.Access_number, _otherCustomerAcctRow.Name));
				}

				_accessNumberRowFromDb.Customer_acct_id = pCustomerAcct.CustomerAcctId;
				_accessNumberRowFromDb.Service_id = pCustomerAcct.ServiceDto.ServiceId;
				_accessNumberRowFromDb.ScriptType = _accessNumberRowFromView.ScriptType;
				_accessNumberRowFromDb.ScriptLanguage = _accessNumberRowFromView.ScriptLanguage;
				_accessNumberRowFromDb.Surcharge = _accessNumberRowFromView.Surcharge;
				_accessNumberRowFromDb.SurchargeType = _accessNumberRowFromView.SurchargeType;
				pDb.AccessNumberListCollection.Update(_accessNumberRowFromDb);
				//pDb.AddChangedObject(new AccessNumberKey(TxType.Delete, _accessNumberRowFromDb.Access_number));
			}
		}

		static AccessNumberListRow[] mapToAccessNumberRows(IEnumerable<AccessNumberDto> pAccessNumbers) {
			var _list = new List<AccessNumberListRow>();
			if (pAccessNumbers != null) {
				foreach (var _accessNumber in pAccessNumbers) {
					_list.Add(mapToAccessNumberRow(_accessNumber));
				}
			}
			return _list.ToArray();
		}

		static AccessNumberListRow mapToAccessNumberRow(AccessNumberDto pAccessNumber) {
			if (pAccessNumber == null) {
				return null;
			}
			var _accessNumberListRow = new AccessNumberListRow
			                           	{
			                           		Access_number = pAccessNumber.Number, 
																		ScriptType = pAccessNumber.ScriptType, 
																		ScriptLanguage = pAccessNumber.Language, 
																		Surcharge = pAccessNumber.Surcharge, 
																		SurchargeType = pAccessNumber.SurchargeType
			                           	};

			if (pAccessNumber.CustomerAcctId > 0) {
				_accessNumberListRow.Customer_acct_id = pAccessNumber.CustomerAcctId;
			}
			if (pAccessNumber.ServiceId > 0) {
				_accessNumberListRow.Service_id = pAccessNumber.ServiceId;
			}

			return _accessNumberListRow;
		}

		static AccessNumberDto[] mapToAccessNumbers(ICollection<AccessNumberListRow> pAccessNumberRows) {
			var _list = new List<AccessNumberDto>();
			if (pAccessNumberRows != null && pAccessNumberRows.Count > 0) {
				foreach (var _accessNumberRow in pAccessNumberRows) {
					_list.Add(mapToAccessNumber(_accessNumberRow));
				}
			}
			return _list.ToArray();
		}

		static AccessNumberDto mapToAccessNumber(AccessNumberListRow pAccessNumberRow) {
			if (pAccessNumberRow == null) {
				return null;
			}
			var _accessNumber = new AccessNumberDto
			                    	{
			                    		Number = pAccessNumberRow.Access_number, 
															ScriptType = pAccessNumberRow.ScriptType, 
															Language = pAccessNumberRow.ScriptLanguage, 
															Surcharge = pAccessNumberRow.Surcharge, 
															SurchargeType = pAccessNumberRow.SurchargeType
			                    	};

			if (!pAccessNumberRow.IsCustomer_acct_idNull) {
				_accessNumber.CustomerAcctId = pAccessNumberRow.Customer_acct_id;
			}
			if (!pAccessNumberRow.IsService_idNull) {
				_accessNumber.ServiceId = pAccessNumberRow.Service_id;
			}
			return _accessNumber;
		}

		#endregion privates
	}
}