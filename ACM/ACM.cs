using System;

using Xamarin.Forms;
using ACM.Views.Pages;
using ACM.Azure;

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
			AzureRepo.Clear();
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

