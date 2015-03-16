using System;
using Xamarin.Forms;
using ACM.Models;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACM.Services
{
	public static class DataService
	{
		private const string FileName = "ACM.Models.data.json";

		public static void FetchData(){
			var assembly = typeof(Member).GetTypeInfo ().Assembly;
			var stream = assembly.GetManifestResourceStream (FileName);
			using (var reader = new StreamReader (stream)) {
				reader.ReadToEndAsync ().ContinueWith((task) => {
					if(!String.IsNullOrEmpty(task.Result)){
						//here result will be a json string of ambassadors
						var data = JsonConvert.DeserializeObject<AllData>(task.Result);
						MessagingCenter.Send<AllData>(data, "AllDataFetched");
					}
				}, TaskScheduler.FromCurrentSynchronizationContext());
			}
		}
	}
}

