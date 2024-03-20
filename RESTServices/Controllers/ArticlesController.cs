using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;

namespace RESTServices.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArticlesController : ControllerBase
	{
		private readonly IArticleBLL _articleBLL;
		public ArticlesController(IArticleBLL articleBLL)
		{
			_articleBLL = articleBLL;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = _articleBLL.GetArticleWithCategory();

			if (result == null)
			{
				return NotFound();
			}

			if (result.Count() == 0)
			{
				return NoContent();
			}

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = _articleBLL.GetArticleById(id);

			if (result == null)
			{
				return NotFound();
			}

			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				_articleBLL.Delete(id);
				return Ok("Berhasil delete artikel");
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] ArticleCreateDTO articleDTO)
		{
			try
			{
				_articleBLL.Insert(articleDTO);
				return Ok("Artikel berhasil dibuat");
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] ArticleUpdateDTO articleDTO)
		{
			try
			{
				var article = _articleBLL.GetArticleById(id);
				if (article == null)
				{
					return NotFound();
				}

				articleDTO.ArticleID = id;
				_articleBLL.Update(articleDTO);
				return Ok("Artikel berhasil diupdate");
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("GetArticleByCategory/{id}")]
		public async Task<IActionResult> GetArticleByCategory(int id)
		{
			var result = _articleBLL.GetArticleByCategory(id);

			if (result == null)
			{
				return NotFound();
			}

			if (result.Count() == 0)
			{
				return NoContent();
			}

			return Ok(result);
		}

	}
}
