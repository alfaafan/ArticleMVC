using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RESTServices.BLL.DTOs
{
	public class LoginDTO
	{
		[Required(ErrorMessage = "Username is required")]
		public string Username { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
