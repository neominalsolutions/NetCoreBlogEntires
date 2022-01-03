using NetCoreBlogEntires.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Data.Repositories
{
    public interface IPostCommentCountRepo: IRepository<Post>
    {
        int GetTotalCommentsCount(string postId);
    }

    public interface IPostRepository: IRepository<Post>, IPostCommentCountRepo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPage">Kaçıncı sayfada olduğumuz</param>
        /// <param name="limit">Limit kaç adet kayıt göstereceğimiziş belirtiğimiz parametre</param>
        /// <param name=""></param>
        /// <returns></returns>
        IQueryable<Post> GetPagedPosts(Expression<Func<Post, bool>> filter, int currentPage = 1, int limit = 10);
        int GetTotalPageNumber(Expression<Func<Post, bool>> filter,int limit);
    }
}
