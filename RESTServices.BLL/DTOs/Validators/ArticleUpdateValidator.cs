using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace RESTServices.BLL.DTOs.Validators
{
	public class ArticleUpdateValidator : AbstractValidator<ArticleUpdateDTO>
	{
		public ArticleUpdateValidator()
		{
			RuleFor(a => a.CategoryID).NotEmpty().WithMessage("Category is required");
			RuleFor(a => a.Title).NotEmpty().WithMessage("Title is required");
			RuleFor(a => a.Title).MaximumLength(100).WithMessage("Title can't be longer than 100 characters");
		}
	}
}
