using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
			TasksViewer = new ObservableCollection<TasksViewer>()
			{
				new TasksViewer{TaskDescription=null,TaskName="Your Tasks"},
				new TasksViewer{TaskDescription=null,TaskName="Your Tasks 2"},
			};
			InitializeComponent();
			this.DataContext = this;
		}
	}
}
