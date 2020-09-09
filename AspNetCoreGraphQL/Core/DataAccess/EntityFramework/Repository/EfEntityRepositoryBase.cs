using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.Core.DataAccess.EntityFramework.Repository
{
    public class EfEntityRepositoryBase<T, TContext> : IEntityRepository<T, TContext>, IDisposable where T : class
       where TContext : DbContext, new()
    {
        protected DbContext _entities;
        protected readonly DbSet<T> _dbset;

        public EfEntityRepositoryBase()
        {
            var context = new TContext();
            _entities = context;
            _dbset = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbset.AsNoTracking().AsQueryable();
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbset.AsNoTracking().ToListAsync();
        }

        public virtual T Get(int id)
        {
            return _dbset.Find(id);
        }

        public virtual async Task<T> GetAsync(long id)
        {
            return await _dbset.FindAsync(id);
        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _dbset.FirstOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _dbset.AsNoTracking().FirstOrDefaultAsync(match);
        }

        public IList<T> FindToList(Expression<Func<T, bool>> match)
        {
            return _dbset.AsNoTracking().Where(match).ToList();
        }

        public async Task<IList<T>> FindToListAsync(Expression<Func<T, bool>> match)
        {
            return await _dbset.AsNoTracking().Where(match).ToListAsync();
        }

        public virtual T FindFirst(Expression<Func<T, bool>> predicate)
        {
            T query = _dbset.AsNoTracking().Where(predicate).FirstOrDefault();
            return query;
        }

        public virtual async Task<T> FindFirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbset.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbset.AsNoTracking().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbset.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual bool SaveCommit()
        {
            bool result = false;
            using (var transaction = _entities.Database.BeginTransaction())
            {
                try
                {
                    _entities.SaveChanges();
                    transaction.Commit();
                    result = true;
                }
                catch
                {
                    transaction.Rollback();
                }
            }
            return result;
        }

        public async virtual Task<bool> SaveCommitAsync()
        {
            bool result = false;
            using (var transaction = _entities.Database.BeginTransaction())
            {
                try
                {
                    await _entities.SaveChangesAsync();
                    transaction.Commit();
                    result = true;
                }
                catch
                {
                    transaction.Rollback();
                }
            }
            return result;
        }

        public virtual T Insert(T t)
        {
            _dbset.Add(t);
            return t;
        }

        public virtual async Task<T> InsertAsync(T t)
        {
            await _dbset.AddAsync(t);
            return t;
        }

        public virtual void InsertRange(IList<T> t)
        {
            _entities.AddRange(t);
        }

        public virtual async Task InsertRangeAsync(T t)
        {
            await _entities.AddRangeAsync(t);
        }

        public virtual T InsertState(T t)
        {
            var addedEntity = _entities.Entry(t);
            addedEntity.State = EntityState.Added;
            return t;
        }

        public virtual T UpdateState(T t)
        {
            _entities.Attach(t);
            _entities.Entry(t).State = EntityState.Modified;
            return t;
        }

        public virtual void UpdateRange(IList<T> t)
        {
            _entities.UpdateRange(t);
        }

        public virtual T UpdateFind(T t, object key)
        {
            if (t == null) { return null; }
            T result = _dbset.Find(key);
            if (result != null)
            { _entities.Entry(result).CurrentValues.SetValues(t); }
            return result;
        }

        public virtual void Delete(T t)
        {
            _dbset.Attach(t);
            _dbset.Remove(t);
        }

        public virtual void DeleteFind(long id)
        {
            var deleteEntity = _dbset.Find(id);
            _dbset.Remove(deleteEntity);
        }

        public virtual void DeleteState(T t)
        {
            var deleteEntity = _entities.Entry(t);
            deleteEntity.State = EntityState.Deleted;
        }

        public virtual void DeleteGet(Expression<Func<T, bool>> filter)
        {
            var rs = _dbset.AsNoTracking().Single(filter);
            var deleteEntity = _entities.Entry(rs);
            deleteEntity.State = EntityState.Deleted;
        }

        public virtual void DeleteRange(IList<T> entity)
        {
            _entities.RemoveRange(entity);
        }

        public long Count()
        {
            return _dbset.AsNoTracking().Count();
        }

        public async Task<long> CountAsync()
        {
            return await _dbset.AsNoTracking().CountAsync();
        }

        public bool Any(Expression<Func<T, bool>> filter = null)
        {
            return _dbset.Any(filter);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null)
        {
            return await _dbset.AnyAsync(filter);
        }

        public virtual async Task<IEnumerable<T>> IncludeEnumerable(Expression<Func<T, object>> includeProperties)
        {
            return await _dbset.AsNoTracking().Include(includeProperties).ToListAsync();
        }

        public IQueryable<T> Including(Expression<Func<T, object>> includeProperties)
        {
            return _dbset.AsNoTracking().Include(includeProperties);
        }

        public IQueryable<T> IncludingAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = null;
            foreach (var include in includeProperties)
            {
                query = _dbset.Include(include);
            }
            return query;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _entities.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EfEntityRepositoryBase()
        {
            Dispose(false);
        }

    }
}
