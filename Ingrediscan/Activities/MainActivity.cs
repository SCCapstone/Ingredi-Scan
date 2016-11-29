using Android.App;
using Android.OS;

using ZXing.Mobile;

namespace Ingrediscan
{
	[Activity (Label = "Ingrediscan", MainLauncher = true, Theme = "@style/AppTheme", Icon = "@mipmap/icon")]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			//TODO Is this slowing everything down?
			MobileBarcodeScanner.Initialize (Application);

			GlobalVariables.loadRecipes ();

			Xamarin.Forms.Forms.Init (this, savedInstanceState);
			LoadApplication (new App ());
		}
	}
}

