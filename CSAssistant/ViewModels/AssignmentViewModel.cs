using System;

namespace CSAssistant.ViewModels
{
	public class AssignmentViewModel
	{
		public string Name {get;set;}
		public string Url {get;set;}
		public AssignmentViewModel (string name, string url)
		{
			Name = name;
			Url = url;
		}
	}
}

