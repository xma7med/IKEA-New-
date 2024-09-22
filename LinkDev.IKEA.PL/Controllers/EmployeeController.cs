﻿using LinkDev.IKEA.BLL.Model.Department;
using LinkDev.IKEA.BLL.Model.Employees;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Service 
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService,
            ILogger<EmployeeController> logger,
            IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _logger = logger;
            _environment = environment;
        }

        #endregion

        #region Index
        [HttpGet] // Get : .. / Employee / Index
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();

            return View(employees);
        }
        #endregion


        #region Details



        [HttpGet] // Get : /Employee/Details/id 

        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var employees = _employeeService.GetEmployeeById(id.Value);

            if (employees == null)
                return NotFound();

            return View(employees);
        }


        #endregion



        #region Create

        [HttpGet] // Get : ../Employee / Create 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // Post 
        public IActionResult Create(CreatedEmployeeDto employee)
        {
            if (!ModelState.IsValid) // Server Side Validation 
                return View(employee);
            var message = string.Empty;
            try
            {
                var result = _employeeService.CreateEmployee(employee);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Employee is not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employee);
                }



            }
            catch (Exception ex)
            {
                // 1. Log Exception 
                _logger.LogError(ex, ex.Message);

                // 2. Set Message 

                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during Creating  the Employee ";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(employee);

        }
        #endregion

        #region Update

        //[HttpGet] // GET : / Employee/Edit/id? 
        //public IActionResult Edit(int? id)
        //{
        //    if (id is null)
        //        return BadRequest();// 400
        //    var employee = _employeeService.GetEmployeeById(id.Value);

        //    if (employee == null)
        //        return NotFound(); // 404
        //    return View(new EmployeeEditViewModel()
        //    {
        //        Code = employee.Code,
        //        Name = employee.Name,
        //        Description = employee.Description,
        //        CreationDate = employee.CreationDate,
        //    });
        //}

        //[HttpPost] // Post 
        //// from ACTION from url 
        //public IActionResult Edit([FromRoute] int id, EmployeeEditViewModel employeeVM)
        //{
        //    if (!ModelState.IsValid)// Server Side Validation 
        //        return View(employeeVM);

        //    var message = string.Empty;
        //    try
        //    {
        //        // Mapping 
        //        var employeeToUpdate = new UpdatedEmployeeDto()
        //        {
        //            Code = employeeVM.Code,
        //            Name = employeeVM.Name,
        //            Description = employeeVM.Description,
        //            CreationDate = employeeVM.CreationDate,

        //        };


        //        var updated = _employeeService.UpdateEmployee(employeeToUpdate) > 0;
        //        if (updated)
        //            return RedirectToAction(nameof(Index));
        //        message = "an error has occured during updating the employee ";

        //    }
        //    catch (Exception ex)
        //    {

        //        // 1. Log Exception 
        //        _logger.LogError(ex, ex.Message);


        //        // 2. Set Message 

        //        message = _environment.IsDevelopment() ? ex.Message : "an error has occured during updating the employee ";

        //    }

        //    // Message 
        //    ModelState.AddModelError(string.Empty, message);
        //    return View(employeeVM);

        //}

        #endregion



         #region Delete

        [HttpGet] // Get: /Employee / Delete / id?
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost] // Post
        public IActionResult Delete(int id) // the Same name in view --> id 
        {
            var messege = string.Empty;

            try
            {
                var deleted = _employeeService.DeleteEmployee(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));

                messege = "an error has occured during Deleting  the employee ";

            }
            catch (Exception ex)
            {
                // 1. Log Exception 
                _logger.LogError(ex, ex.Message);


                // 2. Set Message 

                messege = _environment.IsDevelopment() ? ex.Message : "an error has occured during deleting the employee ";


            }

            //ModelState.AddModelError(string.Empty, messege)
            return RedirectToAction(nameof(Index));

        }


        #endregion
         




    }

}
