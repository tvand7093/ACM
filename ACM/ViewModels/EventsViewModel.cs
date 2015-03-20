using System;

using Xamarin.Forms;
using ACM.Models;
using ACM.Interfaces;
using ACM.Views.Pages;
using ACM.Azure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACM.ViewModels
{
	public class EventsViewModel : BaseListViewModel<Event>, IMessanger
	{
		#region IMessanger implementation

		private const string DataMessage = "AllDataFetched";

		public void Subscribe ()
		{
			MessagingCenter.Subscribe<IEnumerable<Event>> (this, DataMessage,
				(data) => {
					IsRefreshing = false;
					Items.Clear();
					Items.Add(data);
				});
		}

		public void Unsubscribe ()
		{
			MessagingCenter.Unsubscribe<IEnumerable<Event>> (this, DataMessage);
		}

		#endregion

		private void FetchData() {
			IsRefreshing = true;
			AzureRepo.GetEventsList (DataMessage);
		}

		public EventsViewModel ()
		{
			PullToRefreshCommand = new Command (FetchData);

			SelectionChangedCommand = new Command(async () => {
				// Analysis disable once RedundantCheckBeforeAssignment
				if(SelectedItem != null){
					await Navigation.PushAsync(new EventDetailPage {
						BindingContext = new EventDetailsViewModel(SelectedItem)
					});
					SelectedItem = null;
				}
			});
			FetchData ();
		}
	}
}


