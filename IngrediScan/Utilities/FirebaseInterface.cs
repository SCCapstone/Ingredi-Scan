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
			Globals.auth0client = new Auth0ClientLocal (
				Globals.Auth0URI,
				Globals.FirebaseAPIKey);

			if(Application.Current.Properties.ContainsKey("login") && (string)Application.Current.Properties["login"] == "true")
			{
				Auth0User user = new Auth0User();
				user.Auth0AccessToken = (string)Application.Current.Properties["user.auth0accesstoken"];
				user.Profile = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>((string)Application.Current.Properties["user.profile"]);
				user.IdToken = (string)Application.Current.Properties["user.idtoken"];
				user.RefreshToken = (string)Application.Current.Properties["user.refreshtoken"];
				Globals.user = await Globals.auth0client.TryLoginExisting(Forms.Context, user);
			}
			else
			{
				Globals.user = await Globals.auth0client.LoginAsync(Forms.Context);
				Application.Current.Properties["login"] = "true";
				Application.Current.Properties["user.auth0accesstoken"] = Globals.user.Auth0AccessToken;
				Application.Current.Properties["user.profile"] = Globals.user.Profile.ToString();
				Application.Current.Properties["user.idtoken"] = Globals.user.IdToken;
				Application.Current.Properties["user.refreshtoken"] = Globals.user.RefreshToken;
			}

			Globals.userData = JsonConvert.DeserializeObject<Auth0Json> (Globals.user.Profile.ToString());

			Globals.firebaseData = await SaveAndLoad.LoadFromFirebase ();
			SaveAndLoad.LoadToIngrediscan (Globals.firebaseData);
		}
	}
}