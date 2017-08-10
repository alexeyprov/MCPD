using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SimplisticEF.Models;

namespace SimplisticEF.Services
{
    public class Repository<T> : IDisposable
        where T : class
    {
        private readonly MusicContext _context;
        private bool _disposed;

        public Repository()
        {
            _context = new MusicContext();
            DbSet = _context.Set<T>();
        }

        public Repository(MusicContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        protected DbSet<T> DbSet
        {
            get;
            set;
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToArray();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToArrayAsync();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public Task<T> GetAsync(int id)
        {
            return DbSet.FindAsync(id);
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            T entity = DbSet.Find(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
            }
        }

        public virtual async Task DeleteAsync(int id)
        {
            T entity = await DbSet.FindAsync(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
            }
        }

        #endregion
    }
}