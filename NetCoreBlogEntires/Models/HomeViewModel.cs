using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Models
{
    public class HomeViewModel
    {
        public List<PostItemViewModel> PostItems { get; set; }
        public List<CategoryLastPostViewModel> CategoryLastPosts { get; set; }

    }
}
