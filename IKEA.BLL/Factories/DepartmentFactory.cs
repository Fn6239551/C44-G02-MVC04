using IKEA.BLL.DTOS;
using IKEA.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Factories
{
    public static class DepartmentFactory
    {
        //Get All
        public static DepartmentDto ToDepartmentsDto(this Department D)
        {
            return new DepartmentDto()
            {
                DeptId = D.Id,
                Name = D.Name,
                Code = D.Code,
                DateOfCreation = D.CreatedOn
            };

        }

        //Get By Id
        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedBy = department.CreatedBy,
                CreatedOn = department.CreatedOn,
                LastModificationBy = department.LastModificationBy,
                LastModificationOn = department.LastModificationOn,
                IsDeleted = department.IsDeleted
            };
        }


        //Add
        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.CreatedOn

            };
        }

        //Update
        public static Department ToEntity(this UpdateDepartmentDto departmentDto)
        {
            return new Department()
            {
                Id =departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.CreatedOn

            };
        }

    }
}
