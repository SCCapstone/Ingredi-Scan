using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class ScanResultsPage : ContentPage
	{
		ListView resultsView = new ListView ();
		//List<SearchResultItem> recipes = new List<SearchResultItem> ();

		public ScanResultsPage (SpoonacularClasses.FindByUPC resultsFromUPC)
		{
			resultsView = new ListView {
				//ItemsSource = recipes,
				ItemTemplate = new DataTemplate (() => {
					var imageCell = new ImageCell ();
					imageCell.SetBinding (TextCell.TextProperty, "Text");
					imageCell.SetBinding (ImageCell.ImageSourceProperty, "ImageSource");
					return imageCell;
				}),
				VerticalOptions = LayoutOptions.StartAndExpand,
			};
			resultsView.ItemSelected += async (sender, e) => {
				if (Navigation.NavigationStack.Count < 3) {
					var id = ((SearchResultItem)e.SelectedItem).Id;
					var recipe = REST_API.GET_RecipeInformation (id, false).Result;
					await Navigation.PushAsync (new RecipePage (recipe));
					((ListView)sender).SelectedItem = null;
				}
			};

			resultsView.ItemsSource = this.CreateListViewFromUPC (resultsFromUPC);

			Title = "Search Results";
			Content = new StackLayout {
				VerticalOptions = LayoutOptions.Start,
				Children = {
					resultsView,
				},
				Padding = new Thickness (10, Device.OnPlatform (20, 0, 0), 10, 5)
			};
		}

		public List<SearchResultItem> CreateListViewFromUPC (SpoonacularClasses.FindByUPC resultsFromUPC)
		{
			var items = REST_API.GET_FindByIngredients (resultsFromUPC.title).Result;
			List<SearchResultItem> searchResultItems = new List<SearchResultItem> ();
			items.ForEach (x => searchResultItems.Add (new SearchResultItem {
				ImageSource = x.image,
				Text = x.title,
				TargetType = typeof (RecipePage),
				Id = x.id
			}));

			return searchResultItems;
		}
	}
}
