using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Models
{
    public class CategoryLastPostViewModel
    {
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }

        // ilgili kategoride kaç adet makale var bilgisi
        public int CategoryPostCount { get; set; }

        // Bu kategoride son yayınlanan makale tarihi
        public string LastPostDate { get; set; }

        // son Makaleyi yazan şahsın adı
        public string LastPostAuthorName { get; set; }

        // Son makale Başlığı
        public string LastPostTitle { get; set; }

        // son makale Id
        public string LastPostId { get; set; }

        public int LastPostCommentCount { get; set; }



    }
}
