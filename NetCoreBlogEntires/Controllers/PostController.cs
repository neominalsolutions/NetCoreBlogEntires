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


      

            Expression<Func<Post, bool>> queryFilter = x => true;
          

            if (!string.IsNullOrEmpty(searchText))
            {
                queryFilter = x => EF.Functions.Like(x.Title, $"%{searchText}%") || EF.Functions.Like(x.ShortContent, $"%{searchText}%");
            }

            if(!string.IsNullOrEmpty(categoryId))
            {
                queryFilter = x => x.CategoryId == categoryId;
            }

            if(!string.IsNullOrEmpty(tagName))
            {
                queryFilter = x => x.Tags.Select(a=> a.Name).Contains(tagName);
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
    }
}
