using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCoreBlogEntires.Data.Contexts;
using NetCoreBlogEntires.Data.Models;
using NetCoreBlogEntires.Data.Repositories;
using NetCoreBlogEntires.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICategoryRepository _categoryRepository;


        public HomeController(ILogger<HomeController> logger, IPostRepository postRepository, ITagRepository tagRepository, ICategoryRepository categoryRepository)
        {

            _logger = logger;
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index(int currentPage)
        {

            // en çok yorum girilen 5 adet 5 post çektik.
            // daha sonrası approved olan yanı kullanıcya gösterilmesiş onaylanan makalelere göre filtreleme yapıcaz.
            // where asQuearable olduğu için veritabanından sadece 5 adet kayıt seçildi.
            // 2 farklı sıralama yapmak istersek ikinci alan için thenBy kullanırız.
            var posts = _postRepository
                .Where(null)
                .Include(x=> x.Category)
                .Include(x=> x.Comments)
                .Include(x=> x.Tags)
                .OrderByDescending(x => x.Comments.Count())
                .ThenBy(x=> x.PublishDate)
                .Take(5)
                .ToList();

            var postItems = posts.Select(a => new PostItemViewModel
            {
                Id = a.Id,
                AuthorName = a.AuthorName,
                CategoryName = a.Category.Name,
                ShortContent = a.ShortContent,
                Title = a.Title,
                CommentCount = a.Comments.Count(),
                PublishDate = a.PublishDate.ToShortDateString(),
                Tags = a.Tags.Select(y => y.Name).ToArray()
            }).ToList();


            // categoryLastPostView Model

            var categoryLastPosts = _categoryRepository.List();
            var categoryLastPostModels = new List<CategoryLastPostViewModel>();
            categoryLastPosts.ForEach(item =>
            {
                var lastPostQuery = _postRepository.Where(x => x.CategoryId == item.Id).Include(x => x.Comments);

                var categorypostCount = lastPostQuery.Count();
                var lastPostByCategory = lastPostQuery.OrderByDescending(x => x.PublishDate).Take(1).FirstOrDefault();

                var model = new CategoryLastPostViewModel
                {
                    LastPostAuthorName = lastPostByCategory.AuthorName,
                    LastPostDate = lastPostByCategory.PublishDate.ToShortDateString(),
                    CategoryPostCount = categorypostCount,
                    LastPostId = lastPostByCategory.Id,
                    LastPostTitle = lastPostByCategory.Title,
                    Id = item.Id,
                    LastPostCommentCount = lastPostByCategory.Comments.Count(),
                    Name = item.Name
                };

                categoryLastPostModels.Add(model);

            });


            var model = new HomeViewModel
            {
                CategoryLastPosts = null,
                PostItems = postItems
            };


            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
