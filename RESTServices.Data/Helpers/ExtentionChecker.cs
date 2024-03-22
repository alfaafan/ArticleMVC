using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTServices.Data.Helpers
{
	public class ExtentionChecker
	{
		public static bool IsImage(string fileName)
		{
			string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
			return imageExtensions.Contains(System.IO.Path.GetExtension(fileName).ToLower());
		}
	}
}
