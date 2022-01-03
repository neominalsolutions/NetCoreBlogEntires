using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Models
{
    public class PostDetailViewModel
    {
        public string AuthorName { get; set; }

        public string PostId { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string[] TagNames { get; set; }
        public string Content { get; set; }
        public string PublishDate { get; set; }
        public int CommentCount { get; set; }
        public List<CommentViewModel> Comments { get; set; }

        public PostCommentInputModel CommentInput { get; set; }


    }
}
