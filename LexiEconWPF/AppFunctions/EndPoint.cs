using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LexiEconWPF.AppFunctions
{
	public class WordsIdData
	{
		public int Id { get; set; }
	}
	public class EndPointLexi
	{
		public static string ContentType = "application/json";
		public static string Login = "/api/client/login/user";
		public static string AdminGetToken = "/api/client/login/admin";
		public static string QueryTasks = "/api/users/task/query";
		public static string TaskGetWords = "/api/users/task/get_words";
		public static string CheckTask = "/api/users/task/check";
		public static string SubmitWordsStatus = "/api/users/words/submit";
	}
}
