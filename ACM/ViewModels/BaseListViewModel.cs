using System;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ACM
{
	public class BaseListViewModel<T> : BaseViewModel where T : class
	{

		private ObservableCollection<T> items;
		public ObservableCollection<T> Items {
			get { return items; }
			set {
				items.Clear ();
				items.Add (value);
				OnPropertyChanged ("Items");
			}
		}

		private T selectedItem;
		public T SelectedItem {
			get { return selectedItem; }
			set {
				selectedItem = value;
				OnPropertyChanged ("SelectedItem");
				if (SelectionChangedCommand != null && SelectionChangedCommand.CanExecute(null)) {
					SelectionChangedCommand.Execute (null);
				}
			}
		}

		public bool isRefreshing;
		public bool IsRefreshing {
			get { return isRefreshing; }
			set {
				isRefreshing = value;
				if (App.Current.MainPage != null)
					IsBusy = value;
				
				OnPropertyChanged ("IsRefreshing");
			}
		}

		public ICommand SelectionChangedCommand { get; protected set; }
		public ICommand PullToRefreshCommand { get; protected set; }

		public BaseListViewModel ()
		{
			items = new ObservableCollection<T> ();
		}
	}
}

