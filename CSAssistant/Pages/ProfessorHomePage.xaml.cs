using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CSAssistant.Helpers;
using CSAssistant.ViewModels;
using CSAssistant.Services;

namespace CSAssistant.Pages
{
	public partial class ProfessorHomePage : ContentPage
	{
		public ProfessorHomePage (Professor toDisplay)
		{
			var vm = new ProfessorHomePageVM (toDisplay);
			BindingContext = vm;
			HomePageService.FetchHomePage (toDisplay, vm.UpdateMessage);
			InitializeComponent ();
		}
	}
}

