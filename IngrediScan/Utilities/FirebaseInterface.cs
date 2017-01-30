using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using Auth0.SDK;

using Newtonsoft.Json;

namespace Ingrediscan.Utilities
{
	public sealed class FirebaseInterface
	{
		public static async void LoginAuth0()
		{
			Globals.auth0client = new Auth0Client (
				Globals.Auth0URI,
				Globals.FirebaseAPIKey);

			Globals.user = await Globals.auth0client.LoginAsync (Forms.Context);
			Globals.userData = JsonConvert.DeserializeObject<Auth0Json> (Globals.user.Profile.ToString());

			Globals.firebaseData = await SaveAndLoad.LoadFromFirebase ();
			SaveAndLoad.LoadToIngrediscan (Globals.firebaseData);
		}
	}
}