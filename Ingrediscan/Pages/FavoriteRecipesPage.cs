using System;

using Xamarin.Forms;

using Ingrediscan.Utilities;
using System.Collections.Generic;

namespace Ingrediscan
{
	public class FavoriteRecipesPage : ContentPage
	{
		public FavoriteRecipesPage ()
		{
			ListView listView = new ListView {
				ItemsSource = CreateListViewFromSearch(Globals.firebaseData.savedRecipes),
				ItemTemplate = new DataTemplate (() => {
					var imageCell = new ImageCell ();
					imageCell.SetBinding (TextCell.TextProperty, "Text");
					imageCell.SetBinding (ImageCell.ImageSourceProperty, "ImageSource");
					return imageCell;
				}),
			};

			Title = "Favorited Recipes";
			Content = new StackLayout {
				Children = {
					listView
				}
			};
			bool finished = true;
			listView.ItemTapped += async (sender, e) => {
				if (finished) 
				{
					finished = false;
					var id = ((SearchResultItem)e.Item).Id;
					var recipe = await REST_API.GET_RecipeInformation (id, false);
					await Navigation.PushAsync (new RecipePage (recipe));
					listView.SelectedItem = null;
					finished = true;
				}
				else if (finished == false) {
					listView.SelectedItem = null;
				}
			};
		}

		public List<SearchResultItem> CreateListViewFromSearch (List<Recipe> recipe)
		{
			//var items = REST_API.GET_SearchRecipes (recipe).Result;

			List<SearchResultItem> searchResultItems = new List<SearchResultItem> ();
			recipe.ForEach (x => searchResultItems.Add (new SearchResultItem {
				ImageSource = x.image,
				Text = x.name,
				//Detail = 
				TargetType = typeof (RecipePage),
				Id = x.id
			}));

			return searchResultItems;
		}
	}
}
