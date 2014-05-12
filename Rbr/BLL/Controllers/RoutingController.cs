using System;
using System.Collections;
using System.ComponentModel;
using Timok.Core;
using Timok.Logger;
using Timok.Rbr.BLL.DOM;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public interface IRoutingController {
		RoutingPlanDto[] GetRoutingPlans();
		RoutingPlanDto[] GetRoutingPlans(int pCallingPlanId);
		RoutingPlanDto GetRoutingPlan(int pRoutingPlanId);
		void AddRoutingPlan(RoutingPlanDto pRoutingPlan, int[] pSelectedBaseRouteIds, RoutingAlgorithmType pDefaultRoutingAlgorithmType);
		void CloneRoutingPlan(RoutingPlanDto pNewRoutingPlan, RoutingPlanDto pRoutingPlanToClone);
		void UpdateRoutingPlan(RoutingPlanDto pRoutingPlan);
		void DeleteRoutingPlan(RoutingPlanDto pRoutingPlan);

		RoutingTableDto GetRoutingTable(int pRoutingPlanId, int pBaseRouteId);
		RoutingTableDto AddTerminationChoice(int pRoutingPlanId, int pRouteId, int pCarrierRouteId);
		RoutingTableDto UpdateTerminationChoices(int pRoutingPlanId, int pRouteId, TerminationChoiceDto[] pTerminationChoices);
		RoutingTableDto DeleteTerminationChoice(TerminationChoiceDto pTerminationChoice);

		int GetUsageCount(int pRoutingPlanId);
		RoutingPlanDetailDto[] GetRoutingPlanDetails(int pRoutingPlanId, int pCountryId);
		RoutingPlanDetailDto GetRoutingPlanDetail(int pRoutingPlanId, int pRouteId);
		void AddRoutingPlanDetails(RoutingPlanDto pRoutingPlan, int[] pSelectedBaseRouteIds, RoutingAlgorithmType pDefaultRoutingAlgorithmType);
		void UpdateRoutingPlanDetail(RoutingPlanDetailDto pRoutingPlanDetail);
		Result DeleteRoutingPlanDetail(RoutingPlanDetailDto pRoutingPlanDetail);
	
		RatedRouteDto Get(int pRouteId, ViewContext pContext);
		RatedRouteDto GetByAccountIdBaseRouteId(short pAccountId, int pBaseRouteId, ViewContext pContext);
		RatedRouteDto[] GetByAccountIdCountryId(short pAccountId, int pCountryId, ViewContext pContext);
		RatedRouteDto[] GetByAccountId(short pAccountId, int pRoutingPlanId, ViewContext pContext);
		void AddRoutes(short pAccountId, int[] pBaseRouteIds, ViewContext pContext);
		void UpdateRoute(RatedRouteDto pRoute, ViewContext pContext);
		void DeleteRoute(RatedRouteDto pRoute, ViewContext pContext);

		BaseRouteDto[] GetRoutesByCallingPlanId(int pCallingPlanId);
		BaseRouteDto[] GetRoutesByCallingPlanIdRoutingPlanId(int pCallingPlanId, int pRoutingPlanId);
		BaseRouteDto[] GetRoutesByCallingPlanIdCountryId(int pCallingPlanId, int pCountryId);
		BaseRouteDto GetRoute(int pRouteId);
		void AddBaseRoute(BaseRouteDto pBaseRoute);
		void UpdateBaseRoute(BaseRouteDto pBaseRoute);
		void DeleteBaseRoute(BaseRouteDto pBaseRoute);

		TerminationRouteDto[] GetAvailableCarrierLCRTerminationRoutes(int pBaseRouteId, DateTime pDate, short pHour);
		TerminationRouteDto[] GetActiveCarrierLCRTerminationRoutes(int pCallingPlanId, long pDialCode, DateTime pDate, short pHour);
	}

	public static class RoutingControllerFactory {
		public static IRoutingController Create() {
			return new RoutingController();
		}
	}

	internal class RoutingController : IRoutingController {
		#region Public Getters

		public RoutingPlanDto[] GetRoutingPlans() {
			using (var _db = new Rbr_Db()) {
				return RoutingManager.GetRoutingPlans(_db);
			}
		}

		public RoutingPlanDto[] GetRoutingPlans(int pCallingPlanId) {
			using (var _db = new Rbr_Db()) {
				return RoutingManager.GetRoutingPlans(_db, pCallingPlanId);
			}
		}

		public RoutingPlanDto GetRoutingPlan(int pRoutingPlanId) {
			using (var _db = new Rbr_Db()) {
				return RoutingManager.GetRoutingPlan(_db, pRoutingPlanId);
			}
		}

		public int GetUsageCount(int pRoutingPlanId) {
			using (var _db = new Rbr_Db()) {
				var _count = ServiceManager.GetSharedServiceCount(_db, pRoutingPlanId);
				_count += _db.CustomerAcctCollection.GetCountByRoutingPlanId(pRoutingPlanId);
				return _count;
			}
		}

		public RoutingPlanDetailDto[] GetRoutingPlanDetails(int pRoutingPlanId, int pCountryId) {
			using (var _db = new Rbr_Db()) {
				return RoutingManager.GetRoutingPlanDetails(_db, pRoutingPlanId, pCountryId);
			}
		}

		public RoutingPlanDetailDto GetRoutingPlanDetail(int pRoutingPlanId, int pRouteId) {
			using (var _db = new Rbr_Db()) {
				return RoutingManager.GetRoutingPlanDetail(_db, pRoutingPlanId, pRouteId);
			}
		}

		public RatedRouteDto Get(int pRouteId, ViewContext pContext) {
			using (var _db = new Rbr_Db()) {
				if (pContext == ViewContext.Carrier) {
					return CarrierRouteManager.Get(_db, pRouteId);
				}

				if (pContext == ViewContext.Customer) {
					//NOTE: same as ServiceDialPlan
					return CustomerRouteManager.Get(_db, pRouteId);
				}

				if (pContext == ViewContext.Service) {
					//NOTE: same as CustomerDialPlan
					return CustomerRouteManager.Get(_db, pRouteId);
				}

				throw new NotImplementedException("ViewContext: " + pContext);
			}
		}

		public RatedRouteDto GetByAccountIdBaseRouteId(short pAccountId, int pBaseRouteId, ViewContext pContext) {
			using (var _db = new Rbr_Db()) {
				if (pContext == ViewContext.Carrier) {
					return CarrierRouteManager.Get(_db, pAccountId, pBaseRouteId);
				}

				if (pContext == ViewContext.Customer) {
					return CustomerRouteManager.GetRouteByServiceIdBaseRouteId(_db, pAccountId, pBaseRouteId);
				}

				if (pContext == ViewContext.Service) {
					return CustomerRouteManager.GetRouteByServiceIdBaseRouteId(_db, pAccountId, pBaseRouteId);
				}

				throw new NotImplementedException("ViewContext: " + pContext);
			}
		}

		public RatedRouteDto[] GetByAccountIdCountryId(short pAccountId, int pCountryId, ViewContext pContext) {
			using (var _db = new Rbr_Db()) {
				if (pContext == ViewContext.Carrier) {
					return CarrierRouteManager.GetByCarrierAcctIdCountryId(_db, pAccountId, pCountryId);
				}

				if (pContext == ViewContext.Customer || pContext == ViewContext.Service) {
					return CustomerRouteManager.Get(_db, pAccountId, pCountryId);
				}

				throw new NotImplementedException("ViewContext: " + pContext);
			}
		}

		public RatedRouteDto[] GetByAccountId(short pAccountId, int pRoutingPlanId, ViewContext pContext) {
			using (var _db = new Rbr_Db()) {
				if (pContext == ViewContext.Carrier) {
					return CarrierRouteManager.GetByCarrierAcctId(_db, pAccountId);
				}

				if (pContext == ViewContext.Customer) {
					//NOTE: same as ServiceDialPlan
					return CustomerRouteManager.Get(_db, pAccountId, pRoutingPlanId);
				}

				if (pContext == ViewContext.Service) {
					//NOTE: same as CustomerDialPlan
					return CustomerRouteManager.Get(_db, pAccountId, pRoutingPlanId);
				}

				throw new NotImplementedException("ViewContext: " + pContext);
			}
		}

		public BaseRouteDto[] GetRoutesByCallingPlanId(int pCallingPlanId) {
			using (var _db = new Rbr_Db()) {
				return RoutingManager.GetBaseRoutesByRoutingPlanId(_db, pCallingPlanId, 0);
			}
		}

		public BaseRouteDto[] GetRoutesByCallingPlanIdRoutingPlanId(int pCallingPlanId, int pRoutingPlanId) {
			using (var _db = new Rbr_Db()) {
				return RoutingManager.GetBaseRoutesByRoutingPlanId(_db, pCallingPlanId, pRoutingPlanId);
			}
		}

		public BaseRouteDto[] GetRoutesByCallingPlanIdCountryId(int pCallingPlanId, int pCountryId) {
			using (var _db = new Rbr_Db()) {
				return RoutingManager.GetBaseRoutesByCountryId(_db, pCallingPlanId, pCountryId);
			}
		}

		public BaseRouteDto GetRoute(int pRouteId) {
			using (var _db = new Rbr_Db()) {
				return RoutingManager.GetBaseRoute(_db, pRouteId);
			}
		}

		public TerminationRouteDto[] GetAvailableCarrierLCRTerminationRoutes(int pBaseRouteId, DateTime pDate, short pHour) {
			//Stopwatch _s = new Stopwatch();
			//_s.Start();
			var _dateHour = new DateTime(pDate.Year, pDate.Month, pDate.Day, pHour, 0, 0);
			var _termRoutelist = new ArrayList();
			using (var _db = new Rbr_Db()) {
				var _terminationRouteViewRows = _db.TerminationRouteViewCollection.GetAvailableByCustomerBaseRouteId(pBaseRouteId, true);
				foreach (var _terminationRouteViewRow in _terminationRouteViewRows) {
					if (_terminationRouteViewRow.RatingType != RatingType.Disabled) {
						//if rating enabled
						//TODO: make sure carr Route has active Term EPs
						var _activeCarrierRouteEPMapCount = _db.CarrierAcctEPMapCollection.GetActiveCountByCarrierRouteId(_terminationRouteViewRow.Carrier_route_id);

						//TODO: check for DefaultPrimaryTermEP
						if (_activeCarrierRouteEPMapCount > 0 /* || pCustomerServiceRoute.Service.DefaultPrimaryTermEP > 0*/) {
							var _lcrRank = getCarrierLCRRank(_terminationRouteViewRow.Carrier_route_id, _dateHour);
							var _terminationRoute = new TerminationRouteDto(_terminationRouteViewRow.Carrier_route_id,
								                        _terminationRouteViewRow.Route_name,
								                        _terminationRouteViewRow.Carrier_acct_id,
								                        _terminationRouteViewRow.Calling_plan_id,
								                        _terminationRouteViewRow.Carrier_acct_name,
								                        _lcrRank,
								                        _terminationRouteViewRow.PartialCoverage,
								                        _activeCarrierRouteEPMapCount > 0);

							if (!_termRoutelist.Contains(_terminationRoute)) {
								_termRoutelist.Add(_terminationRoute);
							}
						}
					}
				}
			}
			//need to sort here by CarrierName  and RouteName 
			//var _sortInfo = new SortInfo[3];
			//_sortInfo[1] = new SortInfo(TerminationRouteDto.CarrierName_PropName, ListSortDirection.Ascending);
			//_sortInfo[2] = new SortInfo(TerminationRouteDto.CarrierRouteName_PropName, ListSortDirection.Ascending);
			var _sortInfo = new SortInfo[2];
			_sortInfo[0] = new SortInfo(TerminationRouteDto.CarrierName_PropName, ListSortDirection.Ascending);
			_sortInfo[1] = new SortInfo(TerminationRouteDto.CarrierRouteName_PropName, ListSortDirection.Ascending);
			_termRoutelist.Sort(new GenericComparer(_sortInfo));

			//Debug.WriteLine("TermChoiceController.GetRoutingTable [" + _s.Elapsed.TotalSeconds.ToString("0.0000000") + " sec]");
			return (TerminationRouteDto[]) _termRoutelist.ToArray(typeof (TerminationRouteDto));
		}

		public TerminationRouteDto[] GetActiveCarrierLCRTerminationRoutes(int pCallingPlanId, long pDialCode, DateTime pDate, short pHour) {
			var _dateHour = new DateTime(pDate.Year, pDate.Month, pDate.Day, pHour, 0, 0);
			var _termRoutelist = new ArrayList();
			using (var _db = new Rbr_Db()) {
				var _terminationRouteViewRows = _db.TerminationRouteViewCollection.GetActiveByCustomerDialCode(pDialCode, pCallingPlanId);
				foreach (var _terminationRouteViewRow in _terminationRouteViewRows) {
					if (_terminationRouteViewRow.RatingType != RatingType.Disabled) {
						//if rating enabled
						//TODO: make sure carr Route has active Term EPs
						var _activeCarrierRouteEPMapCount = _db.CarrierAcctEPMapCollection.GetActiveCountByCarrierRouteId(_terminationRouteViewRow.Carrier_route_id);

						//TODO: check for DefaultPrimaryTermEP
						if (_activeCarrierRouteEPMapCount == 0 /* && pCustomerServiceRoute.Service.DefaultPrimaryTermEP == 0*/) {
							continue;
						}

						var _lcrRank = getCarrierLCRRank(_terminationRouteViewRow.Carrier_route_id, _dateHour);

						var _terminationRoute = new TerminationRouteDto(_terminationRouteViewRow.Carrier_route_id,
							                        _terminationRouteViewRow.Route_name,
							                        _terminationRouteViewRow.Carrier_acct_id,
							                        _terminationRouteViewRow.Calling_plan_id,
							                        _terminationRouteViewRow.Carrier_acct_name,
							                        _lcrRank,
							                        _terminationRouteViewRow.PartialCoverage,
							                        _activeCarrierRouteEPMapCount > 0);

						if (!_termRoutelist.Contains(_terminationRoute)) {
							_termRoutelist.Add(_terminationRoute);
						}
					}
				}
			}
			//NOTE: need to sort here by Rank, RouteName and CarrierName 
			//(by CarrierName is in cases where Route+Rank are the same for different Carriers)
			var _sortInfo = new SortInfo[3];
			_sortInfo[0] = new SortInfo(TerminationRouteDto.LCRRank_PropName, ListSortDirection.Ascending);
			_sortInfo[1] = new SortInfo(TerminationRouteDto.CarrierRouteName_PropName, ListSortDirection.Ascending);
			_sortInfo[2] = new SortInfo(TerminationRouteDto.CarrierName_PropName, ListSortDirection.Ascending);
			_termRoutelist.Sort(new GenericComparer(_sortInfo));

			return (TerminationRouteDto[]) _termRoutelist.ToArray(typeof (TerminationRouteDto));
		}

		public RoutingTableDto GetRoutingTable(int pRoutingPlanId, int pBaseRouteId) {
			if (pRoutingPlanId > 0 && pBaseRouteId > 0) {
				using (var _db = new Rbr_Db()) {
					var _routingPlanDetailRow = _db.RoutingPlanDetailCollection.GetByPrimaryKey(pRoutingPlanId, pBaseRouteId);
					if (_routingPlanDetailRow == null) {
						return null;
					}

					var _routingTable = new RoutingTableDto
					                    	{
					                    		RoutingPlanId = _routingPlanDetailRow.Routing_plan_id, 
																	BaseRouteId = _routingPlanDetailRow.Route_id, 
																	Algorithm = _routingPlanDetailRow.Algorithm, 
																	AvailableTerminations = RoutingManager.GetAvailableTerminations(_db, pBaseRouteId), 
																	Terminations = RoutingManager.GetAll(_db, pRoutingPlanId, pBaseRouteId)
					                    	};
					return _routingTable;
				}
			}
			return null;
		}

		#endregion Public Getters

		#region Public Actions

		public void AddRoutingPlanDetails(RoutingPlanDto pRoutingPlan, int[] pSelectedBaseRouteIds, RoutingAlgorithmType pDefaultRoutingAlgorithmType) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pRoutingPlan, pSelectedBaseRouteIds, pDefaultRoutingAlgorithmType)) {
					RoutingManager.AddRoutingPlanDetails(_db, pRoutingPlan, pSelectedBaseRouteIds, pDefaultRoutingAlgorithmType);
					_tx.Commit();
				}
			}
		}

		public void UpdateRoutingPlanDetail(RoutingPlanDetailDto pRoutingPlanDetail) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pRoutingPlanDetail)) {
					RoutingManager.UpdateRoutingPlanDetail(_db, pRoutingPlanDetail);
					_tx.Commit();
				}
			}
		}

		public Result DeleteRoutingPlanDetail(RoutingPlanDetailDto pRoutingPlanDetail) {
			var _result = new Result(true, string.Empty);
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pRoutingPlanDetail)) {
					try {
						var _wholesaleRouteCount = CustomerRouteManager.GetCount(_db, pRoutingPlanDetail.BaseRouteId);
						if (_wholesaleRouteCount > 0) {
							throw new Exception("Cannot delete Routing Plan Route, it's used in one or more Wholesale Dial Plans");
						}
						var _retailRouteCount = 0; //TODO: X_Controller.GetRetailRouteCount(_db, pRoutingPlanDetail.BaseRouteId);
						if (_retailRouteCount > 0) {
							throw new Exception("Cannot delete Routing Plan Route, it's used in one or more Retail Dial Plans");
						}

						RoutingManager.DeleteRoutingPlanDetail(_db, pRoutingPlanDetail);
						_tx.Commit();
					}
					catch (Exception _ex) {
						_result.Success = false;
						_result.ErrorMessage = _ex.Message;
						TimokLogger.Instance.LogRbr(LogSeverity.Critical, "RoutingController.DeleteRoutingPlanDetail", string.Format("Exception:\r\n{0}", _ex));
					}
				}
			}
			return _result;
		}

		public void AddRoutingPlan(RoutingPlanDto pRoutingPlan, int[] pSelectedBaseRouteIds, RoutingAlgorithmType pDefaultRoutingAlgorithmType) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pRoutingPlan, pSelectedBaseRouteIds, pDefaultRoutingAlgorithmType)) {
					//TODO: NEW DAL - VirtualSwitch
					pRoutingPlan.VirtualSwitchId = AppConstants.DefaultVirtualSwitchId;
					pRoutingPlan.RoutingPlanId = RoutingManager.AddRoutingPlan(_db, pRoutingPlan);
					RoutingManager.AddRoutingPlanDetails(_db, pRoutingPlan, pSelectedBaseRouteIds, pDefaultRoutingAlgorithmType);
					_tx.Commit();
				}
			}
		}

		public void CloneRoutingPlan(RoutingPlanDto pNewRoutingPlan, RoutingPlanDto pRoutingPlanToClone) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pNewRoutingPlan, pRoutingPlanToClone)) {
					//TODO: NEW DAL - VirtualSwitch
					pNewRoutingPlan.VirtualSwitchId = AppConstants.DefaultVirtualSwitchId;
					pNewRoutingPlan.RoutingPlanId = RoutingManager.AddRoutingPlan(_db, pNewRoutingPlan);
					RoutingManager.CloneRoutingPlanDetails(_db, pNewRoutingPlan, pRoutingPlanToClone);
					_tx.Commit();
				}
			}
		}

		public void UpdateRoutingPlan(RoutingPlanDto pRoutingPlan) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pRoutingPlan)) {
					RoutingManager.UpdateRoutingPlan(_db, pRoutingPlan);
					_tx.Commit();
				}
			}
		}

		public void DeleteRoutingPlan(RoutingPlanDto pRoutingPlan) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pRoutingPlan)) {
					RoutingManager.DeleteRoutingPlan(_db, pRoutingPlan);
					_tx.Commit();
				}
			}
		}

		public void AddRoutes(short pAccountId, int[] pBaseRouteIds, ViewContext pContext) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pAccountId, pBaseRouteIds, pContext)) {
					if (pContext == ViewContext.Carrier) {
						var _carrierAcct = CarrierAcctManager.GetAcct(_db, pAccountId);
						CarrierRouteManager.Add(_db, _carrierAcct, pBaseRouteIds);
					}
					else if (pContext == ViewContext.Customer) {
						//NOTE: same as ServiceDialPlan
						CustomerRouteManager.Add(_db, pAccountId, pBaseRouteIds);
					}
					else if (pContext == ViewContext.Service) {
						//NOTE: same as CustomerDialPlan
						CustomerRouteManager.Add(_db, pAccountId, pBaseRouteIds);
					}
					else {
						throw new NotImplementedException("ViewContext: " + pContext);
					}
					_tx.Commit();
				}
			}
		}

		public void UpdateRoute(RatedRouteDto pRoute, ViewContext pContext) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pRoute, pContext)) {
					if (pContext == ViewContext.Carrier) {
						CarrierRouteManager.Update(_db, pRoute);
					}
					else if (pContext == ViewContext.Customer) {
						CustomerRouteManager.Update(_db, pRoute);
					}
					else if (pContext == ViewContext.Service) {
						CustomerRouteManager.Update(_db, pRoute);
					}
					else {
						throw new NotImplementedException("ViewContext: " + pContext);
					}
					_tx.Commit();
				}
			}
		}

		public void DeleteRoute(RatedRouteDto pRoute, ViewContext pContext) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pRoute, pContext)) {
					if (pContext == ViewContext.Carrier) {
						CarrierRouteManager.Delete(_db, pRoute);
					}
					else if (pContext == ViewContext.Customer) {
						CustomerRouteManager.Delete(_db, pRoute);
					}
					else if (pContext == ViewContext.Service) {
						CustomerRouteManager.Delete(_db, pRoute);
					}
					else {
						throw new NotImplementedException("ViewContext: " + pContext);
					}
					_tx.Commit();
				}
			}
		}

		//---------------------- TerminationChoice
		public RoutingTableDto AddTerminationChoice(int pRoutingPlanId, int pRouteId, int pCarrierRouteId) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pRoutingPlanId, pRouteId, pCarrierRouteId)) {
					RoutingManager.AddTerminationChoice(_db, pRoutingPlanId, pRouteId, pCarrierRouteId);
					_tx.Commit();
				}
			}
			return GetRoutingTable(pRoutingPlanId, pRouteId);
		}

		public RoutingTableDto UpdateTerminationChoices(int pRoutingPlanId, int pRouteId, TerminationChoiceDto[] pTerminationChoices) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pRoutingPlanId, pRouteId, pTerminationChoices)) {
					RoutingManager.UpdateTerminationChoices(_db, pTerminationChoices);
					_tx.Commit();
				}
			}
			return GetRoutingTable(pRoutingPlanId, pRouteId);
		}

		public RoutingTableDto DeleteTerminationChoice(TerminationChoiceDto pTerminationChoice) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pTerminationChoice)) {
					RoutingManager.DeleteTerminationChoice(_db, pTerminationChoice);
					_tx.Commit();
				}
			}
			return GetRoutingTable(pTerminationChoice.RoutingPlanId, pTerminationChoice.RouteId);
		}

		//---------------------- BaseRoute
		public void AddBaseRoute(BaseRouteDto pBaseRoute) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pBaseRoute)) {
					RoutingManager.AddBaseRoute(_db, pBaseRoute);
					_tx.Commit();
				}
			}
		}

		public void UpdateBaseRoute(BaseRouteDto pBaseRoute) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pBaseRoute)) {
					RoutingManager.UpdateBaseRoute(_db, pBaseRoute);
					_tx.Commit();
				}
			}
		}

		public void DeleteBaseRoute(BaseRouteDto pBaseRoute) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pBaseRoute)) {
					RoutingManager.DeleteBaseRoute(_db, pBaseRoute);
					_tx.Commit();
				}
			}
		}

		#endregion Public Actions

		//----------------------------- private ---------------------------------------------
		static int getCarrierLCRRank(int pCarrierRouteId, DateTime pDateHour) {
			var _LCRRank = int.MaxValue;
			var _rateInfo = RateInfo.GetCarrierRateInfo(pCarrierRouteId, pDateHour);
			if (_rateInfo != null) {
				//TODO: combine other CarrierRoute's variables to get the LCRRank 
				_LCRRank = _rateInfo.GetNormalizedCost();
			}
			return _LCRRank;
		}
	}
}