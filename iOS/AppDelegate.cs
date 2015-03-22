using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.MobileServices;

using Foundation;
using UIKit;

namespace ACM.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();
			CurrentPlatform.Init ();
			app.SetStatusBarStyle (UIStatusBarStyle.LightContent, false);
			LoadApplication (new App ());
			return base.FinishedLaunching (app, options);
		}
	}
}

