using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Common.Interfaces
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        public Task<IReadOnlyList<T>> GetAllAsync();

        public IQueryable<T> GetAllRelatedEntities(string includeProperties = "");


        public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>?predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeString = null, bool disableTracking = true);

        public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>,
            IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true);

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");

        public Task<T> GetByIdAsync(int id);

        public Task<T> AddAsync(T entity);


        public void Update(T entity);

        public void Delete(T entity);

    }
}
