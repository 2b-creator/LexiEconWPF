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
	/// WordsCardLeaning.xaml 的交互逻辑
	/// </summary>

	public partial class WordsCardLeaning : UserControl
	{
		public ObservableCollection<Word> Word { get; set; }
		public WordsCardLeaning()
		{
			InitializeComponent();
		}

		private async void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			HttpClient client = new HttpClient();
			HttpResponseMessage responseMessage = await client.GetAsync($"{LexiEconSettings.LexiHost}{EndPointLexi.TaskGetWords}");
			string data = await responseMessage.Content.ReadAsStringAsync();
			dynamic dataGet = JObject.Parse(data);
			int words = dataGet.data.Count();
			HttpResponseMessage responseWords = await client.GetAsync($"{LexiEconSettings.LexiHost}{EndPointLexi.IdGetWords}");
			string wordsData = await responseWords.Content.ReadAsStringAsync();
			dynamic wordsDataGet = JObject.Parse(wordsData);
			Word = new ObservableCollection<Word>();
			for (int i = 0; i < words; i++)
			{

			}
		}
	}
}
