﻿using LexiEconWPF.AppFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
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
	/// NoticeUpdateApp.xaml 的交互逻辑
	/// </summary>
	public partial class NoticeUpdateApp : UserControl
	{
		public NoticeUpdateApp()
		{
			InitializeComponent();
		}

		private void UpdateNow_Click(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo($"{LexiEconSettings.LexiHost}{EndPointLexi.DownloadLatest}") { UseShellExecute = true }); System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo($"{LexiEconSettings.LexiHost}{EndPointLexi.DownloadLatest}") { UseShellExecute = true });
		}
    }
}
