using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
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
	/// WordsCard.xaml 的交互逻辑
	/// </summary>
	public partial class WordsCard : UserControl
	{
		public ObservableCollection<Word> Word { get; set; }
		public WordsCard()
		{
			Word = new ObservableCollection<Word>
			{
				new Word
				{
					Name = "Access",
					WordsMeans = new ObservableCollection<WordsMeans>
					{
						new WordsMeans { PartOfSpeech = "n.", Meaning = "The means or opportunity to approach or enter a place." },
						new WordsMeans { PartOfSpeech = "v.", Meaning = "To approach or enter (a place)." }
					}
				},
			};
			InitializeComponent();
			this.DataContext = this;
		}
	}
}
