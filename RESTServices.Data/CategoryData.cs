using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RESTServices.Data.Interfaces;
using RESTServices.Domain;

namespace RESTServices.Data
{
	public class CategoryData : ICategoryData
	{
		private AppDbContext _context;
		public CategoryData(AppDbContext context)
		{
			_context = context;
		}
		public async Task<bool> Delete(int id)
		{
			var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
			if (category == null)
			{
				throw new Exception("Category not found");
			}
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<Category>> GetAll()
		{
			var categories = await _context.Categories.ToListAsync();
			if (categories == null)
			{
				throw new Exception("No categories found");
			}
			return categories;
		}

		public async Task<Category> GetById(int id)
		{
			var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
			if (category == null)
			{
				throw new Exception("Category not found");
			}
			return category;
		}

		public async Task<IEnumerable<Category>> GetByName(string name)
		{
			var categories = await _context.Categories.Where(x => x.CategoryName.Contains(name)).ToListAsync();
			if (categories == null)
			{
				throw new Exception("No categories found");
			}
			return categories;
		}

		public async Task<int> GetCountCategories(string name)
		{
			var count = await _context.Categories.Where(x => x.CategoryName.Contains(name)).CountAsync();
			return count;
		}

		public async Task<IEnumerable<Category>> GetWithPaging(int pageNumber, int pageSize, string name)
		{
			var categories = await _context.Categories.Where(x => x.CategoryName.Contains(name)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
			if (categories == null)
			{
				throw new Exception("No categories found");
			}
			return categories;
		}

		public async Task<Category> Insert(Category entity)
		{
			_context.Categories.Add(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<int> InsertWithIdentity(Category category)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();
			return category.CategoryId;
		}

		public async Task<Category> Update(Category entity)
		{
			var category = await GetById(entity.CategoryId);
			if (category == null)
			{
				throw new ArgumentException("Category not found");
			}

			category.CategoryName = entity.CategoryName;
			await _context.SaveChangesAsync();
			return category;
		}
	}
}
