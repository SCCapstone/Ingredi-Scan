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
using Xamarin.Auth;
using Android.Content;
using Firebase.Xamarin.Database.Query;

namespace Ingrediscan.Utilities
{
	public sealed class FirebaseInterface
	{
		private static readonly FirebaseInterface _instance = new FirebaseInterface();
		private TokenGenerator _tokenGenerator;
		private FirebaseClient _client;

		private FirebaseInterface()
		{
			// If we do not already have an auth token in memory.
			if(Globals.GoogleAuthToken == "" || Globals.AuthenticatedEmail == "")
			{
				// First we check if we have a saved google authentication token.
				bool authenticated = false;
				List<Account> accounts = AccountStore.Create(Forms.Context).FindAccountsForService("Google").ToList();
				foreach(Account account in accounts)
				{
					if(account.Properties.ContainsKey("token"))
					{
						account.Properties.TryGetValue("token", out Globals.GoogleAuthToken);
						Globals.AuthenticatedEmail = account.Username;
						authenticated = true;
						InitializeClient();
						break;
					}
				}

				// If we do not, we need to authenticate the user.
				if(!authenticated)
				{
					var auth = new OAuth2Authenticator(
						Globals.ClientId,
						Globals.ClientSecret,
						"profile",
						new Uri("https://accounts.google.com/o/oauth2/auth"),
						new Uri("https://www.googleapis.com/plus/v1/people/me"),
						new Uri("https://accounts.google.com/o/oauth2/token"));

					Forms.Context.StartActivity(auth.GetUI(Forms.Context));

					auth.Completed += (sender, e) =>
					{
						if(e.IsAuthenticated)
						{
							if(e.Account.Properties.ContainsKey("token"))
							{
								e.Account.Properties.TryGetValue("token", out Globals.GoogleAuthToken);
								Globals.AuthenticatedEmail = e.Account.Username;
								authenticated = true;
							}
							AccountStore.Create(Forms.Context).Save(e.Account, "Google");
							InitializeClient();
						}
					};
				}
			}
		}

		public void InitializeClient()
		{
			_client = new FirebaseClient(Globals.FirebaseAppURI);
			Console.WriteLine("Attempting to save a recipe.");
			SaveRecipe();
		}

		public async Task SaveRecipe()
		{
			Console.WriteLine("Saving an item.1");
			var authProvider = new FirebaseAuthProvider(new FirebaseConfig("Avx3MuTyKyZzvTawHiegCFwX"));
			Console.WriteLine("Saving an item.2");
			var auth = await authProvider.SignInWithOAuthAsync(FirebaseAuthType.Google, Globals.GoogleAuthToken);
			Console.WriteLine("Saving an item.3");
			Dictionary<string, string> test = new Dictionary<string, string>();
			test.Add("test1Key", "test1Val");
			test.Add("test2Key", "test2Val");
			Console.WriteLine("Saving an item.4");
			await _client.Child(Globals.AuthenticatedEmail).WithAuth(auth.FirebaseToken).PutAsync(test);
			Console.WriteLine("Saving an item.5");
		}

		public static FirebaseInterface Instance
		{
			get
			{
				return _instance;
			}
		}
	}
}