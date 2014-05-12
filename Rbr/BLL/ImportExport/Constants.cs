namespace Timok.Rbr.BLL.ImportExport {
	public enum ImportExportType {
		Import,
		Export 
	}

	public enum ImportExportFilter {
		DialPlan,
		Rates,
		Both
	}

	internal class Constants {
    //NOTE: used by PhoneCardImporter
    public const string CommaDelimiter = ",";

    //TODO: to be removed from here, it was moved into AppConstants
    //public const string PipeDelimiter = "|";
    //public const string SlashDelimiter = "/";

		Constants() { }
	}
}