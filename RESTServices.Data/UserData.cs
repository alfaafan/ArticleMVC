using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RESTServices.Data.Helpers;
using RESTServices.Data.Interfaces;
using RESTServices.Domain;

namespace RESTServices.Data
{
	public class UserData : IUserData
	{
		private readonly AppDbContext _context;
		public UserData(AppDbContext context)
		{
			_context = context;
		}
		public async Task<Task> ChangePassword(string username, string newPassword)
		{
			try
			{
				var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
				if (user == null)
				{
					throw new Exception("User not found");
				}
				user.Password = Hasher.Hash(newPassword);
				await _context.SaveChangesAsync();
				return Task.CompletedTask;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Task<bool> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<User>> GetAll()
		{
			try
			{
				var users = await _context.Users.ToListAsync();
				if (users == null)
				{
					throw new Exception("No users found");
				}
				return users;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<IEnumerable<User>> GetAllWithRoles()
		{
			try
			{
				var users = await _context.Users.Include(x => x.Roles).ToListAsync();
				if (users == null)
				{
					throw new Exception("No users found");
				}
				return users;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Task<User> GetById(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<User> GetByUsername(string username)
		{
			try
			{
				var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
				if (user == null)
				{
					throw new Exception("User not found");
				}
				return user;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<User> GetUserWithRoles(string username)
		{
			try
			{
				var user = await _context.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Username == username);
				if (user == null)
				{
					throw new Exception("User not found");
				}
				return user;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<User> Insert(User entity)
		{
			try
			{
				var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == entity.Username);
				if (user != null)
				{
					throw new Exception("Username already exists");
				}
				await _context.Users.AddAsync(entity);
				await _context.SaveChangesAsync();
				return entity;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<User> Login(string username, string password)
		{
			try
			{
				var hashedPassword = Hasher.Hash(password);
				var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == hashedPassword);
				var userRoles = await _context.Roles.Where(ur => ur.Usernames.Contains(user)).ToListAsync();
				List<Role> roles = new List<Role>();
				foreach (var role in userRoles)
				{
					roles.Add(role);
				}
				if (user == null)
				{
					throw new Exception("Invalid username or password");
				}
				if (hashedPassword != user.Password)
				{
					throw new Exception("Invalid password");
				}
				user.Roles = roles;
				return user;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<User> Update(User entity)
		{
			try
			{
				var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == entity.Username);
				if (user == null)
				{
					throw new Exception("User not found");
				}
				user.Username = entity.Username;
				user.Password = Hasher.Hash(entity.Password);
				user.Email = entity.Email;
				user.Address = entity.Address;
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
