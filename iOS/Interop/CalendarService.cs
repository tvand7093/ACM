using System;
using ACM.Interfaces;
using ACM.Models;
using ACM.iOS.Interop;
using Xamarin.Forms;
using EventKit;
using Foundation;
using UIKit;
using System.Threading.Tasks;

[assembly: Dependency(typeof(CalendarService))]
namespace ACM.iOS.Interop
{
	public class CalendarService : ICalendar
	{
		private static EKEventStore eventStore;
		public static EKEventStore EventStore {
			get { return eventStore; }
		}



		#region ICalendar implementation

		private NSDate ConvertDate(DateTime toConvert){
			DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
				new DateTime(2001, 1, 1, 0, 0, 0) );


			if (toConvert.IsDaylightSavingTime ()) {
				toConvert = toConvert.AddHours (-1);
			}

			return NSDate.FromTimeIntervalSinceReferenceDate(
				(toConvert - reference).TotalSeconds);
		}

		private void SetNewEvent(Event toAdd){
			EKEvent newEvent = EKEvent.FromStore ( EventStore );
			// set the alarm for 10 minutes from now
			newEvent.AddAlarm ( EKAlarm.FromDate ( ConvertDate(toAdd.StartTime.AddMinutes ( -30 )) ) );
			// make the event start 20 minutes from now and last 30 minutes
			newEvent.StartDate = ConvertDate(toAdd.StartTime);
			newEvent.EndDate = ConvertDate(toAdd.EndTime);
			newEvent.Title = toAdd.Topic;
			newEvent.Notes = toAdd.Description;
			newEvent.Calendar = EventStore.DefaultCalendarForNewEvents;

			NSError e;
			EventStore.SaveEvent ( newEvent, EKSpan.ThisEvent, out e );
			if (e == null) {
				UIApplication.SharedApplication.InvokeOnMainThread (() => {
					// Analysis disable once ConvertToLambdaExpression
					new UIAlertView ("Event Created", 
						"This event was added to you calendar.", null,
						"ok", null).Show ();
				});
			}
		}

		public void AddEventToCalendar (Event toAdd)
		{
			EventStore.RequestAccess (EKEntityType.Event, 
				(bool granted, NSError e) => {
					if (granted){
						//do something here
						SetNewEvent(toAdd); 
					}
					else {
						new UIAlertView ( "Access Denied", 
							"User Denied Access to Calendar Data", null,
							"ok", null).Show ();
					}
				} );
		}

		#endregion

		public CalendarService ()
		{

		}

		static CalendarService ()
		{
			eventStore = new EKEventStore ( );
		}
	}
}

