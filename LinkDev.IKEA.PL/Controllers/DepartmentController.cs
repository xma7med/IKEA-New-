using LinkDev.IKEA.BLL.Model.Department;
using LinkDev.IKEA.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace LinkDev.IKEA.PL.Controllers
{
    // Inheritance : DepartmentController is a Controller 
    // Composition : DepartmentController has a IDepartmentService
    public class DepartmentController : Controller
    {
        //[FromServices]
        //public IDepartmentService _departmentService { get; } = null!;

        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService ,
            ILogger<DepartmentController> logger ,
            IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }


        [HttpGet] // Get : .. / Department / Index
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }


        [HttpGet] // Get : ../Department / Create 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // Post 
        public IActionResult Create(CreatedDepartmentDto department)
        { 
            if (!ModelState.IsValid) // Server Side Validation 
                return View(department);
            var message =string.Empty;
            try
            {
                var result = _departmentService.CreateDepartment(department);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                { 
                    message = "Department is not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(department);    
                }

                

            }
            catch (Exception ex)
            {
                // 1. Log Exception 
                _logger.LogError(ex , ex.Message);

                // 2. Set Message 
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(department);
                }
                else
                {
                    message = "Department is not Created";

                    return View("Error" , message);
                }

            }

        }


    }
}
