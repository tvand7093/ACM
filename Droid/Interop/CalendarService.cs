using System;
using Xamarin.Forms;
using ACM.Droid.Interop;
using ACM.Interfaces;
using ACM.Models;
using Android.Content;
using Android.Provider;
using Java.Util;
using Android.Widget;


[assembly: Dependency(typeof(CalendarService))]
namespace ACM.Droid.Interop
{
	public class CalendarService : ICalendar
	{
		#region ICalendar implementation

		public void AddEventToCalendar (Event toAdd)
		{
			var ctx = Android.App.Application.Context.ApplicationContext;
			try{
				AddEvent(ctx, toAdd);
			}
			catch(Exception){
				Toast.MakeText (ctx,
					"There was an error adding the event to your calendar.",
					ToastLength.Long);
			}
		}

		private static void AddEvent(Context ctx, Event toAdd){
			ContentValues eventValues = new ContentValues ();
			var projection = new string[] {
				CalendarContract.Calendars.InterfaceConsts.Id
			};
			var cursor = ctx.ContentResolver.Query(CalendarContract.Events.ContentUri,
				projection, null, null, null);

			var calId = cursor.GetInt (0);

			eventValues.Put (CalendarContract.Events.InterfaceConsts.CalendarId,
				calId);
			eventValues.Put (CalendarContract.Events.InterfaceConsts.Title,
				toAdd.Topic);
			eventValues.Put (CalendarContract.Events.InterfaceConsts.Description,
				toAdd.Description);
			eventValues.Put (CalendarContract.Events.InterfaceConsts.Dtstart,
				GetDateTimeMS (toAdd.StartTime));
			eventValues.Put (CalendarContract.Events.InterfaceConsts.Dtend,
				GetDateTimeMS (toAdd.EndTime));

			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, 
				"PST");
			eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, 
				"PST");

			ctx.ContentResolver.Insert(CalendarContract.Events.ContentUri,
				eventValues);

			Toast.MakeText (ctx,
				"This event was added to your calendar!",
				ToastLength.Long);
		}

		private static long GetDateTimeMS (DateTime date)
		{
			Calendar c = Calendar.GetInstance (Java.Util.TimeZone.Default);
			c.Set (CalendarField.DayOfMonth, date.Day);
			c.Set (CalendarField.HourOfDay, date.Hour);
			c.Set (CalendarField.Minute, date.Minute);
			c.Set (CalendarField.Month, date.Month);
			c.Set (CalendarField.Year, date.Year);

			return c.TimeInMillis;
		}

		#endregion

		public CalendarService ()
		{
		}
	}
}

