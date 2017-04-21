﻿using System;
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

            Title = "Settings";

            var label = new Label
            {
                Text = "Mark Items to Narrow Search.",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center
            };
            StackLayout layout = new StackLayout();

            Settings.LoadSettings(Globals.firebaseData.searchSettings);

			ToolbarItems.Add (new ToolbarItem ("Clear Settings", "drawable/reset.png", async () => {
				
				bool resetSettings = await DisplayAlert ("Reset Settings", "Do you want to reset your settings?", "Confirm", "Cancel");
				if (resetSettings) 
				{
					Settings.clearSettings ();
                    layout.Children.Add(label);
                    layout.Children.Add(Settings.tableView);
					Content = layout;
					Toast.MakeText (Forms.Context, "Settings have been cleared.", ToastLength.Short).Show ();
				}
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

            layout.Children.Add(label);
            layout.Children.Add(Settings.tableView);
            Content = layout;
		}
	}
}