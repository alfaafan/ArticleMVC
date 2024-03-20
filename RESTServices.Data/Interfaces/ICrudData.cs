using System.Collections.Generic;

namespace RESTServices.Data.Interfaces
{
	public interface ICrudData<T>
	{
		Task<IEnumerable<T>> GetAll();
		Task<T> GetById(int id);
		Task<T> Insert(T entity);
		Task<T> Update(T entity);
		Task<bool> Delete(int id);
	}
}
