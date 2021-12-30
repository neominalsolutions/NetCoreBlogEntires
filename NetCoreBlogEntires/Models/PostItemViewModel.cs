using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Models
{
    public class PostItemViewModel
    {
        public string CategoryId { get; set; }

        public string Id { get; set; }

        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string PublishDate { get; set; }
        public int CommentCount { get; set; }

        public string ShortContent { get; set; }

        public string[] Tags { get; set; }
    }
}
