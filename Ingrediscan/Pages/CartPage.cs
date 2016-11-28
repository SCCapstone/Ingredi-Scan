using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Ingrediscan
{
	public class CartPage : ContentPage
	{
		public CartPage ()
		{
			// Create our data from our load data
			var list = this.CreateListViewFromList (GlobalVariables.CurrentRecipes);

			var temp = new DataTemplate (typeof (ImageCell));//CartPageCell));
			temp.SetBinding (TextCell.TextProperty, "Name");
			temp.SetBinding (ImageCell.ImageSourceProperty, "Image");

			var items = new List<Group> ();

			foreach(var rec in list)
			{
				var group = new Group (rec.Name);

				foreach (var ing in rec.Ingredients) {
					group.Add (ing);
				}

				items.Add (group);
			}

			Title = "Cart Page";
			Content = new StackLayout {
				Children = {
					new ListView {
						IsGroupingEnabled = true,
						GroupDisplayBinding = new Binding ("Name"),
						GroupShortNameBinding = new Binding ("Name"),

						ItemTemplate = temp,
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

		public List<CartPageItem.Recipes> CreateListViewFromList (List<Recipe> recipes)
		{
			List<CartPageItem.Recipes> items = new List<CartPageItem.Recipes> ();

			foreach(var s in recipes)
			{
				// Create item
				CartPageItem.Recipes item = new CartPageItem.Recipes ();
				item.Name = s.getName();
				item.Image = s.getImage();

				// Create sub items
				List<CartPageItem.Ingredients> subItems = new List<CartPageItem.Ingredients> ();
				foreach(var ss in s.getIngredientList())
				{
					CartPageItem.Ingredients subItem = new CartPageItem.Ingredients ();
					//subItem.Name = ss.getName ();
					subItem.Name = ss.getFormattedName ();
					subItem.Image = ss.getImage ();

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
