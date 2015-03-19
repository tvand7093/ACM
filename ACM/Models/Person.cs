using System;
using Newtonsoft.Json;
using ACM.Interfaces;

namespace ACM.Models
{
	[JsonObject]
	public class Person : IAzureObject
	{
		#region IAzureObject implementation

		[JsonProperty("id")]
		public string Id { get; set; }

		#endregion

		[JsonProperty("pic")]
		public string Picture {
			get;
			set;
		}

		[JsonProperty("name")]
		public string Name {
			get;
			set;
		}

		[JsonProperty("twitter")]
		public string Twitter {
			get;
			set;
		}

		public Person ()
		{
		}
	}
}

