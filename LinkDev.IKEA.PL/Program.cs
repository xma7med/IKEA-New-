using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Preisitance.Data;
using LinkDev.IKEA.DAL.Preisitance.Repositories.Departments;
using Microsoft.EntityFrameworkCore;

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


            builder.Services.AddScoped<ApplictaionDbContext>();
           // builder.Services.AddDbContext<ApplictaionDbContext>();

            builder.Services.AddDbContext<ApplictaionDbContext>((optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            // if (x>10)
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            // else 
            //builder.Services.AddScoped<IDepartmentService, DepartmentServiceX>();
            

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
