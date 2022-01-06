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
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager )
        {
            _userManager = userManager;
            _signManager = signInManager;
        }
        public async Task<IActionResult> Login()
        {

            //var result = await _userManager.CreateAsync(new ApplicationUser { UserName = "Berkay", Email = "berkay.erarslan@gmail.com" }, "Neominal34?");

            //// kullanıcı oluştuysa
            //if (result.Succeeded)
            //{

            //}

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);


            if(user != null)
            {
                // email confirmed olup olmasını kontrol eder. 
                // signInManager signInAsync bu kontrolü yapmaz.
                await _signManager.PasswordSignInAsync(user,model.Password, model.RememberMe,false);

                return Redirect("/Admin");
            }

            return View();
        }
    }
}
