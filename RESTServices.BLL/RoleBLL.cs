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
	public class RoleBLL : IRoleBLL
	{
		private readonly IRoleData _roleData;
		private readonly IMapper _mapper;
		public RoleBLL(IRoleData roleData, IMapper mapper)
		{
			_roleData = roleData;
			_mapper = mapper;
		}

		public async Task<RoleDTO> AddRole(string roleName)
		{
			var roleModel = new Role
			{
				RoleName = roleName
			};
			try
			{
				var role = await _roleData.Insert(roleModel);
				return _mapper.Map<RoleDTO>(role);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Task> AddUserToRole(string username, int roleId)
		{
			try
			{
				await _roleData.AddUserToRole(username, roleId);
				return Task.CompletedTask;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<IEnumerable<RoleDTO>> GetAllRoles()
		{
			try
			{
				var roles = await _roleData.GetAll();
				return _mapper.Map<IEnumerable<RoleDTO>>(roles);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
