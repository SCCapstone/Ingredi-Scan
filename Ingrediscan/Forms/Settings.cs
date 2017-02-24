using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

using Ingrediscan.Utilities;

namespace Ingrediscan
{
	public static class Settings
	{
		public static Dictionary<string, bool> cuisineSettings = new Dictionary<string, bool> ();
		public static Dictionary<string, bool> dietSettings = new Dictionary<string, bool> ();
		public static Dictionary<string, bool> intolSettings = new Dictionary<string, bool> ();

		public static List<string> cuisines = new List<string> { "African", "Chinese", "Japanese", "Korean", "Vietnamese", "Thai", "Indian",
			"British", "Irish", "French", "Italian", "Mexican", "Spanish", "Middle Eastern", "Jewish", "American", "Cajun",
			"Southern", "Greek", "German", "Nordic", "Eastern European", "Caribbean", "Latin american"};
		public static List<string> diets = new List<string> { "Pescetarian", "Lacto Vegetarian", "Ovo Vegetarian", "Vegan", "Vegetarian" };
		public static List<string> intolerances = new List<string> {"Dairy", "Egg", "Gluten", "Peanut", "Sesame", "Seafood", "Shellfish",
			"Soy", "Sulfite", "Tree Nut",  "Wheat"};

		public static TableView tableView = new TableView ();

		public static void clearSettings()
		{
			cuisineSettings.Clear ();
			dietSettings.Clear ();
			intolSettings.Clear ();

			var settings = initialSettings ();

			Globals.firebaseData.searchSettings.cuisine = settings[0];
			Globals.firebaseData.searchSettings.diets = settings [1];
			Globals.firebaseData.searchSettings.intolerances = settings [2];

			resetTableView ();
			SaveSettings ();
		}

		public static void resetTableView ()
		{
			for (int i = 0; i < tableView.Root.Count; ++i) {
				for (int j = 0; j < tableView.Root [i].Count; ++j) {
					var s = (SwitchCell)tableView.Root [i] [j];
					s.On = false;
				}
			}
		}

		public static List<Dictionary<string, bool>> initialSettings()
		{
			var cuisD = new Dictionary<string, bool> ();
			foreach (var item in cuisines)
				cuisD.Add(item, false);
			
			var dietD = new Dictionary<string, bool> ();
			foreach (var item in diets)
				dietD.Add (item, false);
			
			var intolD = new Dictionary<string, bool> ();
			foreach (var item in intolerances)
				intolD.Add (item, false);

			var l = new List<Dictionary<string, bool>> ();
			l.Add (cuisD);
			l.Add (dietD);
			l.Add (intolD);

			return l;
		}

		public static List<SwitchCell> populateSettings(string item)
		{
			List<SwitchCell> temp = new List<SwitchCell> ();

			switch (item)
			{
			case "cuisine":
				cuisines.ForEach (x => temp.Add(new SwitchCell { Text = x, On = cuisineSettings [x], BindingContext = cuisineSettings[x] }));
				break;
			case "diet":
				diets.ForEach (x => temp.Add (new SwitchCell { Text = x, On = dietSettings [x], BindingContext = dietSettings [x] }));
				break;
			case "intol":
				intolerances.ForEach (x => temp.Add (new SwitchCell { Text = x, On = intolSettings [x], BindingContext = intolSettings [x] }));
				break;
			default:
				break;
			}

			return temp;
		}

		public static void SaveSettings()
		{
			for (int i = 0; i < tableView.Root.Count; ++i)
			{
				for (int j = 0; j < tableView.Root [i].Count; ++j)
				{
					var s = (SwitchCell)tableView.Root [i] [j];

					if(i == 0)
					{
						Globals.firebaseData.searchSettings.cuisine[s.Text] = s.On;
					}
					else if(i == 1)
					{
						Globals.firebaseData.searchSettings.diets[s.Text] = s.On;
					}
					else if(i == 2)
					{
						Globals.firebaseData.searchSettings.intolerances[s.Text] = s.On;
					}
				}
			}

			SaveAndLoad.SaveToFirebase (Globals.firebaseData);
		}

		public static void LoadSettings(SearchSettings searchSettings)
		{
			if (searchSettings != null) {
				if (searchSettings.cuisine.Count == 0 || searchSettings.diets.Count == 0 || searchSettings.intolerances.Count == 0) {
					var l = initialSettings ();

					foreach (KeyValuePair<string, bool> k in l [0])
						Console.WriteLine (k.Key + " " + k.Value);

					cuisineSettings = l [0];
					dietSettings = l [1];
					intolSettings = l [2];

					Globals.firebaseData.searchSettings.cuisine = cuisineSettings;
					Globals.firebaseData.searchSettings.diets = dietSettings;
					Globals.firebaseData.searchSettings.intolerances = intolSettings;

					SaveAndLoad.SaveToFirebase (Globals.firebaseData);
				} else {
					cuisineSettings = searchSettings.cuisine;
					dietSettings = searchSettings.diets;
					intolSettings = searchSettings.intolerances;
				}
			}
		}
	}
}
