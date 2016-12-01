using System;

using Xamarin.Forms;
using ZXing.Mobile;

using Ingrediscan.Utilities;

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

			if (itemResp != null) 
			{
				//await DisplayAlert ("Successful Scan", "Food Item: " + itemResp.title, "OK");
				await Navigation.PushAsync (new ScanResultsPage (itemResp));
			}
			else
			{
				//await DisplayAlert ("Failed Scan", "The barcode, \"" + barScan.getUPC() + "\" was not recognized.", "OK");
			}
		}
	}
}
