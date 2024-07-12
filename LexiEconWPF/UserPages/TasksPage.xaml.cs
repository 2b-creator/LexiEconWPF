using iNKORE.UI.WPF.Modern.Controls;
using LexiEconWPF.AppFunctions;
using LexiEconWPF.UIWidgets;
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
	/// TasksPage.xaml 的交互逻辑
	/// </summary>
	public partial class TasksPage : System.Windows.Controls.Page
	{
		public TasksPage()
		{
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			
		}

		private async void NewTaskHandler_Click(object sender, RoutedEventArgs e)
		{
			ContentDialog contentDialog = new ContentDialog();
			contentDialog.Title = "新的任务";
			contentDialog.PrimaryButtonText = "发布";
			contentDialog.SecondaryButtonText = "取消";
			contentDialog.DefaultButton = ContentDialogButton.Secondary;
			AssignNewTask assignNewTask = new AssignNewTask();
			contentDialog.Content = assignNewTask;
			var res = await contentDialog.ShowAsync();
        }
    }
}
