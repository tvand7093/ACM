using System;

using Xamarin.Forms;
using CSAssistant.ViewModels;
using CSAssistant.Controls;

namespace CSAssistant.Pages
{
	public class DocumentView : ContentPage
	{
		public DocumentView (AssignmentViewModel toShow)
		{
			Title = toShow.Name;
			Content = new CustomWebView () {
				Source = toShow.Url
			};
		}
	}
}


