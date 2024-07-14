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

namespace LexiEconWPF.UIWidgets
{
	/// <summary>
	/// AboutWPF.xaml 的交互逻辑
	/// </summary>
	public partial class AboutWPF : UserControl
	{
		public AboutWPF()
		{
			InitializeComponent();
		}

		private void AboutPageLd_Loaded(object sender, RoutedEventArgs e)
		{
			Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			VersionCode.Text = version.ToString();

		}

		private async void CheckForUpdate_Click(object sender, RoutedEventArgs e)
		{
			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.GetAsync($"{LexiEconSettings.LexiHost}{EndPointLexi.GetLatestClient}");
			string info = await response.Content.ReadAsStringAsync();
			dynamic getInfo = JObject.Parse(info);
			string latestVersion = getInfo.data;
			LatestVersionCode.Text = latestVersion;
            if (LatestVersionCode.Text != VersionCode.Text)
            {
				UpdateImtly.IsEnabled = true;
			}
        }

		private void GiveAuthorStar_Click(object sender, RoutedEventArgs e)
		{

		}

		private void UpdateImtly_Click(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start($"{LexiEconSettings.LexiHost}{EndPointLexi.DownloadLatest}");
		}
	}
}
