using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

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
		}

		/*public static void changeSettings(string item)
		{
			if (cuisineSettings.ContainsKey(item))
			{
				cuisineSettings [item] = !cuisineSettings [item];
			}
			else if (dietSettings.ContainsKey(item))
			{
				dietSettings [item] = !dietSettings [item];
			}
			else if (intolSettings.ContainsKey(item))
			{
				intolSettings [item] = !intolSettings [item];
			}
			saveSettings ();
		}*/

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

		public static void saveSettings()
		{
			Console.WriteLine ("SAVING...");
			SaveAndLoad save = new SaveAndLoad ();
			string file = "";

			file += cuisineSettings.Count + "\n";
			file += dietSettings.Count + "\n";
			file += intolSettings.Count + "\n";

			for (int i = 0; i < tableView.Root.Count; ++i)
			{
				for (int j = 0; j < tableView.Root [i].Count; ++j)
				{
					SwitchCell item = ((SwitchCell)tableView.Root [i] [j]);
					file += item.Text + "," + item.On + "\n";
				}
			}

			Console.WriteLine (file);
			save.SaveText("settings.txt", file);
			Console.WriteLine ("SAVING COMPLETED");
		}

		public static void loadSettings()
		{
			clearSettings ();

			try 
			{
				SaveAndLoad load = new SaveAndLoad ();

				string file = load.LoadText ("settings.txt");
				Console.WriteLine ("LOADING..." + file);
				//Stream stream = File.Open (file, FileMode.Open);
				if (file != "") {
					using (StreamReader reader = new StreamReader (file)) {

						string line = "";//reader.ReadToEnd ();
						//Console.WriteLine (line);

						int cuisine_n = int.Parse (reader.ReadLine ());
						int diet_n = int.Parse (reader.ReadLine ());
						int intol_n = int.Parse (reader.ReadLine ());

						for (int i = 0; i < cuisine_n; ++i) {
							line = reader.ReadLine ();
							string [] splitLine = line.Split (',');
							cuisineSettings.Add (splitLine [0], bool.Parse (splitLine [1]));
						}

						for (int i = 0; i < diet_n; ++i) {
							line = reader.ReadLine ();
							string [] splitLine = line.Split (',');
							dietSettings.Add (splitLine [0], bool.Parse (splitLine [1]));
						}

						for (int i = 0; i < intol_n; ++i) {
							line = reader.ReadLine ();
							string [] splitLine = line.Split (',');
							intolSettings.Add (splitLine [0], bool.Parse (splitLine [1]));
						}
					}
				}

				//stream.Close ();
				Console.WriteLine ("LOAD DONE");
			}
			catch (IOException e)
			{
				Console.WriteLine (e.Message);
			}

		}
	}
}
