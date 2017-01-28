using System;
using System.Collections.Generic;

using Xamarin.Forms;

using Ingrediscan.Utilities;
using Android.Widget;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Ingrediscan
{
	public class RecipePage : TabbedPage
	{
		ToolbarItem faveBefore, addToCart;

		public RecipePage (SpoonacularClasses.RecipeInformation instructions)
		{
			// Creation of ingredients and recipe
			List<Ingredient> ingredients = new List<Ingredient> ();
			foreach(var item in instructions.extendedIngredients)
			{
				Ingredient ing = new Ingredient (item.name, item.amount, item.unit, item.id, item.image, false);
				ing.setFormattedName ();
				Console.WriteLine (ing.formattedName);
				ingredients.Add (ing);
			}
			Recipe recipe = new Recipe (ingredients, instructions.title, instructions.id, instructions.image,
									   instructions.preparationMinutes, instructions.cookingMinutes);//, false);

			//End of creation

			Title = recipe.name;

			// Addition to toolbar
			addToCart = new ToolbarItem ("Add To Cart", "drawable/addToCart.png", async () => {
				bool result = await DisplayAlert ("Add To Cart", "Would you like to add all of these items to your cart?", "Confirm", "Cancel");
				if (result == true) {
					if (Globals.firebaseData.cart.ContainsKey (recipe.name)) {
						bool cont = await DisplayAlert ("Add To Cart", "This item is already contained within your cart. " +
															"Would you still like to add all of these items to your cart?", "Confirm", "Cancel");
						if (cont == true) {
							recipe.addToCart ();

							//await DisplayAlert ("Add To Cart", "This item has been successfully added to the cart.", "Ok");
							Toast.MakeText (Forms.Context, "This item has been added to your cart.", ToastLength.Short).Show ();
						}
					} else {
						recipe.addToCart ();

						//await DisplayAlert ("Add To Cart", "This item has been successfully added to the cart.", "Ok");
						Toast.MakeText (Forms.Context, "This item has been added to your cart.", ToastLength.Short).Show ();
					}

				}
			});

			faveBefore = new ToolbarItem ("Favorite Recipe", "drawable/faveBefore.png", () => {
				try
				{
					bool contains = false;

					foreach(var tempR in Globals.firebaseData.savedRecipes)
					{
						if(recipe.name == tempR.name)
						{
							contains = true;
							break;
						}
					}

					if (!contains) 
					{
						Globals.firebaseData.savedRecipes.Add (recipe);

						//await DisplayAlert ("Favorite Recipe", "This recipe has been saved to your favorited list.", "Ok");
						Toast.MakeText (Forms.Context, "This recipe has been favorited.", ToastLength.Short).Show ();
					}
					else
					{
						//await DisplayAlert ("Favorite Recipe", "This recipe is already a favorited recipe.", "Ok");
						Toast.MakeText (Forms.Context, "This recipe is already a favorited recipe.", ToastLength.Short).Show ();
					}
				}
				catch(Exception e)
				{
					Console.WriteLine (e.Message);
					Globals.firebaseData.savedRecipes = new List<Recipe> ();
					Globals.firebaseData.savedRecipes.Add (recipe);

					//await DisplayAlert ("Favorite Recipe", "This recipe has been saved to your favorited list.", "Ok");
					Toast.MakeText (Forms.Context, "This recipe has been saved to your favorited list.", ToastLength.Short).Show ();
				}

				SaveAndLoad.SaveToFirebase (Globals.firebaseData);
			});

			ToolbarItems.Add (addToCart);
			ToolbarItems.Add (faveBefore);

			// Finished adding to toolbar

			// Creation of Prep Page

			Xamarin.Forms.ListView resultsViewIngredients = new Xamarin.Forms.ListView {
				ItemsSource = ingredients,
				ItemTemplate = new DataTemplate (() => {
					var imageCell = new ImageCell ();
					imageCell.SetBinding (TextCell.TextProperty, "Name");
					imageCell.SetBinding (ImageCell.ImageSourceProperty, "Image");
					return imageCell;
				}),
			};

			resultsViewIngredients.ItemsSource = this.CreateListViewFromInstructions (recipe);

			string prepTime = "";
			if (instructions.preparationMinutes == 0) 
			{
				prepTime = "N/A";
			} else 
			{
				prepTime = instructions.preparationMinutes.ToString () + " Minutes";
			}

			var gridP = new Grid ();

			gridP.Padding = new Thickness (5, 0, 2, 5);
			gridP.Children.Add (new Label { Text = "Ingredients", FontSize = 13.0, FontAttributes = FontAttributes.Italic },0,1);
			gridP.Children.Add (new Label { Text = "Preparation Time: " + prepTime, FontSize = 13.0, FontAttributes = FontAttributes.Bold },1,1);

			var prepPage = new ContentPage {
				Content = new StackLayout {
					Children = {
						gridP,
						resultsViewIngredients
					}
				}
			};

			prepPage.Title = "Preparation";
			// Prep Page Created

			// Creation of Cook Page

			Xamarin.Forms.ListView resultsViewSteps = new Xamarin.Forms.ListView {
				IsGroupingEnabled = true,
				GroupDisplayBinding = new Binding ("Number"),
				GroupShortNameBinding = new Binding ("Number"),

				ItemsSource = this.CreateListViewFromSteps (recipe),
				ItemTemplate = new DataTemplate (typeof (RecipePageCell)),
				HasUnevenRows = true,
				SeparatorVisibility = SeparatorVisibility.None
			};

			string cookTime = "";
			if (instructions.cookingMinutes == 0) 
			{
				cookTime = "N/A";
			} 
			else 
			{
				cookTime = instructions.cookingMinutes.ToString () + " Minutes";
			}

			var gridC = new Grid ();

			gridC.Padding = new Thickness (5, 0, -75, 5);
			gridC.Children.Add (new Label { Text = "Instructions", FontSize = 13.0, FontAttributes = FontAttributes.Italic }, 0, 1);
			gridC.Children.Add (new Label { Text = "Cook Time: " + cookTime, FontSize = 13.0, FontAttributes = FontAttributes.Bold }, 1, 1);


			var cookPage = new ContentPage {
				Content = new StackLayout {
					Children = {
						gridC,
						resultsViewSteps
					}
				}
			};

			resultsViewSteps.ItemSelected += (sender, e) => {
				resultsViewSteps.SelectedItem = null;
			};
			resultsViewIngredients.ItemSelected += (sender, e) => {
				resultsViewIngredients.SelectedItem = null;
			};

			cookPage.Title = "Cook";

			// Cook Page Created

			resultsViewSteps.ItemSelected += (sender, e) => {
				resultsViewSteps.SelectedItem = null;
			};

			resultsViewIngredients.ItemSelected += (sender, e) => {
				resultsViewIngredients.SelectedItem = null;
			};

			// Adding pages to tabbed page
			Children.Add (prepPage);
			Children.Add (cookPage);
		}

		/// <summary>
		/// Creates the list view from instructions.
		/// </summary>
		/// <returns>The list view from instructions.</returns>
		/// <param name="recipe">Recipe.</param>
		public List<RecipePageItem.RecipePageIngredients> CreateListViewFromInstructions (Recipe recipe)
		{
			List<RecipePageItem.RecipePageIngredients> searchResultItems = new List<RecipePageItem.RecipePageIngredients> ();

			recipe.ingredients.ForEach(x => searchResultItems.Add (new RecipePageItem.RecipePageIngredients {
				Name = x.formattedName,
				Image = x.image
			}));

			return searchResultItems;
		}
		/// <summary>
		/// Creates the list view from steps.
		/// </summary>
		/// <returns>The list view from steps.</returns>
		/// <param name="recipe">Recipe.</param>
		public List<GroupRecipe> CreateListViewFromSteps (Recipe recipe)
		{
			List<GroupRecipe> searchResultItems = new List<GroupRecipe> ();
			// TODO An additional API Call... Able to parse steps from RecipeInformation, but format is not always the same
			// Maybe we can create cases for the most general 

			List<SpoonacularClasses.RecipeInstructions> steps = REST_API.GET_RecipeInstructions (recipe.id, false);

			int characterLimit = 50;

			if (steps.Count > 0) 
			{
				var recipeSteps = steps [0].steps;
				int counter = 1;
				Console.WriteLine ("COUNT: " + recipeSteps.Count);
				if (recipeSteps.Count > 1) 
				{
					//TODO Make every recipe available, not just the first one. ie steps[0] <- steps[0...]
					// Have the user decide which recipe variant they want to use.
					for (int i = 0; i < recipeSteps.Count; ++i) 
					{
						if (!char.IsDigit (recipeSteps[i].step [0]) && 
						    recipeSteps[i].step.ToLower () != "kitchen-friendly view") 
						{
							GroupRecipe recGroup = new GroupRecipe ("Step " + counter);
							var text = new RecipePageItem.RecipePageStep ();
							text.Step = recipeSteps[i].step;

							recGroup.Add (text);
							searchResultItems.Add (recGroup);

							++counter;
						}
					}
				
				}
				else
				{
					var recipeStrings = recipeSteps [0].step.Split('.');

					for (int i = 0; i < recipeStrings.Length; ++i) 
					{
						var step = recipeStrings [i];

						if (step.Length > 0) 
						{
							char letter = step [0];
							if (letter == ' ') 
							{
								step = step.Substring (1);
							}

							if (!char.IsDigit (step [0]) &&
							    step.ToLower () != "kitchen-friendly view") 
							{
								GroupRecipe recGroup = new GroupRecipe ("Step " + counter);
								var text = new RecipePageItem.RecipePageStep ();
								text.Step = step + ".";

								recGroup.Add (text);
								searchResultItems.Add (recGroup);

								++counter;
							}
						}
					}
				}
			}

			return searchResultItems;
		}
	}
}
