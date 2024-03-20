using RESTServices.BLL.DTOs;

namespace RESTServices.BLL.Interfaces
{
	public interface IRoleBLL
	{
		Task<IEnumerable<RoleDTO>> GetAllRoles();
		Task<RoleDTO> AddRole(string roleName);
		Task<Task> AddUserToRole(string username, int roleId);
	}
}
