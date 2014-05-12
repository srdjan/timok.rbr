using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Timok.Logger;
using Timok.NetworkLib;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;
using Timok.Rbr.Service;

namespace Timok.IVR.Scripting {
	internal abstract class ScriptBase : IScript {
		readonly CallStatistics callStatistics;
		readonly ISession session;
		ISessionHandler sessionHandler;

		public ScriptInfo Script { get; private set; }

		const int TEST_CARD_LENGTH = 6;
		const int DEST_NUMBER_MIN_LENGTH = 8;
		const int DEST_NUMBER_MAX_LENGTH = 18;
		const int NUMBER_OF_RETRIES = 3;
		const string ONE = "1";
		const string ZERO_ONE_ONE = "011";

		protected LegIn LegIn;
		List<LegOut> legOutOptions;
		protected Account account;
		protected readonly PromptManager promptManager;
		readonly CallRecord cdr;
		
		CallState callState;
		int outboundAttempt;

		protected ScriptBase(ScriptInfo pScript, ISession pSession, CallStatistics pCallStatistics) {
			Script = pScript;
			session = pSession;
			callStatistics = pCallStatistics;

			account = new Account();
			promptManager = new PromptManager(Script);
			callState = CallState.Initializing;
			cdr = new CallRecord(session);
			outboundAttempt = 1;
		}

		//NOTE: each individual Script has to implement this method:
		abstract protected void ExecuteScript();

		//----------------------------------------- Public API  ----------------------------------------------------
		public bool Run() {
      if (Script.ScriptType == ScriptType.Wholesale) {
				sessionHandler = new SessionHandlerWholesale(callStatistics);
			}
			else {
				sessionHandler = new SessionHandlerRetail(callStatistics);
			}

			var _rbrResult = RbrResult.Success;
			callState = sessionHandler.OnStarted();

			try {
				initialize();
				for (var _i = 1; _i <= Script.NumberOfSubSessions; _i++) {
					authenticate(_i);
					ExecuteScript();
				}
			}
			catch (RbrException _ex) {
				_rbrResult = handleRbrException(_ex);
			}
			catch (Exception _ex) {
				_rbrResult = handleException(_ex);
			}
			finally {
				setCallEnd(_rbrResult);
			}
			return _rbrResult == RbrResult.Success;
		}

		//----------------------------------------- for use in inheritors ----------------------------------------------------
		void initialize() {
			if (Script.ScriptType == ScriptType.Wholesale) {
				session.Channel.SendRinging();
			}
			else {
				session.Channel.SendRinging();
				session.Channel.CreateResources();
				session.Channel.SendAcceptCall(Script.ScriptType.ToString());
				session.Channel.ConfigResources();
				session.Channel.WaitForEventIn();
				Play(promptManager.WelcomePrompt, false, false, true);
			}
		}

		void authenticate(int pCount) {
			if (Script.AuthenticationType == ScriptAuthenticationType.None) {
				return;
			}

			if (Script.AuthenticationType == ScriptAuthenticationType.IP) {
				//TODO:
				return;
			}

			if (Script.AuthenticationType == ScriptAuthenticationType.ANI) {
				authenticate(true);
				return;
			}

			if (Script.AuthenticationType == ScriptAuthenticationType.Card) {
				if (pCount == 1) {
					getCardNumberAndAuthenticate();
				}
				else {
					cdr.Set(session.AccessNumber, session.InfoDigits, session.ANI, session.OrigIPAddress);
					authenticate(true);
				}
				return;
			}

			throw new RbrException(RbrResult.Retail_AuthenticationTypeUnknown, string.Format("{0}", Script.AuthenticationType));
		}

		protected int GetUserChoice(StringCollection pChoicesPrompt, int[] pChoices, int pNumberOfRetries) {
			for (var _i = 0; _i < pNumberOfRetries; _i++) {
				Play(pChoicesPrompt, true, true, true);
				string _dtmfBuffer;
				if (session.Channel.CollectDTMF(1, out _dtmfBuffer)) {
					foreach (var _choice in pChoices) {
						var _entered = int.Parse(_dtmfBuffer);
						if (_choice == _entered) {
							return _entered;
						}
					}
				}
				continue;
			}
			throw new RbrException(RbrResult.IVR_GetChoice_Error, "SessionScriptBase.GetUserChoice", string.Format("Error: Max Number of Retries={0} reached", pNumberOfRetries));
		}

		protected void GetDestNumber() {
			for (var _i = 1; _i <= NUMBER_OF_RETRIES; _i++) {
				if (_i == 1) {
					Play(promptManager.EnterPhoneNumber(), true, true, false);
				}
				else {
					Play(promptManager.InvalidPhoneNumberTryAgain, true, true, true);
				}

				string _destNumber;
				if (session.Channel.CollectDTMF(DEST_NUMBER_MIN_LENGTH, DEST_NUMBER_MAX_LENGTH, out _destNumber)) {
					if (validateDestNumber(_destNumber)) {
						cdr.DestNumber = _destNumber;
						if (!cdr.DestNumber.StartsWith(ZERO_ONE_ONE) && !cdr.DestNumber.StartsWith(ONE)) {
							cdr.DestNumber = ONE + cdr.DestNumber;
						}
						session.DestNumber = cdr.DestNumber;
						return;
					}
				}
			}
			Play(promptManager.CallCustomerService, false, true, true);
			throw new RbrException(RbrResult.GetDestNumberDTMF_error, "SessionScriptBase.GetDestNumber", "GetDestNumber");
		}

		protected void Authorize(ServiceType pServiceType) {
			cdr.DestNumber = session.DestNumber;

			RbrResult _rc;
			if (pServiceType == ServiceType.Wholesale) {
				_rc = sessionHandler.AuthorizeWholesale(session.Id, session.OrigIPAddress, string.Empty, session.ANI, session.DestNumber, out LegIn, out legOutOptions);
			}
			else if (pServiceType == ServiceType.Retail) {
				_rc = sessionHandler.AuthorizeRetail(session, out LegIn, out legOutOptions);
			}
			else {
				throw new Exception(string.Format("SessionScriptBase.Authorize: Unknown ServiceType: {0}", pServiceType));
			}

			if (LegIn != null) {
				session.CustomerAcctId = LegIn.CustomerAcctId;	//TODO: remove CustomerID from ISession !
				cdr.Set(LegIn);
			}

			if (_rc != RbrResult.Success) {
				throw new RbrException(_rc, string.Format("SessionScriptBase.Authorize{0}", pServiceType), string.Format("RbrResult={0}", _rc));
			}

			setLegOutOptions();
		}

		protected void MakeCall() {
			try {
				makeCall();
			}
			catch (RbrException _ex) {
				if (legOutOptions.Count <= 1 && Configuration.Instance.Main.WithRerouting) {
					TimokLogger.Instance.LogRbr(LogSeverity.Info, "SessionScriptBase.MakeCall", string.Format("First and only choice, not retrying, RbrException:\r\n{0}", _ex));
					throw;
				}
				if (! Configuration.Instance.Main.WithRerouting) {
					TimokLogger.Instance.LogRbr(LogSeverity.Info, "SessionScriptBase.MakeCall", string.Format("No Rerouting, NOT retrying, RbrException:\r\n{0}", _ex));
					throw;
				}
				if (_ex.RbrResult == RbrResult.IVR_OriginationDisconnect || _ex.RbrResult == RbrResult.IVR_OriginationIdle) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "SessionScriptBase.MakeCall", string.Format("First choice origination disconnect, RbrException:\r\n{0}", _ex));
					throw;
				}
				retry();
			}
			catch (Exception _ex) {
			  TimokLogger.Instance.LogRbr(LogSeverity.Error, "SessionScriptBase.MakeCall", string.Format("First choice failed, exception:\r\n{0}", _ex));
				//if (legOutOptions.Count > 1 && Configuration.Main.WithRerouting) {
				//  retry();
				//}
				//else {
				//  throw;
				//}
				throw;
			}
		}

		protected void Play(StringCollection pFiles, bool pStopOnDTMF, bool pMandatory, bool pShouldResetDTMFBuffer) {
			bool _filesValidated;

			if (pMandatory) {
				validateMandatoryPromptFiles(pFiles);
				_filesValidated = true;
			}
			else {
				_filesValidated = validateOptionalPromptFiles(pFiles);
			}

			if (_filesValidated) {
				session.Channel.Play(pFiles, pStopOnDTMF, pShouldResetDTMFBuffer);
			}
		}

		protected void PlayTone() {
			session.Channel.PlayTone();
		}

		protected void Record(String pFilePath, bool pStopOnDTMFTerminator) {
			session.Channel.Record(pFilePath, 60000, pStopOnDTMFTerminator);
		}

		RbrResult handleRbrException(RbrException pRbrException) {
			TimokLogger.Instance.LogRbr(LogSeverity.Status, "SessionScriptBase.handleRbrException", string.Format("RbrException={0}, Message={1}", pRbrException.RbrResult, pRbrException.Message));
			try {
				switch (pRbrException.RbrResult) {
					case RbrResult.Retail_AcctNotFound:
					case RbrResult.Retail_AcctNotActive:
					case RbrResult.Retail_CardNumberInvalid:
						Play(promptManager.InvalidCardNumber, false, true, true);
						break;
					case RbrResult.Retail_NoBalance:
						Play(promptManager.NoBalance, false, true, true);
						break;
					case RbrResult.Retail_NoBalanceForRoute:
						Play(promptManager.AuthorizationFailed, false, true, true);
						break;
				}
			}
			catch (Exception _ex) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "SessionScriptBase.handleRbrException", string.Format("Exception:\r\n{0}", _ex));
			}
			return pRbrException.RbrResult;
		}

		RbrResult handleException(Exception pEx) {
			TimokLogger.Instance.LogRbr(LogSeverity.Critical, "SessionScriptBase.HadleException", string.Format("Exception:\r\n{0}", pEx));
			return RbrResult.ExceptionThrown;
		}

		//------------------------------------------------ Private -------------------------------------------------
		bool validateDestNumber(string pDestNumber) {
			if (pDestNumber.Length < 5) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "SessionScriptBase.validateDestNumber", string.Format("DestNumber length < 5, [destNumber: {0}]", pDestNumber));
				return false;
			}
			//NOTE: not checking last digit, we should let the call go trough in those cases
			if (pDestNumber.IndexOfAny(new[] { '*', '#' }, 0, pDestNumber.Length - 1) >= 0) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "SessionScriptBase.validateDestNumber", string.Format("INVALID dialed number: {0}", pDestNumber));
				return false;
			}

			//-- strip-off intl-dial-code
			if (pDestNumber.StartsWith(AppConstants.One) && pDestNumber.Length != 11) {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "SessionScriptBase.validateDestNumber", string.Format("INVALID 1+ dialed number length: {0}", pDestNumber));
				return false;
			}
			return true;
		}

		void getCardNumberAndAuthenticate() {
			for (var _i = 0; _i < Configuration.Instance.Main.NumberOfAuthenticationRetries; _i++) {
				if (getCardNumber(Script.PinLength, _i == 0, _i == Configuration.Instance.Main.NumberOfAuthenticationRetries - 1)) {
					try {
						authenticate(_i == Configuration.Instance.Main.NumberOfAuthenticationRetries - 1);
					}
					catch {
						continue;
					}
					return;
				}
			}
			throw new RbrException(RbrResult.Retail_AuthenticationFailed, "SessionScriptBase.getCardNumberAndAuthenticate", "AuthenticateWithRetries");
		}

		bool getCardNumber(int pCardLength, bool pFirstAttempt, bool pLastAttempt) {
			string _dtmfBuffer;

			if (pFirstAttempt) {
				Play(promptManager.EnterCardNumber, true, true, true);
			}

			if (session.Channel.CollectDTMF(TEST_CARD_LENGTH, pCardLength, out _dtmfBuffer)) {
				session.CardNumber = _dtmfBuffer;
				return true;
			}

			if (pLastAttempt) {
				Play(promptManager.InvalidCardNumber, true, true, true);
			}
			else {
				Play(promptManager.InvalidCardNumberTryAgain, true, true, true);
			}
			return false;
		}

		void authenticate(bool pLastAttempt) {
			var _rc = sessionHandler.AuthenticateRetail(session, out account);

			cdr.RetailAcctId = account.RetialAcctId;
			cdr.SerialNumber = account.SerialNumber;
			cdr.CardNumber = session.CardNumber;

			if (_rc == RbrResult.Success) {
				return;
			}

			if (_rc == RbrResult.Retail_NoBalance) {
				Play(promptManager.NoBalance, false, true, true);
				throw new RbrException(RbrResult.Retail_AuthenticationFailed, "SessionScriptBase.authenticate", string.Format("No Balance, Serial#={0}", account.SerialNumber));
			}

			if (pLastAttempt) {
				Play(promptManager.InvalidCardNumber, true, true, true);
			}
			else {
				Play(promptManager.InvalidCardNumberTryAgain, true, true, true);
			}

			throw new RbrException(RbrResult.Retail_AuthenticationFailed, "SessionScriptBase.authenticate", string.Format("Max attempts, Serial#={0}", account.SerialNumber));
		}

		void setConnected(string pDestIPAddress) {
			cdr.Leg2Start();
			var _result = sessionHandler.CallConnect(session.Id,
																				 session.AccessNumber,
																				 session.OrigIPAddress,
																				 pDestIPAddress,
																				 LegIn.CustomerAcctId,
																				 LegIn.CustomerRouteId,
																				 legOutOptions[outboundAttempt - 1].CarrierAcctId,
																				 legOutOptions[outboundAttempt - 1].CarrierBaseRouteId,
																				 cdr.Leg1Length);
			if (_result) {
				callState = sessionHandler.OnConnected(legOutOptions[outboundAttempt - 1].CarrierAcctId, LegIn.CustomerAcctId);
			}
		}

		void retry() {
			if (session.State == SessionState.Leg2Redirecting) {
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "SessionScriptBase.retry", "Redirect failed - NOT retrying failover!");
				return;
			}

			var _retry = setCallEnd(RbrResult.MakeCall_Failed);
			if (_retry) {
				outboundAttempt++;
				setLegOutOptions();

				try {
					makeCall();
				}
				catch (Exception _ex) {
					TimokLogger.Instance.LogRbr(LogSeverity.Error, "SessionScriptBase.retry", string.Format("First choice failed, exception:\r\n{0}", _ex));
				}
			}
		}

		void makeCall() {
			session.State = SessionState.Leg2Signaling;
			var _destIPAddress = IPUtil.ExtractIPAddress(legOutOptions[outboundAttempt - 1].DestIPAndPort);
			var _destNumberAndPrefix = legOutOptions[outboundAttempt - 1].DestNumber;
			//var _destPrefix = getPrefix(_destNumberAndPrefix);
			//var _destNumber = getDestNumber(_destNumberAndPrefix, _destPrefix.Length);
			var _customHeader = legOutOptions[outboundAttempt - 1].CustomHeader ?? string.Empty;
			
			if (session.Channel.MakeCall(session.ANI, _destNumberAndPrefix, _destIPAddress, isNotCallRoutingScript, _customHeader)) {
				if (isCallRoutingScript) {
					session.Channel.SendAcceptCall(Script.ScriptType.ToString());
					session.Channel.WaitForEventIn();
				}

				session.State = SessionState.Leg2Redirecting;
				if (session.Channel.RedirectMedia()) {
					session.State = SessionState.Leg2;
					if (isNotCallRoutingScript) {
						session.Channel.DestroyResources();
					}
					setConnected(_destIPAddress);
					waitForCallEnd();
					return;
				}
				throw new RbrException(RbrResult.IVR_MakeCallOut, "SessionScriptBase.makeCall", "ERROR: RedirectCall failed");
			}
			throw new RbrException(RbrResult.IVR_MakeCallOut, "SessionScriptBase.makeCall", "ERROR: MakeCall Failed");
		}

		//static string getPrefix(string pDestNumberAndPrefix) {
		//  var _index = pDestNumberAndPrefix.IndexOf('#');
		//  return _index > 0 ? pDestNumberAndPrefix.Remove(_index + 1) : string.Empty;
		//}

		//static string getDestNumber(string pDestNumberAndPrefix, int pPrefixLength) {
		//  return pPrefixLength > 0 ? pDestNumberAndPrefix.Substring(pPrefixLength + 1) : pDestNumberAndPrefix;
		//}

		bool isCallRoutingScript { get { return Script.ScriptType == ScriptType.Wholesale ? true : false; } }
		bool isNotCallRoutingScript { get { return Script.ScriptType != ScriptType.Wholesale ? true : false; } }

		void setLegOutOptions() {
			if (legOutOptions[outboundAttempt - 1] != null) {
				cdr.Set(legOutOptions[outboundAttempt - 1]);
			}
			callState = sessionHandler.OnConnecting(LegIn, legOutOptions[outboundAttempt - 1]);
		}

		void waitForCallEnd() {
			session.Channel.WaitForCallEnd(legOutOptions[outboundAttempt - 1].TimeLimit * 1000);
		}

		bool setCallEnd(RbrResult pRbrResult) {
			try {
				cdr.End();

				var _cause = IVRDisconnectCause.LC_NORMAL;
				var _source = IVRDisconnectSource.IVR;
				if (session.Channel.DisconnectCause != (int)IVRDisconnectCause.LC_UNKNOWN) {
					_cause = (IVRDisconnectCause)session.Channel.DisconnectCause;
					_source = (IVRDisconnectSource)session.Channel.DisconnectSource;
				}
				cdr.SetCause(_cause, _source, pRbrResult);

				sessionHandler.CallComplete(session.Id, cdr.CallId, cdr.ToString());
				sessionHandler.OnCompleted(callState, safeCarrierAcctId(), LegIn.CustomerAcctId);

				//TODO: just do DisconnectAll here?
				if (session.State == SessionState.Leg1) {
					session.Channel.DisconnectLeg1();
					return false;
				}
				if (lastAttempt()) {
					session.Channel.DisconnectAll();
					return false;
				}
				session.Channel.DisconnectLeg2();
				return true;
			}
			finally {
				if (lastAttempt() && account.Obtained) {
					sessionHandler.SetRetailAcctNotInUse(session);
				}
			}
		}

		bool lastAttempt() {
			if (legOutOptions.Count > 1 && outboundAttempt == 1) {
				return false;
			}
			return true;
		}

		static void validateMandatoryPromptFiles(StringCollection pFiles) {
			if (pFiles == null || pFiles.Count == 0) {
				throw new RbrException(RbrResult.IVR_InvalidPromptFiles, "SessionScriptBase.validateMandatoryPromptFiles", "pFiles == null || pFiles.Count == 0");
			}

			foreach (var _file in pFiles) {
				if (!File.Exists(_file)) {
					throw new RbrException(RbrResult.IVR_PromptFileNotPresent, "SessionScriptBase.validateMandatoryPromptFiles", string.Format("validatePromptFiles: {0}", _file));
				}
			}
		}

		bool validateOptionalPromptFiles(StringCollection pFiles) {
			if (pFiles == null || pFiles.Count == 0) {
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "validateOptionalPromptFiles", string.Format("IVR_InvalidPromptFiles"));
				return false;
			}

			foreach (var _file in pFiles) {
				if (!File.Exists(_file)) {
					TimokLogger.Instance.LogRbr(LogSeverity.Status, "validateOptionalPromptFiles", string.Format("IVR_InvalidPromptFile {0}", _file));
					return false;
				}
			}
			return true;
		}

		short safeCarrierAcctId() {
			if (legOutOptions == null || legOutOptions[outboundAttempt - 1] == null) {
				return 0;
			}
			return legOutOptions[outboundAttempt - 1].CarrierAcctId;
		}
	}
}