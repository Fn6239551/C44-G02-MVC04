using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using IKEA.DAL.Models.DepartmentModule;
using IKEA.DAL.Models.EmployeeModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace IKEA.DAL.Presistance.Data.Contexts
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Models.EmployeeModule.Employee> Employees { get; set; }
      //  public DbSet<Microsoft.AspNetCore.Identity.IdentityUser> Users {  get; set; }
       // public DbSet<IdentityRoles>Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
