using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDTOs;
using Demo.BusinessLogic.Factories;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {
        // Get All Departments
        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            // Using Extension Method
            return departments.Select(D => D.ToDepartmentDTO());
        }

        // Get Department By Id
        public DepartmentDetailsDTO? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            // Using Extension method
            return department is null ? null : department.ToDepartmentDetailsDTO();
        }

        // Add New Department
        public int AddDepartment(CreatedDepartmentDTO departmentDTO)
        {
            var department = departmentDTO.ToEntity();
            return _departmentRepository.Add(department);
        }

        // Update Department    Single Line with Fat Arrow
        public int UpdateDepartment(UpdatedDepartmentDTO departmentDTO)
        => _departmentRepository.Update(departmentDTO.ToEntity());

        // Delete Department
        public bool DeleteDepartment(int id)
        {
            var dept = _departmentRepository.GetById(id);
            if (dept is null) return false;
            int res = _departmentRepository.Remove(dept);
            return res > 0 ? true : false;
        }
    }
}
