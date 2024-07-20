using iNKORE.UI.WPF.Modern.Controls;
using LexiEconWPF.AppFunctions;
using LexiEconWPF.UIWidgets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBoxEx = iNKORE.UI.WPF.Modern.Controls.MessageBox;
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
			SaveSettings();
		}

		private void SaveSettings()
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

		private async void ChangePwdBtn_Click(object sender, RoutedEventArgs e)
		{
			await ChangePasswd(0);
		}

		private static async Task ChangePasswd(int status)
		{
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "更改密码";
			dialog.PrimaryButtonText = "确定";
			dialog.SecondaryButtonText = "取消";
			dialog.DefaultButton = ContentDialogButton.Secondary;
			ChangePwdControl changePwdControl = new ChangePwdControl();
			if (status == 0)
			{
				changePwdControl.OldPasswdError.Visibility = Visibility.Collapsed;
			}
			else if (status == 1)
			{
				changePwdControl.OldPasswdError.Visibility = Visibility.Visible;
			}
			else if (status == 2)
			{
				changePwdControl.NewPasswdError.Visibility = Visibility.Visible;
			}
			else if (status == 3)
			{
				changePwdControl.ShortPasswdError.Visibility = Visibility.Visible;
			}
			dialog.Content = changePwdControl;
			var res = await dialog.ShowAsync();
			if (res == ContentDialogResult.Primary)
			{
				if (changePwdControl.EnterNewPassword.Password == changePwdControl.EnterNewPasswordConfirm.Password)
				{
					if (changePwdControl.EnterNewPassword.Password.Length >= 8)
					{
						ChangePasswordData data = new ChangePasswordData();
						data.old_password = HashEncryptPassword.ApplyHash(changePwdControl.EnterOldPassword.Password);
						data.new_password = HashEncryptPassword.ApplyHash(changePwdControl.EnterNewPasswordConfirm.Password);
						string postData = JsonConvert.SerializeObject(data);
						var content = new StringContent(postData, Encoding.UTF8, "application/json");
						HttpClient client = new HttpClient();
						client.DefaultRequestHeaders.Add("access-token", UserStatus.AccessToken);
						HttpResponseMessage message = await client.PostAsync(requestUri: $"{LexiEconSettings.LexiHost}{EndPointLexi.UsersChangePasswd}", content: content);
						if (!message.IsSuccessStatusCode)
						{
							SolidColorBrush brushes = new SolidColorBrush(Colors.Red);
							await ChangePasswd(1);
						}
					}
					else
					{
						await ChangePasswd(3);
					}
				}
                else
                {
					await ChangePasswd(2);

				}
            }
		}

		private void ExitLoginLexi_Click(object sender, RoutedEventArgs e)
		{
			var res = MessageBoxEx.Show("确实要退出登录？", "退出登录", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (res == MessageBoxResult.Yes)
			{
				File.Delete("config.json");
				LexiEconSettings.LexiHost = "http://127.0.0.1:5000";
				BeforeStartUp startUp = new BeforeStartUp();
				startUp.ReadJsonAccess();
				DataExchageStatic.window.MainWindowSetState();
				SaveSettings();
			}
		}
	}
}
