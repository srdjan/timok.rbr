using System;
using System.Collections.Generic;
using System.IO;
using Timok.Core;
using Timok.Logger;
using Timok.Rbr.BLL.Entities;
using Timok.Rbr.BLL.Managers;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.DAL.RbrDatabase;
using Timok.Rbr.DAL.RbrDatabase.Base;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.Controllers {
	public class CustomerAcctController {
		CustomerAcctController() { }

		#region Public Getters

		public static int GetDialPeerCount(short pCustomerAcctId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetDialPeerCountByAcctId(_db, pCustomerAcctId);
			}
		}

		public static int GetCabinasCount(short pCustomerAcctId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetDialPeerCountByAcctId(_db, pCustomerAcctId) - 1;
			}
		}

		public static CustomerAcctDto GetAcct(short pCustomerAcctId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetAcct(_db, pCustomerAcctId);
			}
		}

		public static CustomerAcctDto[] GetAccts() {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetAccts(_db);
			}
		}

		public static CustomerAcctDto[] GetAcctsByPartnerId(int pPartnerId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetAcctsByPartnerId(_db, pPartnerId);
			}
		}

		public static CustomerAcctDto[] GetActivePrepaidCustomerAcctsByPartnerId(int pPartnerId) {
			CustomerAcctDto[] _customerAccts;
			using (var _db = new Rbr_Db()) {
				_customerAccts = CustomerAcctManager.GetAcctsByPartnerId(_db, pPartnerId);
			}
			if (_customerAccts != null && _customerAccts.Length > 0) {
				var _customerAcctList = new List<CustomerAcctDto>();
				foreach (CustomerAcctDto _customerAcctDto in _customerAccts) {
					if (_customerAcctDto.IsPrepaid && _customerAcctDto.Status == Status.Active) {
						_customerAcctList.Add(_customerAcctDto);
					}
				}
				return _customerAcctList.ToArray();
			}
			return null;
		}

		public static CustomerAcctDto[] GetActivePrepaidResellCustomerAccts(int pPartnerId) {
			CustomerAcctDto[] _customerAccts;
			using (var _db = new Rbr_Db()) {
				_customerAccts = CustomerAcctManager.GetAllResellAcctsByPartnerId(_db, pPartnerId);
			}
			if (_customerAccts != null && _customerAccts.Length > 0) {
				var _customerAcctList = new List<CustomerAcctDto>();
				foreach (CustomerAcctDto _customerAcctDto in _customerAccts) {
					if (_customerAcctDto.IsPrepaid && _customerAcctDto.Status == Status.Active) {
						_customerAcctList.Add(_customerAcctDto);
					}
				}
				return _customerAcctList.ToArray();
			}
			return null;
		}

		public static CustomerAcctDto[] GetAcctsByRoutingPlanId(int pRoutingPlanId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetAcctsByRoutingPlanId(_db, pRoutingPlanId);
			}
		}

		public static CustomerAcctDto[] GetResellAccts() {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetResellAccts(_db);
			}
		}

		public static CustomerAcctDto[] GetResellAcctsByPartnerId(int pPartnerId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetResellAcctsByPartnerId(_db, pPartnerId);
			}
		}

		public static CustomerAcctDto[] GetResellAcctsByPersonId(int pPersonId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetResellAcctsByPersonId(_db, pPersonId);
			}
		}

		public static CustomerAcctDto[] GetAcctsByServiceId(short pServiceId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetAcctsByServiceId(_db, pServiceId);
			}
		}

		public static CustomerAcctDto[] GetAcctsByVendorId(int pVendorId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetAcctsByVendorId(_db, pVendorId);
			}
		}

		public static bool IsNameInUse(string pName, short pCustomerAcctId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.IsNameInUse(_db, pName, pCustomerAcctId);
			}
		}

		public static bool IsPrefixInInUse(string pPrefixIn, short pCustomerAcctId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.IsPrefixInInUse(_db, pPrefixIn, pCustomerAcctId);
			}
		}

		public static CustomerAcctPaymentDto[] GetPaymentsByCustomerAcctId(short pCustomerAcctId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetByCustomerAcctId(_db, pCustomerAcctId);
			}
		}

		public static CustomerAcctPaymentDto[] GetPaymentsByPersonId(int pPersonId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetByPersonId(_db, pPersonId);
			}
		}

		public static BalanceAdjustmentReasonDto[] GetBalanceAdjustmentReasons() {
			using (var _db = new Rbr_Db()) {
				return BalanceAdjustmentReasonManager.GetByType(_db, BalanceAdjustmentReasonType.Wholesale);
			}
		}

		public static DialPeerViewRow GetDialPeer(short pEndpointId, string pPrefix) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetDialPeer(_db, pEndpointId, pPrefix);
			}
		}

		public static DialPeerViewRow[] GetDialPeers(short pEndpointId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetDialPeersViewByEndpointId(_db, pEndpointId);
			}
		}

		public static OutDialPeerViewRow[] GetAllOutDialPeers(short pEndpointId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetDialPeerViewsByEndPointId(_db, pEndpointId);
			}
		}

		public static DialPeerRow[] GetDialPeersByAcctId(short pCustomerAcctId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetDialPeersByAcctId(_db, pCustomerAcctId);
			}
		}

		public static int GetDialPeerCountByAcctId(short pCustomerAcctId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetDialPeerCountByAcctId(_db, pCustomerAcctId);
			}
		}

		public static int GetDialPeerCountByEndpointId(short pEndpointId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetDialPeerCountByEndpointId(_db, pEndpointId);
			}
		}

		public static List<CabinaDto> GetCabinas(CustomerAcctDto pCustomerAcct) {
			var _cabinas = new List<CabinaDto>();

			using (var _db = new Rbr_Db()) {
				DialPeerRow[] _dialPeerRows = CustomerAcctManager.GetDialPeersByAcctId(_db, pCustomerAcct.CustomerAcctId);
				if (_dialPeerRows != null && _dialPeerRows.Length > 0) {
					EndPointRow[] _endPointRows = EndpointManager.GetAllByCustomerAcct(_db, pCustomerAcct.CustomerAcctId);
					if (_endPointRows != null && _endPointRows.Length > 0) {
						foreach (DialPeerRow _dialpeerRow in _dialPeerRows) {
							if (_dialpeerRow.Prefix_in != string.Empty && _dialpeerRow.Prefix_in != "#") {
								var _cabina = new CabinaDto(_dialpeerRow.End_point_id, getIP(_endPointRows, _dialpeerRow.End_point_id), _dialpeerRow.Prefix_in, _dialpeerRow.Customer_acct_id);
								_cabinas.Add(_cabina);
							}
						}
					}
				}
			}
			return _cabinas;
		}

		public static PrefixInTypeDto[] GetPrefixInTypes() {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetPrefixInTypes(_db);
			}
		}

		public static PrefixInTypeDto[] GetPrefixInTypes(short pExcludePrefixInTypeId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetPrefixInTypes(_db, pExcludePrefixInTypeId);
			}
		}

		public static PrefixInTypeDto GetPrefixType(short pPrefixInTypeId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetPrefixInType(_db, pPrefixInTypeId);
			}
		}

		public static string GetPrefixTypeDescription(short pPrefixInTypeId) {
			using (var _db = new Rbr_Db()) {
				return CustomerAcctManager.GetPrefixInTypeDescription(_db, pPrefixInTypeId);
			}
		}

		#endregion Public Getters

		#region Public Actions

		public static void AddAcctAndRoutes(CustomerAcctDto pCustomerAcct, int[] pSelectedBaseRouteIds) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCustomerAcct, pSelectedBaseRouteIds)) {
					try {
						CustomerAcctManager.AddCustomerAcctsAndRoutes(_db, pCustomerAcct, pSelectedBaseRouteIds);
						_tx.Commit();
					}
					catch {
						pCustomerAcct.CustomerAcctId = 0;
						if (pCustomerAcct.ServiceDto != null && pCustomerAcct.ServiceDto.IsDedicated) {
							pCustomerAcct.ServiceDto.ServiceId = 0;
						}

						if (pCustomerAcct.ResellAccount != null) {
							pCustomerAcct.ResellAccount.ResellAccountId = 0;
						}
						throw;
					}
				}
			}
		}

		public static void UpdateAcct(CustomerAcctDto pCustomerAcct) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCustomerAcct)) {
					CustomerAcctRow _original = CustomerAcctManager.Get(_db, pCustomerAcct.CustomerAcctId);

					if (pCustomerAcct.ServiceDto.IsDedicated) {
						//-- Dedicated, make sure dp's name is created by cust's name
						pCustomerAcct.ServiceDto.Name = AppConstants.CustomerServiceNamePrefix + pCustomerAcct.Name;

						//-- Make sure RoutingPlan is the same for Dedicated Service
						pCustomerAcct.ServiceDto.DefaultRoutingPlan = pCustomerAcct.RoutingPlan;
						ServiceManager.UpdateService(_db, pCustomerAcct.ServiceDto);
					}

					ServiceManager.SaveAccessNumbers(_db, pCustomerAcct);
					CustomerAcctManager.UpdateAcct(_db, pCustomerAcct);

					if (pCustomerAcct.PrefixIn != _original.Prefix_in) {
						CustomerAcctManager.UpdateCustomerDialPeers(_db, pCustomerAcct.CustomerAcctId, pCustomerAcct.PrefixIn, _original.Prefix_in);
					}

					if (pCustomerAcct.ResellAccount != null) {
						CustomerAcctManager.UpdateResellAccount(_db, pCustomerAcct.ResellAccount);
					}

					#region TODO: for the next rev

					//if (CurrentNode.Instance.BelongsToStandalonePlatform) {
					//  //add LB Map for the actual acct only, not the resell one
					//  LoadBalancingMapManager.Add(_db, CurrentNode.Instance.Id, pCustomerAcct.CustomerAcctId);
					//}
					//else {
					//  //TODO: !!!!! review it
					//  //							if ( ! _original.IsPrepaid && pCustomerAcct.CustomerAcctRow.IsPrepaid) {
					//  //								//TODO: check the logic
					//  //								//if changed to Prepaid - delete all LB Maps
					//  //								//user will need to pick Node manually
					//  //								_db.LoadBalancingMapCollection.DeleteByCustomer_acct_id(pCustomerAcct.CustomerAcctRow.Customer_acct_id);
					//  //							}
					//} 

					#endregion for the next rev

					_tx.Commit();
				}
			}
		}

		public static void DeleteAcct(CustomerAcctDto pCustomerAcct) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pCustomerAcct)) {
					//TODO: HasCDRs should become CustomerAcct property !!!
					if (CDRController.HasCDRsByCustomerAcctId(pCustomerAcct.CustomerAcctId)) {
						pCustomerAcct.Status = Status.Archived;
						CustomerAcctManager.UpdateAcct(_db, pCustomerAcct);
						return;
					}

					LoadBalancingMapManager.Delete(_db, pCustomerAcct);

					if (pCustomerAcct.ServiceType == ServiceType.Retail) {
						int _retailAcctsCount = RetailAccountManager.Instance.GetCount(_db, pCustomerAcct.CustomerAcctId);
						if (_retailAcctsCount > 0) {
							throw new Exception("Cannot delete Customer Account, it has Retail Accounts.");
						}
						ServiceManager.SaveAccessNumbers(_db, pCustomerAcct); //delete/unassign CustomerAcct's AccessNumbers...
					}

					CustomerAcctManager.DeleteResellAccountByCustomerAcctId(_db, pCustomerAcct.CustomerAcctId);
					CustomerAcctManager.DeleteDialPeersByCustomerAcctId(_db, pCustomerAcct.CustomerAcctId);
					CustomerAcctManager.DeleteAcct(_db, pCustomerAcct.CustomerAcctId);

					//if (pCustomerAcct.ServiceDto.IsDedicated) {
					//	ServiceManager.DeleteService(_db, pCustomerAcct.ServiceDto);
					//}
					_tx.Commit();
				}
			}
		}

		public static void Credit(PersonDto pPerson, CustomerAcctPaymentDto pCustomerAcctPayment) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pPerson, pCustomerAcctPayment)) {
					pCustomerAcctPayment.DateTime = DateTime.Now;
					//NOTE: make sure we got prev amnt 
					CustomerAcctRow _customerAcctRow = CustomerAcctManager.Get(_db, pCustomerAcctPayment.CustomerAcctId);
					pCustomerAcctPayment.PreviousAmount = _customerAcctRow.Current_amount;
					pCustomerAcctPayment.Person = pPerson;
					CustomerAcctManager.Credit(_db, pCustomerAcctPayment);
					_tx.Commit();

					PartnerDto _partner = PartnerManager.Get(_db, _customerAcctRow.Partner_id);
					ContactInfoDto _contactInfo = ContactInfoManager.Get(_db, _partner.ContactInfo.ContactInfoId);
					sendNotification(pPerson, pCustomerAcctPayment, _customerAcctRow, _partner, _contactInfo);
				}
			}
		}

		#region DialPeers ---------------------------------------

		public static void AddCallCenterCabina(short pEndpointId, string pPrefix, short pCustomerAcctId) { AddDialPeersForEndpoint(pEndpointId, pPrefix, pCustomerAcctId, RetailType.None); }

		public static void AddDialPeersForEndpoint(short pEndpointId, string pPrefix, short pCustomerAcctId, RetailType pRetailType) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pEndpointId, pPrefix, pCustomerAcctId, pRetailType)) {
					var _endpointRow = EndpointManager.Get(_db, pEndpointId);
					if (_endpointRow == null) {
						throw new Exception(string.Format("Endpoint NOT FOUND, EndpointId={0}", pEndpointId));
					}

					var _dialPeerRow = CustomerAcctManager.GetDialPeerRow(_db, pEndpointId, pPrefix);
					if (_dialPeerRow == null) {
						_dialPeerRow = new DialPeerRow
						               	{
						               		End_point_id = pEndpointId, 
															Prefix_in = pPrefix, 
															Customer_acct_id = pCustomerAcctId
						               	};
						CustomerAcctManager.AddDialPeer(_db, _dialPeerRow, _endpointRow);
					}

					//-- If Retail, add accessNumber DialPeers
					if (pRetailType == RetailType.PhoneCard || pRetailType == RetailType.Residential) {
						var _accessNumberRows = ServiceManager.GetAccessNumbers(_db, pCustomerAcctId);
						foreach (var _accessNumberRow in _accessNumberRows) {
							_dialPeerRow = new DialPeerRow
							               	{
							               		End_point_id = pEndpointId, 
																Prefix_in = _accessNumberRow.Access_number.ToString(), 
																Customer_acct_id = pCustomerAcctId
							               	};
							CustomerAcctManager.AddDialPeer(_db, _dialPeerRow, _endpointRow);
						}
					}
					_tx.Commit();
				}
			}
		}

		public static void AddDialPeers(EndPointRow[] pEndpointRows, CustomerAcctDto pCustomerAcct) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pEndpointRows, pCustomerAcct)) {
					foreach (var _endPointRow in pEndpointRows) {
						if (pCustomerAcct != null) {
							var _newDialPeer = new DialPeerRow
							                   	{
							                   		End_point_id = _endPointRow.End_point_id, 
																		Prefix_in = pCustomerAcct.PrefixIn, 
																		Customer_acct_id = pCustomerAcct.CustomerAcctId
							                   	};
							CustomerAcctManager.AddDialPeer(_db, _newDialPeer, _endPointRow);
						}
					}
					_tx.Commit();
				}
			}
		}

		public static void AddChangingEndpointPrefixType(EndPointRow pEndpointRow, CustomerAcctDto pCustomerAcct) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pEndpointRow, pCustomerAcct)) {
					EndpointManager.UpdatePrefixType(_db, pEndpointRow, pCustomerAcct.PrefixInTypeId);

					var _endpointContext = new EndpointContext {CustomerAcct = pCustomerAcct};
					if (_endpointContext.CustomerAcct != null) {
						var _newDialPeer = new DialPeerRow
						                   	{
						                   		End_point_id = pEndpointRow.End_point_id, 
																	Prefix_in = _endpointContext.CustomerAcct.PrefixIn, 
																	Customer_acct_id = _endpointContext.CustomerAcct.CustomerAcctId
						                   	};
						CustomerAcctManager.AddDialPeer(_db, _newDialPeer, pEndpointRow);
					}
					_tx.Commit();
				}
			}
		}

		public static void ReassignDialPeer(EndPointRow pEndpointRow, CustomerAcctDto pFromCustomerAcct, CustomerAcctDto pToCustomerAcct) {
			if (pEndpointRow.WithInPrefixes || pFromCustomerAcct.WithPrefixes || pToCustomerAcct.WithPrefixes) {
				throw new Exception("Invalid operation: expecting Endpoint and Customer without Prefixes ONLY.");
			}

			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pEndpointRow, pFromCustomerAcct, pToCustomerAcct)) {
					CustomerAcctManager.DeleteDialPeer(_db, pEndpointRow.End_point_id, pFromCustomerAcct.PrefixIn);

					var _newDialPeerRow = new DialPeerRow
					                      	{
					                      		End_point_id = pEndpointRow.End_point_id, 
																		Prefix_in = pToCustomerAcct.PrefixIn, 
																		Customer_acct_id = pToCustomerAcct.CustomerAcctId
					                      	};
					CustomerAcctManager.AddDialPeer(_db, _newDialPeerRow, pEndpointRow);
					_tx.Commit();
				}
			}
		}

		public static void DeleteCabina(short pEndPointId, short pCustomerAcctId, string pPrefix) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pEndPointId, pCustomerAcctId, pPrefix)) {
					CustomerAcctManager.DeleteDialPeer(_db, pEndPointId, pPrefix);
					_tx.Commit();
				}
			}
		}

		public static void DeleteDialPeer(EndPointRow pEndpointRow, CustomerAcctDto pCustomerAcct) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pEndpointRow, pCustomerAcct)) {
					CustomerAcctManager.DeleteDialPeer(_db, pEndpointRow.End_point_id, pCustomerAcct.PrefixIn);
					if (pCustomerAcct.ServiceDto.ServiceType == ServiceType.Retail) {
						foreach (var _accessNumberDto in pCustomerAcct.ServiceDto.AccessNumbers) {
							CustomerAcctManager.DeleteDialPeer(_db, pEndpointRow.End_point_id, _accessNumberDto.Number.ToString());
						}
					}
					_tx.Commit();
				}
			}
		}

		public static void DeleteSelectedDialPeersForCustomer(EndPointRow[] pEndpointRows, CustomerAcctDto pCustomerAcct) {
			using (var _db = new Rbr_Db()) {
				using (var _tx = new Transaction(_db, pEndpointRows, pCustomerAcct)) {
					foreach (var _endPointRow in pEndpointRows) {
						CustomerAcctManager.DeleteDialPeer(_db, _endPointRow.End_point_id, pCustomerAcct.PrefixIn);
					}
					_tx.Commit();
				}
			}
		}

		#endregion DialPeer

		#endregion Public Actions

		//------------------------ private ---------------------------------------------------
		static string getIP(IEnumerable<EndPointRow> pEndpointRows, short pEndpointId) {
			foreach (var _endPointRow in pEndpointRows) {
				if (_endPointRow.End_point_id == pEndpointId) {
					return _endPointRow.DottedIPAddressRange;
				}
			}
			throw new Exception("Invalid state, Endpoint has to exist!");
		}

		static void sendNotification(PersonDto pPerson,
		                             CustomerAcctPaymentDto pCustomerAcctPayment,
		                             CustomerAcctRow_Base pCustomerAcctRow,
		                             PartnerDto pPartner,
		                             ContactInfoDto pContactInfo) {
			try {
				var _subject = "Balance Adjustment, [Partner]> " + pPartner.Name + " [Acct]> " + pCustomerAcctRow.Name;
				var _body = "Server Time>      " + pCustomerAcctPayment.DateTime.ToString("yyyy-MM-dd HH:mm:ss");
				_body += Environment.NewLine;
				_body += "Adjust Amount>    " + pCustomerAcctPayment.Amount.ToString("c");
				_body += Environment.NewLine;
				_body += "Previous Balance> " + pCustomerAcctPayment.PreviousAmount.ToString("c");
				_body += Environment.NewLine;
				_body += "Current Balance>  " + decimal.Add(pCustomerAcctPayment.PreviousAmount, pCustomerAcctPayment.Amount).ToString("c");
				_body += Environment.NewLine;
				_body += "Comments>  " + pCustomerAcctPayment.Comments;

				Email.SetForSending(Path.Combine(Configuration.Instance.Folders.EmailFolder, Guid.NewGuid().ToString("N")),
				           Configuration.Instance.Email.ClientFrom,
				           Configuration.Instance.Email.ClientTo,
				           string.Empty,
				           pContactInfo.Email,
				           Configuration.Instance.Email.ClientEmailServer,
				           Configuration.Instance.Email.ClientEmailPassword,
				           _subject,
				           _body);
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "CustomerAcctController.sendNotification", string.Format("Adjusted by:      {0}\r\n{1} {2}", pPerson.Name, _subject, _body));
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Critical, "CustomerAcctController.sendNotification", string.Format("Exception sending notification\r\n{0}", _ex));
			}
		}
	}
}