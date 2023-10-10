using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestTask.Data.Entities;

namespace TestTask.DataAccess.Abstractions
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Get();
        Task<T> GetById(Guid id);
        Task Update(T entity);
        Task Delete(Guid id);
        Task Create(T entity);
        Task<IQueryable<T>> FindBy(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);
    }
}
