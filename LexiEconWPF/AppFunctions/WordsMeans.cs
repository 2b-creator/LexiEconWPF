﻿using System;
using System.Collections.Generic;
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
	public class WordsMeansLearning
	{
		public string PartOfSpeech { get; set; }
		public string Meaning { get; set; }
	}
	public class TasksViewer
	{
		public string TaskName { get; set; }
		public string? TaskDescription { get; set; }
	}
}
