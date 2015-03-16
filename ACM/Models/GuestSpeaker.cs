using System;
using Newtonsoft.Json;

namespace ACM.Models
{
	[JsonObject]
	public class GuestSpeaker : Person
	{
		[JsonProperty("company")]
		public string Company {
			get;
			set;
		}

		[JsonProperty("title")]
		public string JobTitle {
			get;
			set;
		}
			
		public GuestSpeaker ()
		{
		}
	}
}

