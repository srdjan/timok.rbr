using System;

namespace Timok.Core.Events {
		public class GenericEventArgs<T> : EventArgs {
			public GenericEventArgs(T pPayload) {
				Payload = pPayload;
			}

			public T Payload { get; private set; }
		}

		public static class EventExtensions {
			public static void Raise<T>(this EventHandler<GenericEventArgs<T>> pHandler, object pSender, T pPayload) {
				if (pHandler != null)
					pHandler(pSender, new GenericEventArgs<T>(pPayload));
			}
		}  
}