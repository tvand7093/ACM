using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using ACM.Views.Pages;
using ACM.iOS.Renderers;
using Foundation;
using ACM.Views.Themes;
using System.Linq;
using UIKit;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(CustomListView), typeof(CustomListViewRenderer))]
namespace ACM.iOS.Renderers
{
	public class CustomListViewRenderer : ListViewRenderer
	{
		
		private UIRefreshControl refreshControl;

		protected override void OnElementChanged (ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged (e);

			if (refreshControl != null) {
				return; 
			}

			//go through all subviews to find the refresh control.
			foreach (var view in Control.Subviews) {
				refreshControl = view as UIRefreshControl;
				if (refreshControl != null)
					break;
			}
			//set the loader to white
			refreshControl.TintColor = Color.White.ToUIColor ();

		}
		 
		public CustomListViewRenderer ()
		{
			
		}
	}
}

