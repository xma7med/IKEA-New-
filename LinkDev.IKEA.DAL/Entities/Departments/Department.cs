﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Entities.Department
{
    public class Department:ModelBase
    {
        [Required(ErrorMessage ="Name is required ")]
        public string Name { get; set; } = null!;
        public string  Code { get; set; } = null!;
        public string?  Description { get; set; } 
        public DateOnly CreationDate { get; set; }


    }
}
 