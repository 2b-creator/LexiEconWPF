using iNKORE.UI.WPF.Modern.Controls;
using LexiEconWPF.AppFunctions;
using LexiEconWPF.UIWidgets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;

namespace LexiEconWPF.UserPages
{
	/// <summary>
	/// SettingsPage.xaml 的交互逻辑
	/// </summary>
	public partial class SettingsPage : System.Windows.Controls.Page
	{
		public SettingsPage()
		{
			InitializeComponent();
		}

		private void SettingsPage_Loaded(object sender, RoutedEventArgs e)
		{

			Homeserver.Text = LexiEconSettings.LexiHost;
		}

		private void SaveSettingsBtn_Click(object sender, RoutedEventArgs e)
		{
			string currentPath = System.IO.Directory.GetCurrentDirectory();
			string filename = "config.json";
			string[] paths = new string[] { currentPath, filename };
			string fullFilePath = Path.Combine(paths);
			using (StreamReader reader = new StreamReader(fullFilePath, Encoding.UTF8))
			{
				string content = reader.ReadToEnd();
				dynamic serialContent = JObject.Parse(content);
				UserStatus.AccessToken = serialContent.access_token;
				LexiEconSettings.LexiHost = serialContent.lexi_host;
			}
			LexiEconSettings.LexiHost = Homeserver.Text;
			LexiEconSettings.SaveJsonSettings.lexi_host = LexiEconSettings.LexiHost;
			LexiEconSettings.SaveJsonSettings.access_token = UserStatus.AccessToken;
			string writeContent = JsonConvert.SerializeObject(LexiEconSettings.SaveJsonSettings);
			using (StreamWriter sw = new StreamWriter(fullFilePath))
			{
				sw.Write(writeContent);
			}
		}

		private async void AboutPageBtn_Click(object sender, RoutedEventArgs e)
		{
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "关于";
			dialog.PrimaryButtonText = "确定";
			dialog.DefaultButton = ContentDialogButton.Primary;
			AboutWPF aboutWPF = new AboutWPF();
			dialog.Content = aboutWPF;
			var res = await dialog.ShowAsync();
		}
	}
}
