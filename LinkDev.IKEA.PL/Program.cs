using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Preisitance.Data;
using LinkDev.IKEA.DAL.Preisitance.Repositories.Departments;
using LinkDev.IKEA.DAL.Preisitance.Repositories.Employees;
using LinkDev.IKEA.PL.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LinkDev.IKEA.PL
{
    // New 
    public class Program
    {
        //  Entry Point 
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services 



            // Add services to the container.
            builder.Services.AddControllersWithViews();


           // builder.Services.AddScoped<ApplictaionDbContext>();
           // builder.Services.AddDbContext<ApplictaionDbContext>();

            builder.Services.AddDbContext<ApplictaionDbContext>((optionsBuilder) =>
            {
                optionsBuilder.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeReposiory, EmployeeRepository>();



            // if (x>10)
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            // else 
            //builder.Services.AddScoped<IDepartmentService, DepartmentServiceX>();


            // Allow DI for AutoMapper 
            /// Another way 
            ///builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));



            #endregion



            var app = builder.Build();



            #region Configure Kestral Middlwares
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production
                // scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            #endregion

            app.Run();
        }
    }
}
