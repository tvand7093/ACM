﻿using System;

using Xamarin.Forms;
using ACM.Services;
using ACM.Views.Pages;

namespace ACM
{
	public class App : Application
	{
		public App ()
		{
			// The root page of your application
			MainPage = new CustomTabbedPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

