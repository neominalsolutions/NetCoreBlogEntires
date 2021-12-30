using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Models
{
    public class PostListViewModel
    {
        public List<PostItemViewModel> PostItems { get; set; }
        public int PageCount { get; set; }

    }
}
