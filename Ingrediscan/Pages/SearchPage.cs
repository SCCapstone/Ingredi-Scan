﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Ingrediscan.Utilities;
using System.Linq;

namespace Ingrediscan
{
	public class SearchPage : ContentPage
	{
		ListView resultsView = new ListView ();
		List<SearchResultItem> recipes = new List<SearchResultItem> ();

		SearchBar searchBar;
		string prevSearch = "";

		public SearchPage ()
		{
			Picker picker = new Picker {
				Title = "History",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				IsEnabled = false,
				IsVisible = false,
			};

			Globals.firebaseData.history.ToList().ForEach(x => picker.Items.Add(x));

			picker.SelectedIndexChanged += (sender, args) => {
				if (picker.SelectedIndex == -1) 
				{
					// Do nothing
				} 
				else
				{
					if (searchBar.Text != picker.Items [picker.SelectedIndex]) 
					{
						searchBar.Text = picker.Items [picker.SelectedIndex];
						resultsView.ItemsSource = this.CreateListViewFromSearch (searchBar.Text);
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
			resultsView.ItemSelected += async (sender, e) => {
				if (Navigation.NavigationStack.Count < 2) 
				{
					var id = ((SearchResultItem)e.SelectedItem).Id;
					var recipe = REST_API.GET_RecipeInformation (id, false).Result;
					await Navigation.PushAsync (new RecipePage (recipe));
					((ListView)sender).SelectedItem = null;
				}
			};

			// Toolbar addition
			ToolbarItems.Add (new ToolbarItem ("History", "drawable/history.png", () => {
				picker.Focus ();
			}));

			searchBar = new SearchBar {
				Placeholder = "Enter search term",
				SearchCommand = new Command (() => {
					if (prevSearch != searchBar.Text) 
					{
						resultsView.ItemsSource = this.CreateListViewFromSearch (searchBar.Text);

						if (Globals.firebaseData.history.Count > 10) 
						{
							Globals.firebaseData.history.Reverse ();
							Globals.firebaseData.history.RemoveAt (Globals.firebaseData.history.Count - 1);
							Globals.firebaseData.history.Reverse ();
						}

						picker.Items.Add (searchBar.Text);

						Globals.firebaseData.history.Reverse ();
						Globals.firebaseData.history.Add (searchBar.Text);
						Globals.firebaseData.history.Reverse ();

						SaveAndLoad.SaveToFirebase (Globals.firebaseData);

						prevSearch = searchBar.Text;
					}
				})
			};

			Title = "Search By Recipe Name";
			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = {
					searchBar,
					resultsView,
					picker
				},
				Padding = new Thickness (10, Device.OnPlatform (20, 0, 0), 10, 0)
			};
		}

		//TODO Make async
		public List<SearchResultItem> CreateListViewFromSearch(string search)
		{
			var items = REST_API.GET_SearchRecipes (search).Result;

			List<SearchResultItem> searchResultItems = new List<SearchResultItem> ();
			items.results.ForEach (x => searchResultItems.Add (new SearchResultItem {
				ImageSource = items.baseUri + x.imageUrls [0],
				Text = x.title,
				//Detail = 
				TargetType = typeof (RecipePage),
				Id = x.id
			}));

			return searchResultItems;
		}

		public static void LoadSearchHistory(List<string> history)
		{
			//TODO Finish
			Console.WriteLine ("History");
			history.ForEach (x => Console.WriteLine (x));
			Globals.firebaseData.history = history;
		}
	}
}