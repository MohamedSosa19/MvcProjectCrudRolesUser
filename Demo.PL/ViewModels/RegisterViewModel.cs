﻿using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class RegisterViewModel
	{
		public string FName { get; set; }
		public string LName { get; set; }

		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; } // Changed to "Password"

		[Required(ErrorMessage = "ConfirmPassword is Required")]
		[Compare("Password", ErrorMessage = "Confirm Password does not match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get; set; }
	}
}
