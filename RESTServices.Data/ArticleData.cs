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
	public class ArticleData : IArticleData
	{
		private readonly AppDbContext _context;
		public ArticleData(AppDbContext context)
		{
			_context = context;
		}
		public async Task<bool> Delete(int id)
		{
			try
			{
				var article = await GetById(id);
				if (article == null)
				{
					throw new Exception("Article not found");
				}
				_context.Articles.Remove(article);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<IEnumerable<Article>> GetAll()
		{
			var articles = await _context.Articles.ToListAsync();
			if (articles == null || articles.Count() == 0)
			{
				throw new Exception("No articles found");
			}
			return articles;
		}

		public async Task<IEnumerable<Article>> GetArticleByCategory(int categoryId)
		{
			var articles = await _context.Articles.Where(x => x.CategoryId == categoryId).ToListAsync();
			if (articles == null || articles.Count() == 0)
			{
				throw new Exception("No articles found");
			}
			return articles;
		}

		public async Task<IEnumerable<Article>> GetArticleWithCategory()
		{
			var articles = await _context.Articles.Include(x => x.Category).ToListAsync();
			if (articles == null || articles.Count() == 0)
			{
				throw new Exception("No articles found");
			}
			return articles;
		}

		public async Task<Article> GetById(int id)
		{
			var article = await _context.Articles.Include(x => x.Category).FirstOrDefaultAsync(x => x.ArticleId == id);
			if (article == null)
			{
				throw new Exception("Article not found");
			}
			return article;
		}

		public async Task<int> GetCountArticles()
		{
			var count = await _context.Articles.CountAsync();
			return count;
		}

		public async Task<IEnumerable<Article>> GetWithPaging(int pageNumber, int pageSize)
		{
			var articles = await _context.Articles.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
			if (articles == null || articles.Count() == 0)
			{
				throw new Exception("No articles found");
			}
			return articles;
		}

		public async Task<Article> Insert(Article entity)
		{
			try
			{
				_context.Articles.Add(entity);
				await _context.SaveChangesAsync();
				return entity;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Article> InsertArticleWithCategory(Article article)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					_context.Articles.Add(article);
					await _context.SaveChangesAsync();

					var category = new Category
					{
						CategoryName = article.Category.CategoryName
					};
					_context.Categories.Add(category);
					await _context.SaveChangesAsync();

					article.CategoryId = category.CategoryId;
					await _context.SaveChangesAsync();

					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					throw new Exception(ex.Message);
				}

				return article;
			}
		}

		public async Task<int> InsertWithIdentity(Article article)
		{
			try
			{
				_context.Articles.Add(article);
				await _context.SaveChangesAsync();
				return article.ArticleId;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Article> Update(Article entity)
		{
			try
			{
				var article = await _context.Articles.FirstOrDefaultAsync(x => x.ArticleId == entity.ArticleId);
				if (article == null)
				{
					throw new ArgumentException("Article not found");
				}
				article.Title = entity.Title;
				article.Details = entity.Details;
				article.PublishDate = entity.PublishDate;
				article.CategoryId = entity.CategoryId;
				article.IsApproved = entity.IsApproved;
				article.Pic = entity.Pic;
				await _context.SaveChangesAsync();
				return article;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
