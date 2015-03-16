using System;

using Xamarin.Forms;
using ACM.Interfaces;
using ACM.Models;

namespace ACM.ViewModels
{
	public class MembersViewModel : BaseListViewModel<Member>, IMessanger
	{
		
		#region IMessanger implementation

		private const string DataMessage = "AllDataFetched";

		public void Subscribe ()
		{
			MessagingCenter.Subscribe<AllData> (this, DataMessage,
				(data) => {
					Items.Clear();
					Items.Add(data.Members);
				});
		}

		public void Unsubscribe ()
		{
			MessagingCenter.Unsubscribe<AllData> (this, DataMessage);
		}

		#endregion

		public MembersViewModel ()
		{
			SelectionChangedCommand = new Command(() => {
				// Analysis disable once RedundantCheckBeforeAssignment
				if(SelectedItem != null){
					SelectedItem = null;
				}
			});
		}
	}
}


