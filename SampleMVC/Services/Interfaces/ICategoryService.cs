using RESTServices.BLL.DTOs;

namespace SampleMVC.Services.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryDTO>> GetAll();
		Task<CategoryDTO> GetById(int id);
		Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber, int pageSize, string name = "");
		Task<int> GetCountCategories(string name = "");
		Task<CategoryDTO> Insert(CategoryCreateDTO categoryDTO);
		Task<bool> Delete(int id);
		Task<CategoryDTO> Update(CategoryUpdateDTO categoryDTO);
	}
}
