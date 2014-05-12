using System;
using Perst;

namespace Timok.Rbr.BLL.DOM.Repositories {
	public abstract class RepositoryBase : IDisposable {
		static readonly object padlock = new object();
		protected Index ak;
		readonly Storage db;
		protected Index mk;
		readonly string name;

		protected Index pk;
		readonly Root root;

		protected RepositoryBase() {
		}

		//-- file storage has PrimaryKey, or Index consisting of one field
		protected RepositoryBase(string pName, string pFilePath, Type pIXType, bool pIsUnique) {
			name = pName;

			//-- create storage
			db = StorageFactory.Instance.CreateStorage();
			db.SetProperty("perst.alternative.btree", true);
			db.Open(pFilePath, Storage.INFINITE_PAGE_POOL);

			//-- create and set root
			root = new Root	{ PKey = db.CreateIndex(pIXType, pIsUnique) };
			db.Root = root;

			//-- Set indexes
			pk = root.PKey;
			ak = null;
		}


		//-- imdb storage has PrimaryKey, or Index consisting of one field
		protected RepositoryBase(string pName, Type pIXType, bool pIsUnique) {
			name = pName;

			//-- create storage
			db = StorageFactory.Instance.CreateStorage();
			db.SetProperty("perst.alternative.btree", true);
			db.Open(new NullFile(), Storage.INFINITE_PAGE_POOL);

			//-- create and set root
			root = new Root { PKey = db.CreateIndex(pIXType, pIsUnique) };
			db.Root = root;

			//-- Set indexes
			pk = root.PKey;
			ak = null;
		}

		//-- imdb storage has only multifield PrimaryKey/Index
		protected RepositoryBase(string pName, Type pRecordType, string[] pPKFieldNames, bool pIsUnique) {
			name = pName;

			//-- create storage
			db = StorageFactory.Instance.CreateStorage();
			db.SetProperty("perst.alternative.btree", true);
			db.Open(new NullFile(), 0);

			root = (Root) db.Root;
			if (root == null) {
				root = new Root {MKey = db.CreateFieldIndex(pRecordType, pPKFieldNames, pIsUnique)};
				db.Root = root;
			}
			mk = (Index) root.MKey;
		}

		public int Count { get { return pk.Count; } }

		public void RemoveAll() {
			lock (padlock) {
				if (pk != null) {
					pk.Clear();
					pk.Reset();
					pk.Deallocate();
				}
				if (ak != null) {
					ak.Clear();
					ak.Reset();
					ak.Deallocate();
				}
				if (mk != null) {
					mk.Clear();
					mk.Reset();
					mk.Deallocate();
				}
			}
		}

		protected static int createComboKey(short pPkField1, short pPkField2) {
			var _combo = string.Format("{0}{1}", pPkField1, pPkField2);
			return _combo.GetHashCode();
		}

		protected static int createComboKey(int pPkField1, short pPkField2) {
			var _combo = string.Format("{0}{1}", pPkField1, pPkField2);
			return _combo.GetHashCode();
		}

		protected static int createComboKey(short pPkField1, int pPkField2) {
			var _combo = string.Format("{0}{1}", pPkField1, pPkField2);
			return _combo.GetHashCode();
		}

		protected static int createComboKey(short pPkField1, long pPkField2) {
			var _combo = string.Format("{0}{1}", pPkField1, pPkField2);
			return _combo.GetHashCode();
		}

		protected static int createComboKey(short pPkField1, string pPkField2) {
			var _combo = string.Format("{0}{1}", pPkField1, pPkField2);
			return _combo.GetHashCode();
		}

		protected static int createComboKey(int pPkField1, int pPkField2) {
			var _combo = string.Format("{0}{1}", pPkField1, pPkField2);
			return _combo.GetHashCode();
		}

		#region IDisposable Members

		public void Dispose() {
			//T.LogRbr(LogSeverity.Debug, "RepositoryBase.Dispose", string.Format("Disposing Repository, Name: {0}", name));
			//TODO: check if this works for CdrAggregate, if not override
			root.Deallocate();
		}

		#endregion
	}

	public class Persistable : Persistent {
		//-- Mendatory default constructor and required override:
		public override bool RecursiveLoading() {
			return false;
		}
	}

	internal class Root : Persistable {
		public Index AKey;
		public FieldIndex MKey;
		public Index PKey;

		public Root() {
			PKey = null;
			AKey = null;
			MKey = null;
		}
	}
}