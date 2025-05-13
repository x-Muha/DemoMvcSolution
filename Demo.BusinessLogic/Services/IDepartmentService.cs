using Demo.BusinessLogic.DataTransferObjects;

namespace Demo.BusinessLogic.Services
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDTO departmentDTO);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDTO> GetAllDepartmetns();
        DepartmentDetailsDTO? GetDepartmentById(int id);
        int UpdateDepartment(UpdatedDepartmentDTO departmentDTO);
    }
}