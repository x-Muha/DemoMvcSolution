using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDTOs;
using Demo.DataAccess.Models.EmployeeModel;

namespace Demo.BusinessLogic.Factories
{
    static public class EmployeeFactory
    {
        public static EmployeeDTO ToEmployeeDTO(this Employee employee)
        {
            return new EmployeeDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                IsActive = employee.IsActive,
                Salary = employee.Salary,
                Email = employee.Email,
                EmpGender= employee.Gender.ToString(),
                EmpType = employee.EmployeeType.ToString(),
            };
        }

        public static EmployeeDetailsDTO ToEmployeeDetailsDTO(this Employee employee)
        {
            return new EmployeeDetailsDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                IsActive = employee.IsActive,
                Address = employee.Address,
                Salary = employee.Salary,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = DateOnly.FromDateTime(employee.HiringDate),
                Gender = employee.Gender.ToString(),
                EmployeeType = employee.EmployeeType.ToString(),
                CreatedBy = 1,
                CreatedOn = employee.CreatedOn,
                LastModifiedBy = 1,
                LastModifiedOn = employee.LastModifiedOn
            };
        }
        //public static Employee ToEntity(this CreatedEmployeeDTO createdEmployee)
        //{
        //    return new Employee()
        //    {
        //        Name = createdEmployee.Name,
        //        Age = createdEmployee.Age,
        //        Address = createdEmployee.Address,
        //        IsActive = createdEmployee.IsActive,
        //        Salary = createdEmployee.Salary,
        //        Email = createdEmployee.Email,
        //        PhoneNumber = createdEmployee.PhoneNumber,
        //        HiringDate = createdEmployee.HiringDate,
        //        Gender = createdEmployee.Gender,
        //        EmployeeType = createdEmployee.EmployeeType,
        //        CreatedBy = 1,
        //        LastModifiedBy= 1

        //    };
        //}
        //public static Employee ToEntity(this UpdatedEmployee updatedEmployee)
        //{
        //    return new Employee()
        //    {
        //        Name = updatedEmployee.Name,
        //        Age = updatedEmployee.Age,
        //        Address = updatedEmployee.Address,
        //        IsActive = updatedEmployee.IsActive,
        //        Salary = updatedEmployee.Salary,
        //        Email = updatedEmployee.Email,
        //        PhoneNumber = updatedEmployee.PhoneNumber,
        //        HiringDate = updatedEmployee.HiringDate,
        //        Gender = updatedEmployee.Gender,
        //        EmployeeType = updatedEmployee.EmployeeType,
        //        CreatedBy = 1,
        //        LastModifiedBy= 1

        //    };
        //}
        
    }
}
