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
}
