using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RESTServices.BLL.DTOs;

namespace RESTServices.Models;

public class ArticleWithFile
{
	public int CategoryId { get; set; }
	public string? Title { get; set; }
	public string? Details { get; set; }
	public bool IsApproved { get; set; } = false;
	public IFormFile? Pic { get; set; }
}
