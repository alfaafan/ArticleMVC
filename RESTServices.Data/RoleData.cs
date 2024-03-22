using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RESTServices.Data.Interfaces;
using RESTServices.Domain;

namespace RESTServices.Data
{
	public class RoleData : IRoleData
	{
		private readonly AppDbContext _context;
		public RoleData(AppDbContext appDbContext)
		{
			_context = appDbContext;
		}

		public async Task<Task> AddUserToRole(string username, int roleId)
		{
			try
			{
				var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
				if (user == null)
				{
					throw new Exception("User not found");
				}

				var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleId == roleId);
				if (role == null)
				{
					throw new Exception("Role not found");
				}

				role.Usernames.Add(user);
				await _context.SaveChangesAsync();
				return Task.CompletedTask;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> Delete(int id)
		{
			var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleId == id);
			if (role == null)
			{
				throw new Exception("Role not found");
			}
			_context.Roles.Remove(role);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<Role>> GetAll()
		{
			var roles = await _context.Roles.ToListAsync();
			if (roles == null)
			{
				throw new Exception("No roles found");
			}
			return roles;
		}

		public async Task<Role> GetById(int id)
		{
			var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleId == id);
			if (role == null)
			{
				throw new Exception("Role not found");
			}
			return role;
		}

		public async Task<Role> Insert(Role entity)
		{
			try
			{
				await _context.Roles.AddAsync(entity);
				await _context.SaveChangesAsync();
				return entity;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Role> Update(Role entity)
		{
			try
			{
				_context.Roles.Update(entity);
				await _context.SaveChangesAsync();
				return entity;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
