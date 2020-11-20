using GroupClass.Core.Interfaces;
using GroupClass.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GroupClass.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        protected AppDbContext appDbContext { get; set; }

        public RepositoryBase(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
        public virtual async Task<T> Create(T entity)
        {
            await appDbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual void Delete(T entity)
        {
            appDbContext.Set<T>().Remove(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = appDbContext.Set<T>().AsNoTracking().Where(p => p.Id == id).FirstOrDefault();
            appDbContext.Set<T>().Remove(entity);
        }

        public virtual async Task<T> Find(int id)
        {
            return await appDbContext.Set<T>().AsNoTracking().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public virtual IQueryable<T> FindAll()
        {
            return appDbContext.Set<T>().AsNoTracking();
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return  appDbContext.Set<T>().Where(expression);
        }
        public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate, string[] subModels)
        {
            IQueryable<T> entity = appDbContext.Set<T>();
            foreach (string model in subModels)
            {

                entity = entity.Include(model);



            }
            return entity.Where(predicate);
        }
        public virtual void Update(T entity)
        {
            appDbContext.Set<T>().Update(entity);
        }
    }
}
