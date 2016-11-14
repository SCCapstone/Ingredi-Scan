using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class RecipePage : TabbedPage
	{
		public RecipePage (List<SpoonacularClasses.RecipeInstructions> instructions)
		{
			List<RecipePageItem.RecipePageIngredients> recipeIngredients = new List<RecipePageItem.RecipePageIngredients> ();

			ListView resultsViewIngredients = new ListView {
				ItemsSource = recipeIngredients,
				ItemTemplate = new DataTemplate (() => {
					var imageCell = new ImageCell ();
					imageCell.SetBinding (TextCell.TextProperty, "Name");
					imageCell.SetBinding (ImageCell.ImageSourceProperty, "Image");
					return imageCell;
				}),
				VerticalOptions = LayoutOptions.StartAndExpand,
			};

			resultsViewIngredients.ItemsSource = this.CreateListViewFromInstructions (instructions);

			var prepPage = new ContentPage {
				Content = new StackLayout {
					Children = {
						resultsViewIngredients
					}
				}
			};
			prepPage.Title = "Prep Page";

			List<RecipePageItem.RecipePageStep> recipeSteps = new List<RecipePageItem.RecipePageStep> ();

			ListView resultsViewSteps = new ListView {
				ItemsSource = recipeSteps,
				ItemTemplate = new DataTemplate (() => {
					var text = new TextCell ();
					text.SetBinding (TextCell.TextProperty, "Number");
					text.SetBinding (TextCell.DetailProperty, "Step");
					return text;
				}),
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,

			};

			resultsViewSteps.ItemsSource = this.CreateListViewFromSteps (instructions);


			var cookPage = new ContentPage {
				Content = new StackLayout {
					Children = {
						resultsViewSteps
					}
				}
			};
			cookPage.Title = "Cook Page";

			Children.Add (prepPage);
			Children.Add (cookPage);
		}

		public List<RecipePageItem.RecipePageIngredients> CreateListViewFromInstructions (List<SpoonacularClasses.RecipeInstructions> instructions)
		{
			List<RecipePageItem.RecipePageIngredients> searchResultItems = new List<RecipePageItem.RecipePageIngredients> ();

			instructions [0].steps.ForEach(x =>
			                              x.ingredients.ForEach(y => searchResultItems.Add (new RecipePageItem.RecipePageIngredients {
				Name = y.name,
				Image = y.image
			})));

			return searchResultItems;
		}

		public List<RecipePageItem.RecipePageStep> CreateListViewFromSteps (List<SpoonacularClasses.RecipeInstructions> instructions)
		{
			List<RecipePageItem.RecipePageStep> searchResultItems = new List<RecipePageItem.RecipePageStep> ();

			instructions [0].steps.ForEach (x => searchResultItems.Add (new RecipePageItem.RecipePageStep {
				Number = "Step " + x.number,
				Step = x.step
			}));

			return searchResultItems;
		}
	}
}
