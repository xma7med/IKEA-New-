using LinkDev.IKEA.DAL.Models.Department;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Preisitance.Data
{
    public class ApplictaionDbContext : DbContext
    {
         


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = . ; Database = IKEA_G03 ; Trusted_Connection = True ; TrustedServerCertificate = True " );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Dont forgt using --> using System.Reflection;

        }
        public DbSet<Department> Departments { get; set; }


    }
}
