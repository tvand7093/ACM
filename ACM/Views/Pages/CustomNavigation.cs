using System;
using Xamarin.Forms;
using ACM.Interfaces;

namespace ACM.Views.Pages
{
	public class CustomNavigation : NavigationPage
	{
		public CustomNavigation (Page content) : base(content)
		{
			Title = content.Title;

			Popped += (object sender, NavigationEventArgs e) => HandleNavigation(e.Page, true);
			Pushed += (object sender, NavigationEventArgs e) => HandleNavigation (e.Page, false);

			HandleNavigation (content, false);
			Icon = content.Icon;
		}

		private void HandleNavigation(Page page, bool isPopping){
			var messanger = page.BindingContext as IMessanger;
			if(messanger != null){
				if (isPopping) {
					messanger.Unsubscribe ();
				} else {
					messanger.Subscribe();
				}
			}
		}
	}
}

