using System;

using Xamarin.Forms;
using ACM.Interfaces;
using ACM.Models;
using System.Collections.Generic;
using ACM.Azure;

namespace ACM.ViewModels
{
	public class MembersViewModel : BaseListViewModel<Member>, IMessanger
	{
		
		#region IMessanger implementation

		private const string DataMessage = "AllDataFetched";

		public void Subscribe ()
		{
			MessagingCenter.Subscribe<IEnumerable<Member>> (this, DataMessage,
				(data) => {
					IsRefreshing = false;
					Items.Clear();
					Items.Add(data);
				});
		}

		public void Unsubscribe ()
		{
			MessagingCenter.Unsubscribe<IEnumerable<Member>> (this, DataMessage);
		}

		#endregion

		private void FetchData() {
			IsRefreshing = true;
			AzureRepo.GetMembersList (DataMessage);
		}

		public void Loaded(object sender, EventArgs args) {
			FetchData ();
		}

		public MembersViewModel ()
		{
			PullToRefreshCommand = new Command (FetchData);

			SelectionChangedCommand = new Command(() => {
				// Analysis disable once RedundantCheckBeforeAssignment
				if(SelectedItem != null){
					SelectedItem = null;
				}
			});

			FetchData ();
		}
	}
}


