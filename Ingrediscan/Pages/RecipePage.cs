using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class RecipePage : TabbedPage
	{
		public RecipePage (SpoonacularClasses.RecipeInformation instructions)
		{
			// Creation of ingredients and recipe
			List<Ingredient> ingredients = new List<Ingredient> ();
			foreach(var item in instructions.extendedIngredients)
			{
				Ingredient ing = new Ingredient (item.name, item.amount, item.unit, item.id, item.image, false);
				ingredients.Add (ing);
			}
			Recipe recipe = new Recipe(ingredients, instructions.title, instructions.id, instructions.image,
			                           instructions.preparationMinutes, instructions.cookingMinutes);

			//End of creation

			// Addition to toolbar
			ToolbarItems.Add (new ToolbarItem ("Add To Cart", "drawable/addToCart.png", async () => {
				bool result = await DisplayAlert ("Add To Cart", "Would you like to add all of these items to your cart?", "Confirm", "Cancel");
				if (result == true) 
				{
					if(GlobalVariables.CurrentRecipes.Contains(recipe))
					{
						bool cont = await DisplayAlert ("Add To Cart", "This item is already contained within your cart. " +
						                                    "Would you still like to add all of these items to your cart?", "Confirm", "Cancel");
						if(cont == true)
						{
							recipe.addToCart ();
						}
					}
					else
					{
						recipe.addToCart ();
					}

				}
			}));

			// Finished adding to toolbar

			// Creation of Prep Page

			ListView resultsViewIngredients = new ListView {
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

			prepPage.Title = "Preparation Page";
			// Prep Page Created

			// Creation of Cook Page

			//List<RecipePageItem.RecipePageStep> recipeSteps = new List<RecipePageItem.RecipePageStep> ();

			ListView resultsViewSteps = new ListView {
				//ItemsSource = recipeSteps,
				IsGroupingEnabled = true,
				GroupDisplayBinding = new Binding ("Number"),
				GroupShortNameBinding = new Binding ("Number"),

				ItemsSource = this.CreateListViewFromSteps (recipe),
				ItemTemplate = new DataTemplate (typeof (RecipePageCell)),
				HasUnevenRows = true,
				SeparatorVisibility = SeparatorVisibility.None
			};

			//resultsViewSteps.ItemsSource = this.CreateListViewFromSteps (recipe);

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

			cookPage.Title = "Cook Page";

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

			recipe.getIngredientList().ForEach(x => searchResultItems.Add (new RecipePageItem.RecipePageIngredients {
				Name = x.getFormattedName(),
				Image = x.getImage()
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

			List<SpoonacularClasses.RecipeInstructions> steps = REST_API.GET_RecipeInstructions (recipe.getID (), false).Result;

			if (steps.Count > 0) 
			{
				int counter = 1;
				for (int i = 0; i < steps[0].steps.Count; ++i)
				{
					if (!char.IsDigit (steps [0].steps [i].step [0]) && steps[0].steps[i].step.ToLower() != "kitchen-friendly view") 
					{
						GroupRecipe recGroup = new GroupRecipe ("Step " + counter);
						var text = new RecipePageItem.RecipePageStep ();
						text.Step = steps [0].steps [i].step;

						recGroup.Add (text);
						Console.WriteLine (steps [0].steps [i].step);
						searchResultItems.Add (recGroup);

						++counter;
					}
				}
			}

			return searchResultItems;
		}
	}
}
