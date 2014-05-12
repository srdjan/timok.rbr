using System.IO;

namespace Timok.Core {
	public class DiskSpace {
		public static int GetAvailable(string pDrive) {
			int _availableSpace;

			try {
				var _driveInfo = new DriveInfo(pDrive);
				_availableSpace = (int) (_driveInfo.AvailableFreeSpace / 1000000000);
			}
			catch {
				_availableSpace = 0;
			}
			return _availableSpace;
		}
	}
}