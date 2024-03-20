using System.ComponentModel.DataAnnotations;

namespace RESTServices.BLL.DTOs
{
	public class UserCreateDTO
	{
		[Required(ErrorMessage = "Username is required")]
		public string Username { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Repassword is required")]
		[Compare("Password", ErrorMessage = "Password and Repassword must be the same")]
		[DataType(DataType.Password)]
		public string Repassword { get; set; }
		[Required(ErrorMessage = "FirstName is required")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "LastName is required")]
		public string LastName { get; set; }
		public string Address { get; set; }
		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Telp is required")]
		public string Telp { get; set; }

		public string SecurityQuestion { get; set; }
		public string SecurityAnswer { get; set; }
	}
}
