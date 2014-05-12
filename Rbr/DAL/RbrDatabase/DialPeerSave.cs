using System;

namespace Timok.Rbr.DAL.RbrDatabase {

	[Serializable]
	public class DialPeerSave {
		private string newPrefix_in;
		private string oldPrefix_in;
		private DialPeerRow dialPeer;

		public string NewPrefix_in {
			get {return newPrefix_in;}
			set {
				if(value == null){
					throw new NullReferenceException("NewPrefix_in cannot be null");
				}
				else {
					newPrefix_in = value;
				}
			}
		}
		public string OldPrefix_in {
			get {return oldPrefix_in;}
			set {
				if(value == null){
					throw new NullReferenceException("OldPrefix_in cannot be null");
				}
				else {
					oldPrefix_in = value;
				}
			}
		}
		public DialPeerRow DialPeer {
			get {return dialPeer;}
			set {
				if(value == null){
					throw new NullReferenceException("DialPeer cannot be null");
				}
				else {
					dialPeer = value;
				}
			}
		}

		public DialPeerSave() {}
	}
}
