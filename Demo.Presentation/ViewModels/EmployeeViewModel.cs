using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels
{
    public class EmployeeViewModel
    {
        //string is required by default but added to change error msg
        [Required(ErrorMessage = "Name Can't Be Null")]
        [MaxLength(50, ErrorMessage = "Max length should be 50 character")]
        [MinLength(5, ErrorMessage = "Min length should be 5 characters")]
        public string Name { get; set; } = null!;
        [Range(22, 35)]
        public int? Age { get; set; }
        //Ex: 18-aAAAA-Bbbbb-cCCCC , 789-WestNorth-NorthWest-street
        [RegularExpression("^[1-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$",
           ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
    }
}
