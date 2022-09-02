using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dataAPI.Contracts
{
    public interface IRepository<T>
    {
        T Create();
        Task<List<T>> getAll(Expression<Func<T, bool>> predicate);

        Task<T> Single(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetByIdAsync(object id);
        void Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
