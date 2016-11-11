using Android.App;
using Android.Widget;
using Android.OS;

using ZXing.Mobile;
using System;
using Android.Content;

namespace Ingrediscan
{
	[Activity (Label = "Ingrediscan", MainLauncher = true, Theme = "@style/AppTheme", Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		private Android.Support.V4.Widget.DrawerLayout mDrawerLayout;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			MobileBarcodeScanner barcodeScanner = new MobileBarcodeScanner ();
			MobileBarcodeScanner.Initialize (Application);
			BarcodeScanner barScan = new BarcodeScanner ();

			Button button = FindViewById<Button> (Resource.Id.navButton);

			button.Click += delegate {
				StartActivity (typeof(SettingsActivity));
				//Finish ();
			};

			// Get our button from the layout resource,
			// and attach an event to it

			Button scannerButton = FindViewById<Button> (Resource.Id.scannerButton);
			TextView upcText = FindViewById<TextView> (Resource.Id.upcText);
			TextView spoonText = FindViewById<TextView> (Resource.Id.spoonText);

			scannerButton.Click += async (sender, e) => {

				var itemResp = await barScan.scanBarcode (barcodeScanner);

				if(itemResp != null)
				{
					// TODO Maybe move this into a different class?
					// TODO Just grab what's needed from the title
					//string itemTitle = Utilities.ParseTitle (itemJson);

					// Get the item recipes from this GET call
					var recipes = await REST_API.GET_FindByIngredients (false, itemResp.title, false, 5, 1);
					upcText.Text = itemResp.title;
					spoonText.Text = recipes [0].title;
				}
			};

			// TODO Remove below tests

			Button testUPCButton = FindViewById<Button> (Resource.Id.upcButton);
			string upc = "043000955314";

			testUPCButton.Click += async (sender, e) => {
				var text = await REST_API.GET_FindByUPC (upc);

				if (text != null) {
					// TODO Maybe move this into a different class?
					// TODO Just grab what's needed from the title
					upcText.Text = text.title;

					// Get the item recipes from this GET call
					var recipes = await REST_API.GET_FindByIngredients (false, upcText.Text, false, 5, 1);

					spoonText.Text = recipes [0].title;
				}
			};



			Button testSpoonacularButton = FindViewById<Button> (Resource.Id.spoonButton);
			string itemName = "strawberries,chocolate";

			testSpoonacularButton.Click += async (sender, e) => {
				var text = await REST_API.GET_FindByIngredients (false, itemName, false, 5, 1);
				spoonText.Text = text [0].title;
			};
		}
	}
}

