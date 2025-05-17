using System;
using System.Collections.Generic;
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
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}
