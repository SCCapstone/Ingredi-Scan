using System;
using System.Collections.Generic;

using Xamarin.Forms;
using RestSharp;
using Auth0.SDK;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;

using Ingrediscan.Utilities;

using System.Threading.Tasks;
using System.Linq;

[assembly: Dependency (typeof (Ingrediscan.SaveAndLoad))]
namespace Ingrediscan
{
	public class SaveAndLoad
	{
		public static async void SaveToFirebase (FirebaseSaveFormat fbData)
		{
			var id_token = RetrieveToken ();
			var _client = new FirebaseClient (Globals.FirebaseAppURL);

			await _client
				.Child (Globals.userData.name)
				.WithAuth (id_token)
				.PutAsync (fbData);
		}

		public static async Task<FirebaseSaveFormat> LoadFromFirebase ()
		{
			// TODO Loading not working yet
			var id_token = RetrieveToken ();

			var firebase = new FirebaseClient (Globals.FirebaseAppURL);
			var items = await firebase
				.Child (Globals.userData.name)
				.OrderByKey ()
				.WithAuth(id_token) // <-- Add Auth token if required. Auth instructions further down in readme.
				.OnceSingleAsync<FirebaseSaveFormat> ();

			try 
			{
				items.exists = true;
				foreach (KeyValuePair<string, Recipe> k in items.cart) 
				{
					foreach (Ingredient i in k.Value.ingredients) 
					{
						i.setFormattedName ();
					}
				}
			}
			catch(Exception e)
			{
				Console.WriteLine (e);
				items = InitializeFirebase ();
			}

			return items;
		}

		public static string RetrieveToken()
		{
			// The URL we are currently using
			string action = "/delegation";

			// Client creation
			var client = new RestClient ();
			client.BaseUrl = new Uri (Globals.Auth0URL);
			client.AddHandler ("application/json", new RestSharp.Deserializers.JsonDeserializer ());

			Dictionary<string, string> newJson = new Dictionary<string, string> ();

			newJson.Add ("client_id", Globals.FirebaseAPIKey);
			newJson.Add ("grant_type", Globals.GRANT_TYPE);
			newJson.Add ("id_token", Globals.user.IdToken);
			newJson.Add ("target", "");
			newJson.Add ("scope", "openid");
			newJson.Add ("api_type", "firebase");

			// Request creation
			var request = new RestRequest (action, Method.POST);
			request.AddJsonBody (newJson);

			// Response creation TODO Make async
			var response = client.Execute<Auth0Firebase> (request).Data;

			return response.id_token;
		}

		public static FirebaseSaveFormat CreateFirebaseJson (Dictionary<string, Recipe> cart, Dictionary<string, bool> saveCuisines,
												 Dictionary<string, bool> saveDiets, Dictionary<string, bool> saveIntol,
												 List<Recipe> savedRecipes, List<string> saveSearches)
		{
			FirebaseSaveFormat fbData = new FirebaseSaveFormat ();

			fbData.exists = true;
			fbData.user = Globals.userData.name;
			fbData.savedRecipes = savedRecipes;
			fbData.cart = cart;
			fbData.searchSettings = new SearchSettings {
				cuisine = saveCuisines,
				diets = saveDiets,
				intolerances = saveIntol
			};
			fbData.history = saveSearches;

			return fbData;
		}


		public static void LoadToIngrediscan(FirebaseSaveFormat items)
		{
			Settings.LoadSettings (items.searchSettings);
			SearchPage.LoadSearchHistory (items.history);
			//CartPage.LoadCart (items.cart);
			// TODO Implement FavoriteRecipes.LoadRecipes(items.savedRecipes);
		}

		public static FirebaseSaveFormat InitializeFirebase()
		{
			var cart = new Dictionary<string, Recipe> ();
			var cuisines = new Dictionary<string, bool> ();
			var diets = new Dictionary<string, bool> ();
			var intols = new Dictionary<string, bool> (); 
			var savedRecipes = new List<Recipe> ();
			var history = new List<string> ();

			var fbData = CreateFirebaseJson (cart, cuisines, diets, intols, savedRecipes, history);

			SaveToFirebase (fbData);

			return fbData;
		}
	}
}
