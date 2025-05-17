using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferObjects
{
    public class CreatedDepartmentDTO
    {
        [Required(ErrorMessage = "Name is Required !!")]
        public string Name { get; set; } = null!;
        [Required]
        [Range(100,int.MaxValue)]
        public string Code { get; set; } = null!;
        public DateOnly DateOfCreation { get; set; }
        public string? Description { get; set; }

    }
}
