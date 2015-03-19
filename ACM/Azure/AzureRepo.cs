using System;
using Microsoft.WindowsAzure.MobileServices;
using ACM.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using ACM.Models;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Xamarin.Forms;
using System.Diagnostics;

namespace ACM.Azure
{
	public sealed class AzureRepo
	{
		private const string applicationURL = "https://xamandor-acm.azure-mobile.net/";
		private const string applicationKey = "oXZfygXqaBkxZLeANpuVcDjIJnFaUI45";
		private static MobileServiceClient client;
		private static IMobileServiceTable<Member> membersTable;
		private static IMobileServiceTable<Event> eventsTable;
		private static IMobileServiceTable<GuestSpeaker> speakersTable;
		private static IEnumerable<Member> members;
		private static IEnumerable<Event> events;
		private static IEnumerable<GuestSpeaker> speakers;
		private static bool isInitialized;

		static AzureRepo(){
			isInitialized = false;
		}

		#if DEBUG 
		public static void SetupTestData() {
			Init (async () => {
				var newEvent = new Event {
					Topic = "Intorduction to Cross Platform Development",
					Speaker = new GuestSpeaker {
						Name = "Tyler Vanderhoef",
						JobTitle = "Xamarin Student Ambassador",
						Company = "AgencyRM",
						Twitter = "sonoilmedico",
						Picture = "http://www.gravatar.com/avatar/f24777df33c08e6ffd6ef6bc0d4b2619.png"
					},
					StartTime = new DateTime(2015, 3, 23, 14, 30, 0),
					EndTime = new DateTime(2015, 3, 23, 15, 30, 0),
					Location = "VECS 104",
					Description = "An introduction into cross platform development using C# and the Xamarin platform."
				};

				var p = new Member {
					Name = "Elijah Houle",
					Role = "President",
					Twitter = "twitter",
					Picture = "http://xamarin.com/images/index/ide-xamarin-studio.png"
				};

				var vp = new Member {
					Name = "Kyle Nosar",
					Role = "Vice President",
					Twitter = "twitter",
					Picture = "http://xamarin.com/images/index/ide-xamarin-studio.png"
				};

				await membersTable.InsertAsync(p);
				await membersTable.InsertAsync(vp);
				await eventsTable.InsertAsync(newEvent);
			});
		}
		#endif

		public static async void Init(Action complete){
			try {
				if (!isInitialized) {
					client = new MobileServiceClient (applicationURL, applicationKey);

					membersTable = client.GetTable<Member> ();
					eventsTable = client.GetTable<Event> ();
					speakersTable = client.GetTable<GuestSpeaker> ();

					members = await membersTable.ToListAsync ();
					events = await eventsTable.ToListAsync();
					speakers = await speakersTable.ToListAsync ();
					isInitialized = true; 
				}
			}
			catch(Exception e) {
				Debug.WriteLine ("ERROR: There was an error initialing Azure Mobile Services.");
				Debug.WriteLine ("MESSAGE: " + e.Message);
				Debug.WriteLine ("STACK: " + e.StackTrace);
				Debug.WriteLine ("SOURCE " + e.Source);
				isInitialized = false;
			}
			finally {
				if (members == null)
					members = new List<Member> ();
				if (events == null)
					events = new List<Event> ();
				if (speakers == null)
					speakers = new List<GuestSpeaker> ();
			}
			complete ();
		}

		public static void Clear() {
			members = new List<Member>();
			events = new List<Event> ();
			speakers = new List<GuestSpeaker> ();
			isInitialized = false;
		}

		public static void GetEventsList(string message) 
		{
			Init (async () => {
				events = await eventsTable.OrderBy(a => a.StartTime).ToListAsync();
				MessagingCenter.Send<IEnumerable<Event>> (events ?? new List<Event>(), message);
			});
		}

		public static void GetMembersList(string message) 
		{
			Init (async () => {
				members = await membersTable.OrderBy(a => a.Role).ToListAsync();
				MessagingCenter.Send<IEnumerable<Member>> (members ?? new List<Member>(), message);
			});
		}
	}
}

