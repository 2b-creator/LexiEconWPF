using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiEconWPF.AppFunctions
{
	public class LexiEconSettings
	{
		public static SaveJsonSettings SaveJsonSettings { get; set; } = new SaveJsonSettings();
		public static string LexiHost = "http://127.0.0.1:5000";
	}
	public class SaveJsonSettings
	{
		public string access_token { get; set; }
		public string lexi_host { get; set; }
	}
	public class TaskWordsData
	{
		public int taskId { get; set; }
		public int wordsId { get; set; }
	}
}
