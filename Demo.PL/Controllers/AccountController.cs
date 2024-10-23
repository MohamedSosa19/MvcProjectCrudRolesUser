using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(UserManager<ApplicationUser>userManager,SignInManager<ApplicationUser>signInManager,RoleManager<IdentityRole> roleManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		public SignInManager<ApplicationUser> SignInManager { get; }

		public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser()
                    {
                        FName = model.FName,
                        LName = model.LName,
                        UserName = model.Email.Split('@')[0],
                        IsAgree = model.IsAgree,
                        Email = model.Email,
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Login));
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }
            }
            catch(Exception ex)
            {

            }
          
            return View(model);
        }

		public IActionResult Login()
		{
			return View();
		}

        [HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
        {
			if (ModelState.IsValid)
            {
                var user=await _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.password, model.RememberMe, false);
                        if (result.Succeeded)
                            return RedirectToAction("Index","Home");


                    }
					ModelState.AddModelError(string.Empty, "password is incorrect");
				}
                ModelState.AddModelError(string.Empty, "Email is not Exsits");

			}
            return  View();
		}

        public new async Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var user=await  _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordLink = Url.Action("ResetPassword", "Account", new { email = model.Email, Token = token }, Request.Scheme);
                    
                    var email = new Email()
                    {
                        Title = "Reset Password",
                        Body = resetPasswordLink,
                        To = model.Email
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourBox));
                }
                ModelState.AddModelError(string.Empty, "user not exsits");
            }
            return View(model);

        }

        public IActionResult CheckYourBox()
        {
            return View();
        }


        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["email"]  as string;

                string token = TempData["token"] as string;

                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Login));
                    foreach (var Error in result.Errors)
                        ModelState.AddModelError(string.Empty, Error.Description);

                }
            }
            return View(model);
        }


    }
}
