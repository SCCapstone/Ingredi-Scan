using System;
using System.Collections.Generic;

using Xamarin.Forms;

using Ingrediscan.Utilities;
using Android.Widget;

namespace Ingrediscan
{
	public class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			Settings.LoadSettings(Globals.firebaseData.searchSettings);

			ToolbarItems.Add (new ToolbarItem ("Clear Settings", "drawable/save.png", () => {
				Settings.initialSettings();

				Toast.MakeText (Forms.Context, "Settings have been cleared.", ToastLength.Short).Show ();
			}));

            Settings.SaveSettings();

			var cuisineSwitches = Settings.populateSettings("cuisine");
			var dietSwitches = Settings.populateSettings ("diet");
			var intolSwitches = Settings.populateSettings ("intol");

			var cuisineSection = new TableSection ("Cuisine");
			var dietSection = new TableSection ("Diets");
			var intolSection = new TableSection ("Intolerances");

			cuisineSwitches.ForEach (cuisineSection.Add);
			dietSwitches.ForEach (dietSection.Add);
			intolSwitches.ForEach (intolSection.Add);

			//TODO Over winter break implement custom renderer and custom cells

			/*Picker cuisinePicker = new Picker {
				Title = "Cuisines Settings",
			};
			foreach(KeyValuePair<string, bool> kv in Globals.firebaseData.searchSettings.cuisine)
			{
				cuisinePicker.Items.Add (kv.Key);
			}

			cuisinePicker.SelectedIndexChanged += (sender, e) => {
				
			};


			Picker dietPicker = new Picker {
				Title = "Diets Settings",
			};
			foreach (KeyValuePair<string, bool> kv in Globals.firebaseData.searchSettings.diets) {
				dietPicker.Items.Add (kv.Key);
			}

			Picker intolPicker = new Picker {
				Title = "Intolerances Settings",
			};
			foreach (KeyValuePair<string, bool> kv in Globals.firebaseData.searchSettings.intolerances) {
				intolPicker.Items.Add (kv.Key);
			}


			StackLayout layout = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = {
					cuisinePicker,
					dietPicker,
					intolPicker
				}
			};*/

			Settings.tableView = new TableView 
			{
				Intent = TableIntent.Settings,
				Root = new TableRoot ("Settings") 
				{
					cuisineSection,
					dietSection,
					intolSection
				}
			};

			Content = Settings.tableView;
		}
	}
}
