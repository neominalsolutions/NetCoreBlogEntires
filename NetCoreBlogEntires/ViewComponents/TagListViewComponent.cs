using Microsoft.AspNetCore.Mvc;
using NetCoreBlogEntires.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.ViewComponents
{
    public class TagListViewComponent: ViewComponent
    {
        private readonly ITagRepository _tagRepository;

        public TagListViewComponent(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = _tagRepository.List().Select(a => a.Name).ToList();


            return View(await Task.FromResult(model));

        }
    }
}
