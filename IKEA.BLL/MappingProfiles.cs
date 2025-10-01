using AutoMapper;
using IKEA.BLL.DTOS.Employee;
using IKEA.DAL.Models.EmployeeModule;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeesDetailsDto>().
                ForMember(dest => dest.Department, Options => Options.MapFrom(src => src.Department != null ? src.Department.Name:null))
                .ForMember(dest=>dest.Image,Options=>Options.MapFrom(src=>src.ImageName)); 

            CreateMap<Employee, EmployeesDto>().ReverseMap()
                   .ForMember(dest => dest.Gender, options => options.MapFrom(Src => Src.Gender))
                   .ForMember(dest => dest.EmployeeType, option => option.MapFrom(Src => Src.EmployeeType));
            CreateMap<CreatedEmployeeDto, Employee>().ReverseMap();
            CreateMap<UpdatedEmployeeDto, Employee>().ReverseMap();
        }
    }
}
