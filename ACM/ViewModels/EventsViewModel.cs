using System;

using Xamarin.Forms;
using ACM.Models;
using ACM.Interfaces;
using ACM.Views.Pages;

namespace ACM.ViewModels
{
	public class EventsViewModel : BaseListViewModel<Event>, IMessanger
	{
		#region IMessanger implementation

		private const string DataMessage = "AllDataFetched";

		public void Subscribe ()
		{
			MessagingCenter.Subscribe<AllData> (this, DataMessage,
				(data) => {
					Items.Clear();
					Items.Add(data.Events);
				});
		}

		public void Unsubscribe ()
		{
			MessagingCenter.Unsubscribe<AllData> (this, DataMessage);
		}

		#endregion

		public EventsViewModel ()
		{
			SelectionChangedCommand = new Command(async () => {
				// Analysis disable once RedundantCheckBeforeAssignment
				if(SelectedItem != null){
					await Navigation.PushAsync(new EventDetailPage {
						BindingContext = new EventDetailsViewModel(SelectedItem)
					});
					SelectedItem = null;
				}
			});
		}
	}
}


