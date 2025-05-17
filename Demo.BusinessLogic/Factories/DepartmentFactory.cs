using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDTOs;
using Demo.DataAccess.Models.DepartmentModel;

namespace Demo.BusinessLogic.Factories
{
    static class DepartmentFactory
    {
        public static DepartmentDTO ToDepartmentDTO(this Department D)
        {
            return new DepartmentDTO
            {
                DeptId = D.Id,
                Name = D.Name,
                Description = D.Description!,
                Code = D.Code,
                DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)
            };
        }
        public static DepartmentDetailsDTO ToDepartmentDetailsDTO(this Department D)
        {
            return new DepartmentDetailsDTO()
            {
                Id = D.Id,
                Name = D.Name,
                Code = D.Code,
                Description = D.Description,
                CreatedOn = DateOnly.FromDateTime(D.CreatedOn),
                LastModifiedOn = DateOnly.FromDateTime(D.LastModifiedOn),
                IsDeleted = D.IsDeleted
            };
        }
        public static Department ToEntity(this CreatedDepartmentDTO DTO)
        {
            return new Department()
            {
                Name = DTO.Name,
                Code = DTO.Code,
                Description = DTO.Description,
                CreatedOn = DTO.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }
        // ToEntity Overload 
        public static Department ToEntity(this UpdatedDepartmentDTO DTO)
            => new Department() // OverLoad Using Fat arrow to return
            {
                Id = DTO.Id,
                Name = DTO.Name,
                Code = DTO.Code,
                Description = DTO.Description,
                CreatedOn = DTO.DateOfCreation.ToDateTime(new TimeOnly())
            };
    }
}
