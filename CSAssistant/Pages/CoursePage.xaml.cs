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
		public CoursePage (Course toDisplay, Professor prof)
		{
			var vm = new CourseViewModel (toDisplay, prof);
			BindingContext = vm;
			InitializeComponent ();
			Appearing += (object sender, EventArgs e) => {
				if(vm.Assignments.Count == 0)
					CourseService.GetAssignments (toDisplay.Url, vm.UpdateMessage, prof);
			};
		}
	}
}

