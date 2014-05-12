namespace Timok.Rbr.BLL.ImportExport {
	public enum ImportAction {
		Full,
		CallingPlanOnly,
		RatesOnly
	}

	internal class OldConstants {
		public const char CountryLineFirstChar = '#';
		public const string CountryLineFirstCharString = "#";
		public const char FieldDelimiterChar = '|';
		public const string FieldDelimiterString = "|";
		public const char TimeDelimiterChar = ',';
		public const char CostDelimiterChar = ',';
		public const char IncrValueDelimiterChar = '/';

		public const char PnoneCardBatchFieldDelimiter = ',';

		OldConstants() { }
	}
}