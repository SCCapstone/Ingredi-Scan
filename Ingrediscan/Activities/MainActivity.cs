using Android.App;
using Android.Content.PM;
using Android.OS;

using Ingrediscan.Utilities;

namespace Ingrediscan
{
	[Activity (Label = "Ingrediscan", MainLauncher = true, Theme = "@style/AppTheme", Icon = "@mipmap/icon")]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			ZXing.Net.Mobile.Forms.Android.Platform.Init ();
			ZXing.Mobile.MobileBarcodeScanner.Initialize (Application);

			Xamarin.Forms.Forms.Init (this, savedInstanceState);
			LoadApplication (new App ());

			FirebaseInterface.LoginAuth0 ();
		}

		public override void OnRequestPermissionsResult (int requestCode, string [] permissions, Permission [] grantResults)
		{
			global::ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult (requestCode, permissions, grantResults);
		}
	}
}

