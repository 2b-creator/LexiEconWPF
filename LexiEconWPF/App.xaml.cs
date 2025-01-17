﻿using LexiEconWPF.AppFunctions;
using System.Configuration;
using System.Data;
using System.Windows;

namespace LexiEconWPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override async void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			BeforeStartUp startUp = new BeforeStartUp();
			startUp.ReadJsonAccess();
			startUp.CheckAppUpdate();
			//startUp.CreateLogFile();
		}
	}

}
