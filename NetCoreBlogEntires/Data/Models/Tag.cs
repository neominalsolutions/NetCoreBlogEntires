using System;
using System.Collections.Generic;

namespace NetCoreBlogEntires.Data.Models
{
    public class Tag
    {
        public string Id { get; private set; }
        public string Name { get; private set; }


        public Tag(string name)
        {

            Name = name;
            Id = Guid.NewGuid().ToString();
        }

        private void setName(string name)
        {
            if (!name.Contains("#"))
            {
                throw new Exception("etiketler # işareti ile başlanmalıdır");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Etiket ismi boş geçilemez");
            }


            Name = name.Replace(" ", "").Trim().ToLower();
        }

        public List<Post> Posts { get; set; }

    }
}