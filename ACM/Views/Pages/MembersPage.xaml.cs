using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ACM.ViewModels;

namespace ACM.Views.Pages
{
	public partial class MembersPage : ContentPage
	{
		public MembersPage ()
		{
			var vm = new MembersViewModel ();
			BindingContext = vm;
			this.Appearing += vm.Loaded;
			this.Disappearing += (object sender, EventArgs e) => Appearing -= vm.Loaded;
			InitializeComponent ();
		}
	}
}

