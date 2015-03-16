using System;
using Newtonsoft.Json;

namespace ACM.Models
{
	[JsonObject]
	public class Person
	{
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

