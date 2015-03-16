using System;

using Xamarin.Forms;
using ACM.Models;
using ACM.Interfaces;

namespace ACM.ViewModels
{
	public class EventsViewModel : BaseListViewModel<Event>, IMessanger
	{
		#region IMessanger implementation

		public void Subscribe ()
		{
			MessagingCenter.Subscribe<AllData> (this, "AllDataFetched",
				(data) => {
					Items.Clear();
					Items.Add(data.Events);
				});
		}

		public void Unsubscribe ()
		{
			MessagingCenter.Unsubscribe<AllData> (this, "AllDataFetched");
		}

		#endregion

		public EventsViewModel ()
		{
			SelectionChangedCommand = new Command(() => {
				if(SelectedItem != null){
					SelectedItem = null;
				}
			});
		}
	}
}


