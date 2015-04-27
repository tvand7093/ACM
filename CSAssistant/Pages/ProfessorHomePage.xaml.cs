
using Xamarin.Forms;
using CSAssistant.ViewModels;
using CSAssistant.Services;
using CSAssistant.Models;

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

