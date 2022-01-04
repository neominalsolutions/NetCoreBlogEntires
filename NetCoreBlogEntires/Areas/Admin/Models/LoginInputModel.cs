using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Areas.Admin.Models
{
    public class LoginInputModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }


    }
}
