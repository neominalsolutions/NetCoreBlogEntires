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
 
        public async Task<IViewComponentResult> InvokeAsync(PostItemViewModel model)
        {

            return View(await Task.FromResult(model));

        }

        
    }
}
