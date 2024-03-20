using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace RESTServices.BLL.DTOs
{
	public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDTO>
	{
		public CategoryUpdateValidator()
		{
			RuleFor(c => c.CategoryName).NotEmpty().WithMessage("Name is required");
			RuleFor(c => c.CategoryName).MaximumLength(50).WithMessage("Name can't be longer than 50 characters");
		}
	}
}
