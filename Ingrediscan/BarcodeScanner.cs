using System;
using ZXing.Mobile;

namespace Ingrediscan
{
	public class BarcodeScanner : FormTemplate
	{
		private long lastUPC = 0;

		public BarcodeScanner ()
		{
		}

		/// <summary>
		/// Scans the barcode.
		/// </summary>
		/// <returns>The barcode.</returns>
		public async System.Threading.Tasks.Task<int> scanBarcode (MobileBarcodeScanner barcodeScanner)
		{
			Console.WriteLine ("Scan in progress...");

			ZXing.Result upc = await barcodeScanner.Scan ();

			Console.WriteLine ("Scan completed!");

			if (upc != null) 
			{
				Console.WriteLine ("Scanned Barcode: " + upc.Text);
				this.setUPC (long.Parse (upc.Text));
			}

			return 0;//TODO Do we need to return something else here?
		}

		/// <summary>
		/// Sets the upc.
		/// </summary>
		/// <param name="upc">Upc.</param>
		public void setUPC(long upc)
		{
			lastUPC = upc;
		}
		/// <summary>
		/// Gets the upc.
		/// </summary>
		/// <returns>The upc.</returns>
		public long getUPC()
		{
			return lastUPC;
		}
	}
}
