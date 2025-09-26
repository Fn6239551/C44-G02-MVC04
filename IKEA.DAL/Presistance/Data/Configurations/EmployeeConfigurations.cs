using IKEA.DAL.Models.EmployeeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Data.Configurations
{
    public class EmployeeConfigurations :BaseEntityConfigurations<Employee>, IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

            builder.Property(E => E.Name).HasColumnType("varchar(50)");
            builder.Property(E => E.Address).HasColumnType("varchar(50)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");
            builder.Property(E => E.Gender).HasConversion((EmployeeGender) => EmployeeGender.ToString(),
                (_gender) => (Gender)Enum.Parse(typeof(Gender), _gender));
            
            builder.Property(E=>E.EmployeeType)
                .HasConversion((EmpType)=>EmpType.ToString(),
                (_Type) => (EmployeeTypes)Enum.Parse(typeof(EmployeeTypes), _Type));
            base.Configure(builder);
        }
    }
}
