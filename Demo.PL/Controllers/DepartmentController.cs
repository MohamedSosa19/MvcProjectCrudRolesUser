using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private IDepartmentRepository _departmentRepository;
        private IUnitOfWork _unitOfWork;

        public DepartmentController(/*IDepartmentRepository departmentRepository,*/ IUnitOfWork unitOfWork)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var department = _unitOfWork.DepartmentRepository.GetAll();
            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Add(department);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Details(int? id ,string viewName= "Details")
        {
            if (id == null)
                return BadRequest();

            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            if(department is null)
                return NotFound();

            return View(viewName, department);
        }

        public IActionResult Edit(int? id )
        {
            //if(id is null)
            //    return BadRequest();
            //var department=_departmentRepository.GetById(id.Value);
            //if (department is null)
            //    return NotFound();

            //return View(department);
            return Details(id,"Edit");
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id  ,Department department)
        {
            if(id != department.Id)
                return BadRequest();


            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                    _unitOfWork.Complete();
                    return View(department);
                }
                catch(Exception ex) 
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                }
            }

            return View(department);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete([FromRoute] int id ,Department department)
        {
            if (id != department.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Delete(department);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }
    }
}
