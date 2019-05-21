using System;
using System.Collections.Generic;
using System.Linq;

namespace LabStore.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(Func<TEntity, bool> predicate);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        IQueryable<TEntity> GetAllQuery();
        IEnumerable<TEntity> GetAll();
        bool CheckIfExist(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> GetSelected(Func<TEntity, bool> predicate = null);
        void RemoveRange(IEnumerable<TEntity> entity);
    }
}