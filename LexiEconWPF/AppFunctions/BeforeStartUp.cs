using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Markup;

namespace LexiEconWPF.AppFunctions
{
	class BeforeStartUp
	{
		public void ReadJsonAccess()
		{
			string currentPath = System.IO.Directory.GetCurrentDirectory();
			string filename = "config.json";
			string[] paths = new string[] { currentPath, filename };
			string fullFilePath = Path.Combine(paths);
			if (File.Exists(fullFilePath))
			{
				try
				{
					using (StreamReader reader = new StreamReader(fullFilePath,Encoding.UTF8))
					{
						string content = reader.ReadToEnd();
						dynamic serialContent = JObject.Parse(content);
						UserStatus.AccessToken = serialContent.access_token;
					}
				}
				catch (Exception e)
				{

					throw e;
				}
				
			}
			else
			{
				File.Create(fullFilePath).Close();
				using (StreamWriter writer = new StreamWriter(fullFilePath))
				{
					writer.Write("{}");
				}
				
			}
		}
	}
}
