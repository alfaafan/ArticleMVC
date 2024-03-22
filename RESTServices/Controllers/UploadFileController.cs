using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RESTServices.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UploadFileController : ControllerBase
	{
		[Authorize(Roles = "contributor")]
		[HttpPost]
		public async Task<IActionResult> UploadFile(IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				return BadRequest("File is null");
			}

			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

			using (var stream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			return Ok();
		}
	}
}
