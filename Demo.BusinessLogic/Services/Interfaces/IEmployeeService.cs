using Demo.BusinessLogic.DataTransferObjects.EmployeeDTOs;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        int AddEmployee(CreatedEmployeeDTO employeeDTO);
        IEnumerable<EmployeeDTO> GetAllEmployees(bool WithTracking= false);
        EmployeeDetailsDTO? GetEmployeeDetails(int id);
        int UpdateEmployee(UpdatedEmployeeDTO employeeDTO);
        bool DeleteEmployee(int id);
    }
}