using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreBlogEntires.Data.Models;
using NetCoreBlogEntires.Data.Repositories;
using NetCoreBlogEntires.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Controllers
{
    public class PostController : Controller
    {

        private readonly IPostRepository _postRepository;


        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public IActionResult List(string searchText, string categoryId, string tagName, int currentPage = 1)
        {

            // sayfada ne kadar filtereleme varsa bu filtreler querystring üzerinden yakalanıp, viewbag olarak sayfaya geri gönderilir. ve sayfalama butonu bu son filtre değerlerine göre düzgün sayfalama yapabilir.

            ViewBag.CurrentPage = currentPage;
            ViewBag.SearchText = searchText;
            ViewBag.TagName = tagName;
            ViewBag.CategoryId = categoryId;


      

            Expression<Func<Post, bool>> queryFilter = x => x.IsActive;
          

            if (!string.IsNullOrEmpty(searchText))
            {
                queryFilter = x => EF.Functions.Like(x.Title, $"%{searchText}%") || EF.Functions.Like(x.ShortContent, $"%{searchText}%") && x.IsActive;
            }

            if(!string.IsNullOrEmpty(categoryId))
            {
                queryFilter = x => x.CategoryId == categoryId && x.IsActive;
            }

            if(!string.IsNullOrEmpty(tagName))
            {
                queryFilter = x => x.Tags.Select(a=> a.Name).Contains(tagName) && x.IsActive;
            }



            var model = new PostListViewModel
            {

                PostItems = _postRepository.GetPagedPosts(queryFilter, currentPage,4).Select(a => new PostItemViewModel
                {
                    AuthorName = a.AuthorName,
                    CategoryId = a.CategoryId,
                    CategoryName = a.Category.Name,
                    CommentCount = a.Comments.Count(),
                    Tags = a.Tags.Select(a => a.Name).ToArray(),
                    Id = a.Id,
                    PublishDate = a.PublishDate.ToShortDateString(),
                    ShortContent = a.ShortContent,
                    Title = a.Title
   
                }).ToList(),
                PageCount = _postRepository.GetTotalPageNumber(queryFilter,4)

            };


            return View(model);
        }


        public IActionResult Detail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }


           var post =  _postRepository.Find(id);

            var model = new PostDetailViewModel
            {
                CategoryName = post.Category.Name,
                AuthorName = post.AuthorName,
                Content = post.Content,
                PostId = post.Id,
                PublishDate = post.PublishDate.ToShortDateString(),
                TagNames = post.Tags.Select(x => x.Name).ToArray(),
                Title = post.Title

            };

            // EF çekilen sorguya göre post üzerinden comment bilgisini ramde tutuğundan dolayı aşağıdaki sorgunun çalışmasından sonra comment sayısı değişecektir. yukarıdaki var post nesnesinin referansı değieceğei için ekranda 5 adet comment ile tüm comment sayılarını doğru bir şekilde göstermek için sıralı bir şekilde işlem yapmamız gerekti.
            model.Comments = post.Comments.Select(a => new CommentViewModel
            {
                CommentBy = a.CommentBy,
                PublishDate = a.PublishDate.ToShortDateString(),
                Text = a.Text
            }).ToList();

            model.CommentCount = _postRepository.GetTotalCommentsCount(id);

            


          

            return View(model);

        }


        [HttpPost]
        public JsonResult SendComment([FromBody] PostCommentInputModel model)
        {


            var post = _postRepository.Find(model.PostId);
            var comment = new Comment(commentBy: model.CommentBy, text: model.Text);
            post.AddComment(comment);
            _postRepository.Save();

            model.PublishDate = comment.PublishDate.ToShortDateString();

            return Json(model);
        }
    }
}
