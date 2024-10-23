using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser>userManager,SignInManager<ApplicationUser>signInManager
            ,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public async  Task<IActionResult> Index(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                var users = await _userManager.Users.Select(U => new UserViewModel
                {
                    Id = U.Id,
                    FName = U.FName,
                    LName = U.LName,
                    Email = U.Email,
                    PhoneNumber = U.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(U).Result

                }).ToListAsync();
                return View(users);
            }
            else
            {
                var user= await _userManager.FindByEmailAsync(Email);
                var mappedUser = new UserViewModel
                {
                    Id = user.Id,
                    FName = user.FName,
                    LName = user.LName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(user).Result

                };
                return View(new List<UserViewModel> (){ mappedUser });
            }
    
        }


        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            var mapUser = _mapper.Map<ApplicationUser, UserViewModel>(user);

            return View(viewName, mapUser);
        }


        public async Task<IActionResult> Edit(string id)
        {

            return await Details(id, "Edit");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserViewModel userVM)
        {
            if (id != userVM.Id)
                return BadRequest();


            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.FName=userVM.FName;
                    user.LName = userVM.LName;
                    user.Email=userVM.Email;
                    user.PhoneNumber=userVM.PhoneNumber;

               
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(userVM);
        }


        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, UserViewModel userVM)
        {
            if (id != userVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.FName = userVM.FName;
                    user.LName = userVM.LName;
                    user.Email = userVM.Email;
                    user.PhoneNumber = userVM.PhoneNumber;
                    await _userManager.DeleteAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(userVM);
        }
    }
}
