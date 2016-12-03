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
			var auth0 = new Auth0Client (
				Globals.Auth0URI,
				Globals.FirebaseAPIKey);

			Globals.user = await auth0.LoginAsync (Forms.Context);
			Globals.userData = JsonConvert.DeserializeObject<Auth0Json> (Globals.user.Profile.ToString());

			Globals.firebaseData = await SaveAndLoad.LoadFromFirebase ();
			SaveAndLoad.LoadToIngrediscan (Globals.firebaseData);
		}
	}
}