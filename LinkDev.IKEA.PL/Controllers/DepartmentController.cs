using LinkDev.IKEA.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    // Inheritance : DepartmentController is a Controller 
    // Composition : DepartmentController has a IDepartmentService
    public class DepartmentController : Controller
    {
        //[FromServices]
        //public IDepartmentService _departmentService { get; } = null!;

        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService )
        {
            _departmentService = departmentService;
        }


        [HttpGet] // Get : .. / Department / Index
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }
    }
}
