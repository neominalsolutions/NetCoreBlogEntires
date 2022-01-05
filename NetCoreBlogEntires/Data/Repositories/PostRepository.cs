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
            // aşağıdaki örnekte bir entity içerisindeki koleksiyondan yorum tarihine göre ilk 5 tane kayıtı sadece veri tabanından çektik.
           return _dbSet
                .Include(x => x.Tags)
                .Include(x => 
                x.Comments
                .OrderByDescending(y=> y.PublishDate)
                .Take(5))
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == Id);
 
        }


        /// <summary>
        /// Admin panelinde tüm makaleleri görüp isActive olarak ayarlamayız ki son kullanıcı görsün
        /// </summary>
        /// <returns></returns>
        public override List<Post> List()
        {
            return _dbSet.Include(x => x.Comments).Include(x => x.Category).ToList();
        }

        /// <summary>
        /// Gönderilen filtreye göre sayfalama yapar, filtere değerleri kategorisine göre, shortcontent title göre ve tagname göre.
        /// Son kullanıcının sayfalı olarak gördüğü makaleler bu yüzden isactive olanları göstermeliyiz.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="currentPage"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IQueryable<Post> GetPagedPosts(Expression<Func<Post,bool>> filter, int currentPage = 1, int limit = 10)
        {

            if(currentPage < 1)
            {
                throw new Exception("CurrentPage en düşük 1 olarak tanımlanabilir");
            }

            return _dbSet.Where(filter).Include(x => x.Tags).Include(x => x.Comments).Include(x => x.Category).Skip((limit * (currentPage - 1))).Take(limit).AsQueryable();

        }

        /// <summary>
        /// Aktif olan tüm makalelerin sayısını getirir.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int GetTotalPageNumber(Expression<Func<Post, bool>> filter,int limit)
        {
            var totalCount = _dbSet.Where(filter).Count();
            // Math Celing ile sayfa sayısını yukarı yuvarladık.
            return (int)Math.Ceiling((decimal)totalCount / (decimal)limit); 
        }

        /// <summary>
        /// Aktif olan makalelerin comment sayısını getirmek için kullandık
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public int GetTotalCommentsCount(string postId)
        {
            // bir entity içerisine Include ile alt entitylere bağlandığımızda o entity'in alt kolleksiyonlarının hepsi rame çekilir.
            return _context.Posts
                .Include(x => x.Comments)
                .FirstOrDefault(x => x.Id == postId && x.IsActive)
                .Comments
                .Count();
        }
    }
}
