using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Timok.Rbr.Core;
using Timok.Rbr.Core.Config;

namespace Timok.IVR.Scripting {
	internal class PromptManager {
		readonly string rootPromptsFolder;
		readonly string standardPromptsFolder;
		readonly string servicePromptsFolder;
		readonly ScriptInfo scriptInfo;
		readonly string language;
		readonly string languageSuffix;

		public PromptManager(ScriptInfo pScriptInfo) {
			scriptInfo = pScriptInfo;
			language = scriptInfo.ScriptLanguage.ToString();
			languageSuffix = language.Substring(0, 2);

			rootPromptsFolder = Path.Combine(scriptInfo.RbrRoot, PromptConstants.ROOT_FOLDER);
			standardPromptsFolder = Path.Combine(rootPromptsFolder, PromptConstants.STANDARD);
			standardPromptsFolder = Path.Combine(standardPromptsFolder, language);

			servicePromptsFolder = Path.Combine(rootPromptsFolder, scriptInfo.ServiceName);
			if (!Directory.Exists(servicePromptsFolder)) {
				Directory.CreateDirectory(servicePromptsFolder);
			}

			servicePromptsFolder = Path.Combine(servicePromptsFolder, language);
			if (!Directory.Exists(servicePromptsFolder)) {
				Directory.CreateDirectory(servicePromptsFolder);
			}
		}

		public StringCollection WelcomePrompt {
			get {
				var _fileNames = new StringCollection
				                 	{
				                 		Path.Combine(servicePromptsFolder, string.Format("WelcomePrompt_{0}.raw", languageSuffix))
				                 	};
				return _fileNames;
			}
		}

		public StringCollection FirstCallWelcomePrompt {
			get {
				var _fileNames = new StringCollection
				                 	{
				                 		Path.Combine(servicePromptsFolder, string.Format("FirstCallWelcomePrompt_{0}.raw", languageSuffix))
				                 	};
				return _fileNames;
			}
		}

		public StringCollection MainMenuPrompt {
			get {
				var _fileNames = new StringCollection
				                 	{
				                 		Path.Combine(servicePromptsFolder, string.Format("MainMenuPrompt_{0}.raw", languageSuffix))
				                 	};
				return _fileNames;
			}
		}

		public StringCollection MainMenuChoicePrompt(int[] pMenuOptions) {
			var _options = string.Empty;
			foreach (var _option in pMenuOptions) {
				_options += _option.ToString();
			}

			var _fileNames = new StringCollection
			                 	{
			                 		Path.Combine(servicePromptsFolder, string.Format("MainMenuChoice_{0}_Prompt_{1}.raw", _options, languageSuffix))
			                 	};
			return _fileNames;
		}

		public StringCollection ReviewMenuPrompt {
			get {
				var _fileNames = new StringCollection
				                 	{
				                 		Path.Combine(servicePromptsFolder, string.Format("ReviewMenuPrompt_{0}.raw", languageSuffix))
				                 	};
				return _fileNames;
			}
		}

		public StringCollection ReviewInstructionsPrompt {
			get {
				var _fileNames = new StringCollection
				                 	{
				                 		Path.Combine(servicePromptsFolder, "ReviewInstructionsPrompt_" + languageSuffix + ".raw")
				                 	};
				return _fileNames;
			}
		}

		public StringCollection ChoiceIntroMessage(string pContext) {
			var _fileNames = new StringCollection
			                 	{
			                 		Path.Combine(servicePromptsFolder, string.Format("ChoiceIntroPrompt_{0}_{1}.raw", pContext, languageSuffix))
			                 	};
			return _fileNames;
		}

		public StringCollection ChoiceThankYouMessage(string pContext) {
			var _fileNames = new StringCollection
			                 	{
			                 		Path.Combine(servicePromptsFolder, string.Format("ChoiceThankYouPrompt_{0}_{1}.raw", pContext, languageSuffix))
			                 	};
			return _fileNames;
		}

		public StringCollection MonthlyDailyMessage {
			get {
				string _filePath = getDailyMessageFilePath();
				var _fileNames = new StringCollection
				                 	{
				                 		Path.Combine(_filePath, string.Format("{0}.raw", DateTime.Now.Day))
				                 	};
				return _fileNames;
			}
		}

		public string RecordingFilePath(string pSerialNumber, string pContext) {
			string _recordingFolder = getRecordingFolder(pContext);
			return string.Format(Path.Combine(_recordingFolder, string.Format("{0}_{1}{2}.wav", pContext, pSerialNumber, DateTime.Now.ToString("_yyyyMMdd_HHmmss"))));
		}

		public StringCollection EnterPhoneNumber() {
			var _fileNames = new StringCollection
			                 	{
			                 		Path.Combine(standardPromptsFolder, PromptConstants.ENTER_PHONE_NUMBER),
			                 		Path.Combine(standardPromptsFolder, PromptConstants.FOLLOWED_BY_POUND_SIGN)
			                 	};
			return _fileNames;
		}

		public StringCollection Balance(decimal pBalance) {
			return Balance(pBalance, 0);
		}

		public StringCollection Balance(decimal pBalance, int pBonusMinutes) {
			var _fileNames = new StringCollection
			                 	{
			                 		Path.Combine(standardPromptsFolder, PromptConstants.YOUR_BALANCE_IS)
			                 	};

			if (scriptInfo.PromptType == BalancePromptType.Money) {
				_fileNames.AddRange(getDolarsPrompt(pBalance).ToArray());
				_fileNames.AddRange(getCentsPrompt(pBalance).ToArray());
			}
			else {
				int _minutes = (int) (pBalance / scriptInfo.PerUnit) + pBonusMinutes;

				_fileNames.AddRange(getNumberVoicePrompt(_minutes).ToArray());
				if (_minutes == 1) {
					_fileNames.Add(Path.Combine(standardPromptsFolder, PromptConstants.MINUTE));
				}
				else {
					_fileNames.Add(Path.Combine(standardPromptsFolder, PromptConstants.MINUTES));
				}
			}

			_fileNames.Add(Path.Combine(standardPromptsFolder, PromptConstants.REMAINING));
			return _fileNames;
		}

		public StringCollection NoBalance {
			get {
				var _fileNames = new StringCollection
				                 	{
				                 		Path.Combine(standardPromptsFolder, PromptConstants.YOUR_BALANCE_IS),
				                 		Path.Combine(standardPromptsFolder, PromptConstants.ZERO)
				                 	};

				if (scriptInfo.PromptType == BalancePromptType.Money) {
					_fileNames.Add(Path.Combine(standardPromptsFolder, PromptConstants.DOLLARS));
				}
				else {
					_fileNames.Add(Path.Combine(standardPromptsFolder, PromptConstants.MINUTES));
				}
				return _fileNames;
			}
		}

		public StringCollection AuthorizationFailed {
			get {
				var _fileNames = new StringCollection
				                 	{
				                 		Path.Combine(standardPromptsFolder, PromptConstants.YOU_HAVE),
				                 		Path.Combine(standardPromptsFolder, PromptConstants.ZERO),
				                 		Path.Combine(standardPromptsFolder, PromptConstants.MINUTES),
				                 		Path.Combine(standardPromptsFolder, PromptConstants.REMAINING)
				                 	};
				return _fileNames;
			}
		}

		public StringCollection TimeLimitPrompt(int pTimeLimit) {
			int _minutes = pTimeLimit / 60;
			_minutes += pTimeLimit % 60 > 0 ? 1 : 0;

			if (_minutes > 1000) {
				throw new RbrException(RbrResult.PromptPlayer_Failed, "promptManager.TimeLimitPrompt", string.Format("Error: TimeLimit {0}, greter then 1000 minutes", _minutes));
			}

			var _fileNames = new StringCollection
			                 	{
			                 		Path.Combine(standardPromptsFolder, PromptConstants.YOU_HAVE)
			                 	};
			_fileNames.AddRange(getNumberVoicePrompt(_minutes).ToArray());
			if (_minutes == 1) {
				_fileNames.Add(Path.Combine(standardPromptsFolder, PromptConstants.MINUTE));
			}
			else {
				_fileNames.Add(Path.Combine(standardPromptsFolder, PromptConstants.MINUTES));
			}
			_fileNames.Add(Path.Combine(standardPromptsFolder, PromptConstants.REMAINING));
			return _fileNames;
		}

		public StringCollection EnterCardNumber {
			get {
				var _fileNames = new StringCollection
				                 	{
				                 		Path.Combine(standardPromptsFolder, PromptConstants.ENTER_CARD_NUMBER),
				                 		Path.Combine(standardPromptsFolder, PromptConstants.FOLLOWED_BY_POUND_SIGN)
				                 	};
				return _fileNames;
			}
		}

		public StringCollection InvalidCardNumberTryAgain {
			get {
				var _fileNames = new StringCollection
				                 	{
				                 		Path.Combine(standardPromptsFolder, PromptConstants.INVALID_CARD_NUMBER),
				                 		Path.Combine(standardPromptsFolder, PromptConstants.PLEASE_TRY_AGAIN)
				                 	};
				return _fileNames;
			}
		}

		public StringCollection TryAgain {
			get {
				var _fileNames = new StringCollection
				                 	{
				                 		Path.Combine(standardPromptsFolder, PromptConstants.PLEASE_TRY_AGAIN)
				                 	};
				return _fileNames;
			}
		}

		public StringCollection InvalidCardNumber {
			get {
				var _fileNames = new StringCollection
				                 	{
				                 		Path.Combine(standardPromptsFolder, PromptConstants.INVALID_CARD_NUMBER)
				                 	};
				return _fileNames;
			}
		}

		public StringCollection InvalidPhoneNumberTryAgain {
			get {
				var _fileNames = new StringCollection
				                 	{
				                 		Path.Combine(standardPromptsFolder, PromptConstants.INVALID_NUMBER_TRY_AGAIN)
				                 	};
				return _fileNames;
			}
		}

		public StringCollection CallCustomerService {
			get {
				var _fileNames = new StringCollection
				                 	{
				                 		Path.Combine(standardPromptsFolder, PromptConstants.CALL_CUSTOMER_SVC)
				                 	};
				return _fileNames;
			}
		}

		//-------------------------------- Private -----------------------------------
		List<string> getDolarsPrompt(decimal pBalance) {
			string _balance = pBalance.ToString();
			var _prompts = new List<string>();

			int _dotIndex = _balance.IndexOf('.');
			string _dolars = _dotIndex > -1 ? _balance.Substring(0, _dotIndex) : _balance;

			_prompts.AddRange(getNumberVoicePrompt(int.Parse(_dolars)));
			if (_dolars == PromptConstants.ONE_DOLLAR) {
				_prompts.Add(Path.Combine(standardPromptsFolder, PromptConstants.DOLLAR));
			}
			else {
				_prompts.Add(Path.Combine(standardPromptsFolder, PromptConstants.DOLLARS));
			}

			return _prompts;
		}

		List<string> getCentsPrompt(decimal pBalance) {
			string _balance = pBalance.ToString();
			var _prompts = new List<string>();

			int _dotIndex = _balance.IndexOf('.');
			string _cents = _dotIndex > -1 ? _balance.Remove(0, _dotIndex + 1) : "0";

			_prompts.AddRange(getNumberVoicePrompt(int.Parse(_cents)));
			if (_cents == PromptConstants.ONE_CENT) {
				_prompts.Add(Path.Combine(standardPromptsFolder, PromptConstants.CENT));
			}
			else {
				_prompts.Add(Path.Combine(standardPromptsFolder, PromptConstants.CENTS));
			}

			return _prompts;
		}

		List<string> getNumberVoicePrompt(int pNumber) {
			var _prompt = getSingleNumberVoicePrompt(pNumber);
			if (_prompt.Length > 0) {
				var _temp = new List<string>
				            	{
				            		_prompt
				            	};
				return _temp;
			}

			if (pNumber > 20 && pNumber < 100) {
				return getVoicePrompts_from_0_to_100(pNumber);
			}

			if (pNumber > 100 && pNumber < 1000) {
				return getVoicePrompts_from_100_to_1000(pNumber);
			}

			throw new Exception("getNumberVoicePrompt: Unexpected ! ");
		}

		List<string> getVoicePrompts_from_0_to_100(int pNumber) {
			var _prompt = getSingleNumberVoicePrompt(pNumber);
			if (_prompt.Length > 0) {
				var _temp = new List<string>
				            	{
				            		_prompt
				            	};
				return _temp;
			}

			//-- Between 20 and 100
			if (pNumber > 20 && pNumber < 30) {
				var _temp = new List<string>
				            	{
				            		Path.Combine(standardPromptsFolder, PromptConstants.TWENTY),
				            		getSingleNumberVoicePrompt(pNumber % 20)
				            	};
				return _temp;
			}

			if (pNumber > 30 && pNumber < 40) {
				var _temp = new List<string>
				            	{
				            		Path.Combine(standardPromptsFolder, PromptConstants.THIRTY),
				            		getSingleNumberVoicePrompt(pNumber % 30)
				            	};
				return _temp;
			}
			if (pNumber > 40 && pNumber < 50) {
				var _temp = new List<string>
				            	{
				            		Path.Combine(standardPromptsFolder, PromptConstants.FORTY),
				            		getSingleNumberVoicePrompt(pNumber % 40)
				            	};
				return _temp;
			}
			if (pNumber > 50 && pNumber < 60) {
				var _temp = new List<string>
				            	{
				            		Path.Combine(standardPromptsFolder, PromptConstants.FIFTY),
				            		getSingleNumberVoicePrompt(pNumber % 50)
				            	};
				return _temp;
			}
			if (pNumber > 60 && pNumber < 70) {
				var _temp = new List<string>
				            	{
				            		Path.Combine(standardPromptsFolder, PromptConstants.SIXTY),
				            		getSingleNumberVoicePrompt(pNumber % 60)
				            	};
				return _temp;
			}
			if (pNumber > 70 && pNumber < 80) {
				var _temp = new List<string>
				            	{
				            		Path.Combine(standardPromptsFolder, PromptConstants.SEVENTY),
				            		getSingleNumberVoicePrompt(pNumber % 70)
				            	};
				return _temp;
			}
			if (pNumber > 80 && pNumber < 90) {
				var _temp = new List<string>
				            	{
				            		Path.Combine(standardPromptsFolder, PromptConstants.EIGHTY),
				            		getSingleNumberVoicePrompt(pNumber % 80)
				            	};
				return _temp;
			}
			if (pNumber > 90 && pNumber < 100) {
				var _temp = new List<string>
				            	{
				            		Path.Combine(standardPromptsFolder, PromptConstants.NINETY),
				            		getSingleNumberVoicePrompt(pNumber % 90)
				            	};
				return _temp;
			}

			throw new Exception("getVoicePrompts_from_0_to_100: Unexpected number: " + pNumber);
		}

		List<string> getVoicePrompts_from_100_to_1000(int pNumber) {
			var _files = new List<string>();

			if (pNumber > 100 && pNumber < 200) {
				_files.Add(Path.Combine(standardPromptsFolder, PromptConstants.HUNDRED));
				_files.AddRange(getVoicePrompts_from_0_to_100(pNumber - 100));
			}
			else if (pNumber > 200 && pNumber < 300) {
				_files.Add(Path.Combine(standardPromptsFolder, PromptConstants.TWO_HUNDRED));
				_files.AddRange(getVoicePrompts_from_0_to_100(pNumber - 200));
			}
			else if (pNumber > 300 && pNumber < 400) {
				_files.Add(Path.Combine(standardPromptsFolder, PromptConstants.THREE_HUNDRED));
				_files.AddRange(getVoicePrompts_from_0_to_100(pNumber - 300));
			}
			else if (pNumber > 400 && pNumber < 500) {
				_files.Add(Path.Combine(standardPromptsFolder, PromptConstants.FOUR_HUNDRED));
				_files.AddRange(getVoicePrompts_from_0_to_100(pNumber - 400));
			}
			else if (pNumber > 500 && pNumber < 600) {
				_files.Add(Path.Combine(standardPromptsFolder, PromptConstants.FIVE_HUNDRED));
				_files.AddRange(getVoicePrompts_from_0_to_100(pNumber - 500));
			}
			else if (pNumber > 600 && pNumber < 700) {
				_files.Add(Path.Combine(standardPromptsFolder, PromptConstants.SIX_HUNDRED));
				_files.AddRange(getVoicePrompts_from_0_to_100(pNumber - 600));
			}
			else if (pNumber > 700 && pNumber < 800) {
				_files.Add(Path.Combine(standardPromptsFolder, PromptConstants.SEVEN_HUNDRED));
				_files.AddRange(getVoicePrompts_from_0_to_100(pNumber - 700));
			}
			else if (pNumber > 800 && pNumber < 900) {
				_files.Add(Path.Combine(standardPromptsFolder, PromptConstants.EIGHT_HUNDRED));
				_files.AddRange(getVoicePrompts_from_0_to_100(pNumber - 800));
			}
			else if (pNumber > 900 && pNumber < 1000) {
				_files.Add(Path.Combine(standardPromptsFolder, PromptConstants.NINE_HUNDRED));
				_files.AddRange(getVoicePrompts_from_0_to_100(pNumber - 900));
			}
			else {
				throw new Exception("get_100_to_1000_VoicePrompt: Unexpected number: " + pNumber);
			}
			return _files;
		}

		string getSingleNumberVoicePrompt(int pNumber) {
			switch (pNumber) {
				case 0:
					return Path.Combine(standardPromptsFolder, PromptConstants.ZERO);
				case 1:
					return Path.Combine(standardPromptsFolder, PromptConstants.ONE);
				case 2:
					return Path.Combine(standardPromptsFolder, PromptConstants.TWO);
				case 3:
					return Path.Combine(standardPromptsFolder, PromptConstants.THREE);
				case 4:
					return Path.Combine(standardPromptsFolder, PromptConstants.FOUR);
				case 5:
					return Path.Combine(standardPromptsFolder, PromptConstants.FIVE);
				case 6:
					return Path.Combine(standardPromptsFolder, PromptConstants.SIX);
				case 7:
					return Path.Combine(standardPromptsFolder, PromptConstants.SEVEN);
				case 8:
					return Path.Combine(standardPromptsFolder, PromptConstants.EIGHT);
				case 9:
					return Path.Combine(standardPromptsFolder, PromptConstants.NINE);
				case 10:
					return Path.Combine(standardPromptsFolder, PromptConstants.TEN);
				case 11:
					return Path.Combine(standardPromptsFolder, PromptConstants.ELEVEN);
				case 12:
					return Path.Combine(standardPromptsFolder, PromptConstants.TWELVE);
				case 13:
					return Path.Combine(standardPromptsFolder, PromptConstants.THIRTEEN);
				case 14:
					return Path.Combine(standardPromptsFolder, PromptConstants.FOURTEEN);
				case 15:
					return Path.Combine(standardPromptsFolder, PromptConstants.FIFTEEN);
				case 16:
					return Path.Combine(standardPromptsFolder, PromptConstants.SIXTEEN);
				case 17:
					return Path.Combine(standardPromptsFolder, PromptConstants.SEVENTEEN);
				case 18:
					return Path.Combine(standardPromptsFolder, PromptConstants.EIGHTEEN);
				case 19:
					return Path.Combine(standardPromptsFolder, PromptConstants.NINETEEN);
				case 20:
					return Path.Combine(standardPromptsFolder, PromptConstants.TWENTY);
				case 30:
					return Path.Combine(standardPromptsFolder, PromptConstants.THIRTY);
				case 40:
					return Path.Combine(standardPromptsFolder, PromptConstants.FORTY);
				case 50:
					return Path.Combine(standardPromptsFolder, PromptConstants.FIFTY);
				case 60:
					return Path.Combine(standardPromptsFolder, PromptConstants.SIXTY);
				case 70:
					return Path.Combine(standardPromptsFolder, PromptConstants.SEVENTY);
				case 80:
					return Path.Combine(standardPromptsFolder, PromptConstants.EIGHTY);
				case 90:
					return Path.Combine(standardPromptsFolder, PromptConstants.NINETY);
				case 100:
					return Path.Combine(standardPromptsFolder, PromptConstants.HUNDRED);
				case 200:
					return Path.Combine(standardPromptsFolder, PromptConstants.TWO_HUNDRED);
				case 300:
					return Path.Combine(standardPromptsFolder, PromptConstants.THREE_HUNDRED);
				case 400:
					return Path.Combine(standardPromptsFolder, PromptConstants.FOUR_HUNDRED);
				case 500:
					return Path.Combine(standardPromptsFolder, PromptConstants.FIVE_HUNDRED);
				case 600:
					return Path.Combine(standardPromptsFolder, PromptConstants.SIX_HUNDRED);
				case 700:
					return Path.Combine(standardPromptsFolder, PromptConstants.SEVEN_HUNDRED);
				case 800:
					return Path.Combine(standardPromptsFolder, PromptConstants.EIGHT_HUNDRED);
				case 900:
					return Path.Combine(standardPromptsFolder, PromptConstants.NINE_HUNDRED);
			}
			return string.Empty;
		}

		//string getDNISFilePath(string pDnis) {
		//  string _dnisFilePath = Path.Combine(standardPromptsFolder, pDnis);
		//  if (!Directory.Exists(_dnisFilePath)) {
		//    Directory.CreateDirectory(_dnisFilePath);
		//  }
		//  return _dnisFilePath;
		//}

		string getDailyMessageFilePath() {
			var _daulyMsgFilePath = Path.Combine(rootPromptsFolder, scriptInfo.ServiceName);
			_daulyMsgFilePath = Path.Combine(_daulyMsgFilePath, PromptConstants.DAILY);
			if (!Directory.Exists(_daulyMsgFilePath)) {
				Directory.CreateDirectory(_daulyMsgFilePath);
			}
			return _daulyMsgFilePath;
		}

		string getRecordingFolder(string pContext) {
			var _recordingsPath = Path.Combine(rootPromptsFolder, scriptInfo.ServiceName);
			_recordingsPath = Path.Combine(_recordingsPath, PromptConstants.RECORDINGS);
			if (!Directory.Exists(_recordingsPath)) {
				Directory.CreateDirectory(_recordingsPath);
			}

			var _filePath = Path.Combine(_recordingsPath, pContext);
			if (!Directory.Exists(_filePath)) {
				Directory.CreateDirectory(_filePath);
			}

			return _filePath;
		}
	}
}