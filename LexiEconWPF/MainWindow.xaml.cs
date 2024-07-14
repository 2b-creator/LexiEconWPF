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
using iNKORE.UI.WPF.Modern.Controls;

namespace LexiEconWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>

	public partial class MainWindow : Window
	{
		public static bool isLogin = !String.IsNullOrEmpty(UserStatus.AccessToken);
		public static UserPages.MainPage MainPage = new UserPages.MainPage();
		public static UserPages.TasksPage TasksPage = new UserPages.TasksPage();
		public static UserPages.StudyPage StudyPage = new UserPages.StudyPage();
		public static UserPages.UnLoginPage UnLoginPage = new UserPages.UnLoginPage();
		public static UserPages.ClassPage ClassPage = new UserPages.ClassPage();
		public static UserPages.SettingsPage SettingsPage = new UserPages.SettingsPage();
		private NavigationTransitionInfo? _transitionInfo = null;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void NavigationBarSel_SelectionChanged(iNKORE.UI.WPF.Modern.Controls.NavigationView sender, iNKORE.UI.WPF.Modern.Controls.NavigationViewSelectionChangedEventArgs args)
		{
			MainWindowSetState();
		}

		public void MainWindowSetState()
		{
			MainPage = new UserPages.MainPage();
			TasksPage = new UserPages.TasksPage();
			StudyPage = new UserPages.StudyPage();
			UnLoginPage = new UserPages.UnLoginPage();
			ClassPage = new UserPages.ClassPage();
			SettingsPage = new UserPages.SettingsPage();
			isLogin = !String.IsNullOrEmpty(UserStatus.AccessToken);
			var item = NavigationBarSel.SelectedItem;
			System.Windows.Controls.Page? page = null;
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
			else if (item == NavClassPage)
			{
				page = isLogin ? ClassPage : UnLoginPage;
			}
			else if (item == NavSettings)
			{
				page = SettingsPage;
			}
			if (page != null)
			{
				MainFrame.Navigate(page, _transitionInfo = new EntranceNavigationTransitionInfo());
			}
		}

		private void MyMainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			DataExchageStatic.window = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
		}
	}
}