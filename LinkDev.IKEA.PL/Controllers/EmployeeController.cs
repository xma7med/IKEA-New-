﻿using LinkDev.IKEA.BLL.Model.Employees;
using LinkDev.IKEA.BLL.Services.Employees;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Service 
        private readonly IEmployeeService _employeeService;
        //private readonly IDepartmentService _departmentService; 
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService,
            /*IDepartmentService departmentService,*/ // Ask on scope of action
            ILogger<EmployeeController> logger,
            IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            //_departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }

        #endregion

        #region Index
        [HttpGet] // Get : .. / Employee / Index
        public async Task<IActionResult> Index(string search)
        {
            var employees = await  _employeeService.GetEmployeesAsync(search);

            return View(employees);
        }
        #endregion


        #region Details



        [HttpGet] // Get : /Employee/Details/id 

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var employees = await  _employeeService.GetEmployeeByIdAsync(id.Value);

            if (employees == null)
                return NotFound();

            return View(employees);
        }


        #endregion



        #region Create

        [HttpGet] // Get : ../Employee / Create 
        public IActionResult Create(/*[FromServices]IDepartmentService departmentService*/)
        {
            // Ask obj by the  4 way  in Create View 
            //ViewData["Departments"] = departmentService.GetAllDepartments();   
            return View();
        }

        [HttpPost] // Post 
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CreatedEmployeeDto employee)
        {
            if (!ModelState.IsValid) // Server Side Validation 
                return View(employee);
            var message = string.Empty;
            try
            {
                var result = await  _employeeService.CreateEmployeeAsync(employee);
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

       [HttpGet] // GET : / Employee/Edit/id? 
       public async Task<IActionResult> Edit(int? id  /*,[FromServices] IDepartmentService departmentService*/)
       {
           if (id is null)
               return BadRequest();// 400
           var employee = await  _employeeService.GetEmployeeByIdAsync(id.Value);

           if (employee == null)
               return NotFound(); // 404

            //ViewData["Departments"] = departmentService.GetAllDepartments();


            return View(new UpdatedEmployeeDto /*EmployeeEditViewModel*/()
           {
               Name = employee.Name,
               Address = employee.Address,
               Email = employee.Email,  
               Age = employee.Age,
               Salary = employee.Salary,
               PhoneNumber = employee.PhoneNumber,  
               IsActive = employee.IsActive,    
               EmployeeType = employee.EmployeeType,
               Gender = employee.Gender,
               HiringDate = employee.HiringDate,


           });
       }

       [HttpPost] // Post 
                  // from ACTION from url 
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit([FromRoute] int id, UpdatedEmployeeDto /*EmployeeEditViewModel*/ employee)
       {
           if (!ModelState.IsValid)// Server Side Validation 
               return View(employee);

           var message = string.Empty;
           try
           {   ///  Canceled 
               /// //// Mapping 
               /// //var employeeToUpdate = new UpdatedEmployeeDto()
               /// //{
               /// //    Name = employeeVM.Name,
               /// //    Address = employeeVM.Address,    
               /// //    Email = employeeVM.Email,    
               /// //    Age= employeeVM.Age,
               /// //    Salary= employeeVM.Salary,   
               /// //    PhoneNumber = employeeVM.PhoneNumber,            
               /// //    IsActive = employeeVM.IsActive,      
               /// //    EmployeeType = employeeVM.EmployeeType,  
               /// //    Gender = employeeVM.Gender,
               /// //    HiringDate= employeeVM.HiringDate,
               ///     
               /// 
               /// //};


                var updated = await  _employeeService.UpdateEmployeeAsync( employee ) > 0;
                if (updated)
                    return RedirectToAction(nameof(Index));

                message = "an error has occured during updating the employee ";

            }
            catch (Exception ex)
            {

                // 1. Log Exception 
                _logger.LogError(ex, ex.Message);


                // 2. Set Message 

                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during updating the employee ";

            }

            // Message 
            ModelState.AddModelError(string.Empty, message);
            return View(employee);

        }

        #endregion



        #region Delete

        //[HttpGet] // Get: /Employee / Delete / id?
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //        return BadRequest();
        //    var employee = _employeeService.GetEmployeeById(id.Value);
        //    if (employee == null)
        //        return NotFound();

        //    return View(employee);
        //}

        [HttpPost] // Post
        [ValidateAntiForgeryToken]

        public async  Task<IActionResult> Delete(int id) // the Same name in view --> id 
        {
            var messege = string.Empty;

            try
            {
                var deleted =  await _employeeService.DeleteEmployeeAsync(id);
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
