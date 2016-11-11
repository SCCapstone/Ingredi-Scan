using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace Ingrediscan
{
	public class REST_API
	{
		// Base URLs
		//readonly static string UPCItemURL = "https://api.upcitemdb.com/prod";
		readonly static string SpoonacularURL = "https://spoonacular-recipe-food-nutrition-v1.p.mashape.com";

		// KEYS
		readonly static string SpoonacularKey = "BpCpihtIUZmshgWQYLdikVI5LIpMp1c2OCPjsn90PEmdR1oKcK";

		//static RestClient client = new RestClient ();
		//public UPCItemAPI (string url)
		//{
		//	client.BaseUrl = new Uri(url);
		//}

		/// <summary>
		/// Get the specified upc from the upcitemdb database.
		/// </summary>
		/// <param name="upc">Upc retrieved from the Scanner.</param>
		public static async Task<SpoonacularClasses.FindByUPC> GET_FindByUPC(string upc) // TODO Add await
		{
			Console.WriteLine ("Enter GET - UPC");

			// The URL we are currently using
			string action = "/food/products/upc/{upc}";

			// Client creation
			var client = new RestClient ();
			client.BaseUrl = new Uri (SpoonacularURL);
			client.AddHandler ("application/json", new RestSharp.Deserializers.JsonDeserializer ());

			// Request creation
			var request = new RestRequest (action);
			request.AddHeader ("X-Mashape-Key", SpoonacularKey);
			request.AddHeader ("Accept", "application/json");
			request.AddUrlSegment ("upc", upc);
			request.RequestFormat = DataFormat.Json;

			// Generate a response TODO Make async
			var response = client.Execute<SpoonacularClasses.FindByUPC> (request).Data;

			Console.WriteLine ("RESPONSE: " + response.title);
			//var itemResp = JsonConvert.DeserializeObject<SpoonacularClasses.FindByUPC> (response.Body);
			Console.WriteLine ("Exit GET - UPC");
			return response;
		}


		/// <summary>
		/// Get the recipes from Spoonacular.
		/// </summary>
		/// <param name="itemName">Item name which we get from the UPC's title.</param>
		public static async Task<List<SpoonacularClasses.FindByIngredients>> 
		                                                GET_FindByIngredients(bool fillIngredients, string ingredients,
		                                                                     bool limitLicense, int number, int ranking) // TODO Add await
		{
			Console.WriteLine ("Enter GET - SPOONACULAR");

			// The URL we are currently using
			string action = "/recipes/findByIngredients";

			// Client creation
			var client = new RestClient ();
			client.BaseUrl = new Uri (SpoonacularURL);
			client.AddHandler ("application/json", new RestSharp.Deserializers.JsonDeserializer ());

			// Request creation
			var request = new RestRequest (action);
			request.AddHeader ("X-Mashape-Key", SpoonacularKey);
			request.AddHeader ("Accept", "application/json");
			request.AddQueryParameter ("fillIngredients", fillIngredients.ToString());
			request.AddQueryParameter ("ingredients", ingredients);
			request.AddQueryParameter ("limitLicense", limitLicense.ToString ());
			request.AddQueryParameter ("number", number.ToString());
			request.AddQueryParameter ("ranking", ranking.ToString ());
			request.RequestFormat = DataFormat.Json;

			// Generate a response TODO Make async
			var response = client.Execute<List<SpoonacularClasses.FindByIngredients>> (request).Data;

			Console.WriteLine (client.BaseUrl.ToString () + client.BuildUri (request).ToString());

			Console.WriteLine ("RESPONSE: ");
			response.ForEach(x => Console.WriteLine(x.title));

			Console.WriteLine ("Exit GET - SPOONACULAR");
			return response;//TODO Do we need to return something else here?
		}

		public static async Task<List<SpoonacularClasses.AutocompleteIngredientSearch>> 
		                                                GET_AutocompleteIngredientSeach(bool metaInfo, int number, string query)
		{
			Console.WriteLine ("Enter GET - AUTOCOMPLETE INGREDIENT SEARCH");

			if (number >= 1 && number <= 100) 
			{
				string action = "/food/ingredients/autocomplete";

				// Client creation
				var client = new RestClient ();
				client.BaseUrl = new Uri (SpoonacularURL);
				client.AddHandler ("application/json", new RestSharp.Deserializers.JsonDeserializer ());

				// Request creation
				var request = new RestRequest (action);
				request.AddHeader ("X-Mashape-Key", SpoonacularKey);
				request.AddHeader ("Accept", "application/json");
				request.AddQueryParameter ("metaInformation", metaInfo.ToString ());
				request.AddQueryParameter ("number", number.ToString ());
				request.AddQueryParameter ("query", query);
				request.RequestFormat = DataFormat.Json;

				// Generate a response TODO Make async
				var response = client.Execute<List<SpoonacularClasses.AutocompleteIngredientSearch>> (request).Data;

				Console.WriteLine ("RESPONSE: ");
				response.ForEach (x => Console.WriteLine (x.name));
				Console.WriteLine ("Exit GET - AUTOCOMPLETE INGREDIENT SEARCH");

				return response;
			}
			else
			{
				Console.WriteLine ("ERROR: Number is less than 0 or greater than 100");
				return null; // TODO Show an error message, but we should be able to do this ourselves wherein we won't need this check
			}
		}

		public static async Task<List<SpoonacularClasses.AutocompleteRecipeSearch>>
		                                                GET_AutocompleteRecipeSearch(int number, string query)
		{
			Console.WriteLine ("Enter GET - AUTOCOMPLETE RECIPE SEARCH");

			if(number >= 1 && number <= 25)
			{
				string action = "/recipes/autocomplete";

				// Client creation
				var client = new RestClient ();
				client.BaseUrl = new Uri (SpoonacularURL);
				client.AddHandler ("application/json", new RestSharp.Deserializers.JsonDeserializer ());

				// Request creation
				var request = new RestRequest (action);
				request.AddHeader ("X-Mashape-Key", SpoonacularKey);
				request.AddHeader ("Accept", "application/json");
				request.AddQueryParameter ("number", number.ToString ());
				request.AddQueryParameter ("query", query);
				request.RequestFormat = DataFormat.Json;

				// Generate a response TODO Make async
				var response = client.Execute<List<SpoonacularClasses.AutocompleteRecipeSearch>> (request).Data;

				Console.WriteLine ("RESPONSE: ");
				response.ForEach (x => Console.WriteLine (x.title));

				Console.WriteLine ("Exit GET - AUTOCOMPLETE RECIPE SEARCH");

				return response;
			}
			else
			{
				Console.WriteLine ("ERROR: Number is less than 0 or greater than 25");
				return null; // TODO Show an error message, but we should be able to do this ourselves wherein we won't need this check
			}
		}

		public static async Task<List<SpoonacularClasses.FindSimilarRecipes>> GET_FindSimilarRecipes(long id)
		{
			Console.WriteLine ("Enter GET - FIND SIMILAR RECIPES");

			string action = "/recipes/{id}/similar";

			// Client creation
			var client = new RestClient ();
			client.BaseUrl = new Uri (SpoonacularURL);
			client.AddHandler ("application/json", new RestSharp.Deserializers.JsonDeserializer ());

			// Request creation
			var request = new RestRequest (action);
			request.AddHeader ("X-Mashape-Key", SpoonacularKey);
			request.AddHeader ("Accept", "application/json");
			request.AddUrlSegment ("id", id.ToString());
			request.RequestFormat = DataFormat.Json;

			// Generate a response TODO Make async
			var response = client.Execute<List<SpoonacularClasses.FindSimilarRecipes>> (request).Data;

			Console.WriteLine ("RESPONSE: ");
			response.ForEach (x => Console.WriteLine (x.title));

			Console.WriteLine ("Exit GET - FIND SIMILAR RECIPES");

			return response;
		}

		public static async Task<List<SpoonacularClasses.RecipeInstructions>> 
		                                                GET_RecipeInstructions(string id, bool stepBreakdown)
		{
			Console.WriteLine ("Enter GET - RECIPE INSTRUCTIONS");

			string action = "/recipes/{id}/analyzedInstructions";

			// Client creation
			var client = new RestClient ();
			client.BaseUrl = new Uri (SpoonacularURL);
			client.AddHandler ("application/json", new RestSharp.Deserializers.JsonDeserializer ());

			// Request creation
			var request = new RestRequest (action);
			request.AddHeader ("X-Mashape-Key", SpoonacularKey);
			request.AddHeader ("Accept", "application/json");
			request.AddUrlSegment ("id", id.ToString ());
			request.AddQueryParameter ("stepBreakdown", stepBreakdown.ToString());
			request.RequestFormat = DataFormat.Json;

			// Generate a response TODO Make async
			var response = client.Execute<List<SpoonacularClasses.RecipeInstructions>> (request).Data;

			Console.WriteLine ("RESPONSE: ");
			response.ForEach (x => Console.WriteLine (x.name));

			Console.WriteLine ("Exit GET - RECIPE INSTRUCTIONS");

			return response;
		}

		public static async Task<SpoonacularClasses.SummarizeRecipe> GET_SummarizeRecipe (long id)
		{
			Console.WriteLine ("Enter GET - SUMMARIZE RECIPE");

			string action = "/recipes/{id}/summary";

			// Client creation
			var client = new RestClient ();
			client.BaseUrl = new Uri (SpoonacularURL);
			client.AddHandler ("application/json", new RestSharp.Deserializers.JsonDeserializer ());

			// Request creation
			var request = new RestRequest (action);
			request.AddHeader ("X-Mashape-Key", SpoonacularKey);
			request.AddHeader ("Accept", "application/json");
			request.AddUrlSegment ("id", id.ToString ());
			request.RequestFormat = DataFormat.Json;

			// Generate a response TODO Make async
			var response = client.Execute<SpoonacularClasses.SummarizeRecipe> (request).Data;

			Console.WriteLine ("RESPONSE: " + response.title);

			Console.WriteLine ("Exit GET - SUMMARIZE RECIPE");
			return response;
		}
	}
}
