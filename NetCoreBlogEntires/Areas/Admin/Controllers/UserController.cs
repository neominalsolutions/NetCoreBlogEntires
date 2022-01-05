using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreBlogEntires.Areas.Admin.Models;
using NetCoreBlogEntires.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Areas.Admin.Controllers
{
    [Area("Admin")][Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // artık route /admin/users
        [HttpGet("/admin/users")] 
        [Authorize(Roles ="Admin")] // sadece admin user role atayabilir.
        public async Task<IActionResult> List()
        {
            var users = _userManager.Users.ToList();
            var model = new UserPageViewModel();
            model.Users = new List<UserViewModel>();
            model.Roles = _roleManager.Roles.Select(a =>
            new RoleViewModel
            {
                RoleId = a.Id,
                RoleName = a.Name

            }).ToList();

            // select içerisinde await kullanamadığımız için her bir user ait roles bilgisini aşağıdaki gibi foreach içerisinde çektik.

            foreach (var item in users)
            {
                var user = new UserViewModel
                {
                    UserId = item.Id,
                    Email = item.Email,
                    IsActivated = item.EmailConfirmed,
                    Roles = (await _userManager.GetRolesAsync(item)).ToArray(),
                    UserName = item.UserName
                };

                model.Users.Add(user);
            }


            return View(model);
        }


        [HttpPost]
        public async Task<JsonResult> RoleAssignment([FromBody]RoleAssignmentInputModel model)
        {
            var user = await _userManager.FindByIdAsync(model.AssignedUserId);
            var userRoles = await _userManager.GetRolesAsync(user);
            // userRolesleri getir önce tüm user atanan rolleri kaldır.
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            // sonra eğer atanmış bir rol varsa bunun atamasını yapar.
            if(model.AssignedRoleNames.Count() != 0)
            {
                await _userManager.AddToRolesAsync(user, model.AssignedRoleNames.AsEnumerable());
            }

           

            return Json("OK");
        }
    }
}
