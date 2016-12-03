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

			listView.ItemTapped += async (sender, e) => {
				var id = ((SearchResultItem)e.Item).Id;
				var recipe = REST_API.GET_RecipeInformation (id, false).Result;
				await Navigation.PushAsync (new RecipePage (recipe));
				listView.SelectedItem = null;
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
