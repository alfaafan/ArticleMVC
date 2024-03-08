using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebFormApp.BLL;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;

namespace SampleMVC.Controllers
{
	public class ArticlesController : Controller
	{
		private readonly IArticleBLL _articleBLL;
		private readonly ICategoryBLL _categoryBLL;
		public ArticlesController(IArticleBLL articleBLL, ICategoryBLL categoryBLL)
		{
			_articleBLL = articleBLL;
			_categoryBLL = categoryBLL;
		}
		// GET: ArticlesController
		public ActionResult Index()
		{
			ViewData["Title"] = "Article List";
			var articles = _articleBLL.GetArticleWithCategory();
			var categories = _categoryBLL.GetAll();

			ViewBag.Categories = categories;
			return View(articles);
		}

		[HttpPost]
		public ActionResult Index(int categoryID)
		{
			ViewData["Title"] = "Article List";

			ViewBag.CategoryID = categoryID;
			ViewBag.Categories = _categoryBLL.GetAll();
			var articles = _articleBLL.GetArticleByCategory(categoryID);

			return View(articles);
		}

		// GET: ArticlesController/Details/5
		public ActionResult Details(int id)
		{
			ArticleDTO article = _articleBLL.GetArticleById(id);
			ViewData["Title"] = article.Title;
			return View(article);
		}

		// GET: ArticlesController/Create
		public ActionResult Create()
		{
			ViewData["Title"] = "Create Article";
			ViewBag.Categories = _categoryBLL.GetAll();
			return View();
		}

		// POST: ArticlesController/Create
		[HttpPost]
		public ActionResult Create(ArticleCreateDTO article, IFormFile ImageArticle)
		{
			try
			{
				if (ImageArticle != null)
				{
					if (!Helper.IsImageFile(ImageArticle.FileName))
					{
						TempData["message"] = @"<div class='alert alert-danger'><strong>Error!</strong> File is not an image !</div>";
						return RedirectToAction("Index");
					}
					string fileName = $"{Guid.NewGuid()}_{ImageArticle.FileName}" + Path.GetExtension(ImageArticle.FileName);
					string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pics", fileName);

					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						ImageArticle.CopyTo(fileStream);
					}

					article.Pic = fileName;


				}

				_articleBLL.Insert(article);

				TempData["message"] = @"<div class='alert alert-success'><strong>Success!</strong> Add Article Success !</div>";
			}
			catch (Exception ex)
			{
				TempData["message"] = @"<div class='alert alert-danger'><strong>Error!</strong> " + ex.Message + "</div>";
			}

			return RedirectToAction("Index");
		}

		// GET: ArticlesController/Edit/5
		public ActionResult Edit(int id)
		{
			var article = _articleBLL.GetArticleById(id);
			ViewBag.Categories = _categoryBLL.GetAll();

			return View(article);
		}

		// POST: ArticlesController/Edit/5
		[HttpPost]
		public ActionResult Edit(ArticleUpdateDTO article, IFormFile ImageArticle)
		{
			try
			{
				if (ImageArticle != null)
				{
					if (!Helper.IsImageFile(ImageArticle.FileName))
					{
						TempData["message"] = @"<div class='alert alert-danger'><strong>Error!</strong> File is not an image !</div>";
						return RedirectToAction("Index");
					}
					string fileName = $"{Guid.NewGuid()}_{ImageArticle.FileName}" + Path.GetExtension(ImageArticle.FileName);
					string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pics", fileName);

					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						ImageArticle.CopyTo(fileStream);
					}

					article.Pic = fileName;


				}

				_articleBLL.Update(article);

				TempData["message"] = @"<div class='alert alert-success'><strong>Success!</strong> Add Article Success !</div>";
			}
			catch (Exception ex)
			{
				TempData["message"] = @"<div class='alert alert-danger'><strong>Error!</strong> " + ex.Message + "</div>";
			}

			return RedirectToAction("Index");
		}

		// GET: ArticlesController/Delete/5
		public ActionResult Delete(int id)
		{
			_articleBLL.Delete(id);
			return RedirectToAction("Index");
		}

		// POST: ArticlesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
