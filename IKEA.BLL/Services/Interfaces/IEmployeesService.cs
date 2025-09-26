using IKEA.BLL.DTOS.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Interfaces
{
    public interface IEmployeesService
    {
        IEnumerable<EmployeesDto> GetAllEmployees(bool withTracking);
        EmployeesDetailsDto? GetEmployeeById(int id);
        int CreateEmployee(CreatedEmployeeDto employeeDto);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
    }
}
