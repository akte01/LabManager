using LabStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabStore.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : EFContext, new()
    {
        private readonly EFContext _context;
        private Dictionary<Type, object> _repositories;
        private bool _disposed;

        public UnitOfWork()
        {
            _context = new TContext();
            _repositories = new Dictionary<Type, object>();
            _disposed = false;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.Keys.Contains(typeof(T)))
            {
                return _repositories[typeof(T)] as IRepository<T>;
            }

            IRepository<T> repository = new Repository<T>(_context);
            _repositories.Add(typeof(T), repository);

            return repository;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _context.Dispose();

                _disposed = true;
            }
        }
    }
    
    public class EFContext : ApplicationDbContext
    {

    }
}
  