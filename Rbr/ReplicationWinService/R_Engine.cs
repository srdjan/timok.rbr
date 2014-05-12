using System.Collections.Generic;

namespace Timok.Rbr.Replication {
	public sealed class ReplicationEngine {
		//readonly short nodeId;
		//public short NodeId { get { return nodeId; } }
		readonly List<R_Consumer> consumers;
		readonly List<R_Publisher> publishers;

		public ReplicationEngine(/*short pNodeId*/) {
			//nodeId = pNodeId;
			publishers = new List<R_Publisher>();
			consumers = new List<R_Consumer>();
		}

		public void AddConsumer(string pName, string pFromFolder, ConsumerDelegate pDelegate) {
			var _consumer = new R_Consumer(pName, pFromFolder, pDelegate);
			consumers.Add(_consumer);
			_consumer.Start();
		}

		public void AddPublisher(string pName, string pFromFolder, PublisherDelegate pDelegate) {
			var _publisher = new R_Publisher(pName, pFromFolder, pDelegate);
			publishers.Add(_publisher);
			_publisher.Start();
		}

		public void StopAll() {
			foreach (var _consumer in consumers) {
				_consumer.Stop();
			}
			consumers.Clear();

			foreach (var _publisher in publishers) {
				_publisher.Stop();
			}
			publishers.Clear();
		}
	}
}