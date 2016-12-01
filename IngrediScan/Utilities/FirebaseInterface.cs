using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Firebase.Xamarin;

using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

using Firebase.Xamarin.Database;
using Firebase.Xamarin.Token;
using Firebase.Xamarin.Auth;

using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Firebase.Xamarin.Database.Query;
using Newtonsoft.Json;
using Auth0.SDK;

using RestSharp;

namespace Ingrediscan.Utilities
{
	public sealed class FirebaseInterface
	{
		private FirebaseInterface ()
		{
		}

		public static async void LoginAuth0()
		{
			var auth0 = new Auth0Client (
				"ingrediscan.auth0.com",
				"amWUGnPk4AnZT0BSDrbzmywxQruqG57W");

			var user = await auth0.LoginAsync (Forms.Context);

			Globals.user = user;
			Globals.userData = JsonConvert.DeserializeObject<Auth0Json> (user.Profile.ToString ());

			//TODO Remove this/ take it to another method

			// The URL we are currently using
			string action = "/delegation";

			// Client creation
			var client = new RestClient ();
			client.BaseUrl = new Uri ("https://ingrediscan.auth0.com");
			client.AddHandler ("application/json", new RestSharp.Deserializers.JsonDeserializer ());

			Dictionary<string, string> newJson = new Dictionary<string, string> ();

			newJson.Add ("client_id", "amWUGnPk4AnZT0BSDrbzmywxQruqG57W");
			newJson.Add ("grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer");
			newJson.Add ("id_token", user.IdToken);
			newJson.Add ("target", "");
			newJson.Add ("scope", "openid");
			newJson.Add ("api_type", "firebase");

			// Request creation
			var request = new RestRequest (action, Method.POST);
			request.AddJsonBody (newJson);

			// Response creation TODO Make async
			var response = client.Execute<Auth0Firebase> (request).Data;

			var _client = new FirebaseClient ("https://ingrediscan-151115.firebaseio.com/");
			Dictionary<string, string> test = new Dictionary<string, string> ();
			test.Add ("test1Key", "test1Val");
			test.Add ("test2Key", "test2Val");
			Console.WriteLine (response.id_token);
			await _client.Child ("test").WithAuth (response.id_token).PutAsync (test);

		}
	}
}