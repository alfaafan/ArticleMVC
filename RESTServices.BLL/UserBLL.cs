using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RESTServices.BLL.DTOs;
using RESTServices.BLL.Interfaces;
using RESTServices.Data.Interfaces;
using RESTServices.Domain;

namespace RESTServices.BLL
{
	public class UserBLL : IUserBLL
	{
		private readonly IUserData _userData;
		private readonly IMapper _mapper;
		public UserBLL(IUserData userData, IMapper mapper)
		{
			_userData = userData;
			_mapper = mapper;
		}
		public async Task<Task> ChangePassword(string username, string newPassword)
		{
			try
			{
				var user = await _userData.GetByUsername(username);
				if (user == null)
				{
					throw new Exception("User not found");
				}
				user.Password = newPassword;
				await _userData.Update(user);
				return Task.CompletedTask;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Task<bool> Delete(string username)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<UserDTO>> GetAll()
		{
			try
			{
				var users = await _userData.GetAll();
				if (users == null || users.Count() == 0)
				{
					throw new Exception("No users found");
				}
				return _mapper.Map<IEnumerable<UserDTO>>(users);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<IEnumerable<UserDTO>> GetAllWithRoles()
		{
			try
			{
				var users = await _userData.GetAllWithRoles();
				if (users == null || users.Count() == 0)
				{
					throw new Exception("No users found");
				}
				return _mapper.Map<IEnumerable<UserDTO>>(users);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<UserDTO> GetByUsername(string username)
		{
			try
			{
				var user = await _userData.GetByUsername(username);
				if (user == null)
				{
					throw new Exception("User not found");
				}
				return _mapper.Map<UserDTO>(user);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<UserDTO> GetUserWithRoles(string username)
		{
			try
			{
				var user = await _userData.GetUserWithRoles(username);
				if (user == null)
				{
					throw new Exception("User not found");
				}
				return _mapper.Map<UserDTO>(user);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<UserDTO> Insert(UserCreateDTO entity)
		{
			try
			{
				var user = await _userData.Insert(_mapper.Map<User>(entity));
				return _mapper.Map<UserDTO>(user);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<UserDTO> Login(string username, string password)
		{
			try
			{
				var user = await _userData.Login(username, password);
				if (user == null)
				{
					throw new Exception("Invalid username or password");
				}
				return _mapper.Map<UserDTO>(user);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<UserDTO> LoginMVC(LoginDTO loginDTO)
		{
			try
			{
				var user = await _userData.Login(loginDTO.Username, loginDTO.Password);
				if (user == null)
				{
					throw new Exception("Invalid username or password");
				}
				return _mapper.Map<UserDTO>(user);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
