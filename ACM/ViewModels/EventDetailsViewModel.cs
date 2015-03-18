using System;
using ACM.Models;
using System.Windows.Input;
using Xamarin.Forms;
using ACM.Interfaces;

namespace ACM.ViewModels
{
	public class EventDetailsViewModel : BaseViewModel<Event>
	{
		public ICommand AddToCalendarCommand { get; private set; }
		public EventDetailsViewModel (Event dataSource)
		{
			DataSource = dataSource;
			AddToCalendarCommand = new Command(() => {
				var calendar = DependencyService.Get<ICalendar>();
				calendar.AddEventToCalendar(DataSource);
			});
		}
	}
}

