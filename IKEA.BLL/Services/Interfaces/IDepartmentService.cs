using IKEA.BLL.DTOS.Department;

namespace IKEA.BLL.Services.Interfaces
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepaartment();
        DepartmentDetailsDto? GetDartmentById(int id);
        int UpdateDepartment(UpdateDepartmentDto departmentDto);
    }
}