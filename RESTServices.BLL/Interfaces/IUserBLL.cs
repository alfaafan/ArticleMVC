using RESTServices.BLL.DTOs;

namespace RESTServices.BLL.Interfaces
{
	public interface IUserBLL
	{
		Task<Task> ChangePassword(string username, string newPassword);
		Task<bool> Delete(string username);
		Task<IEnumerable<UserDTO>> GetAll();
		Task<UserDTO> GetByUsername(string username);
		Task<UserDTO> Insert(UserCreateDTO entity);
		Task<UserDTO> Login(string username, string password);
		Task<UserDTO> LoginMVC(LoginDTO loginDTO);

		Task<UserDTO> GetUserWithRoles(string username);
		Task<IEnumerable<UserDTO>> GetAllWithRoles();
	}
}