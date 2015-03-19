using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using ACM.Views.Pages;
using ACM.iOS.Renderers;
using Foundation;
using ACM.Views.Themes;

[assembly: ExportRenderer(typeof(CustomListView), typeof(CustomListViewRenderer))]
namespace ACM.iOS.Renderers
{
	public class CustomListViewRenderer : ListViewRenderer
	{
		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
		}
		 
		public CustomListViewRenderer ()
		{
			
		}
	}
}

