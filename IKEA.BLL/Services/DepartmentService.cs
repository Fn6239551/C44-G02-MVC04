using IKEA.BLL.DTOS;
using IKEA.BLL.Factories;
using IKEA.DAL.Models;
using IKEA.DAL.Presistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services
{
    public class DepartmentService(IDepartmentRepository departmentRepository) : IDepartmentService
    {
        // public DepartmentService(DepartmentRepository departmentRepository) { }
        //DepartmentRepository departmentRepository = new DepartmentRepository();
        //Get All
        public IEnumerable<DepartmentDto> GetAllDepaartment()
        {
            var Departments = departmentRepository.GetAll();

            return Departments.Select(D => D.ToDepartmentsDto());
        }
        //Get By Id:
        public DepartmentDetailsDto? GetDartmentById(int id)
        {
            var department = departmentRepository.GetById(id);
            return department is null ? null : department.ToDepartmentDetailsDto();
        }

        //Add
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            return departmentRepository.Add(department);
        }
        //Update

        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {

            return departmentRepository.Update(departmentDto.ToEntity());
        }

        //Delete
        public bool DeleteDepartment(int id)
        {
            var Department = departmentRepository.GetById(id);
            if (Department is null) { return false; }
            else
            {
                int Result = departmentRepository.Remove(Department);
                return Result > 0 ? true : false;

            }
        }
    }
}
