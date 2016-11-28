using System;

using Xamarin.Forms;
using ZXing.Mobile;

namespace Ingrediscan
{
	public class ScanPage : ContentPage
	{
		public ScanPage ()
		{
			Title = "Scan Page";

			Button scanButton = new Button {
				Text = "Scan",
				Font = Font.SystemFontOfSize (NamedSize.Large),
				BorderWidth = 2,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
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
			BarcodeScanner barScan = new BarcodeScanner ();
			MobileBarcodeScanner barcodeScanner = new MobileBarcodeScanner ();
			var itemResp = await barScan.scanBarcode (barcodeScanner);

			if (itemResp != null) {
				// TODO Maybe move this into a different class?
				// TODO Just grab what's needed from the title
				//string itemTitle = Utilities.ParseTitle (itemJson);

				// Get the item recipes from this GET call
				//var recipes = await REST_API.GET_FindByIngredients (itemResp.title);//(false, itemResp.title, false, 5, 1);
				await DisplayAlert ("Successful Scan", "Food Item: " + itemResp.title, "OK");
				await Navigation.PushAsync (new ScanResultsPage (itemResp));
			}
			else
			{
				await DisplayAlert ("Failed Scan", "The barcode, \"" + barScan.getUPC() + "\" was not recognized.", "OK");
				// TODO Test code
				var upcJson = await REST_API.GET_FindByUPC ("041631000564");
				await Navigation.PushAsync (new ScanResultsPage (upcJson));
			}
		}
	}
}
