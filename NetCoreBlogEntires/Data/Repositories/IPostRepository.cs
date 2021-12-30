using NetCoreBlogEntires.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Data.Repositories
{
    public interface IPostRepository: IRepository<Post>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPage">Kaçıncı sayfada olduğumuz</param>
        /// <param name="limit">Limit kaç adet kayıt göstereceğimiziş belirtiğimiz parametre</param>
        /// <param name=""></param>
        /// <returns></returns>
        List<Post> GetPagedPosts(int currentPage = 1, int limit = 10);
        int GetTotalPageNumber(int limit);
    }
}
