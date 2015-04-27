using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CSAssistant.ViewModels;
using CSAssistant.Models;
using CSAssistant.Helpers;
using CSAssistant.Services;

namespace CSAssistant.Pages
{
	public partial class CoursePage : ContentPage
	{
		string url;
		Professor prof;

		public CoursePage (string name, string url, Professor prof)
		{
			BindingContext = new CourseViewModel (name, prof);;
			this.url = url;
			this.prof = prof;

			InitializeComponent ();

		}

		protected override void OnAppearing ()
		{
			var vm = BindingContext as CourseViewModel;
			if(vm.Assignments.Count == 0)
				CourseService.GetAssignments (url, vm.UpdateMessage, prof);
			base.OnAppearing ();
		}

	}
}

