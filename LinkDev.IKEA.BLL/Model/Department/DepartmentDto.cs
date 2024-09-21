using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Model.Department
{
    // Use ut in depts Listting 
    public  class DepartmentDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        //public string Description { get; set; } = null!;
        [Display(Name = "Date of creation ")]
        public DateOnly CreationDate { get; set; }


        //public static /*DepartmentToReturnDto*/ explicit operator DepartmentToReturnDto (Department department)
        //{
        //    return new DepartmentToReturnDto
        //    {
        //        Id = department.Id,
        //        Code = department.Code,
        //        Name = department.Name,
        //        Description = department.Description,
        //        CreationDate = department.CreationDate,
        //    };
        //}
    }
}
