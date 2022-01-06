using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Areas.Admin.Models
{
    public class PostViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }

        public int CommentCount { get; set; }

        public string AuthorName { get; set; }

        public string CategoryName { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsActive { get; set; }


    }
}
