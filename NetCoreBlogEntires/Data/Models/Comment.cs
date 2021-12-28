using System;

namespace NetCoreBlogEntires.Data.Models
{
    public class Comment
    {
        public string Id { get; set; }
        public string CommentBy { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }



    }
}