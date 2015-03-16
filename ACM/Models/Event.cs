using System;
using Newtonsoft.Json;

namespace ACM.Models
{
	[JsonObject]
	public class Event
	{
		[JsonProperty("description")]
		public string Description {
			get;
			set;
		}

		[JsonProperty("speaker")]
		public GuestSpeaker Speaker {
			get;
			set;
		}

		[JsonProperty("topic")]
		public string Topic {
			get;
			set;
		}

		[JsonProperty("start")]
		public DateTime StartTime {
			get;
			set;
		}

		[JsonProperty("end")]
		public DateTime EndTime {
			get;
			set;
		}

		[JsonProperty("location")]
		public string Location {
			get;
			set;
		}

		public string InfoString {
			get { 
				return string.Format("{0} on {1}",
					Location, StartTime.ToString("MM/dd/yyyy @ hh:mm:ss tt"));
			}
		}

		public Event ()
		{
		}
	}
}

