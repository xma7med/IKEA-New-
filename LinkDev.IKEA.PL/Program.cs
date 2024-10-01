using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Entities.Identity;
using LinkDev.IKEA.DAL.Preisitance.Data;
using LinkDev.IKEA.DAL.Preisitance.Repositories.Departments;
using LinkDev.IKEA.DAL.Preisitance.Repositories.Employees;
using LinkDev.IKEA.DAL.Preisitance.UnitOfWork;
using LinkDev.IKEA.PL.Mapping;
using Microsoft.AspNetCore.Identity;
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


            // No Need more
            ///builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            ///builder.Services.AddScoped<IEmployeeReposiory, EmployeeRepository>();

            // Allow DI to the IUnitOfWork 

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();




            // if (x>10)
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            // else 
            //builder.Services.AddScoped<IDepartmentService, DepartmentServiceX>();


            // Allow DI for AutoMapper 
            /// Another way 
            ///builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));





            // for AttachmentService
            builder.Services.AddTransient<IAttachmentService, AttachmentService>();


            /***************************************************************************************************************************************************************************************/
            /// Allow DI for the 3 main Identity Services And Their Dependancies 
            /// First Overload Add Identity Services 
           // builder.Services.AddIdentity<ApplicationUser, IdentityRole>();
            /// Second overload ..... & Identity Configuration  
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true; // Require special characters like #, $, etc.
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;

                // User settings
                options.User.RequireUniqueEmail = true;
                // options.User.AllowedUserNameCharacters = "asdmasd;asdmas;ldfdf"; // Custom allowed characters for usernames (currently commented out)

                // Lockout settings
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);
            })
                .AddEntityFrameworkStores<ApplictaionDbContext>();


			//builder.Services.AddScoped<UserManager<ApplicationUser>>();
			//builder.Services.AddScoped<SignInManager<ApplicationUser>>();
			//builder.Services.AddScoped<RoleManager<IdentityRole>>();



			/***************************************************************************************************************************************************************************************/



			// Configure Default Schema (Identity.Applictation ) For the token 

			//Configre the Defualt Authentication Schema  for the (Specified )default schema 
			builder.Services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Account/SignIn";
                option.AccessDeniedPath = "/Home/Error";

                option.ExpireTimeSpan = TimeSpan.FromDays(1);   
                option.LogoutPath = "/Account/SignIn";

				// Search
			});



			// AddIdentity add the require sevices for security & dependancies  include the [ calling of AddAuthentication ] but i write / call it to do some  Configureation
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = "Identity.Application";// Hamda
				options.DefaultChallengeScheme = "Identity.Application";
			})
            .AddCookie("Hamda", ".AspNetCore.Hamda", options =>
            {
            	options.LoginPath = "/Account/Login";
            	options.AccessDeniedPath = "/Home/Error";
            	options.ExpireTimeSpan = TimeSpan.FromDays(10);
            	options.LogoutPath = "/Account/SignIn";
            });







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


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            #endregion

            app.Run();
        }
    }
}
