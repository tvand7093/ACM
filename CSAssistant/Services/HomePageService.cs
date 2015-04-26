using System;
using System.Collections.Generic;
using CSAssistant.Models;
using System.Xml.Linq;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using System.Net.Http;
using System.Diagnostics;
using System.Text.RegularExpressions;
using CSAssistant.ViewModels;
using Xamarin.Forms;
using CSAssistant.Helpers;
using System.Collections.ObjectModel;
using System.Text;

namespace CSAssistant.Services
{
	public static class HomePageService
	{
		const string LangsAnchorPattern = @"(?<=<strong[^>]*>)\s*<a href=""(.*)"">(.*)</a>";
		const string LangsImagePattern = @"src=""(.*[A-Za-z])""";

		const string CochransImagePattern = @"<a.*><img\s*src=""(.*)""\s.*></a>";
		const string CochransAnchorPattern = @"<a.*""(/~[Cc][Ss]\d{3})"">(.*)</a>";

		public static void FetchHomePage(Professor toLoad, string messageToSend){
			Task.Factory.StartNew(async () => {
				ProfessorHomePageVM vm = null;
				if (toLoad == Professor.Lang)
					vm = await GetLangsHomePage ();
				else if (toLoad == Professor.Cochran)
					vm = await GetCochransHomePage ();

				if(vm != null)
					MessagingCenter.Send<ProfessorHomePageVM> (vm, messageToSend);
			});
		}

		static async Task<ProfessorHomePageVM> GetCochransHomePage()
		{
			var date = DateTime.Now;

			var semester = date.Month > 5 ? "Fall" : "Spring";
			var homePage = new ProfessorHomePageVM (Professor.Cochran) {
				ProfessorName = "Wayne Cochran",
				Title = string.Format("{0} {1}", semester, date.Year)
			};

			using (var httpClient = new HttpClient ()) {
				//grab website html content
				var html = await httpClient.GetStringAsync (ProfessorPages.CochransWebsite);

				//remove comments in html...
				var beginComment = html.LastIndexOf("<!--");
				var endComment = html.LastIndexOf ("-->");

				html = html.Remove (beginComment, endComment - beginComment);

				var matches = Regex.Matches (html, CochransAnchorPattern);

				//parse our class names and links.
				if (matches.Count > 0) {
					//courses found
					for (int i = 0; i < matches.Count; i++) {
						var match = matches [i];
						var url = ProfessorPages.CochransWebsite + match.Groups [1].Value;

						//capatilize everything
						var nameMatch = match.Groups[2].Value;
						var parts = nameMatch.Split (' ');
						StringBuilder name = new StringBuilder();
						foreach (var word in parts) {
							if (Char.IsLower (word [0])) {
								var upper = Char.ToUpper (word [0]);
								name.Append (upper);
								name.Append (word.Substring (1, word.Length - 1));
							} else {
								//already caps, so skip
								name.Append(word);
							}
							name.Append (" ");

						}

						homePage.Courses.Add (new Course () {
							Name = name.ToString().Trim(),
							Url = url
						});
					}
				}

				matches = Regex.Matches (html, CochransImagePattern);
				if (matches.Count > 0) {
					homePage.Image = Path.Combine(ProfessorPages.CochransWebsite, matches [0].Groups [1].Value);
				}
			}
			homePage.Courses = new ObservableCollection<Course>(homePage.Courses.OrderBy (c => c.Name));
			return homePage;
		}
	

		static async Task<ProfessorHomePageVM> GetLangsHomePage()
		{
			var date = DateTime.Now;

			var semester = date.Month > 5 ? "Fall" : "Spring";
			var homePage = new ProfessorHomePageVM (Professor.Lang) {
				ProfessorName = "Dick Lang",
				Title = string.Format("{0} {1}", semester, date.Year)
			};

			using (var httpClient = new HttpClient ()) {
				//grab website html content
				var html = await httpClient.GetStringAsync (ProfessorPages.LangsWebsite);

				var matches = Regex.Matches (html, LangsAnchorPattern);

				//parse our class names and links.
				if (matches.Count > 0) {
					//courses found
					for (int i = 0; i < matches.Count; i++) {
						var match = matches [i];
						homePage.Courses.Add (new Course () {
							Name = match.Groups[2].Value,
							Url = Path.Combine(ProfessorPages.LangsWebsite, match.Groups[1].Value)
						});
					}
				}

				matches = Regex.Matches (html, LangsImagePattern);
				if (matches.Count > 0) {
					homePage.Image = Path.Combine(ProfessorPages.LangsWebsite, matches [0].Groups [1].Value);
				}
			}
			return homePage;
		}
	}
}

