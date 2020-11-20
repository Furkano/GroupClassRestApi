using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupClass.Core.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        Task<T> Find(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        IQueryable<T> Where(Expression<Func<T, bool>> predicate, string[] subModels);
        Task<T> Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
    }
}
