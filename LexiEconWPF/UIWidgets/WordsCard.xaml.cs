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
		public ObservableCollection<WordsMeans> Meanings { get; set; }
		public WordsCard()
		{
			Meanings = new ObservableCollection<WordsMeans>
			{
				new WordsMeans{PartOfSpeech="vt.", Meaning="进入"},
				new WordsMeans{PartOfSpeech="n.", Meaning="使用...的权利"},
			};
			InitializeComponent();
			this.DataContext = this;
		}
	}
}
