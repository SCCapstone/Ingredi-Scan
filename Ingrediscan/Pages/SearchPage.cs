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

		PopupLayout _PopUpLayout = new PopupLayout ();

		public SearchPage ()
		{
			Picker picker = new Picker {
				Title = "History",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				IsEnabled = false,
				IsVisible = false,
			};
			if(Globals.firebaseData.history != null)
				Globals.firebaseData.history.ToList().ForEach(x => picker.Items.Add(x));

			picker.SelectedIndexChanged += async (sender, args) => {
				if (picker.SelectedIndex == -1) 
				{
					// Do nothing
				} 
				else
				{
					if (searchBar.Text != picker.Items [picker.SelectedIndex]) 
					{
						searchBar.Text = picker.Items [picker.SelectedIndex];
						resultsView.ItemsSource = await this.CreateListViewFromSearch (searchBar.Text);
					}
				}
			};

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

			// Toolbar addition
			ToolbarItems.Add (new ToolbarItem ("History", "drawable/history.png", () => {
				picker.Focus ();
			}));

			var tableView = CreatePopUpLayout ();

			var PopUp = new Frame {
				//HorizontalOptions = LayoutOptions.Center,
				//VerticalOptions = LayoutOptions.StartAndExpand,
				WidthRequest = 300,
				HeightRequest = 300,
				Content = tableView,
				HasShadow = true,
				OutlineColor = Color.Silver
			};

			ToolbarItems.Add (new ToolbarItem ("Settings", "drawable/settings", () => {
				_PopUpLayout.ShowPopup (PopUp);
			}));

			searchBar = new SearchBar {
				Placeholder = "Enter search term",
				SearchCommand = new Command (async () => {
					if (prevSearch != searchBar.Text) 
					{
						resultsView.ItemsSource = await this.CreateListViewFromSearch (searchBar.Text);

						if (Globals.firebaseData.history.Count > 10) 
						{
						    Globals.firebaseData.history.Reverse ();
							Globals.firebaseData.history.RemoveAt (Globals.firebaseData.history.Count - 1);
							Globals.firebaseData.history.Reverse ();
						}


                        //if (picker.Items.Count > 2)
                        //{
                        //    picker.Items.RemoveAt(picker.Items.Count - 1);
                        //}
						picker.Items.Insert (0,searchBar.Text); // adds to list in current screen

						Globals.firebaseData.history.Reverse ();
						Globals.firebaseData.history.Add (searchBar.Text); //adds text when returning to screen
						Globals.firebaseData.history.Reverse ();

						SaveAndLoad.SaveToFirebase (Globals.firebaseData);

						prevSearch = searchBar.Text;
					}
				})
			};

			Title = "Search By Recipe Name";

			_PopUpLayout.Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = {
					searchBar,
					resultsView,
					picker
				},
				Padding = new Thickness (10, Device.OnPlatform (20, 0, 0), 10, 0)
			};

			Content = _PopUpLayout;
		}

		//TODO Make async
		public async Task<List<SearchResultItem>> CreateListViewFromSearch(string search)
		{
			var items = await REST_API.GET_SearchRecipes (search);

			List<SearchResultItem> searchResultItems = new List<SearchResultItem> ();

			items.results.ForEach (x => searchResultItems.Add (new SearchResultItem {
				ImageSource = items.baseUri + x.image,
				Text = x.title,
				//Detail = 
				TargetType = typeof (RecipePage),
				Id = x.id
			}));

			return searchResultItems;
		}

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

		public TableView CreatePopUpLayout()
		{
			var tableView = new TableView ();
			tableView.Intent = TableIntent.Settings;

			var tableRoot = new TableRoot ();

			// Search by ingredient(s) or recipe
			var recipeSearch = new SwitchCell {
				Text = "Search By Recipe",
				On = true
			};

			var ingredientSearch = new SwitchCell {
				Text = "Search By Ingredient(s)",
				On = false
			};

			recipeSearch.OnChanged += (sender, e) => {
				ingredientSearch.On = !ingredientSearch.On;
				Globals.firebaseData.searchSettings.recipeSearch = recipeSearch.On;
				Globals.firebaseData.searchSettings.ingredientSearch = ingredientSearch.On;
			};

			ingredientSearch.OnChanged += (sender, e) => {
				recipeSearch.On = !recipeSearch.On;
				Globals.firebaseData.searchSettings.recipeSearch = recipeSearch.On;
				Globals.firebaseData.searchSettings.ingredientSearch = ingredientSearch.On;
			};

			// Increase max limit size
			var maxResults = new EntryCell {
				Text = "10",
				Placeholder = "10",
				Label = "Number Of Items To List"
			};

			maxResults.Completed += (sender, e) => {
				var result = 10;
				if(int.TryParse(maxResults.Text, out result))
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
				Globals.firebaseData.searchSettings.notIncludedIngredients = notIncludedItem.Text.Split (',').ToList ();
			};

			// Persistent Data 
			if (Globals.firebaseData.searchSettings != null)
			{
				if (Globals.firebaseData.searchSettings.ingredientSearch != Globals.firebaseData.searchSettings.recipeSearch)
				{
					recipeSearch.On = Globals.firebaseData.searchSettings.recipeSearch;
					ingredientSearch.On = Globals.firebaseData.searchSettings.ingredientSearch;
				}
				if (Globals.firebaseData.searchSettings.limitOfSearch > 10)
				{
					maxResults.Text = Globals.firebaseData.searchSettings.limitOfSearch.ToString();
				}
				if (Globals.firebaseData.searchSettings.notIncludedIngredients.Count > 0)
				{
					Globals.firebaseData.searchSettings.notIncludedIngredients.ForEach (x => notIncludedItem.Text += x + ",");
					notIncludedItem.Text.Substring (0, notIncludedItem.Text.Length - 2);
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
				_PopUpLayout.DismissPopup ();
			};

			layout.Children.Add (exitSettings);

			var exitSettingsView = new ViewCell {
				View = layout
			};

			var searchSection = new TableSection ("Search By Ingredient(s) Or Recipe Name") {
				recipeSearch,
				ingredientSearch
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
