using System;
using Xamarin.Forms;

namespace ACM.Views.Pages
{
	public class CustomTabbedPage : TabbedPage
	{
		public CustomTabbedPage ()
		{
			Children.Add (new CustomNavigation (new EventsPage ()));
			Children.Add (new CustomNavigation (new MembersPage ()));
		}
	}
}

