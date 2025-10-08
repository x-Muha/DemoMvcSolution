using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;

namespace Demo.BusinessLogic.DataTransferObjects.EmployeeDTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string EmpGender { get; set; }
        [Display(Name = "Employee Type")]
        public string EmpType { get; set; }
        public string? Department { get; set; }

    }
}
