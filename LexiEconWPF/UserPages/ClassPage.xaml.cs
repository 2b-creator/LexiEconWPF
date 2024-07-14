using iNKORE.UI.WPF.Modern.Controls;
using LexiEconWPF.AppFunctions;
using LexiEconWPF.UIWidgets;
using Newtonsoft.Json;
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
using Windows.Media.Protection.PlayReady;

namespace LexiEconWPF.UserPages
{
	/// <summary>
	/// ClassPage.xaml 的交互逻辑
	/// </summary>
	public partial class ClassPage : System.Windows.Controls.Page
	{
		public ClassPage()
		{
			InitializeComponent();
		}

		private async void JoinClass_Click(object sender, RoutedEventArgs e)
		{
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "加入班级";
			dialog.PrimaryButtonText = "加入";
			dialog.SecondaryButtonText = "取消";
			dialog.DefaultButton = ContentDialogButton.Primary;
			JoinClassWidget widget = new JoinClassWidget();
			dialog.Content = widget;
			var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
				HttpClient client = new HttpClient();
				string inviteCode = widget.InviteCodeInput.Text;
				JoinClassViewer joinClassViewer = new JoinClassViewer();
				joinClassViewer.invite_code = inviteCode;
				string postJoinStr = JsonConvert.SerializeObject(joinClassViewer);
				client.DefaultRequestHeaders.Add("access-token", UserStatus.AccessToken);
				var content = new StringContent(postJoinStr, Encoding.UTF8, "application/json");
				HttpResponseMessage resp = await client.PostAsync(requestUri: $"{LexiEconSettings.LexiHost}{EndPointLexi.JoinClass}", content: content);
				DataExchageStatic.window.MainWindowSetState();
				await ClassCardsWidgetInter.ClassLoader();
			}
        }
    }
}
