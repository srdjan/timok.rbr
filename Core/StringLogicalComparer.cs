using System.Collections;
using System.Runtime.InteropServices;

namespace Timok.Core {

	public class StringLogicalComparer : IComparer {
		public int Compare(object x, object y) {
			if (x == y) return 0;

			string text1 = x as string;
			if (text1 != null) {
				string text2 = y as string;
				if (text2 != null) {
					return StrCmpLogicalW(text1, text2);
				}
			}

			return Comparer.Default.Compare(x, y);
		}

		[DllImport("shlwapi.dll", CharSet=CharSet.Unicode, ExactSpelling=true)]
		private static extern int StrCmpLogicalW(string strA, string strB);
	}
}
