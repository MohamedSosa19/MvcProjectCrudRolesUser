using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "password is Required")]
		[DataType(DataType.Password)]
		public string password { get; set; }

        public bool RememberMe { get; set; }
    }
}
