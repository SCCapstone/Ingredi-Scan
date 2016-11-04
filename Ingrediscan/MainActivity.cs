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

				await barScan.scanBarcode (barcodeScanner);

			};

			// TODO Remove below tests

			Button testUPCButton = FindViewById<Button> (Resource.Id.upcButton);
			string upc = "1230001103410";

			testUPCButton.Click += async (sender, e) => {
				var text = await REST_API.GET_UPC (upc);
				upcText.Text = text.items [0].title;
			};



			Button testSpoonacularButton = FindViewById<Button> (Resource.Id.spoonButton);
			string itemName = "strawberries";

			testSpoonacularButton.Click += async (sender, e) => {
				var text = await REST_API.GET_SpoonacularRecipe (itemName);
				spoonText.Text = text [0].title;
			};
		}
	}
}

