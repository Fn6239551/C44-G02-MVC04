using IKEA.BLL.DTOS;

namespace IKEA.BLL.Services
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