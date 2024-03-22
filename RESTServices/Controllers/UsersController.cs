using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RESTServices.BLL.DTOs;
using RESTServices.BLL.Interfaces;
using RESTServices.Helpers;
using RESTServices.ViewModels;

namespace RESTServices.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserBLL _userBLL;
		private readonly AppSettings _appSettings;
		public UsersController(IUserBLL userBLL, IOptions<AppSettings> appSettings)
		{
			_userBLL = userBLL;
			_appSettings = appSettings.Value;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _userBLL.GetAll();
			if (result == null || result.Count() == 0)
			{
				return NotFound();
			}
			return Ok(result);
		}

		[HttpGet("{username}")]
		public async Task<IActionResult> GetByUsername(string username)
		{
			try
			{
				var result = await _userBLL.GetByUsername(username);
				if (result == null)
				{
					return NotFound();
				}
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create(UserCreateDTO user)
		{
			try
			{
				var result = await _userBLL.Insert(user);
				if (result == null)
				{
					return BadRequest();
				}
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDTO user)
		{
			try
			{
				var result = await _userBLL.Login(user.Username, user.Password);
				if (result == null)
				{
					return BadRequest();
				}
				List<Claim> claims = new List<Claim>();
				claims.Add(new Claim(ClaimTypes.Name, result.Username));
				foreach (var role in result.Roles)
				{
					claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
				}
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(claims),
					Expires = DateTime.UtcNow.AddHours(1),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				var userWithToken = new UserWithToken
				{
					Username = result.Username,
					FirstName = result.FirstName,
					LastName = result.LastName,
					Email = result.Email,
					Address = result.Address,
					Telp = result.Telp,
					Roles = result.Roles,
					Token = tokenHandler.WriteToken(token)
				};
				return Ok(userWithToken);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("changePassword")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO user)
		{
			try
			{
				var result = await _userBLL.ChangePassword(user.Username, user.NewPassword);
				if (result == null)
				{
					return BadRequest();
				}
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}
}
