using Timok.Logger;

namespace Timok.Rbr.BLL.DOM.Repositories {
	public sealed class CdrAggregateRepository : RepositoryBase, IRepository { 
		public CdrAggregateRepository() : base("CdrAggregate", typeof(string), true) {	}

		public void Put(object pCdrAggregate) {
			var _cdrAggregate = pCdrAggregate as CdrAggregate;
			if (_cdrAggregate != null) {
				pk.Put(_cdrAggregate.PK, _cdrAggregate);
				TimokLogger.Instance.LogRbr(LogSeverity.Status, "CdrAggregateRepository.Put:", "ADDED to Imdb: " + _cdrAggregate.PK);
			}
			else {
				TimokLogger.Instance.LogRbr(LogSeverity.Error, "CdrAggregateRepository.Put:", "NULL");
			}
		}

		public object GetByPK(string pPk) {
			var _cdrAggr = pk.Get(pPk) as CdrAggregate;
			if (_cdrAggr != null) {
				TimokLogger.Instance.LogRbr(LogSeverity.Debug, "CdrAggregateRepository.GetByPK:", "FOUND in Imdb: " + pPk);
			}		
			else {
				TimokLogger.Instance.LogRbr(LogSeverity.Debug, "CdrAggregateRepository.GetByPK:", "NOT FOUND in Imdb: " + pPk);
			}
			return _cdrAggr;
		}

		//TODO: see if we can do without copying !!!
		public CdrAggregate[] ToArray() {
			var _cdrAggrs = new CdrAggregate[pk.Count];
			pk.CopyTo(_cdrAggrs, 0);
			return _cdrAggrs;
		}
	}
}