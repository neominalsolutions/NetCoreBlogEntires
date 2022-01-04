﻿using Microsoft.AspNetCore.Identity;
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

            //var result = await _userManager.CreateAsync(new ApplicationUser { UserName = "Mert", Email = "mert.alptekin@neominal.com" }, "Neominal35?");

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
                await _signManager.SignInAsync(user, model.RememberMe, null);

                return Redirect("/Admin");
            }

            return View();
        }
    }
}