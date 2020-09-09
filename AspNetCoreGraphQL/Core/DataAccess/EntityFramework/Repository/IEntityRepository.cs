using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.Core.DataAccess.EntityFramework.Repository
{
    public interface IEntityRepository<T, TContext> where T : class
    {
        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        T Get(int id);
        Task<T> GetAsync(long id);
        T Find(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IList<T> FindToList(Expression<Func<T, bool>> match);
        Task<IList<T>> FindToListAsync(Expression<Func<T, bool>> match);
        T FindFirst(Expression<Func<T, bool>> predicate);
        Task<T> FindFirstAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        bool SaveCommit();
        Task<bool> SaveCommitAsync();
        T Insert(T t);
        Task<T> InsertAsync(T t);
        void InsertRange(IList<T> t);
        Task InsertRangeAsync(T t);
        T InsertState(T t);
        T UpdateState(T t);
        void UpdateRange(IList<T> t);
        T UpdateFind(T t, object key);
        void Delete(T t);
        void DeleteFind(long id);
        void DeleteState(T t);
        void DeleteGet(Expression<Func<T, bool>> filter);
        void DeleteRange(IList<T> entity);
        long Count();
        Task<long> CountAsync();
        bool Any(Expression<Func<T, bool>> filter = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null);
        IQueryable<T> Including(Expression<Func<T, object>> includeProperties);
        IQueryable<T> IncludingAll(params Expression<Func<T, object>>[] includeProperties);
    }
}
