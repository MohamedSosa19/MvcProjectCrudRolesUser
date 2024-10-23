using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        //private IEmployeeRepository _employeeRepository;
        //private IDepartmentRepository _departmentRepository;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public EmployeeController(/*IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository*/
            IMapper mapper,IUnitOfWork unitOfWork)
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(string SearchValue) 
        {
           IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
             employees =await _unitOfWork.EmployeeRepository.GetAll();
            else 
                employees= _unitOfWork.EmployeeRepository.SearchByName(SearchValue);


            var mapEmp = _mapper.Map< IEnumerable<Employee>, IEnumerable< EmployeeViewModel> >(employees);
            return View(mapEmp);
        }

        public async Task<IActionResult> CreateAsync()
        {

            ViewBag.Department= await _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");

                var mapEmp=_mapper.Map<EmployeeViewModel,Employee>(employeeVM);
              await _unitOfWork.EmployeeRepository.Add(mapEmp);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id == null)
                return BadRequest();

            var employee =await _unitOfWork.EmployeeRepository.GetById(id.Value);
            if (employee is null)
                return NotFound();

            var mapEmp=_mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(viewName, mapEmp);
        }

        public async Task<IActionResult> Edit(int? id)
        {
           
            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.id)
                return BadRequest();


            if (ModelState.IsValid)
            {
                try
                {
                    var mapEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(mapEmp);
                    await _unitOfWork.Complete();
                    return View(employeeVM);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(employeeVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var mapEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Delete(mapEmp);
                   int count= await _unitOfWork.Complete();
                    if (count > 0)
                        DocumentSettings.DeleteFile(mapEmp.ImageName, "images");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);
        }
    }
}
