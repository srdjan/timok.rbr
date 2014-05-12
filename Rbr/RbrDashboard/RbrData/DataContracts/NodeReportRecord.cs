using System;
using System.Runtime.Serialization;

namespace RbrData.DataContracts {
	[DataContract]
	public class NodeReportRecord {
		[DataMember]
		public short? NodeId { get; private set; }
		[DataMember]
		public int DateHour { get; private set; }
		[DataMember]
		public int? InMinutes { get; private set; }
		[DataMember]
		public decimal? OutMinutes { get; private set; }
		[DataMember]
		public int? Total { get; private set; }
		[DataMember]
		public int? Completed { get; private set; }

		public NodeReportRecord(short? pNodeId, 
		                             int pDateHour,
		                             int? pInMinutes,	
		                             decimal? pOutMinutes,	
		                             int? pTotalCalls,	
		                             int? pCompletedCalls) {
			NodeId = pNodeId;
			DateHour = pDateHour;
			InMinutes = pInMinutes;
			OutMinutes = pOutMinutes;
			Total = pTotalCalls;
			Completed = pCompletedCalls;
		}
	}
}