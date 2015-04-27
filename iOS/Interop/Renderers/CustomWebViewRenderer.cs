using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CSAssistant.iOS.Interop.Renderers;
using CSAssistant.Controls;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace CSAssistant.iOS.Interop.Renderers
{
	internal sealed class CustomWebViewRenderer : WebViewRenderer
	{
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
			var view = (UIWebView)NativeView;
			view.ScrollView.ScrollEnabled = true;
			view.ScalesPageToFit = true;
		}

	}
}

