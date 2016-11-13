using System;

using Xamarin.Forms;

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
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.StartAndExpand
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
			var itemResp = await GlobalVariables.barScan.scanBarcode (GlobalVariables.barcodeScanner);

			if (itemResp != null) {
				// TODO Maybe move this into a different class?
				// TODO Just grab what's needed from the title
				//string itemTitle = Utilities.ParseTitle (itemJson);

				// Get the item recipes from this GET call
				var recipes = await REST_API.GET_FindByIngredients (false, itemResp.title, false, 5, 1);

			}
		}
	}
}
