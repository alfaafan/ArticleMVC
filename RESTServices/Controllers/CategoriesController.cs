using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTServices.BLL.DTOs;
using RESTServices.BLL.Interfaces;

namespace RESTServices.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryBLL _categoryBLL;
		public CategoriesController(ICategoryBLL categoryBLL)
		{
			_categoryBLL = categoryBLL;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _categoryBLL.GetAll();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _categoryBLL.GetById(id);
			if (result == null)
			{
				return NotFound();
			}
			return Ok(result);
		}

		[HttpGet("GetWithPaging")]
		public async Task<IActionResult> GetWithPaging(int pageNumber, int pageSize, string name = "")
		{
			var result = await _categoryBLL.GetWithPaging(pageNumber, pageSize, name);
			if (result == null)
			{
				return NotFound();
			}
			return Ok(result);
		}

		[HttpGet("GetCountCategories")]
		public async Task<IActionResult> GetCountCategories(string name = "")
		{
			var result = await _categoryBLL.GetCountCategories(name);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var deleted = await _categoryBLL.Delete(id);
				return Ok(deleted);
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CategoryCreateDTO categoryDTO)
		{
			try
			{
				var category = await _categoryBLL.Insert(categoryDTO);
				return Ok(category);
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] CategoryUpdateDTO categoryDTO)
		{
			try
			{
				categoryDTO.CategoryID = id;
				var updatedCategory = await _categoryBLL.Update(categoryDTO);
				if (updatedCategory == null)
				{
					return NotFound();
				}
				return Ok(updatedCategory);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("GetByName/{name}")]
		public async Task<IActionResult> GetByName(string name)
		{
			var result = await _categoryBLL.GetByName(name);
			if (result == null)
			{
				return NotFound();
			}

			return Ok(result);
		}

	}
}
