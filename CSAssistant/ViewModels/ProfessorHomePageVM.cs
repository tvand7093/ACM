using System;
using System.Collections.Generic;
using CSAssistant.Models;
using CSAssistant.Services;
using CSAssistant.Interfaces;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using CSAssistant.Helpers;
using CSAssistant.Pages;

namespace CSAssistant.ViewModels
{
	public class ProfessorHomePageVM : BaseViewModel, ISubscriber
	{
		public ObservableCollection<Course> Courses { get; set; }
		public string Title {get;set;}
		public string ProfessorName {get;set;}
		public string Image { get;set;}
		public bool ShowLoading {get;set;}
		public bool ShowContent { get; set; }

		Course selection;
		public Course Selection {
			get { return selection; }
			set {
				selection = value;
				OnPropertyChanged ("Selection");
				if (selection != null) {
					MessagingCenter.Send (Selection, PushMessage);
				}
			}
		}

		public string UpdateMessage {get; private set;}
		public string PushMessage { get; private set; }
		public Color ImageBackgoundColor {get;private set;}
		public Color SemesterTextColor { get; private set; }
		Professor professor;

		public ProfessorHomePageVM (Professor toLoad)
		{
			ShowLoading = true;
			ShowContent = false;
			Courses = new ObservableCollection<Course> ();

			if (toLoad == Professor.Lang) {
				UpdateMessage = Messaging.LangsHomePageRecieved;
				PushMessage = Messaging.LangsCourseChanged;
				ImageBackgoundColor = Color.FromHex ("#CCCCCC");
				SemesterTextColor = Color.Black;
			} else {
				UpdateMessage = Messaging.CochransHomePageRecieved;
				PushMessage = Messaging.CochransCourseChanged;
				ImageBackgoundColor = Color.Black;
				SemesterTextColor = Color.Yellow;
			}

			professor = toLoad;

		}
			
		void NotifyAll(){
			OnPropertyChanged ("Title");
			OnPropertyChanged ("Professor");
			OnPropertyChanged ("Image");
			OnPropertyChanged ("ShowLoading");
			OnPropertyChanged ("ShowContent");
		}

		void Reload(ProfessorHomePageVM recieved){
			if (recieved != null) {
				Title = recieved.Title;
				ProfessorName = recieved.ProfessorName;
				Image = recieved.Image;
				ShowLoading = false;
				ShowContent = true;
				Courses.Clear ();
				Courses.AddRange (recieved.Courses);
				NotifyAll ();
			}

		}

		#region ISubscriber implementation

		public void Subscribe ()
		{
			MessagingCenter.Subscribe<ProfessorHomePageVM> (this, UpdateMessage, Reload);
			MessagingCenter.Subscribe<Course> (this, PushMessage, async (toPush) => {
				await Navigation.PushAsync(new CoursePage(toPush, professor), true);
				Selection = null;
			});

		}

		public void Unsubscribe ()
		{
			MessagingCenter.Unsubscribe<ProfessorHomePageVM> (this, UpdateMessage);
			MessagingCenter.Unsubscribe<Course> (this, PushMessage);
		}

		#endregion
	}
}

