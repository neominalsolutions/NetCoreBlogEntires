using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCoreBlogEntires.Data.Contexts;
using NetCoreBlogEntires.Data.Models;
using NetCoreBlogEntires.Data.Repositories;
using NetCoreBlogEntires.Models;
using NetCoreBlogEntires.Services;
using NetCoreBlogEntires.Validators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ContactInputModelValidator _contactInputModelValidator;
        private readonly IEmailService _emailService;


        public HomeController(ILogger<HomeController> logger, IPostRepository postRepository, ITagRepository tagRepository, ICategoryRepository categoryRepository, ContactInputModelValidator contactInputValidator, IEmailService emailService)
        {

            _logger = logger;
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _contactInputModelValidator = contactInputValidator;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Contact()
        {


            var post = _postRepository.Find("2705ea3b-cb82-4c5a-8cd5-5fca208a25cf");
            //post.AddComment(new Comment("me", "comment1"));

            var comment = post.Comments.FirstOrDefault(x => x.Id == "59466430-46bb-41c2-badc-3a9e1aba67b5");
            post.RemoveComment(comment);
            _postRepository.Save();


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactInputModel model)
        {

            //  https://www.google.com/recaptcha/admin#list
            // recaptche admin list

            var captchaImage = HttpContext.Request.Form["g-recaptcha-response"];

            if (string.IsNullOrEmpty(captchaImage))
            {
                ModelState.AddModelError("captcha", "Captcha doğrulanmamış");
                return View();
            }

            var verified = await CheckCaptcha();
            if (!verified)
            {
                ModelState.AddModelError("captcha", "Captcha yanlış doğrulanmış");
                return View();
            }


            // valid olup olmadığını Validate methodu ile kontrol ederiz.
            var result =  _contactInputModelValidator.Validate(model);

            if (result.IsValid)
            {
                await _emailService.SendEmailAsync("mert.alptekin@neominal.com", model.Email, model.Subject, $"<p>{model.MessageBody}</p>");

                ViewBag.Message = "Mesajınız iletildi.sizinle en kısa zamanda iletişime geçeceğiz.";

                // Mail gönder
            }

            return View();
        }


        private async Task<bool> CheckCaptcha()
        {
            var postData = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("secret", "6Ldj7vgdAAAAAIAQv1bzjel_jPHD0TtQOBB9XFBd"),
                new KeyValuePair<string, string>("remoteip", HttpContext.Connection.RemoteIpAddress.ToString()),
                new KeyValuePair<string, string>("response", HttpContext.Request.Form["g-recaptcha-response"])
            };

            var client = new HttpClient();
            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData));

            var o = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            return (bool)o["success"];
        }

        public IActionResult Index()
        {

            // en çok yorum girilen 5 adet 5 post çektik.
            // daha sonrası approved olan yanı kullanıcya gösterilmesiş onaylanan makalelere göre filtreleme yapıcaz.
            // where asQuearable olduğu için veritabanından sadece 5 adet kayıt seçildi.
            // 2 farklı sıralama yapmak istersek ikinci alan için thenBy kullanırız.
            var posts = _postRepository
                .Where(x=> x.IsActive)
                .Include(x=> x.Category)
                .Include(x=> x.Comments)
                .Include(x=> x.Tags)
                .OrderByDescending(x => x.Comments.Count())
                .ThenBy(x=> x.PublishDate)
                .Take(5)
                .ToList();

            var postItemsModel = posts.Select(a => new PostItemViewModel
            {
                Id = a.Id,
                CategoryId = a.CategoryId,
                AuthorName = a.AuthorName,
                CategoryName = a.Category.Name,
                ShortContent = a.ShortContent,
                Title = a.Title,
                CommentCount = a.Comments.Count(),
                PublishDate = a.PublishDate.ToShortDateString(),
                Tags = a.Tags.Select(y => y.Name).ToArray()
            }).ToList();


            // categoryLastPostView Model

            var categories = _categoryRepository.List();
            var categoryLastPostModels = new List<CategoryLastPostViewModel>();


            foreach (var item in categories)
            {
                // kategorisi göre post sorgusu, last po
                var lastPostQuery = _postRepository.Where(x => x.CategoryId == item.Id && x.IsActive);

                // bu kategoriye ait postların sayısı
                var categorypostCount = lastPostQuery.Count();
                // bu kategoriye girilmiş son postun kendisi
                var lastPostByCategory = lastPostQuery.OrderByDescending(x => x.PublishDate).Include(x => x.Comments).Take(1).FirstOrDefault();

                // ilgili kategori altında herhangi bir makele yoksa boş olan makale kontrolü yapmamız lazım aşağıdaki gibi instance alırken problme yaşıytacağımızdan dolayı. ternery if ile boş null kontrolü yaptık

                var categoryLastPostModel = new CategoryLastPostViewModel
                {
                    LastPostAuthorName = lastPostByCategory == null ?  string.Empty: lastPostByCategory?.AuthorName,
                    LastPostDate = lastPostByCategory == null  ? string.Empty: lastPostByCategory?.PublishDate.ToShortDateString(),
                    CategoryPostCount = categorypostCount,
                    LastPostId = lastPostByCategory == null ?  string.Empty: lastPostByCategory?.Id,
                    LastPostTitle = lastPostByCategory == null ? string.Empty : lastPostByCategory?.Title,
                    CategoryId = item.Id,
                    LastPostCommentCount = lastPostByCategory == null ? 0 : lastPostByCategory.Comments.Count(),
                    CategoryName = item.Name
                };

                categoryLastPostModels.Add(categoryLastPostModel);
            }


            //categoryLastPosts.ForEach(item =>
            //{
              

            //});


            var model = new HomeViewModel
            {
                CategoryLastPosts = categoryLastPostModels,
                PostItems = postItemsModel
            };


            return View(model);
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
