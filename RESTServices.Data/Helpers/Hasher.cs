using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTServices.Data.Helpers
{
	public class Hasher
	{
		public static string Hash(string password)
		{
			using (var md5 = System.Security.Cryptography.MD5.Create())
			{
				var result = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
				return Encoding.ASCII.GetString(result);
			}
		}

		public static bool Verify(string password, string hash)
		{
			return Hash(password) == hash;
		}
	}
}
