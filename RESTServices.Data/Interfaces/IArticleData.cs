﻿using RESTServices.Domain;

namespace RESTServices.Data.Interfaces;

public interface IArticleData : ICrudData<Article>
{
	Task<IEnumerable<Article>> GetArticleWithCategory();
	Task<IEnumerable<Article>> GetArticleByCategory(int categoryId);
	Task<IEnumerable<Article>> GetWithPaging(int pageNumber, int pageSize);
	Task<int> GetCountArticles();
	Task<int> InsertWithIdentity(Article article);

	Task<Article> InsertArticleWithCategory(Article article);
}
