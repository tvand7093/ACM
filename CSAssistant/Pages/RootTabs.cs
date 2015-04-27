using Xamarin.Forms;
using CSAssistant.Models;

namespace CSAssistant.Pages
{
	internal sealed class RootTabs : TabbedPage
	{
		public RootTabs ()
		{
			Children.Add (new SubscriberNavigationPage(new ProfessorHomePage(Professor.Lang){
				Title = "Dick Lang"
			}));
			Children.Add (new SubscriberNavigationPage(new ProfessorHomePage(Professor.Cochran){
				Title = "Wayne Cochran"
			}));
		}
	}
}

