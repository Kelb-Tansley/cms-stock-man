using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CMS.Systems.StockManagement.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        Task AddAsync(T entity);
        void Remove(T entity);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, bool asNoTracking = false);
        Task<IList<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<List<T>> GetAllAsync(bool shouldTrackChanges = true);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entity);
    }
}
