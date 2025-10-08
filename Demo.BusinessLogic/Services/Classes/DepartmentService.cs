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
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {
        // Get All Departments
        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            var departments = _unitOfWork.departmentRepository.GetAll();
            // Using Extension Method
            return departments.Select(D => D.ToDepartmentDTO());
        }

        // Get Department By Id
        public DepartmentDetailsDTO? GetDepartmentById(int id)
        {
            var department = _unitOfWork.departmentRepository.GetById(id);
            // Using Extension method
            return department is null ? null : department.ToDepartmentDetailsDTO();
        }

        // Add New Department
        public int AddDepartment(CreatedDepartmentDTO departmentDTO)
        {
            var department = departmentDTO.ToEntity();
            _unitOfWork.departmentRepository.Add(department);
            return _unitOfWork.SaveChanges();
        }

        // Update Department    Single Line with Fat Arrow
        public int UpdateDepartment(UpdatedDepartmentDTO departmentDTO)
        {
            _unitOfWork.departmentRepository.Update(departmentDTO.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        // Delete Department
        public bool DeleteDepartment(int id)
        {
            var dept = _unitOfWork.departmentRepository.GetById(id);
            if (dept is null) return false;
            _unitOfWork.departmentRepository.Remove(dept);

            return _unitOfWork.SaveChanges() > 0 ? true : false;
        }
    }
}
