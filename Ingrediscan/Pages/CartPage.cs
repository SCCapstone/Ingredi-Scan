using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

using Ingrediscan.Utilities;

namespace Ingrediscan
{
	public class CartPage : ContentPage
	{
		public CartPage ()
		{
			ToolbarItems.Add (new ToolbarItem ("Edit Cart", "drawable/edit.png", async () => {
				await DisplayAlert ("Edit Cart", "This feature has not been implemented yet.", "OK");
			}));

			ToolbarItems.Add (new ToolbarItem ("Delete Cart", "drawable/delete.png", async () => {
				await DisplayAlert ("Delete Cart", "This feature has not been implemented yet.", "OK");
			}));

			// Create our data from our load data
			var list = this.CreateListViewFromList (Globals.firebaseData.cart);//GlobalVariables.CurrentRecipes);

			var template = new DataTemplate (typeof (CartPageCell));
			var items = new List<GroupCart> ();

			foreach(var rec in list)
			{
				var group = new GroupCart (rec.Name);

				foreach (var ing in rec.Ingredients) {
					group.Add (ing);
				}

				items.Add (group);
			}

			SearchBar searchBar = new SearchBar {
				Placeholder = "Enter search term",
				SearchCommand = new Command (() => {
					DisplayAlert ("Search Cart", "This feature has not been implemented yet.", "OK");
				})
			};

			Title = "Shopping Cart";
			Content = new StackLayout {
				Children = {
					searchBar,
					new ListView {
						IsGroupingEnabled = true,
						GroupDisplayBinding = new Binding ("Name"),
						GroupShortNameBinding = new Binding ("Name"),

						ItemTemplate = template,
						ItemsSource = items
					}
				}
			};
		}

		public List<string> searchCart(string query)
		{
			List<string> items = new List<string> ();


			return items;
		}

		public List<CartPageItem.Recipes> CreateListViewFromList (Dictionary<string, Recipe> recipes)
		{
			List<CartPageItem.Recipes> items = new List<CartPageItem.Recipes> ();

			foreach(KeyValuePair<string, Recipe> recipe in recipes)
			{
				// Create item
				CartPageItem.Recipes item = new CartPageItem.Recipes ();
				item.Name = recipe.Value.name;
				item.Image = recipe.Value.image;

				// Create sub items
				List<CartPageItem.Ingredients> subItems = new List<CartPageItem.Ingredients> ();
				foreach(var ss in recipe.Value.ingredients)
				{
					CartPageItem.Ingredients subItem = new CartPageItem.Ingredients ();
					//subItem.Name = ss.getName ();
					subItem.Name = ss.formattedName;
					subItem.Image = ss.image;
					subItem.Switch = new Switch();
					subItem.Switch.SetValue(Switch.IsEnabledProperty, ss.itemSwitch);

					subItems.Add (subItem);
				}
				//Sub items complete

				item.Ingredients = subItems;
				// Item complete

				items.Add (item);
			}

			return items;
		}
	}
}
