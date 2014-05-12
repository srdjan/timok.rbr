using System;
using System.Collections.Specialized;
using System.IO;
using Timok.Core.Logging;
using Timok.Rbr.Core;
using T = Timok.Core.Logger.TimokLogger;

namespace Timok.IVR.Scripting {
	public enum CallState {
		Initializing,
		Started,
		Connecting,
		Connected
	}

	internal abstract class SessionScriptBase : ISessionScript {
		const int destNumberMaxLength = 18;
		const int destNumberMinLength = 8;
		const int numberOfRetries = 3;

		const string one = "1";
		const string zeroOneOne = "011";
		readonly ScriptInfo scriptInfo;
		CallState callState;
		protected int cardLength;

		protected decimal currentBalance;
		protected int currentBonusBalance;
		protected int leg1Length;
		protected bool neverUsed;
		protected PromptManager promptManager;
		protected string serialNumber;
		protected ISession session;
		protected decimal startingBalance;

		public SessionScriptBase(RbrResult pInitResult, ISession pSession, ScriptInfo pScriptInfo) {
			session = pSession;
			scriptInfo = pScriptInfo;

			promptManager = new PromptManager(scriptInfo);
			leg1Length = 0;
			cardLength = scriptInfo.PinLength;
			neverUsed = false;

			startingBalance = currentBalance = decimal.Zero;
			currentBonusBalance = 0;
			serialNumber = string.Empty;
			callState = CallState.Initializing;
		}

		#region ISessionScript Members

		public int NumberOfSubSessions { get { return scriptInfo.NumberOfSubSessions; } }

		//-- ------------------------------- Public API  ----------------------------------------------------
		public virtual bool Welcome() {
			return true;
		}

		public virtual bool Authenticate(int pCount) {
			return true;
		}

		public void Run(bool pIsFinal) {
			RbrResult _rbrResult = RbrResult.Success;

			try {
				run();
			}
			catch (Exception _ex) {
				_rbrResult = handleException(_ex);
			}
			finally {
				SetCallEnd(_rbrResult, pIsFinal);
			}
		}

		#endregion

		//----------------------------------------------------------------------------------------------------------
		//NOTE: each individual Script implements these methods:
		//----------------------------------------------------------------------------------------------------------
		protected virtual void run() {
			throw new NotImplementedException();
		}

		//----------------------------------------------------------------------------------------------------------
		//-- Common methods provided for inheritors
		//----------------------------------------------------------------------------------------------------------
		protected int GetChoice(StringCollection pChoicesPrompt, int[] pChoices, int pNumberOfRetries) {
			for (int _i = 0; _i < pNumberOfRetries; _i++) {
				Play(pChoicesPrompt, true, true);
				string _dtmfBuffer;
				if (session.Channel.CollectDTMF(1, out _dtmfBuffer)) {
					foreach (int _choice in pChoices) {
						int _entered = int.Parse(_dtmfBuffer);
						if (_choice == _entered) {
							return _entered;
						}
					}
				}
				continue;
			}
			throw new RbrException(RbrResult.IVR_GetChoice_Error, "SessionScriptBase.GetChoice", string.Format("Error: Max Number of Retries={0} reached", pNumberOfRetries));
		}

		protected void GetDestNumber() {
			for (int _i = 1; _i <= numberOfRetries; _i++) {
				if (_i == 1) {
					Play(promptManager.EnterPhoneNumber(), true, true);
				}
				else {
					Play(promptManager.InvalidPhoneNumberTryAgain, true, true);
				}

				string _destNumber;
				if (session.Channel.CollectDTMF(destNumberMinLength, destNumberMaxLength, out _destNumber)) {
					if (! _destNumber.StartsWith(zeroOneOne) && ! _destNumber.StartsWith(one)) {
						_destNumber = one + _destNumber;
					}
					session.DestNumber = _destNumber;
					return;
				}
			}
			Play(promptManager.CallCustomerService, false, true);
			throw new RbrException(RbrResult.GetDestNumberDTMF_error, "SessionScriptBase.GetDestNumber", "GetDestNumber");
		}

		protected void Play(StringCollection pFiles, bool pStopOnDTMF, bool pMandatory) {
			bool _filesValidated;

			if (pMandatory) {
				validateMandatoryPromptFiles(pFiles);
				_filesValidated = true;
			}
			else {
				_filesValidated = validateOptionalPromptFiles(pFiles);
			}

			if (_filesValidated) {
				session.Channel.Play(pFiles, pStopOnDTMF);
			}
		}

		protected void PlayTone() {
			session.Channel.PlayTone();
		}

		protected void Record(String pFilePath, bool pStopOnDTMFTerminator) {
			session.Channel.Record(pFilePath, AppConstants.MaxRecordingTime, pStopOnDTMFTerminator);
		}

		protected void SetConnected(string pDestIPAddress) {
			callState = CallState.Connected;
		}

		protected void MakeCall(string pDestNumber, string pDestIPAddress, EndPointProtocol pProtocol) {
			session.State = SessionState.Leg2;
			if (pProtocol == EndPointProtocol.SIP) {
				session.Channel.MakeCall(session.ANI, pDestNumber, session.OrigIPAddress, pDestIPAddress, IVRConfig.Instance.CodecType);
			}
			else if (pProtocol == EndPointProtocol.H323) {
				//TODO:
				//session.Channel.MakeCallH323(session.ANI, session.DestNumber, session.OrigIPAddress, pDestIPAddress, IVRConfig.Instance.CodecType);
			}
			else {
				throw new Exception("Unknown protocol");
			}
			SetConnected(pDestIPAddress);
		}

		protected void WaitForCallEnd(int pTimeLimit) {
			session.Channel.WaitForCallEnd(pTimeLimit * 1000);
		}

		protected void SetCallEnd(RbrResult pRbrResult, bool pIsFinal) {
			if (pIsFinal) {
				if (session.State == SessionState.Leg1) {
					session.Channel.DisconnectLeg1();
				}
				else if (session.State == SessionState.Leg2) {
					session.Channel.DisconnectAll();
				}
			}
		}

		//------------------------------------------------ Private -------------------------------------------------
		RbrResult handleException(Exception pEx) {
			T.LogRbr(LogSeverity.Critical, "SessionScriptBase.HadleException", string.Format("{0},{1}, Exception:\r\n{2}", session.Id, session.Channel, pEx));
			return RbrResult.ExceptionThrown;
		}

		void validateMandatoryPromptFiles(StringCollection pFiles) {
			if (pFiles == null || pFiles.Count == 0) {
				throw new RbrException(RbrResult.IVR_InvalidPromptFiles, "SessionScriptBase.validateMandatoryPromptFiles", "validatePromptFiles");
			}

			foreach (string _file in pFiles) {
				if (!File.Exists(_file)) {
					throw new RbrException(RbrResult.IVR_PromptFileNotPresent, "SessionScriptBase.validateMandatoryPromptFiles", string.Format("validatePromptFiles: {0}", _file));
				}
			}
		}

		bool validateOptionalPromptFiles(StringCollection pFiles) {
			if (pFiles == null || pFiles.Count == 0) {
				T.LogRbr(LogSeverity.Status, "validateOptionalPromptFiles", string.Format("IVR_InvalidPromptFiles"));
				return false;
			}

			foreach (string _file in pFiles) {
				if (!File.Exists(_file)) {
					T.LogRbr(LogSeverity.Status, "validateOptionalPromptFiles", string.Format("IVR_InvalidPromptFile {0}", _file));
					return false;
				}
			}
			return true;
		}
	}
}