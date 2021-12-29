using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreBlogEntires.Areas.Admin.Models;
using NetCoreBlogEntires.Data.Repositories;
using NetCoreBlogEntires.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostService _postService;

        private List<SelectListItem> CategoryDrp 
        {
           get
            {
                return _categoryRepository.List().OrderBy(x=> x.Name).Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id
                }).ToList();
            }
        }

        public PostController(ICategoryRepository categoryRepository, IPostService postService)
        {
            _categoryRepository = categoryRepository;
            _postService = postService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = CategoryDrp;

            return View();
        }

        [HttpPost]
        public IActionResult Create(PostInputModel model)
        {
            ViewBag.Categories = CategoryDrp;

            if(ModelState.IsValid)
            {
                _postService.Save(model);
                ViewBag.Message = "Makale Kayıt işlemi başarılıdır";
            }
          
            return View();
        }


    }
}
