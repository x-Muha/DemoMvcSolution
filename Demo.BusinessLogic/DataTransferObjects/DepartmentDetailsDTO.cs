using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Models;

namespace Demo.BusinessLogic.DataTransferObjects
{
    public class DepartmentDetailsDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Id { get; set; } // Pk
        public int CreatedBy { get; set; } // User Id
        public DateOnly CreatedOn { get; set; }
        public int LastModifiedBy { get; set; } // User Id
        public DateOnly LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; } //Soft Delete
    }
}
