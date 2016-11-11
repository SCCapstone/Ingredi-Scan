using System;
using ZXing.Mobile;

namespace Ingrediscan
{
	public class BarcodeScanner : FormTemplate
	{
		private string lastUPC = "";

		public BarcodeScanner ()
		{
		}

		/// <summary>
		/// Scans the barcode.
		/// </summary>
		/// <returns>The barcode.</returns>
		public async System.Threading.Tasks.Task<SpoonacularClasses.FindByUPC> scanBarcode (MobileBarcodeScanner barcodeScanner)
		{
			Console.WriteLine ("Scan in progress...");

			// This handles the scanning
			ZXing.Result upc = await barcodeScanner.Scan ();

			Console.WriteLine ("Scan completed!");

			if (upc != null) 
			{
				Console.WriteLine ("Scanned Barcode: " + upc.Text);

				// Set our last UPC scanned from the text which we parse to a long
				this.setUPC (upc.Text);
				// Grab our item json from this GET call
				var upcJson = await REST_API.GET_FindByUPC (this.lastUPC);

				return upcJson;
			}

			return null;//TODO Do we need to return something else here?
		}

		/// <summary>
		/// Sets the upc.
		/// </summary>
		/// <param name="upc">Upc.</param>
		public void setUPC(string upc)
		{
			lastUPC = upc;
		}
		/// <summary>
		/// Gets the upc.
		/// </summary>
		/// <returns>The upc.</returns>
		public string getUPC()
		{
			return lastUPC;
		}
	}
}
