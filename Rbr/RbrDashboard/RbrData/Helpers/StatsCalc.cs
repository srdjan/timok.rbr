namespace RbrData.Helpers {
	internal static class StatsCalc {
		const string Zero = "0";

		public static string GetASR(int? pTotal, int? pCompleted) {
			if (pTotal > 0) {
				var _div = (decimal) pCompleted/(decimal) pTotal;
				var _formatted = (_div*100).ToString("F");
				return _formatted;
			}
			return Zero;
		}

		public static string GetACD(decimal? pOutMinutes, int? pCompleted) {
			if (pOutMinutes > 0 && pCompleted > 0) {
				var _div = (decimal) pOutMinutes/(decimal) pCompleted;
				var _formatted = _div.ToString("F");
				return _formatted;
			}
			return Zero;
		}
	}
}