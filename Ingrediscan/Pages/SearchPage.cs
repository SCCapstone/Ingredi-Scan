using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Ingrediscan.Utilities;
using System.Linq;
using System.Threading.Tasks;

using XLabs.Forms.Controls;

namespace Ingrediscan
{
	public class SearchPage : ContentPage
	{
		ListView resultsView = new ListView ();
		List<SearchResultItem> recipes = new List<SearchResultItem> ();

		SearchBar searchBar;
		string prevSearch = "";

		Picker dishSelector;

		PopupLayout _PopUpLayout = new PopupLayout ();

		public SearchPage ()
		{
			Picker history = new Picker {
				Title = "History",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				IsEnabled = false,
				IsVisible = false,
			};

			dishSelector = new Picker {
				Title = "Dish Selection",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				IsVisible = true,
				IsEnabled = true,
			};

			var listOfDishSelections = new List<string> () { "Main Course", "Side Dish", "Dessert", "Appetizer", "Salad", 
				"Bread", "Breakfast", "Soup", "Beverage", "Sauce", "Drink" };

			listOfDishSelections.ForEach (x => dishSelector.Items.Add (x));
			dishSelector.SelectedIndexChanged += async (sender, args) => {
				if (dishSelector.SelectedIndex == -1) {
					// Do nothing
				} else {
					Globals.firebaseData.searchSettings.dishType = dishSelector.Items [dishSelector.SelectedIndex];
					SaveAndLoad.SaveToFirebase (Globals.firebaseData);

					if(searchBar.Text != null)
					{
						resultsView.ItemsSource = await this.CreateListViewFromSearch (searchBar.Text);
					}
				}
			};

			if (Globals.firebaseData.searchSettings.dishType != null) {
				dishSelector.SelectedIndex = dishSelector.Items.IndexOf (Globals.firebaseData.searchSettings.dishType);
			} else {
				dishSelector.SelectedIndex = 0;
			}

			Globals.firebaseData.searchSettings.dishType = dishSelector.Items [dishSelector.SelectedIndex];

			// Search Bar creation
			searchBar = new SearchBar {
				Placeholder = "Enter search term",
				SearchCommand = new Command (async () => {
					if (prevSearch != searchBar.Text) {
						resultsView.ItemsSource = await this.CreateListViewFromSearch (searchBar.Text);

						if (Globals.firebaseData.history.Count > 10) {
							Globals.firebaseData.history.Reverse ();
							Globals.firebaseData.history.RemoveAt (Globals.firebaseData.history.Count - 1);
							Globals.firebaseData.history.Reverse ();
						}

						history.Items.Insert (0, searchBar.Text); // adds to list in current screen

						Globals.firebaseData.history.Reverse ();
						Globals.firebaseData.history.Add (searchBar.Text); //adds text when returning to screen
						Globals.firebaseData.history.Reverse ();

						SaveAndLoad.SaveToFirebase (Globals.firebaseData);

						prevSearch = searchBar.Text;
					}
				})
			};

			// Result View creation
			resultsView = new ListView {
				ItemsSource = recipes,
				ItemTemplate = new DataTemplate (() => {
					var imageCell = new ImageCell ();
					imageCell.SetBinding (TextCell.TextProperty, "Text");
					imageCell.SetBinding (ImageCell.ImageSourceProperty, "ImageSource");
					return imageCell;
				}),
				VerticalOptions = LayoutOptions.StartAndExpand,
			};

			// History management
			if(Globals.firebaseData.history != null)
				Globals.firebaseData.history.ToList().ForEach(x => history.Items.Add(x));

			history.SelectedIndexChanged += async (sender, args) => {
				if (history.SelectedIndex == -1) 
				{
					// Do nothing
				} 
				else
				{
					if (searchBar.Text != history.Items [history.SelectedIndex]) 
					{
						searchBar.Text = history.Items [history.SelectedIndex];
						resultsView.ItemsSource = await this.CreateListViewFromSearch (searchBar.Text);
					}
				}
			};

			// Results View selection management
			bool finished = true;
			resultsView.ItemSelected += async (sender, e) => {
				if (Navigation.NavigationStack.Count < 2 && finished == true) 
				{
					finished = false;
					var id = ((SearchResultItem)e.SelectedItem).Id;
					var recipe = await REST_API.GET_RecipeInformation (id, false);
					Console.WriteLine ("Recipe Page:");
					await Navigation.PushAsync (new RecipePage (recipe));
					Console.WriteLine ("Recipe Page: Complete");
					((ListView)sender).SelectedItem = null;
					finished = true;
				}
				else if(finished == false)
				{
					((ListView)sender).SelectedItem = null;
				}
			};

			// Popup layout creation
			var tableView = CreatePopUpLayout ();

			var PopUp = new Frame {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.StartAndExpand,
				WidthRequest = 260,
				HeightRequest = 400,
				Content = tableView,
				HasShadow = true,
				OutlineColor = Color.Silver,
			};

			// Toolbar History addition
			ToolbarItems.Add (new ToolbarItem ("History", "drawable/history.png", () => {
				history.Focus ();
			}));

			// Toolbar Settings addition
			ToolbarItems.Add (new ToolbarItem ("Settings", "drawable/settings", () => {
				double width = this.Width;
				double height = this.Height;
				double Position = (width - 300) / 2;

				_PopUpLayout.ShowPopup (PopUp, Constraint.Constant (Position), Constraint.Constant (20));

				_PopUpLayout.Content.IsVisible = false;

				_PopUpLayout.ShowPopup (PopUp);
			}));

			Title = "Search";

			_PopUpLayout.Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = {
					dishSelector,
					searchBar,
					resultsView,
					history,
				},
				//Padding = new Thickness (10, Device.OnPlatform (20, 0, 0), 10, 0)
			};

			Content = _PopUpLayout;

			SaveAndLoad.SaveToFirebase (Globals.firebaseData);
		}

		//TODO Make async
		/// <summary>
		/// Creates the list view from search.
		/// </summary>
		/// <returns>The list view from search.</returns>
		/// <param name="search">Search.</param>
		public async Task<List<SearchResultItem>> CreateListViewFromSearch(string search)
		{
			var settings = Globals.firebaseData.searchSettings;

			List<SearchResultItem> searchResultItems = new List<SearchResultItem> ();

			if (settings.searchPref == "By Recipe") 
			{
				var items = await REST_API.GET_SearchRecipes (search, settings.limitOfSearch, settings.dishType.ToLower());

				items.results.ForEach (x => searchResultItems.Add (new SearchResultItem {
					ImageSource = items.baseUri + x.image,
					Text = x.title,
					TargetType = typeof (RecipePage),
					Id = x.id
				}));
			} 
			else if (settings.searchPref == "By Ingredients")
			{
				var items = await REST_API.GET_FindByIngredients (search, settings.limitOfSearch);

				items.ForEach (x => searchResultItems.Add (new SearchResultItem {
					ImageSource = x.image,
					Text = x.title,
					TargetType = typeof (RecipePage),
					Id = x.id
				}));
			}

			return searchResultItems;
		}

		/// <summary>
		/// Loads the search history.
		/// </summary>
		/// <param name="history">History.</param>
		public static void LoadSearchHistory(List<string> history)
		{
			if (history != null) {
				Console.WriteLine ("History");
				history.ForEach (x => Console.WriteLine (x));

				Globals.firebaseData.history = history;
			}
			else
			{
				Globals.firebaseData.history = new List<string> ();
			}
		}

		/// <summary>
		/// Creates the pop up layout.
		/// </summary>
		/// <returns>The pop up layout.</returns>
		public TableView CreatePopUpLayout()
		{
			var tableView = new TableView ();
			tableView.Intent = TableIntent.Settings;

			var tableRoot = new TableRoot ();

			// Search by ingredient(s) or recipe
			var searchPref = new Picker {
				Title = "Search Preference",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				IsVisible = true,
				IsEnabled = true,
			};

			var choices = new List<string> () { "By Recipe", "By Ingredients" };
			choices.ForEach(x => searchPref.Items.Add (x));

			searchPref.SelectedIndexChanged += (sender, e) => {
				Globals.firebaseData.searchSettings.searchPref = searchPref.Items [searchPref.SelectedIndex];

				if (Globals.firebaseData.searchSettings.searchPref == "By Recipe")
				{
					dishSelector.IsVisible = true;
					dishSelector.IsEnabled = true;

					searchBar.Placeholder = "Recipe Name";
				} 
				else if (Globals.firebaseData.searchSettings.searchPref == "By Ingredients")
				{
					dishSelector.IsVisible = false;
					dishSelector.IsEnabled = false;

					searchBar.Placeholder = "Ingredient1,Ingredient2,..";
				}
			};

			searchPref.SelectedIndex = 0;

			// Increase max limit size
			var maxResults = new EntryCell {
				Text = "10",
				Placeholder = "10",
				Label = "Number Of Items To List"
			};

			maxResults.Completed += (sender, e) => {
				var result = 10;
				if(int.TryParse(maxResults.Text, out result) && result < 100 && result > 0)
				{
					Globals.firebaseData.searchSettings.limitOfSearch = result;
				}
			};

			// Do Not include this kind of item(s)
			var notIncludedItem = new EntryCell {
				Label = "Search Without Ingredient(s)",
				Placeholder = "ingredient1,ingredient2,..",
			};

			notIncludedItem.Completed += (sender, e) => {
				Globals.firebaseData.searchSettings.excludeIngredients = notIncludedItem.Text;
			};

			// Persistent Data 
			if (Globals.firebaseData.searchSettings != null)
			{
				if (Globals.firebaseData.searchSettings.searchPref != "")
				{
					searchPref.SelectedIndex = searchPref.Items.IndexOf (Globals.firebaseData.searchSettings.searchPref);
				}
				if (Globals.firebaseData.searchSettings.limitOfSearch > 10)
				{
					maxResults.Text = Globals.firebaseData.searchSettings.limitOfSearch.ToString();
				}
				if (Globals.firebaseData.searchSettings.excludeIngredients != "")
				{
					notIncludedItem.Text = Globals.firebaseData.searchSettings.excludeIngredients;
				}
			}

			// Exit Settings
			var layout = new StackLayout {
				Orientation = StackOrientation.Vertical
			};
			var exitSettings = new Button {
				Text = "Save Settings",
				BackgroundColor = Color.FromHex ("#1D89E4"),
				TextColor = Color.White,
			};

			exitSettings.Clicked += (sender, e) => {
				SaveAndLoad.SaveToFirebase (Globals.firebaseData);
				_PopUpLayout.Content.IsVisible = true;
				_PopUpLayout.DismissPopup ();
			};

			layout.Children.Add (exitSettings);

			var exitSettingsView = new ViewCell {
				View = layout
			};

			var searchSection = new TableSection ("Search By Ingredient(s) Or Recipe Name") {
				new ViewCell {View = searchPref}
			};

			var limitSection = new TableSection ("Number Of Items To List") {
				maxResults
			};

			var notIncludeSection = new TableSection ("Do Not Include Ingredient(s)") {
				notIncludedItem
			};

			var exitSettingsSection = new TableSection {
				exitSettingsView
			};

			tableRoot.Add (searchSection);
			tableRoot.Add (limitSection);
			tableRoot.Add (notIncludeSection);
			tableRoot.Add (exitSettingsSection);

			tableView.Root = tableRoot;

			return tableView;
		}
	}
}
