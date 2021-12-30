using Microsoft.AspNetCore.Mvc;
using NetCoreBlogEntires.Data.Repositories;
using NetCoreBlogEntires.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.ViewComponents
{
    public class PostItemViewComponent: ViewComponent
    {
        //private readonly IPostRepository _postRepository;

        //public PostItemViewComponent(IPostRepository postRepository)
        //{
        //    _postRepository = postRepository;
        //}


        public async Task<IViewComponentResult> InvokeAsync(int currentPage, PostItemViewModel model)
        {

           // var posts = _postRepository.GetPagedPosts(currentPage);

           //var model =  posts.Select(a => new PostItemViewModel
           // {
           //     Id = a.Id,
           //     AuthorName = a.AuthorName,
           //     CategoryName = a.Category.Name,
           //     ShortContent = a.ShortContent,
           //     Title = a.Title,
           //     CommentCount = a.Comments.Count(),
           //     PublishDate = a.PublishDate.ToShortDateString(),
           //     Tags = a.Tags.Select(y => y.Name).ToArray()
           // }).ToList();


            return View(await Task.FromResult(model));

        }

        
    }
}
