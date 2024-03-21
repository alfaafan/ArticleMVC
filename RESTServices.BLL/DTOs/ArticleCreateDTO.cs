using System.ComponentModel.DataAnnotations;

namespace RESTServices.BLL.DTOs
{
	public class ArticleCreateDTO
	{
		public int CategoryID { get; set; }
		public string? Title { get; set; }
		public string? Details { get; set; }
		public bool IsApproved { get; set; }
		public DateOnly PublishDate { get; set; }
		public string? Pic { get; set; }
	}
}
