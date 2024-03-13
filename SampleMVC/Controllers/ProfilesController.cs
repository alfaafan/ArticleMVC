using Microsoft.AspNetCore.Mvc;

namespace SampleMVC.Controllers
{
	public class ProfilesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
