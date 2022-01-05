using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }

        /// <summary>
        /// registerdan sonra email aktif edilip edilmediğine baksın
        /// </summary>
        public bool IsActivated { get; set; }

    }
}
