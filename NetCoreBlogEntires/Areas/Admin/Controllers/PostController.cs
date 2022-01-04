﻿using Microsoft.AspNetCore.Authorization;
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
    [Area("Admin")][Authorize]
    public class PostController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostService _postService;
        private readonly IPostRepository _postRepository;


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

        public PostController(ICategoryRepository categoryRepository, IPostService postService, IPostRepository postRepository)
        {
            _categoryRepository = categoryRepository;
            _postService = postService;
            _postRepository = postRepository;
        }
        public IActionResult Index()
        {
            var model = _postRepository.List().Select(a=> new PostViewModel
            {
                Title = a.Title,
                Id = a.Id,
                CategoryName = a.Category.Name,
                AuthorName = a.AuthorName,
                CommentCount = a.Comments.Count(),
                PublishDate = a.PublishDate.ToShortDateString(),
                ShortContent = a.ShortContent
            }
            ).ToList();

            return View(model);
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


        public IActionResult Detail(string id)
        {
            var post = _postRepository.Find(id);
         

            if(post != null)
            {

                var model = new PostDetailViewModel
                {
                    AuthorName = post.AuthorName,
                    CategoryName = post.Category.Name,
                    HtmlContent = post.Content,
                    PublishDate = post.PublishDate.ToShortDateString(),
                    Tags = post.Tags.Select(x => x.Name).ToList(),
                    Title = post.Title
                };

                return View(model);
            }


            return NotFound();
        }

    }
}
