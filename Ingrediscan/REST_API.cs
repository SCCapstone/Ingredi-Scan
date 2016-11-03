using System;
using System.Threading.Tasks;
using RestSharp;
using System.Collections.Generic;
using RestSharp.Deserializers;
using Newtonsoft.Json;

namespace Ingrediscan
{
	public class REST_API
	{
		// Base URLs
		readonly static string UPCItemURL = "https://api.upcitemdb.com/prod";
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
		public static async Task<UPCItemDBClasses.UPCJson> GET(long upc)
		{
			Console.WriteLine ("Enter GET - UPC");

			// The URL we are currently using
			string action = "/trial/lookup";

			// Create a new client with the Uri of the above static string and the action
			var client = new RestClient ();
			client.BaseUrl = new Uri (UPCItemURL + action);

			// Create a new empty request and attach the parameter of upc with param type of QueryString
			var request = new RestRequest ("");
			request.AddParameter ("upc", upc.ToString ().PadLeft (12, '0'), ParameterType.QueryString);

			// TODO Make this async
			// Create a new response from the Execution of the client with the request
			var response = client.Execute<UPCItemDBClasses.UPCJson>(request);
			// Create an instance of ItemsResponse deserialized from the response's content
			var itemResp = JsonConvert.DeserializeObject<UPCItemDBClasses.UPCJson> (response.Content);

			// TODO Remove these as these are just testing the results
			// Found in the Application Output window
			Console.WriteLine ("Response: " + itemResp);
			if (itemResp != null)
				itemResp.DisplayItemsResponse ();
			

			Console.WriteLine ("Exit GET - UPC");
			return itemResp;//TODO Do we need to return something else here?
		}

		/// <summary>
		/// Get the recipes from Spoonacular.
		/// </summary>
		/// <param name="itemName">Item name which we get from the UPC's title.</param>
		public static async Task<List<SpoonacularClasses.FindByIngredients>> GET(string itemName)
		{
			Console.WriteLine ("Enter GET - SPOONACULAR");

			// The URL we are currently using
			string action = "/recipes/findByIngredients";

			// Create a new client with the Uri of the above static string and the action
			var client = new RestClient ();
			client.BaseUrl = new Uri (SpoonacularURL + action);

			// Create a new empty request and attach the parameter of upc with param type of QueryString + 
			// Add neccessary items to header
			var request = new RestRequest ("");
			request.AddParameter ("ingredients", itemName, ParameterType.QueryString);
			request.AddHeader ("X-Mashape-Key", SpoonacularKey);
			request.AddHeader ("Accept", "application/json");

			// Create a new response from the Execution of the client with the request
			var response = client.Execute<List<SpoonacularClasses.FindByIngredients>> (request);
			// Create an instance of List<Spoonacular.FindByIngredients> deserialized from the response's content
			var spoonRecipe = JsonConvert.DeserializeObject<List<SpoonacularClasses.FindByIngredients>> (response.Content);

			// TODO Remove these as these are just testing the results
			// Found in the Application Output window
			spoonRecipe.ForEach (x => Console.WriteLine (x));

			Console.WriteLine ("Exit GET - SPOONACULAR");
			return spoonRecipe;//TODO Do we need to return something else here?
		}
	}
}
