using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTServices.BLL.DTOs;
using RESTServices.BLL.Interfaces;

namespace RESTServices.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserBLL _userBLL;
		public UsersController(IUserBLL userBLL)
		{
			_userBLL = userBLL;
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
				return Ok(result);
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
