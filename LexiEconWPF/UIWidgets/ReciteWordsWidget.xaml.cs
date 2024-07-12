using iNKORE.UI.WPF.Modern.Controls;
using LexiEconWPF.AppFunctions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Windows.Media.Protection.PlayReady;

namespace LexiEconWPF.UIWidgets
{
	/// <summary>
	/// ReciteWordsWidget.xaml 的交互逻辑
	/// </summary>
	public partial class ReciteWordsWidget : UserControl
	{
		public ObservableCollection<Word> WordsLearning { get; set; }
		public ObservableCollection<WordsMeans> WordsMean { get; set; }
		public ObservableCollection<ExampleSentences> ExampleSentences { get; set; }
		public static string PreviousValue { get; set; }
		public ReciteWordsWidget()
		{
			InitializeComponent();
		}

		private async void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Add("access-token", UserStatus.AccessToken);
			HttpResponseMessage responseMessage = await client.GetAsync($"{LexiEconSettings.LexiHost}{EndPointLexi.TaskGetWords}");
			string data = await responseMessage.Content.ReadAsStringAsync();
			dynamic dataGet = JObject.Parse(data);

			int words = dataGet.data.Count;

			WordsLearning = new ObservableCollection<Word>();

			for (int i = 0; i < words; i++)
			{
				List<Translation> translations = GetTranslationStr(dataGet, i);
				List<ExampleSentences> sentences = GetSentenceDeserialStr(dataGet, i);
				int wordsMeansCount = translations.Count;
				int sentenceCount = sentences.Count;
				int taskId = dataGet.data[i].task_id;
				HttpResponseMessage getTaskFinished = await client.GetAsync($"{LexiEconSettings.LexiHost}{EndPointLexi.CheckTask}?task_id={taskId}");
				string taskInfo = await getTaskFinished.Content.ReadAsStringAsync();
				dynamic taskInfoGet = JObject.Parse(taskInfo);

				try
				{
					if (taskInfoGet.data[0].status == "已完成" && DataExchageStatic.TaskId != taskId) { }
					else
					{
						string ukPhone = dataGet.data[i].uk_phone;
						string usPhone = dataGet.data[i].us_phone;
						WordsMean = new ObservableCollection<WordsMeans>();
						ExampleSentences = new ObservableCollection<ExampleSentences>();
						for (int j = 0; j < wordsMeansCount; j++)
						{
							WordsMean.Add(new WordsMeans
							{
								Meaning = translations[j].tranCn,
								PartOfSpeech = translations[j].pos
							});
						}
						for (int j = 0; j < sentenceCount; j++)
						{
							ExampleSentences.Add(new LexiEconWPF.ExampleSentences
							{
								sContent = sentences[j].sContent,
								sCn = sentences[j].sCn,
							});
						}
						WordsLearning.Add(new Word
						{
							WordId = dataGet.data[i].word_id,
							UkPhone = $"/{ukPhone}/",
							UsPhone = $"/{usPhone}/",
							Name = dataGet.data[i].word_name,
							WordsMeans = WordsMean,
							ExampleSentences = ExampleSentences,
						});
					}
				}
				catch (ArgumentOutOfRangeException)
				{

				}
			}
			this.DataContext = this;
		}
		private static List<ExampleSentences> GetSentenceDeserialStr(dynamic dataGet, int i)
		{
			string sentenceInfo = dataGet.data[i].sentence;
			sentenceInfo = sentenceInfo.Replace("'", "\"");
			sentenceInfo = sentenceInfo.Replace(@"\", @"\\");
			List<ExampleSentences> sentences = JsonConvert.DeserializeObject<List<ExampleSentences>>(sentenceInfo);
			return sentences;
		}

		private static List<Translation> GetTranslationStr(dynamic dataGet, int i)
		{
			string wordInfo = dataGet.data[i].trans;
			wordInfo = wordInfo.Replace("'", "\"");
			wordInfo = wordInfo.Replace(@"\", @"\\");
			List<Translation> translations = JsonConvert.DeserializeObject<List<Translation>>(wordInfo);
			return translations;
		}

		private async void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			var items = StudyItem.Items;
			int wordsCheckCounts = StudyItem.Items.Count;
			for (int i = 0; i < wordsCheckCounts; i++)
			{
				ContentPresenter cp = StudyItem.ItemContainerGenerator.ContainerFromItem(items[i]) as ContentPresenter;
				AutoSuggestBox asb = FindVisualChild<AutoSuggestBox>(cp);
				StackPanel stackPanel = FindVisualChild<StackPanel>(cp);

				foreach (var item in items)
				{
					if (stackPanel != null)
					{
						var nameTextBlock = stackPanel.Children.OfType<TextBlock>().FirstOrDefault();

						if (nameTextBlock != null)
						{
							nameTextBlock.Visibility = Visibility.Visible;
							await Handler(asb, nameTextBlock);
						}
					}
				}
			}
		}

		private static async Task Handler(AutoSuggestBox asb, TextBlock? nameTextBlock)
		{
			HttpClient httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Add("access-token", UserStatus.AccessToken);
			int correctNums = 0;
			if (asb.Text == asb.Tag.ToString())
			{
				if (PreviousValue == asb.Text)
				{
					return;
				}
				else
				{
					correctNums++;
					PreviousValue = asb.Text;
					asb.Foreground = new SolidColorBrush(Colors.Black);
					int wordId = Convert.ToInt32(nameTextBlock.Tag.ToString());
					UserReciteStatus status = new UserReciteStatus();
					status.word_id = wordId;
					status.review_category = "spell";
					status.situation = "true";
					string postStatusStr = JsonConvert.SerializeObject(status);
					var content = new StringContent(postStatusStr, Encoding.UTF8, "application/json");
					HttpResponseMessage resp = await httpClient.PostAsync(requestUri: $"{LexiEconSettings.LexiHost}{EndPointLexi.SubmitWordsStatus}", content: content);
				}
			}
			else if (asb.Text != asb.Tag.ToString())
			{
				if (PreviousValue == asb.Text)
				{
					return;
				}
				else
				{
					PreviousValue = asb.Text;
					asb.Foreground = new SolidColorBrush(Colors.Red);
					int wordId = Convert.ToInt32(nameTextBlock.Tag.ToString());
					UserReciteStatus status = new UserReciteStatus();
					status.word_id = wordId;
					status.review_category = "spell";
					status.situation = "false";
					string postStatusStr = JsonConvert.SerializeObject(status);
					var content = new StringContent(postStatusStr, Encoding.UTF8, "application/json");
					HttpResponseMessage resp = await httpClient.PostAsync(requestUri: $"{LexiEconSettings.LexiHost}{EndPointLexi.SubmitWordsStatus}", content: content);
				}

			}
		}

		public static T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
		{
			if (depObj != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					if (child != null && child is T)
					{
						return (T)child;
					}

					T childItem = FindVisualChild<T>(child);
					if (childItem != null) return childItem;
				}
			}
			return null;
		}

	}

}
