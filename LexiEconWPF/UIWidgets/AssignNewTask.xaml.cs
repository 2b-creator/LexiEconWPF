using LexiEconWPF.AppFunctions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	/// AssignNewTask.xaml 的交互逻辑
	/// </summary>
	public partial class AssignNewTask : UserControl
	{
		public ObservableCollection<CateWithId> CateWithIds { get; set; }
		public AssignNewTask()
		{
			InitializeComponent();
		}

		private async void AssignNewTaskControl_GotFocus(object sender, RoutedEventArgs e)
		{
			DataExchageStatic.CateWithId = new ObservableCollection<CateWithId> { };
			HttpClient httpClient = new HttpClient();
			HttpResponseMessage message = await httpClient.GetAsync($"{LexiEconSettings.LexiHost}{EndPointLexi.GetCate}");
			string data = await message.Content.ReadAsStringAsync();
			dynamic dataGet = JObject.Parse(data);
			int cateCounts = dataGet.data.Count;
			CateWithIds = new ObservableCollection<CateWithId>();
			for (int i = 0; i < cateCounts; i++)
			{
				CateWithId ids = new CateWithId
				{
					Id = dataGet.data[i].cate_id,
					Name = dataGet.data[i].cate_name,
				};
				CateWithIds.Add(ids);
			}
			this.DataContext = this;
		}
	}
}
