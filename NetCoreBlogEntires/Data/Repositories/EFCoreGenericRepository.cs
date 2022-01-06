using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Data.Repositories
{
    // bu repository ile uygulama birden fazla DBContext ile uyum halinde çalışıp ilgili dbContext altındaki DbSetlerin repolarına erişmemiz sağlar.
    public class EFCoreGenericRepository<TContext, TEntity>:IRepository<TEntity>
        where TContext:DbContext
        where TEntity:class
    {
        protected TContext _context;
        protected DbSet<TEntity> _dbSet;

        public EFCoreGenericRepository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();

        }

        public void Add(TEntity item)
        {
            _dbSet.Add(item);
        }

        public virtual long Count()
        {
            return _dbSet.Count();
        }

        public virtual void Delete(string Id)
        {
            var entity = Find(Id);
            _dbSet.Remove(entity);
     
        }

        public virtual TEntity Find(string Id)
        {
            return _dbSet.Find(Id);
        }

        public virtual List<TEntity> List()
        {
            return _dbSet.ToList();
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Update(TEntity item)
        {
            _dbSet.Update(item);
        }

        IQueryable<TEntity> IRepository<TEntity>.Where(Expression<Func<TEntity, bool>> lamda = null)
        {
            if (lamda == null)
                return _dbSet.AsQueryable();
           

            return _dbSet.Where(lamda).AsQueryable();
        }
    }
}
