using System;
using System.Collections.Specialized;

namespace Timok.Rbr.Core {
	public interface ISessionChannel {
		string ChannelId { get; }
		string CallId { get; }
		string OrigIPAddress { get; }
		string AccessNumber { get; }
		string ANI { get; }

		int DisconnectCause { get; }
		int DisconnectSource { get; }

		bool WaitForCall();

		void PlayTone();
		void Play(StringCollection pFileNames, Boolean pStopOnDTMF, bool pShouldResetDTMFBuffer);
		void Record(string pFilePath, int pMaxTime, bool pStopOnDTMFTerminator);
		bool CollectDTMF(int pLength, out string pDtmfBuffer);
		bool CollectDTMF(int pMinLength, int pMaxLength, out string pDtmfBuffer);

		void SendAcceptCall(string pCallType);
		void WaitForEventIn();
		void SendCallProgress();
		void SendRinging();
		void CreateResources();
		void ConfigResources();
		void DestroyResources();
		bool MakeCall(string pANI, string pDestNumber, string pDestIP, bool pShouldRing, string pCustomHeaderSource);
		bool RedirectMedia();
		void WaitForCallEnd(int pTimeLimit);
		void DisconnectLeg1();
		void DisconnectLeg2();
		void DisconnectAll();
	}
}