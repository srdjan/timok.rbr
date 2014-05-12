using System;
using System.IO;
using System.Windows.Browser;

namespace RbrSiverlight.Helpers {
	public class FpSaveFileDialog {
		const string UPLOADCONTENT_ID = "fpSaveFileDialog_UploadContentInput";
		const string UPLOADCONTENTDIS_ID = "fpSaveFileDialog_UploadContentDisInput";
		const string UPLOADDATA_ID = "fpSaveFileDialog_UploadDataInput";
		const string UPLOADFORM_ID = "fpSaveFileDialog_UploadForm";

		public FpSaveFileDialog(Uri pHandlerUri) {
			if (pHandlerUri == null) throw new ArgumentNullException("pHandlerUri");
			HandlerUri = pHandlerUri;
		}

		public Uri HandlerUri { get; set; }

		internal static HtmlDocument Document {
			get {
				if (HtmlPage.IsEnabled == false || HtmlPage.Document == null || HtmlPage.Document.Body == null) throw new Exception("Document is unavalibe. Can not save file.");
				return HtmlPage.Document;
			}
		}

		internal HtmlElement UploadForm {
			get {
				var _form = Document.GetElementById(UPLOADFORM_ID);
				if (_form == null) {
					_form = Document.CreateElement("Form");
					_form.SetStyleAttribute("display", "none");
					_form.SetStyleAttribute("visibility", "hidden");
					_form.SetAttribute("id", UPLOADFORM_ID);
					_form.SetAttribute("name", UPLOADFORM_ID);
					_form.SetAttribute("method", "post");
					_form.SetAttribute("action", HandlerUri.ToString());
					Document.Body.AppendChild(_form);
				}
				return _form;
			}
		}

		internal HtmlElement UploadDataInput {
			get { return GetHtmlInput(UPLOADDATA_ID); }
		}

		internal HtmlElement UploadContentTypeInput {
			get { return GetHtmlInput(UPLOADCONTENT_ID); }
		}

		internal HtmlElement UploadContentDisInput {
			get { return GetHtmlInput(UPLOADCONTENTDIS_ID); }
		}

		HtmlElement GetHtmlInput(string pId) {
			if (String.IsNullOrEmpty(pId)) throw new ArgumentNullException("pId");
			var _ipt = Document.GetElementById(pId);
			if (_ipt == null) {
				_ipt = CreateHtmlHiddenInput(pId);
				UploadForm.AppendChild(_ipt);
			}
			return _ipt;
		}

		static HtmlElement CreateHtmlHiddenInput(string pId) {
			var _ipt = Document.CreateElement("Input");
			_ipt.SetAttribute("pId", pId);
			_ipt.SetAttribute("name", pId);
			_ipt.SetAttribute("type", "hidden");
			return _ipt;
		}

		protected virtual string GetDataString(byte[] pBuf, int pIndex, int pCount) {
			var _data = Convert.ToBase64String(pBuf, pIndex, pCount);
			return _data;
		}

		public bool Save(Stream pStream, FpSaveFileDialogOption pOpt) {
			if (pStream == null || pStream.CanRead == false) throw new ArgumentException("pStream");

			int _length;
			unchecked {
				_length = (int) (pStream.Length - pStream.Position - 1);
			}
			if (_length == 0) return false;
			var _buf = new byte[_length];
			pStream.Read(_buf, 0, _length);
			return Save(_buf, 0, _length, pOpt);
		}

		public bool Save(Stream pStream) {
			return Save(pStream, new FpSaveFileDialogOption());
		}

		public bool Save(byte[] pBuf, int pIndex, int pCount, FpSaveFileDialogOption pOpt) {
			if (pBuf == null) {
				throw new ArgumentNullException("pBuf", "ArgumentNull_Array");
			}
			if ((pIndex < 0) || (pCount < 0)) {
				throw new ArgumentOutOfRangeException((pIndex < 0) ? "pIndex" : "pCount", "ArgumentOutOfRange_NeedNonNegNum");
			}
			if ((pBuf.Length - pIndex) < pCount) {
				throw new ArgumentOutOfRangeException("pBuf", "ArgumentOutOfRange_IndexCountBuffer");
			}
			if (pBuf.Length == 0) {
				return false;
			}

			var _data = GetDataString(pBuf, pIndex, pCount);
			UploadDataInput.SetAttribute("value", _data);
			if (pOpt != null) {
				if (!String.IsNullOrEmpty(pOpt.ContentType)) UploadContentTypeInput.SetAttribute("value", pOpt.ContentType);
				else UploadContentTypeInput.RemoveAttribute("value");

				var _dis = String.Empty;
				if (!String.IsNullOrEmpty(pOpt.ContentDisposition)) _dis += pOpt.ContentDisposition;
				if (!String.IsNullOrEmpty(pOpt.FileName)) _dis += "filename=" + pOpt.FileName;
				if (!String.IsNullOrEmpty(_dis)) UploadContentDisInput.SetAttribute("value", _dis);
				else UploadContentDisInput.RemoveAttribute("value");
			}

			Document.Submit(UploadForm.Id);
			return true;
		}

		public bool Save(byte[] pBuf) {
			return Save(pBuf, new FpSaveFileDialogOption());
		}

		public bool Save(byte[] pBuf, FpSaveFileDialogOption pOpt) {
			return Save(pBuf, 0, pBuf.Length, pOpt);
		}

		public class FpSaveFileDialogOption {
			public FpSaveFileDialogOption() {
				ContentType = "application/octet-stream";
				ContentDisposition = "attachment;";
				FileName = "unnamed";
			}

			public FpSaveFileDialogOption(string pFileName) : this() {
				FileName = pFileName;
			}

			public string FileName { get; set; }
			protected internal virtual string ContentType { get; set; }
			protected internal virtual string ContentDisposition { get; set; }
		}
	}
}