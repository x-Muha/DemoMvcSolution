using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;
    }
}
