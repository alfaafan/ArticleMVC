using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebFormApp.BLL;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;
using MyWebFormApp.BO;

namespace SampleMVC.Controllers
{
	public class UsersController : Controller
	{
		private readonly IUserBLL _userBLL;
		private readonly IRoleBLL _roleBLL;
		public UsersController(IUserBLL userBLL, IRoleBLL roleBLL)
		{
			_userBLL = userBLL;
			_roleBLL = roleBLL;
		}
		public IActionResult Index()
		{
			if (HttpContext.Session.GetString("User") == null)
			{
				return RedirectToAction("Login");
			}

			if (!HttpContext.Session.GetString("Roles").ToString().Contains("admin"))
			{
				return RedirectToAction("Index", "Home");
			}

			var users = _userBLL.GetAll();
			var listUsers = new SelectList(users, "Username", "Username");
			ViewBag.Users = listUsers;

			var roles = _roleBLL.GetAllRoles();
			var listRoles = new SelectList(roles, "RoleID", "RoleName");
			ViewBag.Roles = listRoles;


			var usersWithRoles = _userBLL.GetAllWithRoles();


			return View(usersWithRoles);
		}

		[HttpPost]
		public IActionResult Index(string Username, int RoleID)
		{
			try
			{
				_roleBLL.AddUserToRole(Username, RoleID);
				return RedirectToAction("Index");
			}
			catch (Exception e)
			{
				ViewBag.Message = "<div class=\"alert alert-danger\" role=\"alert\">" + e.Message + "</div>";
				return View();
			}
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

		public IActionResult Edit(string username)
		{
			var user = _userBLL.GetByUsername(username);
			return View(user);
		}
	}
}
