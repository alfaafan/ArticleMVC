using Microsoft.AspNetCore.Mvc;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;

namespace SampleMVC.Controllers;

public class CategoriesController : Controller
{
	private readonly ICategoryBLL _categoryBLL;

	public CategoriesController(ICategoryBLL categoryBLL)
	{
		_categoryBLL = categoryBLL;
	}

	public IActionResult Index(int pageNumber = 1, int pageSize = 5, string search = "", string act = "")
	{
		ViewData["Title"] = "Category List";
		if (TempData["message"] != null)
		{
			ViewData["message"] = TempData["message"];
		}

		ViewData["search"] = search;
		var models = _categoryBLL.GetWithPaging(pageNumber, pageSize, search);
		var maxsize = _categoryBLL.GetCountCategories(search);

		if (act == "next")
		{
			if (pageNumber * pageSize < maxsize)
			{
				pageNumber += 1;
			}
			ViewData["pageNumber"] = pageNumber;
		}
		else if (act == "prev")
		{
			if (pageNumber > 1)
			{
				pageNumber -= 1;
			}
			ViewData["pageNumber"] = pageNumber;
		}
		else
		{
			ViewData["pageNumber"] = 2;
		}

		ViewData["pageSize"] = pageSize;
		//ViewData["action"] = action;


		return View(models);
	}


	public IActionResult Detail(int id)
	{
		var model = _categoryBLL.GetById(id);
		ViewData["Title"] = model.CategoryName;
		return View(model);
	}

	public IActionResult Create()
	{
		ViewData["Title"] = "Add New Category";
		return View();
	}

	[HttpPost]
	public IActionResult Create(CategoryCreateDTO categoryCreate)
	{
		try
		{
			_categoryBLL.Insert(categoryCreate);
			TempData["message"] = @"<div class='alert alert-success'><strong>Success!</strong>Add Data Category Success !</div>";
		}
		catch (Exception ex)
		{
			TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
		}
		return RedirectToAction("Index");
	}

	public IActionResult Edit(int id)
	{
		var model = _categoryBLL.GetById(id);
		if (model == null)
		{
			TempData["message"] = @"<div class='alert alert-danger'><strong>Error!</strong>Category Not Found !</div>";
			return RedirectToAction("Index");
		}
		ViewData["Title"] = $"{model.CategoryName} | Edit Category";
		return View(model);
	}

	[HttpPost]
	public IActionResult Edit(int id, CategoryUpdateDTO categoryEdit)
	{
		try
		{
			_categoryBLL.Update(categoryEdit);
			TempData["message"] = @"<div class='alert alert-success'><strong>Success!</strong>Edit Data Category Success !</div>";
		}
		catch (Exception ex)
		{
			ViewData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
			return View(categoryEdit);
		}
		return RedirectToAction("Index");
	}



	public IActionResult Delete(int id)
	{
		var model = _categoryBLL.GetById(id);
		if (model == null)
		{
			TempData["message"] = @"<div class='alert alert-danger'><strong>Error!</strong>Category Not Found !</div>";
			return RedirectToAction("Index");
		}
		ViewData["Title"] = $"{model.CategoryName} | Delete Category";
		return View(model);
	}

	[HttpPost]
	public IActionResult Delete(int id, CategoryDTO category)
	{
		try
		{
			_categoryBLL.Delete(id);
			TempData["message"] = @"<div class='alert alert-success'><strong>Success!</strong>Delete Data Category Success !</div>";
		}
		catch (Exception ex)
		{
			TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
			return View(category);
		}
		return RedirectToAction("Index");
	}

	public IActionResult DisplayDropdownList()
	{
		var categories = _categoryBLL.GetAll();
		ViewBag.Categories = categories;
		return View();
	}

	[HttpPost]
	public IActionResult DisplayDropdownList(string CategoryID)
	{
		ViewBag.CategoryID = CategoryID;
		ViewBag.Message = $"You selected {CategoryID}";

		ViewBag.Categories = _categoryBLL.GetAll();

		return View();
	}

}
