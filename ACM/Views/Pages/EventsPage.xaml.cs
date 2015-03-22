using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ACM.ViewModels;

namespace ACM.Views.Pages
{
	public partial class EventsPage : ContentPage
	{
		public EventsPage ()
		{
			var vm = new EventsViewModel ();
			BindingContext = vm;
			this.Appearing += vm.Loaded;
			this.Disappearing += (object sender, EventArgs e) => Appearing -= vm.Loaded;
			InitializeComponent ();
		}
	}
}

