using System;
using System.Linq;

namespace Xero.Shop.Models
{
	public interface IRepository<T> where T : class
	{
		T Get(Guid id);
		IQueryable<T> GetEntities();
		void AddAsync(T entity);
		void Update(T entity);
		void Delete(T entity);
		void BeginTransaction();
		void Commit();
		void RollBack();
	}
}
