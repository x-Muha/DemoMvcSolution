using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDTOs;
using Demo.BusinessLogic.Factories;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services
{
    public class EmployeeService(IEmployeeRepository _employeeRepository)
    {
        public IEnumerable<EmployeeDTO> GetAll()
        {
            var employees = _employeeRepository.GetAll();
            return employees.Select(e=> e.ToEmployeeDTO());
        }
        public EmployeeDetailsDTO GetEmployeeDetails(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null) return null!;
            return employee.ToEmployeeDetailsDTO();
        }
        public int AddEmployee(CreatedEmployeeDTO employeeDTO)
        {
            var employee = employeeDTO.ToEntity();
            return _employeeRepository.Add(employee);
        }
        public int UpdateEmployee(UpdatedEmployee employeeDTO)
        {
            var employee = employeeDTO.ToEntity();
            return _employeeRepository.Update(employee);
        }
    }
}
