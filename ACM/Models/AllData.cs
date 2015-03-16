using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ACM.Models
{
	[JsonObject]
	public class AllData
	{
		[JsonProperty("events")]
		public List<Event> Events { get; set; }

		[JsonProperty("members")]
		public List<Member> Members { get; set; }

		public AllData ()
		{
			Events = new List<Event> ();
			Members = new List<Member> ();
		}
	}
}

