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
	/// ClassCardWidget.xaml 的交互逻辑
	/// </summary>
	public partial class ClassCardWidget : UserControl
	{
		public ObservableCollection<ClassViewer> ClassViewers { get; set; }
		public ClassCardWidget()
		{
			InitializeComponent();
		}

		private async void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Add("access-token", UserStatus.AccessToken);
			HttpResponseMessage classInfo = await client.GetAsync($"{LexiEconSettings.LexiHost}{EndPointLexi.GetClassInfo}");
			string info = await classInfo.Content.ReadAsStringAsync();
			dynamic infoGet = JObject.Parse(info);
			int classCounts = infoGet.data.Count;
			ClassViewers = new ObservableCollection<ClassViewer>();
			for (int i = 0; i < classCounts; i++)
			{
				int classId = infoGet.data[i].class_id;
				string className = infoGet.data[i].class_name;
				ClassViewers.Add(new ClassViewer
				{
					ClassId = classId,
					ClassName = className,
				});
			}
			this.DataContext = this;
		}
	}
}
