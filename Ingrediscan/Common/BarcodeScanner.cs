using Ingrediscan.Utilities;
using System;
//using ZXing.Mobile;
using System.Threading.Tasks;
using ZXing.Net.Mobile.Forms;
using Xamarin.Forms;

namespace Ingrediscan
{
	public class BarcodeScanner
	{
		private string lastUPC { get; set; }

		/// <summary>
		/// Scans the barcode.
		/// </summary>
		/// <returns>The barcode.</returns>
		public async Task<SpoonacularClasses.FindByUPC> scanBarcode (ZXing.Result upc)
		{
			Console.WriteLine ("Scan in progress...");

			Console.WriteLine ("Scan completed! " + upc.Text);

			if (upc != null) 
			{
				Console.WriteLine ("Scanned Barcode: " + upc.Text);

				// Set our last UPC scanned from the text which we parse to a long
				lastUPC = upc.Text;
				// Grab our item json from this GET call
				var upcJson = await REST_API.GET_FindByUPC (this.lastUPC);

				if (upcJson != null) 
				{

					return upcJson;
				}
			}

			return null;
		}
	}
}
