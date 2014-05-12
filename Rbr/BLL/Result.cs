using System;

namespace Timok.Rbr.BLL {
	[Serializable]
	public class Result {
		bool success = true;
		public bool Success { get { return success; } set { success = value; } }

		string errorMessage = string.Empty;
		public string ErrorMessage { get { return errorMessage; } set { errorMessage = value; } }

		public Result() {}

		public Result(bool pSuccess, string pErrorMessage) {
			success = pSuccess;
			errorMessage = pErrorMessage ?? string.Empty;
		}
	}
}