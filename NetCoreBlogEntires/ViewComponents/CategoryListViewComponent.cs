using Microsoft.AspNetCore.Mvc;
using NetCoreBlogEntires.Data.Repositories;
using NetCoreBlogEntires.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.ViewComponents
{
    public class CategoryListViewComponent: ViewComponent
    {


        private readonly ICategoryRepository _categoryRepository;

        public CategoryListViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = _categoryRepository
                .List()
                .Select(a=> new CategoryViewModel { 
            
                Id = a.Id,
                CategoryName = a.Name
            })
                .ToList();


            return View(await Task.FromResult(model));

        }
    }
}
