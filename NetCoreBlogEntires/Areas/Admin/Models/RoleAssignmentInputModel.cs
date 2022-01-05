using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Areas.Admin.Models
{
    public class RoleAssignmentInputModel
    {
        public string[] AssignedRoleNames { get; set; }
        public string AssignedUserId { get; set; }
    }
}
