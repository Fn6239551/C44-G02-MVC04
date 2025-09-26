using AutoMapper;
using IKEA.BLL.DTOS.Employee;
using IKEA.DAL.Models.EmployeeModule;
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
            CreateMap<Employee, EmployeesDetailsDto>().ReverseMap();

            CreateMap<Employee, EmployeesDto>().ReverseMap()
                   .ForMember(dest => dest.Gender, options => options.MapFrom(Src => Src.Gender))
                   .ForMember(dest => dest.EmployeeType, option => option.MapFrom(Src => Src.EmployeeType));
            CreateMap<CreatedEmployeeDto, Employee>().ReverseMap();
            CreateMap<UpdatedEmployeeDto, Employee>().ReverseMap();
        }
    }
}
