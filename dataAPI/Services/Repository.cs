using dataAPI.Contracts;
using dataAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dataAPI.Services
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly DB _Db;
        private readonly DbSet<T> _DbSet;

        public Repository(DB dbcontext)
        {
            _Db = dbcontext;
            _DbSet = _Db.Set<T>();
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> getAll(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Single(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
