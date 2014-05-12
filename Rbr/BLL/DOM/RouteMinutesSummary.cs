using System;

namespace Timok.Rbr.DOM 
{
	[Serializable]
	public class RouteMinutesSummary {
		public const string Id_PropName = "Id";
		public const string Name_PropName = "Name";
		public const string Minutes_PropName = "Minutes";

		int id;
		public int Id { get { return id; } set { id = value; } }

		string name = " UNKNOWN";
		public string Name { get { return name; } set { name = value; } }

		decimal minutes;
		public decimal Minutes { get { return minutes; } set { minutes = value; } }
	}
}