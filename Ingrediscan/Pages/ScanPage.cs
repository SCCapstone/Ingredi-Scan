using System;

using Xamarin.Forms;

using ZXing.Net.Mobile.Forms;
using Android.Widget;
using System.Threading.Tasks;

namespace Ingrediscan
{
	public class ScanPage : ContentPage
	{
		public ScanPage ()
		{
			Title = "Scanner";

			Xamarin.Forms.Button scanButton = new Xamarin.Forms.Button {
				Text = "Scan",
				Font = Font.SystemFontOfSize (NamedSize.Large),
				BorderWidth = 2,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				AutomationId = "ScanButton"
			};
			scanButton.Clicked += OnScanClicked;

			Content = new StackLayout {
				Children = {
					scanButton
				}
			};
		}

		async void OnScanClicked (object sender, EventArgs e)
		{
			var scanPage = new ZXingScannerPage ();

			scanPage.OnScanResult += (result) => {
				// Stop scanning
				scanPage.IsScanning = false;

				// Pop the page and show the result
				Device.BeginInvokeOnMainThread (async () => {
					await Navigation.PopAsync ();

					BarcodeScanner barScan = new BarcodeScanner ();
					var itemResp = await barScan.scanBarcode (result);

					if (itemResp != null) 
					{
						Toast.MakeText (Forms.Context, "You just scanned " + itemResp.title + ".", ToastLength.Short).Show ();
						try {
							await Navigation.PushAsync (new ScanResultsPage (itemResp));
						}
						catch(Exception)
						{
							Toast.MakeText (Forms.Context, "An error occurred. It's possible the barcode is not a valid ingredient or not " +
										"currently in Spoonacular's database.", ToastLength.Short).Show ();
						}
					}
					else
					{
						Toast.MakeText (Forms.Context, "An error occurred. It's possible the barcode is not a valid ingredient or not " +
						                "currently in Spoonacular's database.", ToastLength.Short).Show ();
					}
				});
			};

			// Navigate to our scanner page
			await Navigation.PushAsync (scanPage);
		}
	}
}
