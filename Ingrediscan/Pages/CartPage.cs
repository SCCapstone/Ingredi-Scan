using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

using Ingrediscan.Utilities;
using Android.Widget;
using System.Threading.Tasks;

using XLabs.Forms.Controls;

namespace Ingrediscan
{
	public class CartPage : ContentPage
	{
        public static Dictionary<string, bool> markedItems = new Dictionary<string, bool>();
		public static List<GroupCart> items;
		public bool sortByRecipe = true;
		PopupLayout popupLayout = new PopupLayout ();

        public CartPage ()
		{
            UpdateCheckBoxes();


			SearchBar searchBar = new SearchBar {
				Placeholder = "Enter search term",
				SearchCommand = new Command (() => {
					//DisplayAlert ("Search Cart", "This feature has not been implemented yet.", "OK");
					Toast.MakeText (Forms.Context, "This feature has not been implemented yet.", ToastLength.Short).Show ();
				})
			};
			var template = new DataTemplate (typeof (CartPageCell));

			var sortCartToolbarItem = new ToolbarItem("Sort Cart", "drawable/list.png", () =>
			{
				if(!sortByRecipe)
				{
					// Create our data from our load data
					var _list = CreateRecipeListViewFromList(Globals.firebaseData.cart);
					items = new List<GroupCart>();
					foreach(var rec in _list)
					{
						var group = new GroupCart(rec.Name);
						foreach(var ing in rec.Ingredients)
						{
							group.Add(ing);
						}
						items.Add(group);
					}
					Title = "Shopping Cart";
					Content = new StackLayout
					{
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
				else
				{
					// Create our data from our load data
					var _list = CreateIngredientListViewFromList(Globals.firebaseData.cart);
					items = new List<GroupCart>();
					var group = new GroupCart("Ingredients");
					foreach(var rec in _list)
					{
						foreach(var ing in rec.Ingredients)
						{
							group.Add(ing);
						}
					}
					items.Add(group);
					Console.WriteLine("We are here2");
					Title = "Shopping Cart";
					Content = new StackLayout
					{
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
				if(sortByRecipe)
				{
					sortByRecipe = false;
				}
				else
				{
					sortByRecipe = true;
				}
			});

			//ToolbarItems.Add(sortCartToolbarItem);
			/*
			ToolbarItems.Add (new ToolbarItem ("Edit Cart", "drawable/edit.png", () => {
				//await DisplayAlert ("Edit Cart", "This feature has not been implemented yet.", "OK");
				Toast.MakeText (Forms.Context, "This feature has not been implemented yet.", ToastLength.Short).Show ();
			}));

			ToolbarItems.Add (new ToolbarItem ("Delete Cart", "drawable/delete.png", () => {
				//await DisplayAlert ("Delete Cart", "This feature has not been implemented yet.", "OK");
				Toast.MakeText (Forms.Context, "This feature has not been implemented yet.", ToastLength.Short).Show ();
			}));*/

			// Create our data from our load data
			var list = this.CreateRecipeListViewFromList (Globals.firebaseData.cart);

			items = new List<GroupCart> ();

			foreach(var rec in list)
			{
				var group = new GroupCart (rec.Name);

				foreach (var ing in rec.Ingredients) {
					group.Add (ing);
				}

				items.Add (group);
			}


			Title = "Shopping Cart";
			var listView = new Xamarin.Forms.ListView {
				IsGroupingEnabled = true,
				GroupDisplayBinding = new Binding ("Name"),
				GroupShortNameBinding = new Binding ("Name"),

				ItemTemplate = template,
				ItemsSource = items
			};

			/*Content = new StackLayout {
				Children = {
					searchBar,
					listView
				}
			};*/

			popupLayout.Content = new StackLayout {
				Children = {
					searchBar,
					listView
				}
			};

			Content = popupLayout;

			ToolbarItems.Add (new ToolbarItem ("Add To Cart", "drawable/add.png", () => {
				//await DisplayAlert ("Edit Cart", "This feature has not been implemented yet.", "OK");
				//Toast.MakeText (Forms.Context, "This feature has not been implemented yet.", ToastLength.Short).Show ();
				var label = new Label {
					Text = "Add To Cart"
				};

				SearchBar search = new SearchBar ();
				search = new SearchBar {
					Placeholder = "Item to add...",
					SearchCommand = new Command (() => {
						var groupItem = new GroupCart (search.Text);
						var tempList = new List<GroupCart> ();
						foreach (var l in listView.ItemsSource) {
							tempList.Add ((GroupCart)l);
						}
						tempList.Add (groupItem);
						listView.ItemsSource = tempList;
						popupLayout.DismissPopup ();
					})
				};

				var frame = new Frame // define a Frame as PopUp and add the StackLayout as content
				{
					Content = new StackLayout {
						Children = {
						label, search
						}
					},
					HasShadow = true,
					OutlineColor = Color.White,
				};

				popupLayout.ShowPopup (frame);
				search.Focus ();
			}));

			ToolbarItems.Add (new ToolbarItem ("Delete From Cart", "drawable/delete.png", async () => {
                //await DisplayAlert ("Delete Cart", "This feature has not been implemented yet.", "OK");
                //Toast.MakeText (Forms.Context, "This feature has not been implemented yet.", ToastLength.Short).Show ();
                UpdateCheckBoxes();
                bool result = await DisplayAlert("Delete From Cart", "Would you like to delete all of these items from your cart?", "Confirm", "Cancel");

                if (result)
                {
                    foreach (KeyValuePair<string, bool> marked in markedItems)
                    {
                        if (marked.Value == true)
                        {
                            markedItems.Remove(marked.Key);
                        }
                    }
                }

			}));
		}

		// TODO Not yet implemented
		public List<string> searchCart(string query)
		{
			List<string> items = new List<string> ();


			return items;
		}

		public List<CartPageItem.Recipes> CreateIngredientListViewFromList(Dictionary<string, Recipe> recipes)
		{
			List<CartPageItem.Recipes> items = new List<CartPageItem.Recipes>();

			// Create item
			CartPageItem.Recipes item = new CartPageItem.Recipes();
			item.Name = "Ingredients";

			List<Ingredient> ings = new List<Ingredient>();
			foreach(KeyValuePair<string, Recipe> recipe in recipes)
			{
				// Add ingredients to array
				foreach(var ss in recipe.Value.ingredients)
				{
					var existing = ings.FindAll(x => x.name == ss.name);
					if(existing.Count == 0)
					{
						ings.Add(ss);
					}
					else
					{
						var found = false;
						for(int i = 0; i < existing.Count; i++)
						{
							if(ss.units == existing[i].units)
							{
								existing[i].amount += ss.amount;
								found = true;
								break;
							}
						}
						if(!found)
						{
							ings.Add(ss);
						}
					}
				}
			}
			List<CartPageItem.RecipeIngredients> subItems = new List<CartPageItem.RecipeIngredients>();
			foreach(var ing in ings)
			{
				CartPageItem.RecipeIngredients subItem = new CartPageItem.RecipeIngredients();
				ing.setFormattedName();
				subItem.Name = ing.formattedName;
				subItem.Image = ing.image;
				subItem.CheckBox = new Xamarin.Forms.Button();
				subItem.CheckBox.SetValue(Xamarin.Forms.Button.IsEnabledProperty, ing.itemSwitch);

				if(ing.itemSwitch)
				{
					subItem.CheckBoxName = "drawable/selected.png";
				}
				else
				{
					subItem.CheckBoxName = null;
				}

				subItems.Add(subItem);
			}
			item.Ingredients = subItems;
			items.Add(item);

			return items;
		}

		public List<CartPageItem.Recipes> CreateRecipeListViewFromList (Dictionary<string, Recipe> recipes)
		{
			List<CartPageItem.Recipes> items = new List<CartPageItem.Recipes> ();

			foreach(KeyValuePair<string, Recipe> recipe in recipes)
			{
				// Create item
				CartPageItem.Recipes item = new CartPageItem.Recipes ();
				item.Name = recipe.Value.name;
				item.Image = recipe.Value.image;

				// Create sub items
				List<CartPageItem.RecipeIngredients> subItems = new List<CartPageItem.RecipeIngredients> ();
				foreach(var ss in recipe.Value.ingredients)
				{
                    CartPageItem.RecipeIngredients subItem = new CartPageItem.RecipeIngredients ();
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
