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
		public static string FinishTask = "/api/users/task/finish";
		public static string GetClassInfo = "/api/users/class/info";
		public static string JoinClass = "/api/users/join";
		public static string GetCate = "/api/client/get_cate";
		public static string GetNewTaskId = "/api/users/task/new";
		public static string AssginNewTask = "/api/users/task/assign";
		public static string ReleaseNewTask = "/api/users/task/release";
		public static string GetLatestClient = "/api/client/latest";
		public static string DownloadLatest = "/download/latest.zip";
		public static string BuyMeACoffee = "/download/donate.png";
		public static string GetrealName = "/api/client/query";
		public static string UsersChangePasswd = "/api/users/change_pwd";
	}
}
