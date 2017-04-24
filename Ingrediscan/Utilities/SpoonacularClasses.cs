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
			public string id { get; set; }
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

		// TODO Use RecipeInformation instead - Much more information
		// TODO But we can't because the steps are what we need from here.
		// RecipeInformation has Steps, but they are not in a particular format
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

		public class SearchRecipes
		{
			public List<Results> results { get; set; }
			public string baseUri { get; set; }
			public int offset { get; set; }
			public int number { get; set; }
			public long processingTimeMs { get; set; }
			public string expires { get; set; }
			public bool isStale { get; set; }
		}
		public class Results
		{
			public string id { get; set; }
			public string title { get; set; }
			public int readInMinutes { get; set; }
			public string image { get; set; }
			public List<string> imageUrls { get; set; }
		}

		public class RecipeInformation
		{
			public bool vegetarian { get; set; }
			public bool vegan { get; set; }
			public bool glutenFree { get; set; }
			public bool dairyFree { get; set; }
			public bool veryHealthy { get; set; }
			public bool cheap { get; set; }
			public bool veryPopular { get; set; }
			public bool sustainable { get; set; }
			public int weightWatcherSmartPoints { get; set; }
			public string gaps { get; set; }
			public bool lowFodmap { get; set; }
			public bool ketogenic { get; set; }
			public bool whole30 { get; set; }
			public int servings { get; set; }
			public int preparationMinutes { get; set; }
			public int cookingMinutes { get; set; }
			public string sourceUrl { get; set; }
			public string spoonacularSourceUrl { get; set; }
			public int aggregateLikes { get; set; }
			public int spoonacularScore { get; set; }
			public string creditText { get; set; }
			public string sourceName { get; set; }
			public List<Ingredients> extendedIngredients { get; set; }
			public string id { get; set; }
			public string title { get; set; }
			public int readyInMinutes { get; set; }
			public string image { get; set; }
			public string imageType { get; set; }
			public List<string> cuisines { get; set; }
			public string instructions { get; set; }
		}
		public class Ingredients
		{
			public string id { get; set; }
			public string aisle { get; set; }
			public string image { get; set; }
			public string name { get; set; }
			public double amount { get; set; }
			public string unit { get; set; }
			public string unitShort { get; set; }
			public string unitLong { get; set; }
			public string originalString { get; set; }
			public List<string> metaInformation { get; set; }
		}
        public class RandomJoke
        {
            public string text { get; set; }
        }
        public class RandomTrivia
        {
            public string text { get; set;  }
        }
	}
}
