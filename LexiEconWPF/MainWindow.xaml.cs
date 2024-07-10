using System.Text;
using System.Windows;
using System.Windows.Controls;
using iNKORE.UI.WPF.Modern.Media.Animation;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LexiEconWPF.AppFunctions;

namespace LexiEconWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	
	public partial class MainWindow : Window
	{
		public static bool isLogin = !String.IsNullOrEmpty(UserStatus.AccessToken);
		public UserPages.MainPage MainPage = new UserPages.MainPage();
		public UserPages.TasksPage TasksPage = new UserPages.TasksPage();
		public UserPages.StudyPage StudyPage = new UserPages.StudyPage();
		public UserPages.UnLoginPage UnLoginPage = new UserPages.UnLoginPage();
		private NavigationTransitionInfo? _transitionInfo = null;
		
		public MainWindow()
		{
			InitializeComponent();
		}

		private void NavigationBarSel_SelectionChanged(iNKORE.UI.WPF.Modern.Controls.NavigationView sender, iNKORE.UI.WPF.Modern.Controls.NavigationViewSelectionChangedEventArgs args)
		{
			var item = sender.SelectedItem;
			Page? page = null;
			if (item == NavHomePage)
			{
				page = MainPage;
			}
			else if (item == NavTaskPage)
			{
				page = isLogin ? TasksPage : UnLoginPage;
			}
			else if (item == NavStudyPage)
			{
				page = isLogin ? StudyPage : UnLoginPage;
			}
			if (page != null)
			{
				MainFrame.Navigate(page, _transitionInfo = new EntranceNavigationTransitionInfo());
			}
		}
	}
}