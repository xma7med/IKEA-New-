using LinkDev.IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Model.Employees
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;


        public int? Age { get; set; }


        public string? Address { get; set; }

        [DataType(DataType.Currency)] // Client side

        public decimal Salary { get; set; }
        [Display(Name = "Is Active")] // Client side

        public bool IsActive { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }


        public string? PhoneNumber { get; set; }


        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }  

        public EmployeeType EmployeeType { get; set; }

        #region Adminstration
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        #endregion
    }
}
