using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RESTServices.BLL.DTOs;
using RESTServices.BLL.Interfaces;
using RESTServices.Data.Interfaces;
using RESTServices.Domain;

namespace RESTServices.BLL
{
	public class CategoryBLL : ICategoryBLL
	{
		private ICategoryData _categoryData;
		private IMapper _mapper;
		public CategoryBLL(ICategoryData categoryData, IMapper mapper)
		{
			_categoryData = categoryData;
			_mapper = mapper;
		}

		public async Task<bool> Delete(int id)
		{
			try
			{
				var category = await _categoryData.GetById(id);
				if (category == null)
				{
					throw new ArgumentException("Category not found");
				}

				var deleted = await _categoryData.Delete(id);
				return deleted;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<IEnumerable<CategoryDTO>> GetAll()
		{
			var categories = await _categoryData.GetAll();
			if (categories == null || categories.Count() == 0)
			{
				throw new ArgumentException("No categories found");
			}
			return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
		}

		public async Task<CategoryDTO> GetById(int id)
		{
			var category = await _categoryData.GetById(id);
			if (category == null)
			{
				throw new Exception("Category not found");
			}
			return _mapper.Map<CategoryDTO>(category);
		}

		public async Task<IEnumerable<CategoryDTO>> GetByName(string name)
		{
			var categories = await _categoryData.GetByName(name);
			if (categories == null || categories.Count() == 0)
			{
				throw new ArgumentException("No categories found");
			}
			return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
		}

		public async Task<int> GetCountCategories(string name = "")
		{
			var count = await _categoryData.GetCountCategories(name);
			return count;
		}

		public async Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber, int pageSize, string name = "")
		{
			var categories = await _categoryData.GetWithPaging(pageNumber, pageSize, name);
			if (categories == null || categories.Count() == 0)
			{
				throw new ArgumentException("No categories found");
			}
			return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
		}

		public async Task<CategoryDTO> Insert(CategoryCreateDTO entity)
		{
			try
			{
				var category = _mapper.Map<Category>(entity);
				var inserted = await _categoryData.Insert(category);
				return _mapper.Map<CategoryDTO>(inserted);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<CategoryDTO> Update(CategoryUpdateDTO entity)
		{
			try
			{
				var existingCategory = await _categoryData.GetById(entity.CategoryID);

				var updateCategory = _mapper.Map<Category>(entity);

				var updated = await _categoryData.Update(updateCategory);

				var result = _mapper.Map<CategoryDTO>(updated);

				return result;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
