using LinkDev.IKEA.BLL.Model.Department;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace LinkDev.IKEA.PL.Controllers
{
    
    public class DepartmentController : Controller
    {
        //[FromServices]
        //public IDepartmentService _departmentService { get; } = null!;

        #region Services
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService,
            ILogger<DepartmentController> logger,
            IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }
        #endregion



        #region Index
        [HttpGet] 
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }
        #endregion

        #region Details



        [HttpGet]

        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);

            if (department == null)
                return NotFound();

            return View(department);
        }


        #endregion


        #region Create

        [HttpGet] // Get : ../Department / Create 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // Post 
        [ValidateAntiForgeryToken]

        public IActionResult Create(DepartmentViewModel  departmentVM)
        {
            if (!ModelState.IsValid) // Server Side Validation 
                return View(departmentVM);
            var message = string.Empty;
            try
            {
                var CreatedDepartment = new CreatedDepartmentDto()
                {
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate,

                };
                var result = _departmentService.CreateDepartment(CreatedDepartment);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {

                    message = "Department is not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentVM);
                }



            }
            catch (Exception ex)
            {
                // 1. Log Exception 
                _logger.LogError(ex, ex.Message);

                // 2. Set Message 

                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during Creating  the department ";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);

        }
        #endregion

        #region Update

        [HttpGet] // GET : / Department/Edit/id? 
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();// 400
            var department = _departmentService.GetDepartmentById(id.Value);

            if (department == null)
                return NotFound(); // 404
            return View(new CreatedDepartmentDto()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
            });
        }

        [HttpPost] // Post 
                   // from ACTION from url 
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)// Server Side Validation 
                return View(departmentVM);

            var message = string.Empty;
            try
            {
                // Mapping 
                var departmentToUpdate = new UpdatedDepartmentDto()
                {
                    Id = departmentVM.Id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate,

                };


                var updated = _departmentService.UpdateDepartment(departmentToUpdate) > 0;
                if (updated)
                    return RedirectToAction(nameof(Index));
                message = "an error has occured during updating the department ";

            }
            catch (Exception ex)
            {

                // 1. Log Exception 
                _logger.LogError(ex, ex.Message);


                // 2. Set Message 

                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during updating the department ";

            }

            // Message 
            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);

        }

        #endregion



        #region Delete

        [HttpGet] // Get: /Department / Delete / id?
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department == null)
                return NotFound();

            return View(department);
        }

        [HttpPost] // Post
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int id) // the Same name in view --> id 
        {
            var messege = string.Empty;

            try
            {
                var deleted = _departmentService.DeleteDepartment(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));

                messege = "an error has occured during Deleting  the department ";

            }
            catch (Exception ex)
            {
                // 1. Log Exception 
                _logger.LogError(ex, ex.Message);


                // 2. Set Message 

                messege = _environment.IsDevelopment() ? ex.Message : "an error has occured during deleting the department ";


            }

            //ModelState.AddModelError(string.Empty, messege)
            return RedirectToAction(nameof(Index));

        }


        #endregion




    } 
}
