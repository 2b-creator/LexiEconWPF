using LexiEconWPF.AppFunctions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LexiEconWPF.UserPages
{
	/// <summary>
	/// MainPage.xaml 的交互逻辑
	/// </summary>
	public partial class MainPage : Page
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private async void AllMainPage_Loaded(object sender, RoutedEventArgs e)
		{
			HttpClient client = new HttpClient();
			HttpResponseMessage message = await client.GetAsync($"{LexiEconSettings.LexiHost}{EndPointLexi.GetrealName}?access_token={UserStatus.AccessToken}");
			if (message.StatusCode == System.Net.HttpStatusCode.OK)
			{
				string info = await message.Content.ReadAsStringAsync();
				dynamic getInfo = JObject.Parse(info);
				UsergetName.Text = getInfo.realname;
			}
		}
	}
}
