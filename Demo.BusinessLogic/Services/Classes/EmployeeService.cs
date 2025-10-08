using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDTOs;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services
{
    public class EmployeeService(IEmployeeRepository _employeeRepository,IMapper _mapper) :IEmployeeService
    {
        public IEnumerable<EmployeeDTO> GetAllEmployees(string? EmployeeSearchName)
        {

            IEnumerable<Employee> employees;
            if(string.IsNullOrEmpty(EmployeeSearchName))
                employees = _employeeRepository.GetAll();
            else
                employees = _employeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            var employeesDTO = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(employees);
            return employeesDTO;
            // using the new overload GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)

            //var employees = _employeeRepository.GetAll(WithTracking);
            ////Auto Mapping
            ////Source: Employee | Destination: EmployeeDTO       || 1st Overload <Src,Dest>
            //return employeesDTO;
        }
        public EmployeeDetailsDTO? GetEmployeeDetails(int id)
        {
            var employee = _employeeRepository.GetById(id);
            //Auto Mapping || 2nd Overload <Dest>(object) will detect Src from object (bad performance)
            //return employee is null? null : _mapper.Map<EmployeeDetailsDTO>(employee);
            return employee is null? null : _mapper.Map<Employee,EmployeeDetailsDTO>(employee);
        }
        public int AddEmployee(CreatedEmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<CreatedEmployeeDTO,Employee>(employeeDTO);
            return _employeeRepository.Add(employee);
        }
        public int UpdateEmployee(UpdatedEmployeeDTO employeeDTO)
        =>_employeeRepository.Update(_mapper.Map<UpdatedEmployeeDTO, Employee>(employeeDTO));
        public bool DeleteEmployee(int id) //Soft Delete
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            employee.IsDeleted = true;
            return _employeeRepository.Update(employee) > 0 ? true : false;
        }
    }
}
