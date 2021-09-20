using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xero.Shop.Models;

namespace Xero.Shop.Repositories
{
	public abstract class BaseRepository<T> : IRepository<T> where T : class
	{
		private readonly ShopDbContext _dataContext;

		protected BaseRepository(ShopDbContext dataContext)
		{
			_dataContext = dataContext;
		}

		public void Delete(T entity)
		{
			_dataContext.Set<T>().Remove(entity);
			_dataContext.SaveChanges();
		}

		public T Get(Guid id)
		{
			return _dataContext.Set<T>().Find(id);
		}

		public void AddAsync(T entity)
		{
			_dataContext.Set<T>().Add(entity);
			 _dataContext.SaveChanges();
		}

		public IQueryable<T> GetEntities()
		{
			return _dataContext.Set<T>().AsQueryable();
		}

		public IQueryable<T> GetEntities(string sqlRawQuery)
		{
			return _dataContext.Set<T>().FromSqlRaw(sqlRawQuery).AsQueryable();
		}

		public void Update(T entity)
		{
			_dataContext.Set<T>().Update(entity);
			 _dataContext.SaveChanges();
		}

		public void BeginTransaction()
		{
			_dataContext.Database.BeginTransaction();
		}

		public void Commit()
		{
			_dataContext.Database.CommitTransaction();
		}

		public void RollBack()
		{
			_dataContext.Database.RollbackTransaction();
		}
	}
}
