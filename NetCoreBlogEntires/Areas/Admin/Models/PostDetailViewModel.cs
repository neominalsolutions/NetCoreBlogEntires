using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Areas.Admin.Models
{
    public class PostDetailViewModel
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string PublishDate { get; set; }
        public string CategoryName { get; set; }
        public string HtmlContent { get; set; }
        public List<string> Tags { get; set; }


    }
}
