using System;
using Xamarin.Forms;
using CSAssistant.Interfaces;

namespace CSAssistant.Pages
{
	public class SubscriberNavigationPage : NavigationPage
	{
		public SubscriberNavigationPage (Page root) : base(root)
		{
			Title = root.Title;
			Icon = root.Icon;
			Pushed += (object sender, NavigationEventArgs e) => ChangedPages (e.Page, true);
			Popped += (object sender, NavigationEventArgs e) => ChangedPages (e.Page, false);
			ChangedPages (root, true);
		}

		void ChangedPages(Page newPage, bool pushing){
			var sub = newPage.BindingContext as ISubscriber;
			if (sub != null) {
				if (pushing)
					sub.Subscribe ();
				else
					sub.Unsubscribe ();
			}
		}
	}
}

