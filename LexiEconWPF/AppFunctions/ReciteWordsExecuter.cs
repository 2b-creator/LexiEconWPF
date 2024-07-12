using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiEconWPF.AppFunctions
{
	public class ReciteWordsExecuter
	{
		private readonly int _taskId;
		public ReciteWordsExecuter(int taskId)
		{
			_taskId = taskId;
		}
		public void Execute()
		{
			DataExchageStatic.TaskId = _taskId;
			ReciteWordsWindow reciteWordsWindow = new ReciteWordsWindow();
			reciteWordsWindow.ShowDialog();
		}
	}
}
