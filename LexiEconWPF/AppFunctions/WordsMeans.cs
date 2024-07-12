﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiEconWPF
{
    public class WordsMeans
    {
		public string PartOfSpeech { get; set; }
		public string Meaning { get; set; }
		
	}
	public class TasksViewer
	{
		public int TaskId { get; set; }
		public string TaskName { get; set; }
		public string? TaskDescription { get; set; }
	}
	public class Word
	{
		public string Name { get; set; }
		public string UsPhone { get; set; }
		public string UkPhone { get; set; }
		public ObservableCollection<ExampleSentences> ExampleSentences { get; set; }
		public ObservableCollection<WordsMeans> WordsMeans { get; set; }
	}
	public class WordsLearning
	{
		public string Name { get; set; }
		public string UsPhone { get; set; }
		public string UkPhone { get; set; }
		public string ExampleSentence { get; set; }
		public ObservableCollection<WordsMeans> WordsMeans { get; set;}
	}
	public class Translation
	{
		public string tranCn { get; set; }
		public string descOther { get; set; }
		public string pos { get; set; }
		public string descCn { get; set; }
		public string tranOther { get; set; }
	}
	public class ExampleSentences
	{
		public string sContent { get; set; }
		public string sCn { get; set; }
	}
}
