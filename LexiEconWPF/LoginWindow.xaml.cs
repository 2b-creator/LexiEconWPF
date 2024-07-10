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
			var resp = await client.GetAsync($"{LexiEconSettings.LexiHost}/api/users/task/query");
			string data = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            dynamic dataGet = JObject.Parse(data);
			int statusCode = dataGet.data[0].task_id;
        }
	}
}
