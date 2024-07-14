using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Markup;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Xml.Linq;
using MessageBoxEx = iNKORE.UI.WPF.Modern.Controls.MessageBox;

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
					using (StreamReader reader = new StreamReader(fullFilePath, Encoding.UTF8))
					{
						string content = reader.ReadToEnd();
						dynamic serialContent = JObject.Parse(content);
						UserStatus.AccessToken = serialContent.access_token;
						LexiEconSettings.LexiHost = serialContent.lexi_host;
					}
				}
				catch (Exception e)
				{
					MessageBoxEx.Show(e.ToString(), "错误!", MessageBoxButton.OK, MessageBoxImage.Error);
					LogHelper.Fatal(e.ToString(), e);
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
		public async void ReadCate()
		{
			DataExchageStatic.CateWithId = new ObservableCollection<CateWithId> { };
			HttpClient httpClient = new HttpClient();
			HttpResponseMessage message = await httpClient.GetAsync($"{LexiEconSettings.LexiHost}{EndPointLexi.GetCate}");
			string data = await message.Content.ReadAsStringAsync();
			dynamic dataGet = JObject.Parse(data);
			int cateCounts = dataGet.data.Count;
			
			for (int i = 0; i < cateCounts; i++)
			{
				CateWithId ids = new CateWithId
				{
					Id = dataGet.data[i].cate_id,
					Name = dataGet.data[i].cated_name,
				};
				DataExchageStatic.CateWithId.Add(ids);
			}

		}
		public void CreateLogFile()
		{
			string currentPath = System.IO.Directory.GetCurrentDirectory();
			string filename = "log.txt";
			string[] paths = new string[] { currentPath, filename };
			string fullFilePath = Path.Combine(paths);
			if (!File.Exists(fullFilePath))
			{
				File.Create(fullFilePath).Close();
			}
			LogHelper.SetConfig(fullFilePath);
		}
	}
}
