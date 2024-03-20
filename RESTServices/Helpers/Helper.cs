using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RESTServices.Helpers
{
	public class Helper
	{
		public static void AddToModelState(ValidationResult result, ModelStateDictionary modelState)
		{
			foreach (var error in result.Errors)
			{
				modelState.AddModelError(error.PropertyName, error.ErrorMessage);
			}
		}
	}
}
