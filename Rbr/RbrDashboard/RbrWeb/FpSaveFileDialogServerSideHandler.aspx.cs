using System;
using System.Text;
using System.Web.UI;
using RbrData.DataContracts;
using RbrWeb.Services;

namespace RbrWeb {
	public partial class FpSaveFileDialogServerSideHandler : Page {
		protected void Page_Load(object sender, EventArgs e) {
			var _service = new RbrDashboardServiceInternal();
			var _data = _service.GetRouteReport(0, "today"); //Request["fpSaveFileDialog_UploadDataInput"];

			var _stringBuilder = new StringBuilder();
			foreach (var _routeReportRecord in _data.RouteReport) {
				_stringBuilder.AppendLine(_routeReportRecord.ToString());
			}

			if (_stringBuilder.Length == 0) {
				return;
			}
			_stringBuilder.Insert(0, RouteReportRecord.ROUTE_REPORT_HEADER);
			var _routeReportString = _stringBuilder.ToString();

			var _contentType = Request["fpSaveFileDialog_UploadContentInput"];
			var _contentDis = Request["fpSaveFileDialog_UploadContentDisInput"];
			var _buf = Encoding.ASCII.GetBytes(_routeReportString);

			Response.Clear();

			if (!String.IsNullOrEmpty(_contentType)) Response.ContentType = _contentType;
			if (!String.IsNullOrEmpty(_contentDis)) Response.AddHeader("Content-Disposition", _contentDis);
			Response.OutputStream.Write(_buf, 0, _buf.Length);
			Response.Flush();
			Response.Close();
		}
	}
}