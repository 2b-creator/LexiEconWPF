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
using MessageBoxEx = iNKORE.UI.WPF.Modern.Controls.MessageBox;
using System.Windows.Shapes;

namespace LexiEconWPF.UserPages
{
	/// <summary>
	/// MainPage.xaml 的交互逻辑
	/// </summary>
	public partial class MainPage : Page
	{
		public HttpResponseMessage message;
		public MainPage()
		{
			InitializeComponent();
		}

		private async void AllMainPage_Loaded(object sender, RoutedEventArgs e)
		{
			HttpClient client = new HttpClient();
			try
			{
				message = await client.GetAsync($"{LexiEconSettings.LexiHost}{EndPointLexi.GetrealName}?access_token={UserStatus.AccessToken}");
			}
			catch (Exception ex)
			{
				MessageBoxEx.Show(e.ToString(), "错误!", MessageBoxButton.OK, MessageBoxImage.Error);
				LogHelper.Fatal(e.ToString()!, ex);
				//throw;
			}
			if (message.StatusCode == System.Net.HttpStatusCode.OK)
			{
				string info = await message.Content.ReadAsStringAsync();
				dynamic getInfo = JObject.Parse(info);
				UsergetName.Text = getInfo.realname;
			}
		}
	}
}
