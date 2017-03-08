using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Auth0.SDK;

namespace Ingrediscan.Utilities
{
	public class Auth0ClientLocal : Auth0Client
	{
		public Auth0ClientLocal(string domain, string clientId) : base(domain, clientId)
		{
		}

		public async Task<Auth0User> TryLoginExisting(Context context, Auth0User user)
		{
			try
			{
				Dictionary<string, string> user_dict = new Dictionary<string, string>();
				user_dict["access_token"] = user.Auth0AccessToken;
				user_dict["id_token"] = user.IdToken;
				user_dict["profile"] = user.Profile.ToString();
				user_dict["refresh_token"] = user.RefreshToken;
				SetupCurrentUser(user_dict);
				return CurrentUser;
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.InnerException);
				return await LoginAsync(context);
			}
		}
	}
}