using EntitiesLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace RepositoryLibrary
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected EntitiesContext EntitiesContext;

        public RepositoryBase(EntitiesContext entitiesContext)
        {
            EntitiesContext = entitiesContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges
                ? EntitiesContext.Set<T>().AsNoTracking()
                : EntitiesContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges
                ? EntitiesContext.Set<T>().Where(expression).AsNoTracking()
                : EntitiesContext.Set<T>().Where(expression);

        public void Create(T entity) => EntitiesContext.Set<T>().Add(entity);
        public void Update(T entity) => EntitiesContext.Set<T>().Update(entity);
        public void Delete(T entity) => EntitiesContext.Set<T>().Remove(entity);
    }
}
