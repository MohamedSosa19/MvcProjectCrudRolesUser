using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "NewPassword is required")]
        [MinLength(5, ErrorMessage = "Minimum Password Length is 5")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [MinLength(5, ErrorMessage = "Minimum Confirm Password Length is 5")]
        [Compare("NewPassword", ErrorMessage = "Password doesn't Match")]
        public string ConfirmPassword { get; set; }
    }
}
