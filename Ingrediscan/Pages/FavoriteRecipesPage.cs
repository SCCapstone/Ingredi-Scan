using System;

using Xamarin.Forms;

using Ingrediscan.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ingrediscan
{
	public class FavoriteRecipesPage : ContentPage
	{
		public static ListView faveView;
		ToolbarItem faveDeleteButton;
		public static ObservableCollection<FavoritePageItem> savedRecipes;

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if(faveView != null)
			{
				faveView.ItemsSource = CreateListViewFromSearch (Globals.firebaseData.savedRecipes);
			}
		}

		public FavoriteRecipesPage ()
		{
			faveDeleteButton = new ToolbarItem ("Delete Favorites", "drawable/delete.png", () => { });

			ToolbarItems.Add (faveDeleteButton);

			ListView listView = new ListView {
				ItemsSource = CreateListViewFromSearch (Globals.firebaseData.savedRecipes),
				ItemTemplate = new DataTemplate (typeof (FavoritePageCell))
			};

			faveDeleteButton.Clicked += (sender, e) => {

				//TODO Awful way to do this, but there doesn't seem to be
				// a better way to do it right now
				var tempList = new ObservableCollection<FavoritePageItem> ();
				if (faveDeleteButton.Icon == "drawable/delete.png") 
				{
					faveDeleteButton.Icon = "drawable/cancel.png";


					foreach (var item in listView.ItemsSource)
					{
						var buttonItem = ((FavoritePageItem)item);
						buttonItem.Visible = true;
						buttonItem.Enabled = true;
						tempList.Add (buttonItem);
					}
				}
				else 
				{
					faveDeleteButton.Icon = "drawable/delete.png";
					foreach (var item in listView.ItemsSource) 
					{
						var buttonItem = ((FavoritePageItem)item);
						buttonItem.Visible = false;
						buttonItem.Enabled = false;
						tempList.Add (buttonItem);
					}
				}
				listView.ItemsSource = tempList;
			};

			faveView = listView;

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
					var id = ((FavoritePageItem)e.Item).Id;
					var recipe = await REST_API.GET_RecipeInformation (id, false);
					var recipePage = new RecipePage (recipe);
					await Navigation.PushAsync (recipePage);
					listView.SelectedItem = null;
					finished = true;
				}
				else if (finished == false) {
					listView.SelectedItem = null;
				}
			};
		}

		public ObservableCollection<FavoritePageItem>  CreateListViewFromSearch (List<Recipe> recipe)
		{
			//var items = REST_API.GET_SearchRecipes (recipe).Result;
			var searchResultItems = new ObservableCollection<FavoritePageItem> ();

			if (recipe != null) 
			{
				recipe.ForEach (x => searchResultItems.Add (new FavoritePageItem {
					Image = x.image,
					Name = x.name,
					//RemoveButton = new Button {
					//	Image = "drawable/remove.png",
					//	BackgroundColor = Color.Transparent,
					//	IsEnabled = false,
					//	IsVisible = true,
					//},
					Id = x.id,
					Enabled = false,
					Visible = false,
				}));
			}

			savedRecipes = searchResultItems;

			return searchResultItems;
		}
	}
}
