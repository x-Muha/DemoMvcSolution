using System.Reflection;
using Demo.DataAccess.Models.DepartmentModel;
using Demo.DataAccess.Models.IdentityModel;
using Demo.DataAccess.Models.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Demo.DataAccess.Data.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<BaseEntity>();
            // to apply all configuration files for the running project
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // to apply all configuration files for the ApplicationDbContext project
            // modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
