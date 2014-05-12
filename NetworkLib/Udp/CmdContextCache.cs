using System;
using System.Collections;

namespace Timok.NetworkLib.Udp {
	internal class CmdContextCache {
		static readonly SortedList cache;

		static CmdContextCache() {
			cache = SortedList.Synchronized(new SortedList());
		}

		static public bool Save(uint pKey, CmdContext pRequestCtx) {
			if (cache.ContainsKey(pKey)) {
				throw new Exception(string.Format("CmdContextCache.Save: Key={0}, in USE", pKey));
			}

			cache.Add(pKey, pRequestCtx);
			return true;
		}

		static public CmdContext Get(uint pKey) {
			CmdContext _ctx = null;
			if (cache.ContainsKey(pKey)) {
				_ctx = (CmdContext) cache[pKey];
			}
			return _ctx;
		}

		static public bool Remove(uint pKey) {
			if (cache.ContainsKey(pKey)) {
				cache.Remove(pKey);
			}
			else {
				throw new Exception(string.Format("CmdContextCache.Remove: Key={0}, NOT found", pKey));
			}
			return true;
		}
	}
}