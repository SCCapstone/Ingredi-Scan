using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

using Ingrediscan.Utilities;
using Android.Widget;
using System.Threading.Tasks;

namespace Ingrediscan
{
	public class CartPage : ContentPage
	{
        public static Dictionary<string, bool> markedItems = new Dictionary<string, bool>();

        public CartPage ()
		{
            UpdateCheckBoxes();

			ToolbarItems.Add (new ToolbarItem ("Edit Cart", "drawable/edit.png", () => {
				//await DisplayAlert ("Edit Cart", "This feature has not been implemented yet.", "OK");
				Toast.MakeText (Forms.Context, "This feature has not been implemented yet.", ToastLength.Short).Show ();
			}));

			ToolbarItems.Add (new ToolbarItem ("Delete Cart", "drawable/delete.png", () => {
				//await DisplayAlert ("Delete Cart", "This feature has not been implemented yet.", "OK");
				Toast.MakeText (Forms.Context, "This feature has not been implemented yet.", ToastLength.Short).Show ();
			}));

			// Create our data from our load data
			var list = this.CreateListViewFromList (Globals.firebaseData.cart);

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
					//DisplayAlert ("Search Cart", "This feature has not been implemented yet.", "OK");
					Toast.MakeText (Forms.Context, "This feature has not been implemented yet.", ToastLength.Short).Show ();
				})
			};

			Title = "Shopping Cart";
			Content = new StackLayout {
				Children = {
					searchBar,
					new Xamarin.Forms.ListView {
						IsGroupingEnabled = true,
						GroupDisplayBinding = new Binding ("Name"),
						GroupShortNameBinding = new Binding ("Name"),

						ItemTemplate = template,
						ItemsSource = items
					}
				}
			};
		}

		// TODO Not yet implemented
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
                    subItem.RecipeName = recipe.Value.name;
					subItem.CheckBox = new Xamarin.Forms.Button();
					subItem.CheckBox.SetValue(Xamarin.Forms.Button.IsEnabledProperty, ss.itemSwitch);

                    if (ss.itemSwitch)
                    {
                        subItem.CheckBoxName = "drawable/selected.png";
                    }
                    else
                    {
                        subItem.CheckBoxName = null;
                    }

                    subItems.Add (subItem);
                   
				}
                
				//Sub items complete

				item.Ingredients = subItems;
				// Item complete

				items.Add (item);
			}
            
			return items;
		}
        
        public static void UpdateCheckBoxes()
        {
            foreach (var rec in Globals.firebaseData.cart)
            {
                var recipeName = rec.Value.name;
                foreach (var ing in rec.Value.ingredients)
                {
                    Console.WriteLine(recipeName + " " + ing.formattedName);
                    if (!markedItems.ContainsKey(recipeName + " " + ing.formattedName))
                    {
                        markedItems.Add(recipeName + " " + ing.formattedName, ing.itemSwitch);
                    }
                    else
                    {
                        markedItems[recipeName + " " + ing.formattedName] = ing.itemSwitch;
                    }
                }
            }
            Console.WriteLine("UPDATED!!!!!!!!!!!");
        }

        public static void saveCart()
        {
            foreach(var rec in Globals.firebaseData.cart)
            {
                var recipeName = rec.Value.name;
                foreach(var ing in rec.Value.ingredients)
                {
                    ing.itemSwitch = markedItems[recipeName + " " + ing.formattedName];
                    //Console.WriteLine(recipeName + " " + ing.formattedName + ":" + markedItems[recipeName + " " + ing.formattedName]);
                }
            }
            
            SaveAndLoad.SaveToFirebase(Globals.firebaseData);
        }

	}
}
