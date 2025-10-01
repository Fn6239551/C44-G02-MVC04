using IKEA.BLL.DTOS.Department;
using IKEA.BLL.Factories;
using IKEA.BLL.Services.Interfaces;
using IKEA.DAL.Models;
using IKEA.DAL.Presistance.Repositories.Classess;
using IKEA.DAL.Presistance.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Classess
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IunitOfWork _unitOfWork ;
        public DepartmentService(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<DepartmentDto> GetAllDepaartment()
        {
            var Departments = _unitOfWork.DepartmentRepository.GetAll();

            return Departments.Select(D => D.ToDepartmentsDto());
        }
        //Get By Id:
        public DepartmentDetailsDto? GetDartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            return department is null ? null : department.ToDepartmentDetailsDto();
        }

        //Add
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            _unitOfWork. DepartmentRepository.Add(department);
            return _unitOfWork.SaveChanges();
        }
        //Update

        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {

            _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        //Delete
        public bool DeleteDepartment(int id)
        {
            var Department = _unitOfWork.DepartmentRepository.GetById(id);
            if (Department is null) { return false; }
            else
            {
                _unitOfWork.DepartmentRepository.Remove(Department);
                return _unitOfWork.SaveChanges() > 0;

            }
        }
    }
}
