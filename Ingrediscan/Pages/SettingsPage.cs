using System;
using System.Collections.Generic;

using Xamarin.Forms;

using Ingrediscan.Utilities;
using Android.Widget;

using XLabs.Forms.Controls;
using System.Linq;

namespace Ingrediscan
{
	public class SettingsPage : ContentPage
	{
        Label resultsLabel;
        SearchBar searchBar;
        public static List<string> _terms = new List<string> { "African", "Chinese", "Japanese", "Korean", "Vietnamese", "Thai", "Indian",
            "British", "Irish", "French", "Italian", "Mexican", "Spanish", "Middle Eastern", "Jewish", "American", "Cajun",
            "Southern", "Greek", "German", "Nordic", "Eastern European", "Caribbean", "Latin american", "Pescetarian", "Lacto Vegetarian",
                 "Ovo Vegetarian", "Vegan", "Vegetarian", "Dairy", "Egg", "Gluten", "Peanut", "Sesame", "Seafood", "Shellfish"};
        public SettingsPage ()
		{

            Title = "Settings";
            

            resultsLabel = new Label
            {
                Text = "Result will appear here.",
                VerticalOptions = LayoutOptions.FillAndExpand,
                FontSize = 25
            };

            searchBar = new SearchBar
            {
                Placeholder = "Enter search term",
                SearchCommand = new Command(() => { resultsLabel.Text = "Result: " + searchBar.Text + " is what you asked for."; })
            };

            var label = new Label
            {
                Text = "Mark Items to Narrow Recipe Search.",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center
            };

            

        var listView = new Xamarin.Forms.ListView();
            listView.ItemsSource = _terms;

            var popUpButton = new Xamarin.Forms.Button { Text = "Search Through Settings", TextColor = Color.White, BackgroundColor = Color.Blue };

            PopupLayout _PopUpLayout = new PopupLayout();

           

            StackLayout layout = new StackLayout();

            searchBar.TextChanged += (sender, e) =>
            {
                listView.BeginRefresh();
                if (string.IsNullOrWhiteSpace(e.NewTextValue.ToLower()))
                    listView.ItemsSource = _terms;
                else
                    listView.ItemsSource = _terms.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));
                listView.EndRefresh();
            };

            layout.Children.Add(popUpButton);

           

 

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

            

            listView.ItemTapped += (sender, e) =>
            {
               
                
                //e.Item.ToString() gets text from list, ex "Thai"
                // was hoping this info could be used to go to tableview cell with same name, or toggle switch, but havn't been able to figure out how

                if (_PopUpLayout.IsPopupActive)
                {
                    _PopUpLayout.DismissPopup();// Close the PopUp
                }
                layout.IsVisible = true;
            };

            var PopUp = new StackLayout
            {
                WidthRequest = 300,
                HeightRequest = 400,
                BackgroundColor = Color.White,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    searchBar,
                    resultsLabel,
                    listView,
                }
            };

            popUpButton.Clicked += (sender, e) =>
            {
                layout.IsVisible = false;
                _PopUpLayout.ShowPopup(PopUp);
                searchBar.Focus(); // set Focus to SearchBar so that the user can start to type…
            };

            _PopUpLayout.Content = layout;
            Content = _PopUpLayout;
		}
	}
}
