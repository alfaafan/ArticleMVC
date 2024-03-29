﻿using Microsoft.AspNetCore.Mvc;
//using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;
using RESTServices.BLL.DTOs;
using SampleMVC.Models;
using SampleMVC.Services.Interfaces;

namespace SampleMVC.Controllers;

public class CategoriesController : Controller
{
	private readonly ICategoryBLL _categoryBLL;
	private readonly ICategoryService _categoryService;

	public CategoriesController(ICategoryBLL categoryBLL, ICategoryService categoryService)
	{
		_categoryBLL = categoryBLL;
		_categoryService = categoryService;
	}

	public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, string name = "", string act = "")
	{
		ViewData["Title"] = "Category List";
		if (TempData["message"] != null)
		{
			ViewData["message"] = TempData["message"];
		}

		ViewData["name"] = name;

		var maxsize = await _categoryService.GetCountCategories(name);

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

		if (act == "first")
		{
			pageNumber = 1;
			ViewData["pageNumber"] = pageNumber;
		}
		else if (act == "last")
		{
			pageNumber = maxsize / pageSize;
			if (maxsize % pageSize > 0)
			{
				pageNumber += 1;
			}
			ViewData["pageNumber"] = pageNumber;
		}



		ViewData["pageNumber"] = pageNumber;

		ViewData["pageSize"] = pageSize;

		var models = await _categoryService.GetWithPaging(pageNumber, pageSize, name);
		List<Category> categoryList = new List<Category>();
		//if (maxsize == 0)
		//{
		//	ViewData["pageNumber"] = 1;
		//	return View(categoryList);
		//}
		foreach (var category in models)
		{
			categoryList.Add(new Category
			{
				CategoryID = category.CategoryID,
				CategoryName = category.CategoryName
			});
		}

		if (categoryList.Count <= pageSize)
		{
			ViewData["pageNumber"] = 1;
		}

		return View(categoryList);
	}

	public async Task<IActionResult> GetFromService()
	{
		if (TempData["message"] != null)
		{
			ViewData["message"] = TempData["message"];
		}
		var categories = await _categoryService.GetAll();
		List<Category> categoryList = new List<Category>();
		foreach (var category in categories)
		{
			categoryList.Add(new Category
			{
				CategoryID = category.CategoryID,
				CategoryName = category.CategoryName
			});
		}
		return View(categoryList);
	}


	public async Task<IActionResult> Detail(int id)
	{
		var category = await _categoryService.GetById(id);
		if (category == null)
		{
			TempData["message"] = @"<div class='alert alert-danger'><strong>Error!</strong>Category Not Found !</div>";
			return RedirectToAction("GetFromService");
		}
		ViewData["Title"] = $"{category.CategoryName} | Detail Category";
		return View(category);
	}

	public IActionResult Create()
	{
		ViewData["Title"] = "Add New Category";
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(CategoryCreateDTO categoryCreate)
	{
		try
		{
			await _categoryService.Insert(categoryCreate);
			TempData["message"] = @"<div class='alert alert-success'><strong>Success!</strong>Add New Category Success !</div>";
		}
		catch (Exception ex)
		{
			ViewData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
			return View(categoryCreate);
		}
		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Edit(int id)
	{
		var category = await _categoryService.GetById(id);
		if (category == null)
		{
			TempData["message"] = @"<div class='alert alert-danger'><strong>Error!</strong>Category Not Found !</div>";
			return RedirectToAction("GetFromService");
		}
		ViewData["Title"] = $"{category.CategoryName} | Edit Category";
		return View(category);
	}

	[HttpPost]
	public async Task<IActionResult> Edit(int id, CategoryUpdateDTO categoryEdit)
	{
		try
		{
			await _categoryService.Update(categoryEdit);
			TempData["message"] = @"<div class='alert alert-success'><strong>Success!</strong>Edit Data Category Success !</div>";
		}
		catch (Exception ex)
		{
			TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
		}
		return RedirectToAction("Index");
	}



	public async Task<IActionResult> Delete(int id)
	{
		var category = await _categoryService.GetById(id);
		if (category == null
					)
		{
			TempData["message"] = @"<div class='alert alert-danger'><strong>Error!</strong>Category Not Found !</div>";
			return RedirectToAction("GetFromService");
		}
		ViewData["Title"] = $"{category.CategoryName} | Delete Category";
		return View(category);
	}

	[HttpPost]
	public async Task<IActionResult> Delete(int id, Category category)
	{
		try
		{
			await _categoryService.Delete(id);
			TempData["message"] = @"<div class='alert alert-success'><strong>Success!</strong>Delete Data Category Success !</div>";
		}
		catch (Exception ex)
		{
			TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
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
