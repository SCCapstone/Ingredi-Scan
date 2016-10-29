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

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);

			button.Click += delegate { button.Text = string.Format ("{0} clicks!", count++); };

			Button scannerButton = FindViewById<Button> (Resource.Id.scannerButton);

			scannerButton.Click += async (sender, e) => {

				await barScan.scanBarcode (barcodeScanner);

			};
		}
	}
}

