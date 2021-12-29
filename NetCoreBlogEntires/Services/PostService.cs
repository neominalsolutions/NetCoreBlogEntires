using NetCoreBlogEntires.Areas.Admin.Models;
using NetCoreBlogEntires.Data.Models;
using NetCoreBlogEntires.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;


        public PostService(IPostRepository postRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }

        /// <summary>
        /// Makale oluştuğunda makaleyi yayınlayan editöre mail atma vs gibi işlemleri ve makale onay sürecini başlatma vs gibi işlemlerin kontrolünü de yapıcaz.
        /// </summary>
        /// <param name="request"></param>
        public void Save(PostInputModel request)
        {

            var post = new Post(title: request.Title, content: request.Content, shortContent: request.ShortContent, categoryId: request.CategoryId, authorName: request.AuthorName);

            if(!string.IsNullOrEmpty(request.Tags))
            {
                var tagList = request.Tags.Split(",");

                foreach (var tagName in tagList)
                {
                    post.AddTag(new Tag(name: tagName), _tagRepository);
                }
            }

            _postRepository.Add(post);
            _postRepository.Save();

        }
    }
}
