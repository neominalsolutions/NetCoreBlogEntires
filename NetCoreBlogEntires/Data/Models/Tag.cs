using System;
using System.Collections.Generic;

namespace NetCoreBlogEntires.Data.Models
{
    public class Tag
    {
        public string Id { get; set; }
        public string Name { get; set; }


        public Tag()
        {
            Id = Guid.NewGuid().ToString();
        }

        public List<Post> Posts { get; set; }

    }
}