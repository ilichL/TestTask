using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestTask.Data;
using TestTask.Data.Entities;
using TestTask.DataAccess.Abstractions;

namespace TestTask.DataAccess.Implementations
{
    public class Repository<T>: IRepository<T> where T :  BaseEntity
    {
        public readonly Context Context;

        public Repository(Context context) 
        {
            Context = context;
        }

        public IQueryable<T> Get()
        {
            return Context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(Guid id)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(T entity)
        {
            Context.Set<T>().Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            if(entity is null)
            {
                return;
            }

            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Create(T entity) 
        {
            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task<IQueryable<T>> FindBy(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            var result = Context.Set<T>().Where(predicate);

            if (includes.Any())
                result = includes.Aggregate(result, (current, include) => current.Include(include));

            return result;
        }


    }
}
