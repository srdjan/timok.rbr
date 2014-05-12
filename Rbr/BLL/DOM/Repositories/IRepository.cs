namespace Timok.Rbr.BLL.DOM.Repositories {
	public interface IRepository {
		void Put(object pObject);

		object GetByPK(string pPk);

		void Dispose();
	}
}