using Microsoft.EntityFrameworkCore;
using NetCoreBlogEntires.Data.Contexts;
using NetCoreBlogEntires.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Data.Repositories
{
    public class PostRepository : EFCoreGenericRepository<AppDbContext, Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }

        // Post repositoryde find polimorfik olarak davranmalı normal find işimize yaramıyor. Include Joinli çekmek zorundayız.
        public override Post Find(string Id)
        {
           return _dbSet.Include(x => x.Tags).Include(x => x.Comments).Include(x => x.Category).FirstOrDefault(x => x.Id == Id);
 
        }


        public override List<Post> List()
        {
            return _dbSet.Include(x => x.Comments).Include(x => x.Category).ToList();
        }

        public List<Post> GetPagedPosts(int currentPage = 1, int limit = 10)
        {

            if(currentPage < 1)
            {
                throw new Exception("CurrentPage en düşük 1 olarak tanımlanabilir");
            }

            return _dbSet.Include(x => x.Tags).Include(x => x.Comments).Include(x => x.Category).Skip((limit * (currentPage - 1))).Take(limit).ToList();

        }

        public int GetTotalPageNumber(int limit)
        {
            var totalCount = _dbSet.Count();
            // Math Celing ile sayfa sayısını yukarı yuvarladık.
            return (int)Math.Ceiling((decimal)totalCount / (decimal)limit); 
        }
    }
}
