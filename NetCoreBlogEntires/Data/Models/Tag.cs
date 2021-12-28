using System.Collections.Generic;

namespace NetCoreBlogEntires.Data.Models
{
    public class Tag
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<Post> Posts { get; set; }

    }
}