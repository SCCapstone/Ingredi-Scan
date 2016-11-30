using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Ingrediscan
{
	public class SearchPage : ContentPage
	{
		ListView resultsView = new ListView ();
		List<SearchResultItem> recipes = new List<SearchResultItem> ();

		SearchBar searchBar;

		public SearchPage ()
		{
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

			searchBar = new SearchBar {
				Placeholder = "Enter search term",
				SearchCommand = new Command (() => {
					resultsView.ItemsSource = this.CreateListViewFromSearch (searchBar.Text); })
			};

			Title = "Search Page";
			Content = new StackLayout {
				VerticalOptions = LayoutOptions.Start,
				Children = {
					searchBar,
					resultsView,
				},
				Padding = new Thickness (10, Device.OnPlatform (20, 0, 0), 10, 5)
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
	}
}
