using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Preisitance.Data
{
    public class ApplictaionDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplictaionDbContext(DbContextOptions<ApplictaionDbContext> option):base(option)
        {
            
        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = . ; Database = IKEA_G03 ; Trusted_Connection = True ; TrustServerCertificate=True; ");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // u have to call OnModelCreating for the base .. Why ? it has a dbsets = Entitiy which have Configuration 

            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Dont forgt using --> using System.Reflection;

        }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

    }
}
