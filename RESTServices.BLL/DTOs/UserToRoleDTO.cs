using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTServices.BLL.DTOs
{
	public class UserToRoleDTO
	{
		public required string Username { get; set; }
		public int RoleId { get; set; }
	}
}
