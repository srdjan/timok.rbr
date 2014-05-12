using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows;

namespace RbrSiverlight.Extensions {
	public static class SLExtension {
		public static bool IsInVisualTree(this FrameworkElement element) {
			return IsInVisualTree(element, Application.Current.RootVisual as FrameworkElement);
		}

		public static bool IsInVisualTree(this FrameworkElement element, FrameworkElement ancestor) {
			var _fe = element;
			while (_fe != null) {
				if (_fe == ancestor) {
					return true;
				}

				_fe = _fe.Parent as FrameworkElement;
			}
			return false;
		}

		public static string ToJson(this object objectToSerialize) {
			using (var _ms = new MemoryStream()) {
				var _serializer = new DataContractJsonSerializer(objectToSerialize.GetType());

				_serializer.WriteObject(_ms, objectToSerialize);
				_ms.Position = 0;

				using (var reader = new StreamReader(_ms)) {
					return reader.ReadToEnd();
				}
			}
		}
	}
}