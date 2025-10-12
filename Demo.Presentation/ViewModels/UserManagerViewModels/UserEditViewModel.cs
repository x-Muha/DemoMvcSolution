using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.UserManagerViewModels
{
    public class UserEditViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
    }
}
