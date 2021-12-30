using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Data.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> Where(Expression<Func<T, bool>> lamda = null);
        List<T> List();
        long Count();
        T Find(string Id);

        void Add(T item);

        void Update(T item);

        void Delete(string Id);

        void Save();
    }
}
