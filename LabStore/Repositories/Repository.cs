using System;
using System.Collections.Generic;
using System.Linq;

namespace LabStore.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly EFContext _context;

        public Repository(EFContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public TEntity Get(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAllQuery()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public bool CheckIfExist(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }

        public IEnumerable<TEntity> GetSelected(Func<TEntity, bool> predicate = null)
        {
            return _context.Set<TEntity>().Where(predicate).ToList();
        }

        public void RemoveRange(IEnumerable<TEntity> entity)
        {
            _context.Set<TEntity>().RemoveRange(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
    }
}