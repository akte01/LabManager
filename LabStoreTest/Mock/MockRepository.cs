using LabStore.Models;
using LabStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabStoreTest
{
    public class MockRepository<T> : IRepository<T> where T: class
    {
        public List<T> _context;

        public MockRepository(List<T> ctx)
        {
            _context = ctx;
        }

        public virtual void Add(T entity)
        {
            _context.Add(entity);
        }

        public virtual T Get(Func<T, bool> predicate)
        {
            return _context.SingleOrDefault(predicate);
        }

        public virtual IQueryable<T> GetAllQuery()
        {
            return _context.AsQueryable();
        }

        public virtual void Remove(T entity)
        {
            _context.Remove(entity);
        }

        public virtual bool CheckIfExist(Func<T, bool> predicate)
        {
            return _context.Any(predicate);
        }

        public virtual IEnumerable<T> GetSelected(Func<T, bool> predicate = null)
        {
            return _context.Where(predicate).ToList();
        }

        public virtual void RemoveRange(IEnumerable<T> entity)
        {
            _context.RemoveAll(x => entity.Contains(x));
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _context.ToList();
        }
    }
}
