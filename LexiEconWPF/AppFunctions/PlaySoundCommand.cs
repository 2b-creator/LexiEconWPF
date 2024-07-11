using LexiEconWPF.UIWidgets;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LexiEconWPF.AppFunctions
{
	public class PlaySoundCommand : ICommand
	{
		private readonly WordsCardLeaning _wordsCardLeaning;
		private readonly string _wordName;
		private readonly int _UKorUs;
		public event EventHandler? CanExecuteChanged;
		public PlaySoundCommand(WordsCardLeaning wordsCardLeaning, string wordName, int ukOrUs)
		{
			_wordsCardLeaning = wordsCardLeaning;
			_wordName = wordName;
			_UKorUs = ukOrUs;
		}

		public bool CanExecute(object? parameter)
		{
			return true;
		}

		public void Execute(object? parameter)
		{
			if (_UKorUs == 0)
			{
				using (var mf = new MediaFoundationReader($"http://dict.youdao.com/dictvoice?type=1&audio={_wordName}"))
				{
					using (var wo = new WasapiOut())
					{
						wo.Init(mf);
						wo.Play();
						while (wo.PlaybackState == PlaybackState.Playing)
						{
							Thread.Sleep(1000);
						}
					}
				}
			}
            else
            {
				using (var mf = new MediaFoundationReader($"http://dict.youdao.com/dictvoice?type=0&audio={_wordName}"))
				{
					using (var wo = new WasapiOut())
					{
						wo.Init(mf);
						wo.Play();
						while (wo.PlaybackState == PlaybackState.Playing)
						{
							Thread.Sleep(1000);
						}
					}
				}
			}
        }
	}
}
