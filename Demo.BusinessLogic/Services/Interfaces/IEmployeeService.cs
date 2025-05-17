using Demo.BusinessLogic.DataTransferObjects.EmployeeDTOs;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        int AddEmployee(CreatedEmployeeDTO employeeDTO);
        IEnumerable<EmployeeDTO> GetAll(bool WithTracking);
        EmployeeDetailsDTO? GetEmployeeDetails(int id);
        int UpdateEmployee(UpdatedEmployeeDTO employeeDTO);
        bool DeleteEmployee(int id);
    }
}