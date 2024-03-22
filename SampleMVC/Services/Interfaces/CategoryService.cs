using RESTServices.BLL.DTOs;
using System.Text;
using System.Text.Json;

namespace SampleMVC.Services.Interfaces
{
	public class CategoryService : ICategoryService
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;

		public CategoryService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_configuration = configuration;
		}

		private string GetBaseURL()
		{
			return _configuration.GetSection("BaseUrl").Value + "/Categories";
		}

		public async Task<bool> Delete(int id)
		{
			var httpResponse = await _httpClient.DeleteAsync($"{GetBaseURL()}/{id}");
			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new Exception("Cannot delete category");
			}
			return true;
		}

		public async Task<IEnumerable<CategoryDTO>> GetAll()
		{
			var httpResponse = await _httpClient.GetAsync(GetBaseURL());

			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new Exception("Cannot retrieve categories");
			}

			var content = await httpResponse.Content.ReadAsStringAsync();
			var categories = JsonSerializer.Deserialize<IEnumerable<CategoryDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return categories;
		}

		public async Task<CategoryDTO> GetById(int id)
		{
			var httpResponse = await _httpClient.GetAsync($"{GetBaseURL()}/{id}");
			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new Exception("Cannot retrieve category");
			}
			var content = await httpResponse.Content.ReadAsStringAsync();
			var category = JsonSerializer.Deserialize<CategoryDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return category;
		}

		public async Task<CategoryDTO> Insert(CategoryCreateDTO categoryDTO)
		{
			var json = JsonSerializer.Serialize(categoryDTO);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var httpResponse = await _httpClient.PostAsync(GetBaseURL(), content);

			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new Exception("Cannot insert category");
			}

			var responseContent = await httpResponse.Content.ReadAsStringAsync();

			var category = JsonSerializer.Deserialize<CategoryDTO>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return category;
		}

		public async Task<CategoryDTO> Update(CategoryUpdateDTO categoryDTO)
		{
			var json = JsonSerializer.Serialize(categoryDTO);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var httpResponse = await _httpClient.PutAsync($"{GetBaseURL()}/{categoryDTO.CategoryID}", content);

			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new Exception("Cannot update category");
			}

			var responseContent = await httpResponse.Content.ReadAsStringAsync();

			var category = JsonSerializer.Deserialize<CategoryDTO>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return category;
		}

		public async Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber, int pageSize, string name = "")
		{
			try
			{
				var httpResponse = await _httpClient.GetAsync($"{GetBaseURL()}/GetWithPaging?pageNumber={pageNumber}&pageSize={pageSize}&name={name}");
				if (!httpResponse.IsSuccessStatusCode)
				{
					throw new Exception("Cannot retrieve categories");
				}
				var content = await httpResponse.Content.ReadAsStringAsync();
				var categories = JsonSerializer.Deserialize<IEnumerable<CategoryDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
				return categories;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<int> GetCountCategories(string name = "")
		{
			var httpResponse = await _httpClient.GetAsync($"{GetBaseURL()}/GetCountCategories?name={name}");
			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new Exception("Cannot retrieve categories");
			}
			var content = await httpResponse.Content.ReadAsStringAsync();
			var count = JsonSerializer.Deserialize<int>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return count;
		}
	}
}
