using System.Security.Principal;
using Demo.BusinessLogic.Profiles;
using Demo.BusinessLogic.Services;
using Demo.BusinessLogic.Services.AttachmentService;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models;
using Demo.DataAccess.Models.IdentityModel;
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });



            //builder.Services.AddScoped<ApplicationDbContext>(); // 2. Register To Services In DI Container
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            }); // Same As AddScoped but with options

            //builder.Services.AddScoped<DepartmentRepository>(); // Enable DI for DepartmentService 
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IAttachmentService,AttachmentService>();
            //Enable DI for Auto Mapper
            //1. if Mapper is private we create public Ref class in it's project
            //builder.Services.AddAutoMapper(typeof(ProjectReference).Assembly);
            //2. if Mapper is public we add add Profile without getting it's Assembly
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

            builder.Services.AddIdentity<ApplicationUser,IdentityRole>(/*additional options*/)
                   .AddEntityFrameworkStores<ApplicationDbContext>();       //for validation

            #endregion

            var app = builder.Build();

            #region Configure the HTTP request pipeline


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // Check if all requests are Secure
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            #endregion

            app.Run();
        }
    }
}
