using AutoMapper;
using Azure;
using IKEA.BLL.AttachementsService;
using IKEA.BLL.DTOS.Employee;
using IKEA.BLL.Services.Interfaces;
using IKEA.DAL.Models.EmployeeModule;
using IKEA.DAL.Models.Shared.Enums;
using IKEA.DAL.Presistance.Repositories.Classess;
using IKEA.DAL.Presistance.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Classess
{
    public class EmployeeSevice: IEmployeesService
    {
        private readonly IunitOfWork _unitOfWork ;
        private readonly IMapper _mapper ;
        private readonly IAttachementService _attachementService ;
        public EmployeeSevice(IunitOfWork unitOfWork, IMapper mapper,IAttachementService attachementService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachementService = attachementService;
        }

        public IAttachementService AttachementService { get; }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var Employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            if (employeeDto.Image is not null)
            {
                Employee.ImageName = _attachementService.Upload(employeeDto.Image, "Images");
            }
             _unitOfWork.EmployeeRepository.Add(Employee);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var Employee=_unitOfWork.EmployeeRepository.GetById(id);
            if (Employee is null) return false;

            else
            {
                Employee.IsDeleted = true;
                 _unitOfWork.EmployeeRepository.Update(Employee);
                return _unitOfWork.SaveChanges() > 0 ;
            }
        }

        public IEnumerable<EmployeesDto> GetAllEmployees(bool withTracking=false)
        {
            var Employees=_unitOfWork.EmployeeRepository.GetAll(withTracking).Where(e => !e.IsDeleted); ;
            var EmployeeDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeesDto>>(Employees);
            return EmployeeDto;
        }

        public EmployeesDetailsDto? GetEmployeeById(int id)
        {
            var Employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (Employee is null || Employee.IsDeleted) return null;
            //  var EmployeeDetailsDto=_mapper.Map<Employee,EmployeesDetailsDto>(Employee);
            //  return EmployeeDetailsDto;
            return _mapper.Map<Employee, EmployeesDetailsDto>(Employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDto,Employee>(employeeDto));
            return _unitOfWork.SaveChanges();
        }
    }
}
