﻿using LinkDev.IKEA.DAL.Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Model.Employees
{
    public class CreatedEmployeeDto
    {
        //[Required]
        [MaxLength(50, ErrorMessage = "Max Length of Name is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length of Name is 5 Chars")]
        public string Name { get; set; } = null!;

        [Range(22, 30)]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
         ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }

        public decimal Salary { get; set; }

        [Display(Name = "Is Active")] // Client side
        public bool IsActive { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        //[DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
         
        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }
        [Display(Name="Employee Type  ")]
        public EmployeeType EmployeeType { get; set; }
        [Display(Name="Department")]
        public int? DepartmentId { get; set; }

        public IFormFile? Image { get; set; }


    }
}
