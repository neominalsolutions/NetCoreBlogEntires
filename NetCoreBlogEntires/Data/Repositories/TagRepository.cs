using NetCoreBlogEntires.Data.Contexts;
using NetCoreBlogEntires.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Data.Repositories
{
    public class TagRepository : EFCoreGenericRepository<AppDbContext, Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context)
        {
        }

        public Tag FindByName(string tagName)
        {
            // firstOrDefault bulamaz ise null döndürür.
            return _dbSet.FirstOrDefault(x => x.Name == tagName);
        }
    }
}
