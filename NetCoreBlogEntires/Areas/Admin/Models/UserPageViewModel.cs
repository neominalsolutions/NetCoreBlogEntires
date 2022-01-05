using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Areas.Admin.Models
{
    public class UserPageViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public List<RoleViewModel> Roles  { get; set; }

    }
}
