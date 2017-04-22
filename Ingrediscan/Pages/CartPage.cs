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

		public static Entry textInput;
		public static List<Ingredient> newItemList = new List<Ingredient>();
		public static Recipe userInputRec = new Recipe(newItemList, "My List", "", "", 0, 0);
		public static bool commaCheck = false;


        public CartPage ()
		{
            UpdateCheckBoxes();

            var template = new DataTemplate(typeof(CartPageCell));

            ToolbarItems.Add(new ToolbarItem("Sort Cart", "drawable/list.png", () =>
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
                            new Xamarin.Forms.ListView {
                                IsGroupingEnabled = true,
                                GroupDisplayBinding = new Binding ("Name"),
                                GroupShortNameBinding = new Binding ("Name"),

								GroupHeaderTemplate = new DataTemplate(typeof(CartPageHeader)),
								SeparatorVisibility = SeparatorVisibility.None,

								ItemTemplate = template,
								ItemsSource = items,
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
							new Xamarin.Forms.ListView {
								IsGroupingEnabled = true,
								GroupDisplayBinding = new Binding ("Name"),
								GroupShortNameBinding = new Binding ("Name"),

								GroupHeaderTemplate = new DataTemplate(typeof(CartPageHeader)),
								SeparatorVisibility = SeparatorVisibility.None,

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
			}));

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

				GroupHeaderTemplate = new DataTemplate(typeof(CartPageHeader)),
				SeparatorVisibility = SeparatorVisibility.None,

				ItemTemplate = template,
				ItemsSource = items
			};

			Content = new StackLayout {
				Children = {
					listView
				}
			};

			popupLayout.Content = new StackLayout {
				Children = {
					listView
				}
			};

			Content = popupLayout;
            sortByRecipe = false;

			// adding to cart
			ToolbarItem addToCartButton = new ToolbarItem("Add to Cart", "drawable/add.png", () => { });
			ToolbarItems.Add(addToCartButton);

			addToCartButton.Clicked += async (sender, e) =>
			{
				var userInput = await InputBox(this.Navigation);
				Console.WriteLine("USER INPUT: " + userInput);
				textInput.Unfocus(); //puts keyboard away after submit

				//default values for input without amount specified (without comma)
				var itemName = userInput;
				var itemAmount = "";
				var itemAmountD = 1.0;

				//checking for comma and breaking down name, amount accordingly
				if (commaCheck)
				{
					Console.WriteLine("INPUT CHECK FOR COMMA TRUE");
					string[] inputArray = userInput.Split(',');
					itemName = inputArray[0];
					itemAmount = inputArray[1];
					itemAmountD = Convert.ToDouble(itemAmount);
				}

				Console.WriteLine("ITEM NAME: " + itemName + ", AMOUNT: " + itemAmount);

				//new Ingredient from new item, adds new Ingredient to firebase cart
				Ingredient newItem = new Ingredient(itemName, itemAmountD, "", "", "", false, false);

                if(!Globals.firebaseData.cart.ContainsKey("My List"))
                {
                    Globals.firebaseData.cart.Add("My List", userInputRec);
                }
				Globals.firebaseData.cart["My List"].ingredients.Add(newItem);

				Console.WriteLine("INGREDIENTS IN MY LIST AFTER ADDING NEW INPUT: ");
				foreach (var item in Globals.firebaseData.cart["My List"].ingredients)
				{
					Console.WriteLine("ITEM =" + item.name);
				}

				//			saveCart(); //fairly certain this is not necessary, leaving here in case
				SaveAndLoad.SaveToFirebase(Globals.firebaseData); //?? does this automatically when adding to firebasedata ??

                /*
				Recipe userListHead = new Recipe(newItemList, "My List", "", "", 0, 0);
				userListHead.addToCart();
				Console.WriteLine("NEW RECIPE CREATED AND ADDED TO CART");

				GroupCart userList = new GroupCart(userListHead.name);
				items.Add(userList);
				Console.WriteLine("NEW GROUP CART ADDED TO ITEMS");
				*/

                /* ?? After submit, load new cart page? better than loading new data??
				/ creates new cart page on top of other -- bad idea, leaving here in case
				bool newCartPg = await DisplayAlert("Add To Cart", "This item is added to your cart!", "OK", "Cancel");
				if (newCartPg == true)
				{
					Console.WriteLine("WE ARE ABOUT TO PUSH NEW CART PAGE HERE");
					await (this.Navigation).PushAsync(new CartPage());
				} */


                //loading data from saved after new Ingredient added to user input recipe/header
				var list2 = CreateRecipeListViewFromList(Globals.firebaseData.cart);
				var items2 = new List<GroupCart>();
				foreach (var rec in list2)
				{
					var group = new GroupCart(rec.Name);

					foreach (var ing in rec.Ingredients)
					{
						group.Add(ing);
					//	Console.WriteLine("GROUP " + group.Name + ", ADDING INGREDIENT " + ing.Name);
					}

					items2.Add(group);
				}


				Title = "Shopping Cart";
				popupLayout.Content = new StackLayout
				{
					Children = {
							new Xamarin.Forms.ListView {
								IsGroupingEnabled = true,
								GroupDisplayBinding = new Binding ("Name"),
								GroupShortNameBinding = new Binding ("Name"),

								GroupHeaderTemplate = new DataTemplate(typeof(CartPageHeader)),
								SeparatorVisibility = SeparatorVisibility.None,

								ItemTemplate = template,
								ItemsSource = items2
							}
						}
				};
				Content = popupLayout;

			};


			ToolbarItems.Add (new ToolbarItem ("Delete From Cart", "drawable/delete.png", async () => {
                //await DisplayAlert ("Delete Cart", "This feature has not been implemented yet.", "OK");
                //Toast.MakeText (Forms.Context, "This feature has not been implemented yet.", ToastLength.Short).Show ();
                //UpdateCheckBoxes();
                var markedI = new List<string>();
                bool result = await DisplayAlert("Delete From Cart", "Would you like to delete all of these items from your cart?", "Confirm", "Cancel");

                if (result)
                {
                    foreach (KeyValuePair<string, bool> marked in markedItems)
                    {
                        if (marked.Value == true)
                        {
                            //markedI.Add(marked.Key);
                            var recipeName = marked.Key.Split('!')[0];
                            var ingName = marked.Key.Split('!')[1];
                            Console.WriteLine("DELETING " + marked.Key);

                            // Globals.firebaseData.cart.Remove(marked.Key);

                            var itemTemp = new Ingredient("",0.0,"","","",false,false);
                            Console.WriteLine("Recipe name " + ingName);
                            Console.WriteLine("marked.Key " + marked.Key);
                            var ings = Globals.firebaseData.cart[recipeName].ingredients;
                            for (int i = 0; i < ings.Count; ++i)
                            {
                                if (ings[i].formattedName.ToLower() == ingName.ToLower())
                                {
                                    Console.WriteLine("REMOVED");
                                    Globals.firebaseData.cart[recipeName].ingredients.RemoveAt(i);
                                    break;
                                }
                            }
                            if(Globals.firebaseData.cart[recipeName].ingredients.Count == 0)
                            {
                                Globals.firebaseData.cart.Remove(recipeName);
                            }
                            //Globals.firebaseData.cart[recipeName].ingredients.Remove(itemTemp);
                            SaveAndLoad.SaveToFirebase(Globals.firebaseData);

                        }
                    }
                    markedI.ForEach(x => markedItems.Remove(x));
                    SaveAndLoad.SaveToFirebase(Globals.firebaseData);
                    //await SaveAndLoad.LoadFromFirebase();
                    if (!sortByRecipe)
                    {
                        // Create our data from our load data
                        var _list = CreateRecipeListViewFromList(Globals.firebaseData.cart);
                        items = new List<GroupCart>();
                        foreach (var rec in _list)
                        {
                            var group = new GroupCart(rec.Name);
                            foreach (var ing in rec.Ingredients)
                            {
                                group.Add(ing);
                            }
                            items.Add(group);
                        }
                        Title = "Shopping Cart";

                        popupLayout.Content = new StackLayout
                        {
                            Children = {

                            new Xamarin.Forms.ListView {
                                IsGroupingEnabled = true,
                                GroupDisplayBinding = new Binding ("Name"),
                                GroupShortNameBinding = new Binding ("Name"),

                                GroupHeaderTemplate = new DataTemplate(typeof(CartPageHeader)),
                                SeparatorVisibility = SeparatorVisibility.None,

                                ItemTemplate = template,
                                ItemsSource = items
                            }
                        }
                        };
                        Content = popupLayout;
                        /*Content = new StackLayout
                        {

                            Children = {
                                searchBar,
                                new Xamarin.Forms.ListView {
                                    IsGroupingEnabled = true,
                                    GroupDisplayBinding = new Binding ("Name"),
                                    GroupShortNameBinding = new Binding ("Name"),

                                    GroupHeaderTemplate = new DataTemplate(typeof(CartPageHeader)),
                                    SeparatorVisibility = SeparatorVisibility.None,

                                    ItemTemplate = template,
                                    ItemsSource = items
                                }
                            }
                        };*/
                    }
                    else
                    {
                        // Create our data from our load data
                        var _list = CreateIngredientListViewFromList(Globals.firebaseData.cart);
                        items = new List<GroupCart>();
                        var group = new GroupCart("Ingredients");
                        foreach (var rec in _list)
                        {
                            foreach (var ing in rec.Ingredients)
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
                            new Xamarin.Forms.ListView {
                                IsGroupingEnabled = true,
                                GroupDisplayBinding = new Binding ("Name"),
                                GroupShortNameBinding = new Binding ("Name"),

                                GroupHeaderTemplate = new DataTemplate(typeof(CartPageHeader)),
                                SeparatorVisibility = SeparatorVisibility.None,

                                ItemTemplate = template,
                                ItemsSource = items
                            }
                        }
                        };
                    }
                }

                //Used code from above to regenerate the List of Ingredients
                


            }));
		}

		public static Task<string> InputBox(INavigation navigation)
		{
			Console.WriteLine("MADE IT TO USER INPUT PAGE ON ADD CART");
			//instance of TaskCompletionSource to return result of user input
			var tcs = new TaskCompletionSource<string>();

			//title of popup input page
			var labelTitle = new Label
			{
				Text = "Add To Cart",
				HorizontalOptions = LayoutOptions.Center,
				FontAttributes = FontAttributes.Bold,
				Font = Font.SystemFontOfSize(NamedSize.Large),
				TextColor = Color.FromHex("#095594")
			};
			//message within input page, prompt user to enter new item
			var labelMessage = new Label { Text = "Please enter the item you would like to add to your cart, followed by a comma and the amount: " };
			//initialize public variable textInput as Entry to get user input
			textInput = new Entry { Text = "" };

			//submit button creation
			var submit = new Xamarin.Forms.Button
			{
				Text = "Submit",
				WidthRequest = 100,
				BackgroundColor = Color.FromHex("#095594"),
				TextColor = Color.White

			};

			//submits user input as a new item, and passes to tcs for return at end of function
			submit.Clicked += async (sender, e) =>
			{
				//close keyboard
				textInput.Unfocus();
				//get result
				var newItem = "";
				var result = textInput.Text;

				/* doing this seems to throw unhandled NRE, seemingly an issue with PopModalAsync, 
				possible solution is to check for these after receiving input within the addToCart.Clicked method  */
				//check for null input, 'blank', or no item name input from user, if so close entry form
				/*				if (result != null)
								{
									await navigation.PopModalAsync();
								}
								else if (result == " ")
								{
									await navigation.PopModalAsync();
								}
								else if (result == ",")
								{
									await navigation.PopModalAsync();
								} 
								else
								{ }  */

				//check that comma is present in input
				var commaPresent = result.IndexOf(",");
				if ((commaPresent >= 0))
				{
					Console.WriteLine("COMMA CHECK GOOD");
					commaCheck = true;
				}
				newItem = result;
				Toast.MakeText(Forms.Context, "Adding item to cart . . . ", ToastLength.Long).Show();
				await navigation.PopModalAsync();

				Console.WriteLine("SUBMIT CLICKED GOOD");
				//pass result to tcs
				tcs.SetResult(newItem);
			};

			//cancel button description
			var cancel = new Xamarin.Forms.Button
			{
				Text = "Cancel",
				WidthRequest = 100,
				BackgroundColor = Color.FromHex("#095594"),
				TextColor = Color.White
			};

			//closes popup page if user input is canceled
			cancel.Clicked += async (sender, e) =>
			{
				await navigation.PopModalAsync();
			};

			//sets horizontal layout for buttons
			var buttonLayout = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					submit,
					cancel
				}
			};

			//sets layout for title, message, text input and buttons
			var layout = new StackLayout
			{
				Padding = new Thickness(20, 40, 20, 0),
				VerticalOptions = LayoutOptions.StartAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Orientation = StackOrientation.Vertical,
				//	Style = Application.Current.Resources["aboutBodyLabelStyle"] as Style,
				Children = {
					labelTitle,
					labelMessage,
					textInput,
					buttonLayout
				}
			};

			//create and show page when called
			var page = new ContentPage();
			page.Content = layout;
			navigation.PushModalAsync(page);
			//brings keyboard to screen
			textInput.Focus();

			return tcs.Task;
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
					subItem.CheckBoxName = "drawable/unchecked.png";
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
                foreach (var ss in recipe.Value.ingredients)
                {
                    if (!ss.deleted)
                    { 
                        CartPageItem.RecipeIngredients subItem = new CartPageItem.RecipeIngredients();
                        ss.setFormattedName();
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
                            subItem.CheckBoxName = "drawable/unchecked.png";
                        }

                        subItems.Add(subItem);
                    }
                   
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
                    if (!markedItems.ContainsKey(recipeName + "!" + ing.formattedName))
                    {
                        markedItems.Add(recipeName + "!" + ing.formattedName, ing.itemSwitch);
                    }
                    else
                    {
                        markedItems[recipeName + "!" + ing.formattedName] = ing.itemSwitch;
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
                    ing.itemSwitch = markedItems[recipeName + "!" + ing.formattedName];
                    //Console.WriteLine(recipeName + " " + ing.formattedName + ":" + markedItems[recipeName + " " + ing.formattedName]);
                }
            }
            
            SaveAndLoad.SaveToFirebase(Globals.firebaseData);
        }

	}
}
