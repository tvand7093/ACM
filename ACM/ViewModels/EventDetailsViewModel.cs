using System;
using ACM.Models;

namespace ACM.ViewModels
{
	public class EventDetailsViewModel : BaseViewModel<Event>
	{
		public EventDetailsViewModel (Event dataSource)
		{
			DataSource = dataSource;
		}
	}
}

