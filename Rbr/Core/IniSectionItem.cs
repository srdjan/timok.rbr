using System;

namespace Timok.Rbr.Core {
	/// <summary>
	/// Summary description for INISectionItem.
	/// This is a Base class for XXX_IniSectionItem classes in corresponding INI sections
	/// </summary>
	public abstract class IniSectionItem {
		protected string sectionName;
		protected string key;
		protected string keyValue;

		protected IniSectionItem(string pFileName, string pSectionName, string pKey, string pValue) {
			if(pFileName.Trim() == string.Empty)
				throw new ArgumentException("File Name cannot be empty", "pFileName");
			if(pSectionName.Trim() == string.Empty)
				throw new ArgumentException("Section Name cannot be empty", "pSectionName");
			if(pKey.Trim() == string.Empty)
				throw new ArgumentException("Key cannot be empty", "pKey");
			if(pValue.Trim() == string.Empty)
				throw new ArgumentException("Value cannot be empty", "pValue");

			sectionName = pSectionName;
			key = pKey;
			keyValue = pValue;
		}

		protected IniSectionItem(string pFileName, string pSectionName, string pKey) {
			if(pFileName.Trim() == string.Empty)
				throw new ArgumentException("File Name cannot be empty", "pFileName");
			if(pSectionName.Trim() == string.Empty)
				throw new ArgumentException("Section Name cannot be empty", "pSectionName");
			if(pKey.Trim() == string.Empty)
				throw new ArgumentException("Key cannot be empty", "pKey");

			sectionName = pSectionName;
			key = pKey;
			keyValue = String.Empty;
		}

		virtual public void CompareToIni() {
			string iniValue = IniFileIO.GetKey(sectionName, key);
			if(iniValue == string.Empty || iniValue != keyValue)
				throw new ApplicationException("INI inconsistency:\r\n" + 
					"Section: [" + sectionName + "] \r\n" + 
					"key: " + key + "\r\n" +	 
					"dbValue : [" + keyValue + "]\r\n" + 
					"iniValue: [" + iniValue + "]\r\n");
		}

		virtual public bool Delete() {
			return IniFileIO.DeleteKey(sectionName, key);
		}
		virtual public bool Write() {
			return IniFileIO.Write(sectionName, key, keyValue);
		}
	}

	//***********************************************************
	/// <summary>
	///Main section	
	/// </summary>

	//***********************************************************
	/// <summary>
	/// Example:
	/// 10.0.1.5=Citron;;
	/// </summary>
	public class OrigNeighborsItem: IniSectionItem {
		public const string SectionName = "RasSrv::Neighbors";
		private const string valueSuffix = ";;";

		public OrigNeighborsItem(string pFileName, string pAddress, string pAlias):
			base(pFileName, SectionName, pAlias, pAddress + valueSuffix) {
		}
	}

	//***********************************************************
	/// <summary>
	/// Example:
	/// 10.0.1.5=Citron;*
	/// </summary>
	public class TermNeighborsItem: IniSectionItem {
		public const string SectionName = "RasSrv::Neighbors";
		private const string valueSuffix = ";*";

		public TermNeighborsItem(string pFileName, string pAddress, string pAlias):
			base(pFileName, SectionName, pAlias, pAddress + valueSuffix) {
		}
	}
	
	//**************************************************************
	/// <summary>
	/// Example:
	/// GK1=192.168.0.5;*
	/// </summary>
	public class PermEPItem: IniSectionItem {
		public const string SectionName = "RasSrv::PermanentEndpoints";
		private const string valueSuffix = ";;";

		public PermEPItem(string pFileName, string pAddress, string pAlias): 
			base(pFileName, SectionName, pAddress, pAlias + valueSuffix) {}
	}

	//***************************************************
	/// <summary>
	/// Example: 
	/// gw01=sigip:160.79.23.60:1720
	/// </summary>
	public class RRQAuthItem: IniSectionItem {
		public const string SectionName = "RasSrv::RRQAuth";//default=reject		
		private const string valuePrefix = "sigip:";

		public RRQAuthItem(string pFileName, string pAddress, string pAlias): 
			base(pFileName, SectionName, pAlias, valuePrefix + pAddress) {
		}
	}
}
