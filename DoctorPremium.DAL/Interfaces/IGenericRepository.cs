using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPremium.DAL.Interfaces
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		TEntity GetById(object id);
		IQueryable<TEntity> GetAll();
		IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
		TEntity Add(TEntity entity);
		void Delete(object id);
		void Delete(TEntity entity);
		void Update(TEntity entity);
	    IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters);
	}
}
