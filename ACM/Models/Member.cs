using System;
using Newtonsoft.Json;

namespace ACM.Models
{
	[JsonObject]
	public class Member : Person
	{
		[JsonProperty("role")]
		public string Role {
			get;
			set;
		}

		public Member ()
		{
		}
	}
}

