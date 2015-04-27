﻿using System.Collections.ObjectModel;
using CSAssistant.Models;
using CSAssistant.Interfaces;
using CSAssistant.Helpers;
using Xamarin.Forms;
using System.Collections.Generic;
using CSAssistant.Pages;
using System.Diagnostics;
using System;

namespace CSAssistant.ViewModels
{
	internal sealed class CourseViewModel : BaseViewModel, ISubscriber
	{
		public string Title {get;set;}
		public string UpdateMessage { get; private set; }
		string PushMessage {get;set;}
		public bool ShowLoading {get;set;}
		public bool ShowContent { get; set; }
		AssignmentViewModel selection;
		public AssignmentViewModel Selection {
			get { return selection; }
			set {
				selection = value;
				OnPropertyChanged ("Selection");
				if (selection != null) {
					Push ();
				}
			}
		}

		async void Push(){
			await Navigation.PushAsync(new DocumentView(Selection.Name, Selection.Url));
			Selection = null;
		}

		public ObservableCollection<AssignmentViewModel> Assignments { get; set; }

		public CourseViewModel (string name, Professor prof)
		{
			Title = name;
			ShowLoading = true;
			ShowContent = false;
			UpdateMessage = prof == Professor.Lang ? Messaging.LangsCourseRecieved
				: Messaging.CochransCourseRecieved;			
			Assignments = new ObservableCollection<AssignmentViewModel> ();
		}

		#region ISubscriber implementation

		public void Subscribe ()
		{
			//Lambda!
			MessagingCenter.Subscribe<List<AssignmentViewModel>> (this, UpdateMessage,
				(assignments) => {
					Assignments.AddRange (assignments);
					ShowLoading = false;
					ShowContent = true;
					OnPropertyChanged("ShowLoading");
					OnPropertyChanged("ShowContent");
				});
		}

		public void Unsubscribe ()
		{
			MessagingCenter.Unsubscribe<List<AssignmentViewModel>>  (this, UpdateMessage);
			Selection = null;
			Assignments.Clear ();
			Assignments = null;
		}

		#endregion
	}
}

