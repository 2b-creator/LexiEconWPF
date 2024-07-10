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
		public static string IdGetWords = "/api/users/words/query";
		public async static Task<string> IdGetWordsHandler(int id)
		{
			HttpClient client = new HttpClient();
			WordsIdData dataWords = new WordsIdData();
			dataWords.Id = id;
			string postQueryWords = JsonConvert.SerializeObject(dataWords);
			var content = new StringContent(postQueryWords, Encoding.UTF8, "application/json");
			HttpResponseMessage resp = await client.PostAsync(requestUri: $"{LexiEconSettings.LexiHost}{EndPointLexi.Login}", content: content);
			string data = await resp.Content.ReadAsStringAsync();
			dynamic serialData = JObject.Parse(data);
			string res = serialData.data[0];
			return res;

		}
	}
}
