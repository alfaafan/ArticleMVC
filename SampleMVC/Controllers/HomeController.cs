using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MyWebFormApp.BLL.DTOs;
namespace SampleMVC.Controllers;

public class HomeController : Controller
{
    // Home/Index
    public IActionResult Index()
    {
        ViewData["Title"] = "Home Page";
        if (HttpContext.Session.GetString("User") != null)
        {
            var user = JsonSerializer.Deserialize<UserDTO>(HttpContext.Session.GetString("User"));
            ViewBag.Message = $"Welcome {user.FirstName} {user.LastName}!";
		}
        else
        {
            return RedirectToAction("Login", "Users");
        }
        return View();
    }

    [Route("/Hello/ASP")]
    public IActionResult HelloASP()
    {
        return Content("Hello ASP.NET Core MVC!");
    }

    // Home/About
    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return Content("This is the Contact action method...");
    }
}
