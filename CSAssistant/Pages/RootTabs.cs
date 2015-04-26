﻿using System;
using Xamarin.Forms;
using CSAssistant.Helpers;

namespace CSAssistant.Pages
{
	public class RootTabs : TabbedPage
	{
		public RootTabs ()
		{
			Children.Add (new SubscriberNavigationPage(new ProfessorHomePage(Professor.Lang){
				Title = "Dick Lang"
			}));
			Children.Add (new SubscriberNavigationPage(new ProfessorHomePage(Professor.Cochran){
				Title = "Wayne Cochran"
			}));
		}
	}
}

