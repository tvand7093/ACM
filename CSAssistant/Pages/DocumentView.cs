using System;

using Xamarin.Forms;
using CSAssistant.ViewModels;
using CSAssistant.Controls;

namespace CSAssistant.Pages
{
	public class DocumentView : ContentPage
	{
		readonly WeakReference webView;

		public DocumentView (string title, string url)
		{
			Title = title;
	
			var customWeb = new CustomWebView () {
				Source = url
			};

			customWeb.Navigating += Navigating;
			customWeb.Navigated += Navigated;
				
			webView = new WeakReference (customWeb);

			Content = webView.Target as CustomWebView;
		}

		void Navigated(object sender, WebNavigatedEventArgs e) {
			IsBusy = false;
		}

		void Navigating(object sender, WebNavigatingEventArgs e) {
			IsBusy = true;
		}

		protected override void OnDisappearing ()
		{
			if (webView.IsAlive && webView.Target != null) {
				var view = webView.Target as CustomWebView;
				view.Navigated -= Navigated;
				view.Navigating -= Navigating;
				webView.Target = null;
			}
			base.OnDisappearing ();
		}
	}
}


