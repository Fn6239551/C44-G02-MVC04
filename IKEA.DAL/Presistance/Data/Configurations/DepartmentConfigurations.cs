using IKEA.DAL.Models.DepartmentModule;
using IKEA.DAL.Models.EmployeeModule;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Data.Configurations
{
    public class DepartmentConfigurations:BaseEntityConfigurations<Department>,IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.Property(D=>D.Id).UseIdentityColumn(10,10);
            builder.Property(D => D.Name).HasColumnType("varchar(20)");
            builder.Property(D => D.Code).HasColumnType("varchar(20)");
            builder.HasMany(D => D.Employees)
                .WithOne(E => E.Department)
                .HasForeignKey(E => E.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasData(
                new Department { Id = 1, Name = "HR", Code = "HR01" },
                new Department { Id = 2, Name = "IT", Code = "IT01" },
                new Department { Id = 3, Name = "Finance", Code = "FIN01" }
            );
            base.Configure(builder);
        }
    }
}
