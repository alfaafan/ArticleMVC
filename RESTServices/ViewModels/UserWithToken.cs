using RESTServices.BLL.DTOs;

namespace RESTServices.ViewModels
{
	public class UserWithToken
	{
		public string? Username { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Address { get; set; }
		public string? Email { get; set; }
		public string? Telp { get; set; }

		public IEnumerable<RoleDTO>? Roles { get; set; }
		public string? Token { get; set; }
	}
}
