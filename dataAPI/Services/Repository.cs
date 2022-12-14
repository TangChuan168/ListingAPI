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


        public T Create()
        {
            var entity = new T();
            return entity;
        }

        public Task Delete(T entity)
        {
            //await _DbSet.DeleteOneAsync(w => w.Id.Equals(entity.Id));
            throw new NotImplementedException();
        }

        public IEnumerable<T> getAll(Expression<Func<T, bool>> predicate)
        {
            return _DbSet.AsQueryable().Where(predicate).AsEnumerable();
                
        }

        public Task<List<T>> GetByIdAsync(object id)
        {
            //return  _DbSet.Find(x => x. == id.ToString()).FirstOrDefault();
            throw new NotImplementedException();
        }

        public Task<T> Single(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                //await _DbSet.ReplaceOneAsync(w => w.Id.Equals(entity.Id),
                    //entity, new UpdateOptions { IsUpsert = true });
            }
            catch (Exception dbEx)
            {
                throw dbEx;
            }
        }

        public async Task Add(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity is null");
                }

                await _DbSet.AddAsync(entity);
                _Db.SaveChanges();

            }
            catch (Exception)
            {
                throw new ArgumentNullException("exception db"); ;
            }
        }
    }
}
