﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Departments
{
    public class DepartmentEditViewModel
    {
        [Required(ErrorMessage ="Code is Required ya broooooooooo?")]
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name ="Creation Date " )] 
        public DateOnly CreationDate { get; set; }
    }
}
