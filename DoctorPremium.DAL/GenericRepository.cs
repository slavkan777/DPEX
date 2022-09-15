using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Interfaces;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.DAL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected DoctorPremiumEntities DbContext;
        private DbSet<TEntity> _entitySet;
        public GenericRepository(IUnitOfWork unitOfWork)
        {

            this.DbContext = unitOfWork.DbContext;
            if (DbContext == null)
            {
                throw new ArgumentNullException("DoctorPremiumEntities");
            }
            else
            {
                _entitySet = DbContext.Set<TEntity>();
            }
        }

        public TEntity GetById(object id)
        {
            return _entitySet.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = _entitySet;
            return query;
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _entitySet.Where(predicate);
            return query;
        }

        public TEntity Add(TEntity entity)
        {
            TEntity item = _entitySet.Add(entity);
            SaveChanges();
            return item;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _entitySet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entity)
        {
            _entitySet.Remove(entity);
            SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _entitySet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return DbContext.Database.SqlQuery<TEntity>(query, parameters);
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}
