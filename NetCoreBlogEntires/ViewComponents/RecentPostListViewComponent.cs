using Microsoft.AspNetCore.Mvc;
using NetCoreBlogEntires.Data.Repositories;
using NetCoreBlogEntires.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.ViewComponents
{
    public class RecentPostListViewComponent: ViewComponent
    {

        private readonly IPostRepository _postRepository;

        public RecentPostListViewComponent(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = _postRepository
                .Where(x=> x.IsActive)
                 .OrderByDescending(x=> x.PublishDate)
                 .Take(3)
                .Select(a => new RecentPostViewModel
                {

                    PostId = a.Id,
                    Title = a.Title,
                    PublishDate = a.PublishDate.ToShortDateString()
                })
                .ToList();


            return View(await Task.FromResult(model));

        }
    }
}
