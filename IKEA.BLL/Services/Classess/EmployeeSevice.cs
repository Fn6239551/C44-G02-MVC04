using AutoMapper;
using Azure;
using IKEA.BLL.DTOS.Employee;
using IKEA.BLL.Services.Interfaces;
using IKEA.DAL.Models.EmployeeModule;
using IKEA.DAL.Models.Shared.Enums;
using IKEA.DAL.Presistance.Repositories.Classess;
using IKEA.DAL.Presistance.Repositories.Interface;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Classess
{
    public class EmployeeSevice(IEmployeeRepository employeeRepository,IMapper mapper) : IEmployeesService
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IMapper _mapper = mapper;

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
           var Employee=_mapper.Map<CreatedEmployeeDto,Employee>(employeeDto);
            return _employeeRepository.Add(Employee);
        }

        public bool DeleteEmployee(int id)
        {
            var Employee=_employeeRepository.GetById(id);
            if (Employee is null) return false;

            else
            {
                Employee.IsDeleted = true;
                return _employeeRepository.Update(Employee)>0? true :false;
            }
        }

        public IEnumerable<EmployeesDto> GetAllEmployees(bool withTracking=false)
        {
            var Employees=_employeeRepository.GetAll(withTracking).Where(e => !e.IsDeleted); ;
            var EmployeeDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeesDto>>(Employees);
            return EmployeeDto;
        }

        public EmployeesDetailsDto? GetEmployeeById(int id)
        {
            var Employee = _employeeRepository.GetById(id);
            if (Employee is null || Employee.IsDeleted) return null;
            //  var EmployeeDetailsDto=_mapper.Map<Employee,EmployeesDetailsDto>(Employee);
            //  return EmployeeDetailsDto;
            return _mapper.Map<Employee, EmployeesDetailsDto>(Employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
           return _employeeRepository.Update(_mapper.Map<UpdatedEmployeeDto,Employee>(employeeDto));
        }
    }
}
