using System.Threading.Tasks;
using Auth0.SDK;

namespace Ingrediscan.Utilities
{
	public static class Globals
	{
		// This is the client ID given from the google developer console for the Ingredi-Scan-Test application.
		public const string ClientId = "274250351924-bnlu3binpqegle353vpvp8i0k7pn4dsc.apps.googleusercontent.com";
		// This is the client secret given from the google developer console for the Ingredi-Scan-Test application.
		public const string ClientSecret = "2zmzMcgNEQziSF0bFWuuplpO";

		public const string FirebaseAppURI = "ingrediscan-151115.firebaseio.com";

		public static string GoogleAuthToken = "";
		public static string AuthenticatedEmail = "";

		public static Auth0User user;
		public static Auth0Json userData;
	}
}