using System;
using System.Collections.Generic;
using System.Text;
using Timok.Rbr.BLL.ImportExport.Retail;
using Timok.Rbr.DTO;

namespace Timok.Rbr.BLL.ImportExport {

  public class PhoneCardImporterArgs {
    public string FilePath;
    public PhoneCardBatch PhoneCardBatch;

    public PhoneCardImporterArgs() { }

  }
}
