﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Entities.Identity
{
	public  class ApplicationUser : IdentityUser
	{
		public string FName { get; set; } = null!;
		public string LName { get; set; } =null!;

        public bool  IsAgree  { get; set; }
		 

    }
}
