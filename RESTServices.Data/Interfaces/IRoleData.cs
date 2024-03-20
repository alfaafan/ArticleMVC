using RESTServices.Domain;

namespace RESTServices.Data.Interfaces
{
	public interface IRoleData : ICrudData<Role>
	{
		Task<Task> AddUserToRole(string username, int roleId);
	}
}
