using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using ACM.Views.Themes;
using Xamarin.Forms;
using ACM.Views.Pages;
using ACM.iOS.Renderers;

[assembly: ExportRenderer (typeof (CustomListView), typeof (CustomListViewRenderer))]
namespace ACM.iOS.Renderers
{
	public class CustomListViewRenderer : ListViewRenderer
	{
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			var control = (UITableView)Control;
			control.BackgroundColor = ColorThemes.CougarGrey.ToUIColor();
		}
	}
}

