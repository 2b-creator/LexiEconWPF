using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiEconWPF.AppFunctions
{
	public class UserStatus
	{
		public static string? AccessToken { get; set; }
	}
	public class UserAttribute
	{
		public string username { get; set; }
		public string password { get; set; }
	}
	public class UserReciteStatus
	{
		public int word_id { get; set; }
		public string review_category { get; set; } = "spell";
		public string situation { get; set; }
	}
}
