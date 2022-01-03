using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Models
{
    public class CommentViewModel
    {
        public string CommentBy { get; set; }
        public string Text { get; set; }
        public string PublishDate { get; set; }
    }
}
