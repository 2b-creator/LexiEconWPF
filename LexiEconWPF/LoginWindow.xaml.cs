using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using LexiEconWPF.AppFunctions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Path = System.IO.Path;

namespace LexiEconWPF
{
	/// <summary>
	/// LoginWindow.xaml 的交互逻辑
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{

			InitializeComponent();
		}

		private async void LoginButton_Click(object sender, RoutedEventArgs e)
		{
			HttpClient client = new HttpClient();
			string currentPath = System.IO.Directory.GetCurrentDirectory();
			string filename = "config.json";
			string[] paths = new string[] { currentPath, filename };
			string fullFilePath = Path.Combine(paths);
			UserAttribute attribute = new UserAttribute();
			attribute.username = UsernameInput.Text;
			attribute.password = HashEncryptPassword.ApplyHash(PasswordInput.Password);
			string postLoginStr = JsonConvert.SerializeObject(attribute);
			var content = new StringContent(postLoginStr, Encoding.UTF8, "application/json");
			HttpResponseMessage resp = await client.PostAsync(requestUri: $"{LexiEconSettings.LexiHost}{EndPointLexi.Login}", content: content);
			string data = await resp.Content.ReadAsStringAsync();
			dynamic serialData = JObject.Parse(data);
			string token = serialData.access_token;
			UserStatus.AccessToken = token;
			string writeContent = $"{{\"access_token\": \"{token}\"}}";
			MainWindow.isLogin = true;
			using (StreamWriter sw = new StreamWriter(fullFilePath))
			{
				sw.Write(writeContent);
			}
			this.Close();
		}
	}
}
