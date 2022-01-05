using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Areas.Admin.Models
{
    public class PostChangeStatusInputModel
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }

    }
}
