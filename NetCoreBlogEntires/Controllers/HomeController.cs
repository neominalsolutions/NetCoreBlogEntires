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

        public HomeController(ILogger<HomeController> logger, IPostRepository postRepository, ITagRepository tagRepository)
        {

            _logger = logger;
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            //var post = new Post("title-17", "content-17", "content-17", "0b900082-f18d-456c-a948-d55598a83d39", "Berkay");

            //var existingTag = _tagRepository.Find("90a5fe15-676f-4383-ae7c-678975febc2r");

            //post.AddTag(new Tag { Name = "sfdsafdsf" }, _tagRepository);
            //post.AddTag(existingTag, _tagRepository);

            //_postRepository.Add(post);
            //_postRepository.Save();

           //var data =  _postRepository.GetPagedPosts(2, 2);
           // var pageCount = _postRepository.GetTotalPageNumber(7);
            return View();
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
