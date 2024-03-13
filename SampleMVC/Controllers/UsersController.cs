using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MyWebFormApp.BLL;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;
using MyWebFormApp.BO;

namespace SampleMVC.Controllers
{
	public class UsersController : Controller
	{
		private readonly IUserBLL _userBLL;
		public UsersController()
		{
			_userBLL = new UserBLL();
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(UserCreateDTO user)
		{
			if (!ModelState.IsValid)
			{
				return View(user);
			}

			try
			{
				_userBLL.Insert(user);
				ViewBag.Message = @"<div class=""alert alert-success"" role=""alert"">User has been registered</div>";
			}
			catch (Exception)
			{
				ViewBag.Message = @"<div class=""alert alert-danger"" role=""alert"">Failed to register user</div>";
				return View(user);
			}

			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginDTO user)
		{
			if (!ModelState.IsValid)
			{
				return View(user);
			}

			try
			{
				var userLogin = _userBLL.LoginMVC(user);
				if (userLogin != null)
				{
					StringBuilder roles = new StringBuilder();
					foreach (var role in userLogin.Roles)
					{
						roles.Append(role.RoleName + ",");
					}

					HttpContext.Session.SetString("Roles", roles.ToString());

					ViewBag.Message = @"<div class=""alert alert-success"" role=""alert"">Login success</div>";

					var userDTOSerialized = JsonSerializer.Serialize(userLogin);
					HttpContext.Session.SetString("User", userDTOSerialized);
					HttpContext.Session.SetString("Username", userLogin.Username.ToString());
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ViewBag.Message = @"<div class=""alert alert-danger"" role=""alert"">Login failed: Invalid username or password</div>";
					return View();
				}
			}
			catch (Exception)
			{
				ViewBag.Message = @"<div class=""alert alert-danger"" role=""alert"">Login failed</div>";
				return View();
			}

		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login");
		}
	}
}
