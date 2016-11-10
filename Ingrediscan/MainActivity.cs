using Android.App;
using Android.Widget;
using Android.OS;

using ZXing.Mobile;
using System;

namespace Ingrediscan
{
	[Activity (Label = "Ingrediscan", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			MobileBarcodeScanner barcodeScanner = new MobileBarcodeScanner ();
			MobileBarcodeScanner.Initialize (Application);
			BarcodeScanner barScan = new BarcodeScanner ();

			Button button = FindViewById<Button> (Resource.Id.navButton);
			
			Button testButton = null; 

			FormTemplate newForm = new FormTemplate ();

			Xamarin.Forms.StackLayout newLayout = new Xamarin.Forms.StackLayout {
				Spacing = 20, Padding = 50,
				VerticalOptions = Xamarin.Forms.LayoutOptions.Center,
				Children = {
					new Xamarin.Forms.Entry { Placeholder = "Username" },
					new Xamarin.Forms.Entry { Placeholder = "Password", IsPassword = true },
					new Xamarin.Forms.Button {
						Text = "Login",
						TextColor = Xamarin.Forms.Color.White,
						BackgroundColor = Xamarin.Forms.Color.Green },
					new Xamarin.Forms.Button {
						Text = "Sign Up",
						TextColor = Xamarin.Forms.Color.White,
						BackgroundColor = Xamarin.Forms.Color.Red },
				}
			};
			Xamarin.Forms.Page newPage = newForm.createPage ("Test", "N/A", newLayout);

			button.Click += async (sender, e) => {

				await newForm.NavigateToPage (newPage);

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

