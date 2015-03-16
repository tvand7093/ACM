using System;

using Xamarin.Forms;

namespace ACM.ViewModels
{
	public class MembersViewModel : ContentPage
	{
		public MembersViewModel ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


