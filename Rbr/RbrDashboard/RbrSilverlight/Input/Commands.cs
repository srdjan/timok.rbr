
namespace RbrSiverlight.Input {
	public static class Commands {
		static Commands() {
			GetDives = new Command("GetDives");
			NewDive = new Command("NewDive");
			SaveDives = new Command("SaveDives");
			DeleteDives = new Command("DeleteDive");
			StartProgress = new Command("StartProgress");
			StopProgress = new Command("StopProgress");
			UpdateDirtyCounter = new Command("UpdateDirtyCounter");
		}

		public static Command GetDives { get; private set; }
		public static Command NewDive { get; private set; }
		public static Command SaveDives { get; private set; }
		public static Command DeleteDives { get; private set; }
		public static Command StartProgress { get; private set; }
		public static Command StopProgress { get; private set; }
		public static Command UpdateDirtyCounter { get; private set; }
	}
}