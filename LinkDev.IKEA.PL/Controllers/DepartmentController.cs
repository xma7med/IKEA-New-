using AutoMapper;
using LinkDev.IKEA.BLL.Model.Department;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Entities.Departments;
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
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService,
            IMapper mapper,
            ILogger<DepartmentController> logger,
            IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _logger = logger;
            _environment = environment;
        }
        #endregion



        #region Index
        [HttpGet] 
        public IActionResult Index()
        {

            //Viwe's Dictionary : Pass Data from Controller [Action] to View (View -->PartialView , (View)_layout  ) 


            /// ViewData is Dictionary Type Property (introduced in Asp.Net Framework 3.5 )
            ///     => iterator helps to transfer the data from controller[Action ] to View 

            ViewData["Massage"] = "Hello ViewData";



            /// ViewBag is a Dynamic Type Property (introduced in Asp.Net Framework 4.4 based on dynamic Feature  ) 
            ///     => iterator helps to transfer the data from controller[Action ] to View 

            ViewBag.Message = "Hello ViewBag ";

            ViewBag.Message = new { Id =10 , Name ="Ahmed"};

            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }
        #endregion
         
        #region Details- Get ById



        [HttpGet]

        public IActionResult Details([FromRoute]int? id)
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
                // Manual Mappin
                ///var CreatedDepartment = new CreatedDepartmentDto()
                ///{
                ///    Code = departmentVM.Code,
                ///    Name = departmentVM.Name,
                ///    Description = departmentVM.Description,
                ///    CreationDate = departmentVM.CreationDate,
                ///
                ///};
                ///

                var CreatedDepartment = _mapper.Map<CreatedDepartmentDto>(departmentVM);
                var Created = _departmentService.CreateDepartment(CreatedDepartment) > 0;





                // 3. TempData: is a property of type Dictionary Object (introduced in .NET Framework 3.5)
                //            : Used for Transfering the Data between 2 Consuctive Requests

                if (!Created)
                { 
                    message = "Department is not Created ";

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

                TempData["Message"] = message;
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));

            // Refactored
            ///  if (!ModelState.IsValid) // Server Side Validation 
            ///      return View(departmentVM);
            ///  var message = string.Empty;
            ///  try
            ///  {
            ///      var CreatedDepartment = new CreatedDepartmentDto()
            ///   {
            ///       Code = departmentVM.Code,
            ///       Name = departmentVM.Name,
            ///       Description = departmentVM.Description,
            ///       CreationDate = departmentVM.CreationDate,
            ///
            ///   };
            ///      var Created = _departmentService.CreateDepartment(CreatedDepartment)>0;
            ///
            ///      // 3. TempData: is a property of type Dictionary Object (introduced in .NET Framework 3.5)
            ///      //            : Used for Transfering the Data between 2 Consuctive Requests
            ///
            ///      if (Created)
            ///      {
            ///          TempData["Message"] = "Department is Created ";
            ///
            ///          return RedirectToAction(nameof(Index)); 
            ///      }
            ///      else
            ///      {
            ///          TempData["Message"] = "Department is not Created ";
            ///
            ///          message = "Department is not Created";
            ///          ModelState.AddModelError(string.Empty, message);
            ///          return View(departmentVM);
            ///      }
            ///
            ///
            ///
            ///   }
            ///   catch (Exception ex)
            ///   {
            ///       // 1. Log Exception 
            ///       _logger.LogError(ex, ex.Message);
            ///   
            ///       // 2. Set Message 
            ///   
            ///       message = _environment.IsDevelopment() ? ex.Message : "an error has occured during Creating  the department ";
            ///   
            ///   }
            ///   ModelState.AddModelError(string.Empty, message);
            ///   return View(departmentVM);
            ///


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

            // Mapping Using Aut Mapper 
            var departmentVM = _mapper.Map<DepartmentDetailsDto, DepartmentViewModel>(department);
            return View(departmentVM
            //new /*CreatedDepartmentDto*/DepartmentViewModel()
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    Description = department.Description,
            //    CreationDate = department.CreationDate,
            //}
            );
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

                var departmentToUpdate = _mapper.Map<UpdatedDepartmentDto>(departmentVM);


                // Manual  Mapping 
                /// var departmentToUpdate = new UpdatedDepartmentDto()
                /// {
                ///     Id = departmentVM.Id,
                ///     Code = departmentVM.Code,
                ///     Name = departmentVM.Name,
                ///     Description = departmentVM.Description,
                ///     CreationDate = departmentVM.CreationDate,
                ///
                /// };
                ///

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
