using RESTServices.BLL.DTOs;

namespace SampleMVC.Services.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryDTO>> GetAll();
		Task<CategoryDTO> GetById(int id);
		Task<CategoryDTO> Insert(CategoryCreateDTO categoryDTO);
		Task<bool> Delete(int id);
		Task<CategoryDTO> Update(CategoryUpdateDTO categoryDTO);
	}
}
