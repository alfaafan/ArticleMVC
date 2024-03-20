using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTServices.BLL.DTOs;
using RESTServices.BLL.Interfaces;
using RESTServices.Helpers;

namespace RESTServices.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArticlesController : ControllerBase
	{
		private readonly IArticleBLL _articleBLL;
		private readonly IValidator<ArticleCreateDTO> _articleCreateValidator;
		private readonly IValidator<ArticleUpdateDTO> _articleUpdateValidator;
		public ArticlesController(IArticleBLL articleBLL, IValidator<ArticleUpdateDTO> articleUpdateValidator, IValidator<ArticleCreateDTO> articleCreateValidator)
		{
			_articleBLL = articleBLL;
			_articleUpdateValidator = articleUpdateValidator;
			_articleCreateValidator = articleCreateValidator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _articleBLL.GetArticleWithCategory();

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
			var result = await _articleBLL.GetArticleById(id);

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
				await _articleBLL.Delete(id);
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
				var validationResult = await _articleCreateValidator.ValidateAsync(articleDTO);
				if (!validationResult.IsValid)
				{
					Helper.AddToModelState(validationResult, ModelState);
					return BadRequest(ModelState);
				}
				var newArticle = await _articleBLL.Insert(articleDTO);
				return CreatedAtAction(nameof(Get), new { id = newArticle.ArticleID }, newArticle);
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
				var validationResult = await _articleUpdateValidator.ValidateAsync(articleDTO);
				if (!validationResult.IsValid)
				{
					Helper.AddToModelState(validationResult, ModelState);
					return BadRequest(ModelState);
				}
				var article = await _articleBLL.GetArticleById(id);
				if (article == null)
				{
					return NotFound();
				}

				articleDTO.ArticleID = id;
				var updatedArticle = await _articleBLL.Update(articleDTO);
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
			var result = await _articleBLL.GetArticleByCategory(id);

			if (result == null)
			{
				return NotFound();
			}

			return Ok(result);
		}

	}
}
