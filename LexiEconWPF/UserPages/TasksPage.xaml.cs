using iNKORE.UI.WPF.Helpers;
using iNKORE.UI.WPF.Modern.Controls;
using LexiEconWPF.AppFunctions;
using LexiEconWPF.UIWidgets;
using Newtonsoft.Json;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBoxEx = iNKORE.UI.WPF.Modern.Controls.MessageBox;

namespace LexiEconWPF.UserPages
{
	/// <summary>
	/// TasksPage.xaml 的交互逻辑
	/// </summary>
	public partial class TasksPage : System.Windows.Controls.Page
	{
		public NewTaskClass newTaskClass { get; set; } = new NewTaskClass();
		
		public TasksPage()
		{
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private async void NewTaskHandler_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				await TaskHanders();
				DataExchageStatic.window.MainWindowSetState();
				await tCardsWidget.TasksCardSetState();
			}
			catch (Exception ex)
			{
				string exName = ex.ToString();
				MessageBoxEx.Show(exName, "错误!", MessageBoxButton.OK, MessageBoxImage.Error);
				LogHelper.Error(exName, ex);
				//throw ex;
			}
		}

		private async Task TaskHanders()
		{
			ContentDialog contentDialog = new ContentDialog();
			contentDialog.Title = "新的任务";
			contentDialog.PrimaryButtonText = "发布";
			contentDialog.SecondaryButtonText = "取消";
			contentDialog.DefaultButton = ContentDialogButton.Secondary;
			AssignNewTask assignNewTask = new AssignNewTask();
			contentDialog.Content = assignNewTask;
			var res = await contentDialog.ShowAsync();
			if (res == ContentDialogResult.Primary)
			{
				int classId = Convert.ToInt32(assignNewTask.TaskClassId.Text);
				string taskName = assignNewTask.NewTaskName.Text;
				dynamic selectedIndex = assignNewTask.NewCateComboBox;
				string cate = selectedIndex.Items.CurrentItem.Id;
				int cateId = Convert.ToInt32(cate);
				int end = Convert.ToInt32(assignNewTask.WordsEndId.Text);
				int start = Convert.ToInt32(assignNewTask.WordsStartId.Text);
				newTaskClass.name = taskName;
				newTaskClass.class_id = classId;
				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Add("access-token", UserStatus.AccessToken);
				string postNewTaskStr = JsonConvert.SerializeObject(newTaskClass);
				var content = new StringContent(postNewTaskStr, Encoding.UTF8, "application/json");
				HttpResponseMessage resp = await client.PostAsync(requestUri: $"{LexiEconSettings.LexiHost}{EndPointLexi.GetNewTaskId}", content: content);
				string data = await resp.Content.ReadAsStringAsync();
				dynamic dataGet = JObject.Parse(data);
				int taskId = dataGet.task_id;
				newTaskClass.task_id = taskId;
				newTaskClass.words = new List<TaskClassWords>
				{
					new TaskClassWords { cate_id = cateId, end = end, start = start }
				};
				postNewTaskStr = JsonConvert.SerializeObject(newTaskClass);
				content = new StringContent(postNewTaskStr, Encoding.UTF8, "application/json");
				resp = await client.PostAsync(requestUri: $"{LexiEconSettings.LexiHost}{EndPointLexi.AssginNewTask}", content: content);
				HttpResponseMessage release = await client.PostAsync(requestUri: $"{LexiEconSettings.LexiHost}{EndPointLexi.ReleaseNewTask}", content: content);
			}
		}
	}
}
