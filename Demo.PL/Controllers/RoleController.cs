using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Demo.PL.Helper;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole>roleManager,IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;          
        }
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var role = await _roleManager.Roles.Select(R => new RoleViewModel
                {
                    Id = R.Id,
                    RoleName=R.Name

                }).ToListAsync();
                return View(role);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(name);
                var mappedRole = new RoleViewModel
                {
                    Id = role.Id,
                    RoleName = role.Name

                };
                return View(new List<RoleViewModel>() { mappedRole });
            }

        }

        public  ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleVM)
        {
            if (ModelState.IsValid)
            {
              

                var mapEmp = _mapper.Map<RoleViewModel, IdentityRole>(roleVM);
                await _roleManager.CreateAsync(mapEmp);
                return RedirectToAction(nameof(Index));
            }
            return View(roleVM);
        }




        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            var mapRole = _mapper.Map<IdentityRole, RoleViewModel>(role);

            return View(viewName, mapRole);
        }


        public async Task<IActionResult> Edit(string id)
        {

            return await Details(id, "Edit");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string id, RoleViewModel roleVM)
        {
            if (id != roleVM.Id)
                return BadRequest();


            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = roleVM.RoleName;
                   


                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(roleVM);
        }


        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, RoleViewModel roleVM)
        {
            if (id != roleVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = roleVM.RoleName;
                    
                    await _roleManager.DeleteAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(roleVM);
        }
    }
}
