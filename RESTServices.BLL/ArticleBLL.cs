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
	public class ArticleBLL : IArticleBLL
	{
		private readonly IArticleData _articleData;
		private readonly IMapper _mapper;
		public ArticleBLL(IArticleData articleData, IMapper mapper)
		{
			_articleData = articleData;
			_mapper = mapper;
		}
		public async Task<bool> Delete(int id)
		{
			try
			{
				var deleted = await _articleData.Delete(id);
				return deleted;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<IEnumerable<ArticleDTO>> GetArticleByCategory(int categoryId)
		{
			var articles = await _articleData.GetArticleByCategory(categoryId);
			if (articles == null || articles.Count() == 0)
			{
				throw new Exception("No articles found");
			}
			return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
		}

		public async Task<ArticleDTO> GetArticleById(int id)
		{
			var article = await _articleData.GetById(id);
			if (article == null)
			{
				throw new Exception("Article not found");
			}
			return _mapper.Map<ArticleDTO>(article);
		}

		public async Task<IEnumerable<ArticleDTO>> GetArticleWithCategory()
		{
			var articles = await _articleData.GetArticleWithCategory();
			if (articles == null || articles.Count() == 0)
			{
				throw new Exception("No articles found");
			}
			return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
		}

		public async Task<ArticleDTO> Insert(ArticleCreateDTO article)
		{
			try
			{
				var articleObject = _mapper.Map<Article>(article);
				var newArticle = await _articleData.Insert(articleObject);
				return _mapper.Map<ArticleDTO>(newArticle);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<int> InsertWithIdentity(ArticleCreateDTO article)
		{
			try
			{
				var articleObject = _mapper.Map<Article>(article);
				var newArticle = await _articleData.InsertWithIdentity(articleObject);
				return newArticle;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<ArticleDTO> Update(ArticleUpdateDTO article)
		{
			try
			{
				var articleObject = _mapper.Map<Article>(article);
				var updatedArticle = await _articleData.Update(articleObject);
				return _mapper.Map<ArticleDTO>(updatedArticle);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
