using System;
using System.Collections.Generic;

namespace Ingrediscan
{
	public class SpoonacularClasses
	{
		// Used by REST_API.GET_FindByUPC
		public class FindByUPC
		{
			public long id { get; set; }
			public string title { get; set; }
			public double? price { get; set; }
			public int? likes { get; set; }
			public List<string> badges { get; set; }
			public List<string> important_badges { get; set; }
			public string nutrition_widget { get; set; }
			public string serving_size { get; set; }
			public int number_of_servings { get; set; }
			public int spoonacular_score { get; set; }
			public List<string> breadcrumbs { get; set; }
			public string generated_text { get; set; }
			public double? ingredientCount { get; set; }
			public List<string> images { get; set; }
		}

		// Used by REST_API.GET_FindByIngredients
		public class FindByIngredients
		{
			public long id { get; set; }
			public string title { get; set; }
			public string image { get; set; }
			public string imageType { get; set; }
			public int usedIngredientCount { get; set; }
			public int missedIngredientCount { get; set; }
			public int likes { get; set; }
		}

		// Used by REST_API.GET_AutocompleteIngredientSeach
		public class AutocompleteIngredientSearch
		{
			public string name { get; set; }
			public string image { get; set; }
		}

		// Used by REST_API.GET_AutocompleteRecipeSearch
		public class AutocompleteRecipeSearch
		{
			public long id { get; set; }
			public string title { get; set; }
		}

		// Used by REST_API.GET_FindSimilarRecipes
		public class FindSimilarRecipes
		{
			public long id { get; set; }
			public string title { get; set; }
			public int readyInMinutes { get; set; }
			public string image { get; set; }
			public List<string> imageUrls { get; set; }
		}

		public class RandomRecipes
		{
			// TODO
		}

		// Used by REST_API.RecipeInstructions
		public class RecipeInstructions
		{
			public string name { get; set; }
			public List<Step> steps { get; set; }
		}
		public class Step
		{
			public int number { get; set; }
			public string step { get; set; }
			public List<Items> ingredients { get; set; }
			public List<Items> equipment { get; set; }
		}
		public class Items
		{
			public long id { get; set; }
			public string name { get; set; }
			public string image { get; set; }
		}

		public class SummarizeRecipe
		{
			public long id { get; set; }
			public string title { get; set; }
			public string summary { get; set; }
		}
	}
}
