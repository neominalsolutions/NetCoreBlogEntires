using NetCoreBlogEntires.Data.Contexts;
using NetCoreBlogEntires.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Data.Repositories
{
    public class CategoryRepository : EFCoreGenericRepository<AppDbContext, Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
