using RESTServices.BLL.DTOs;
using System.Collections.Generic;

namespace RESTServices.BLL.Interfaces
{
	public interface IArticleBLL
	{
		Task<ArticleDTO> Insert(ArticleCreateDTO article);
		Task<IEnumerable<ArticleDTO>> GetArticleWithCategory();
		Task<IEnumerable<ArticleDTO>> GetArticleByCategory(int categoryId);
		Task<int> InsertWithIdentity(ArticleCreateDTO article);
		Task<ArticleDTO> Update(ArticleUpdateDTO article);
		Task<bool> Delete(int id);
		Task<ArticleDTO> GetArticleById(int id);
	}
}
