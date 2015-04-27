using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using CSAssistant.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using System.Text;
using CSAssistant.Models;

namespace CSAssistant.Services
{
	internal static class CourseService
	{
		const string LangsAssignmentsRegex = @"<a href=""([Aa|Ff].*)"">([Aa|Ff].*)</a>";
		const string CochransAssignmentsRegex = @"<a.*href=""projects(.*)"">\[PDF\](.*)[^\r\n+]?";

		public static void GetAssignments(string url, string messageToEmit, Professor prof){
			Task.Factory.StartNew(async () => {
				List<AssignmentViewModel> vms = null;

				if (prof == Professor.Lang)
					vms = await GetAssignments(url, ProcessLangs);
				else if (prof == Professor.Cochran)
					vms = await GetAssignments(url, ProcessCochrans);
				
				MessagingCenter.Send<List<AssignmentViewModel>> (vms, messageToEmit);
			});
		}

		static async Task<List<AssignmentViewModel>> GetAssignments(string url,
			Func<string, string, List<AssignmentViewModel>> process){

			using (var client = new HttpClient ()) {
				var html = await client.GetStringAsync(url);
				return process (html, url);
			}
		}

		static List<AssignmentViewModel> ProcessLangs(string html, string url){
			var matches = Regex.Matches (html, LangsAssignmentsRegex);
			List<AssignmentViewModel> assignments = new List<AssignmentViewModel> ();
			if (matches.Count > 0) {
				for (int i = matches.Count-1; i >= 0; i--) {
					var match = matches [i];
					if (match.Success) {
						var index = url.LastIndexOf ("/");
						var tmpUrl = url.Remove (index, url.Length - index);
						var docUrl = Path.Combine(tmpUrl, match.Groups [1].Value);
						docUrl = docUrl.Replace (" ", "%20");

						var name = match.Groups[2].Value;
						assignments.Add (new AssignmentViewModel (name, docUrl));
					}
				}
			}
			return assignments;
		}

		static List<AssignmentViewModel> ProcessCochrans(string html, string url){
			var matches = Regex.Matches (html, CochransAssignmentsRegex);
			List<AssignmentViewModel> assignments = new List<AssignmentViewModel> ();
			if (matches.Count > 0) {
				for (int i = matches.Count-1; i >= 0; i--) {
					var match = matches [i];
					if (match.Success) {
						var nameMatch = match.Groups [2];
						StringBuilder name = new StringBuilder ();

						int startIndex = nameMatch.Index;

						var last = html [startIndex];
						while (true) {
							var current = html [startIndex];
							if ((last == ' ' && current == ' ') || current == '\r' || current == '\n') {
								startIndex++;
								continue;
							}
							//found the end, so stop.
							if (current == '<')
								break;

							//not whitespace, so add this char and progress.
							name.Append (current);
							startIndex++;
							last = current;
						}

						var docUrl = url + "/projects" + match.Groups [1].Value;
						docUrl = docUrl.Replace (" ", "%20");

						assignments.Add (new AssignmentViewModel (name.ToString().Trim(), docUrl));
					}
				}
			}
			return assignments;
		}

	}
}

