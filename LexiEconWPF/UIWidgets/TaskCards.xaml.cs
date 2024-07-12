using LexiEconWPF.AppFunctions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
	/// TaskCards.xaml 的交互逻辑
	/// </summary>
	public partial class TaskCards : UserControl
	{
		public ObservableCollection<TasksViewer> TasksViewer { get; set; }
		public TaskCards()
		{
			InitializeComponent();
		}

		private async void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Add("access-token", UserStatus.AccessToken);
			var request = new HttpRequestMessage(HttpMethod.Get, $"{LexiEconSettings.LexiHost}{EndPointLexi.QueryTasks}");
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			HttpResponseMessage responseMessage = await client.SendAsync(request);
			string data = await responseMessage.Content.ReadAsStringAsync();
			dynamic dataGet = JObject.Parse(data);
			int taskCounts = dataGet.data.Count;
			TasksViewer = new ObservableCollection<TasksViewer>() { };
			for (int i = 0; i < taskCounts; i++)
			{
				if (dataGet.data[i].status != "已完成")
				{
					TasksViewer.Add(new LexiEconWPF.TasksViewer
					{
						TaskId = dataGet.data[i].task_id,
						TaskName = dataGet.data[i].task_name
					});
				}
			}
			this.DataContext = this;
		}

		private void FinishTasks_Click(object sender, RoutedEventArgs e)
		{
			ReciteWordsExecuter executer = new ReciteWordsExecuter(Convert.ToInt32(((Button)sender).Tag));
			executer.Execute();
		}
	}
}
