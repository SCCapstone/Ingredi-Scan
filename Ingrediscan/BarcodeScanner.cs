using System;
using ZXing.Mobile;


namespace Ingrediscan
{
	public class BarcodeScanner : FormTemplate
	{
		private int lastUPC = 0;

		public BarcodeScanner ()
		{
		}

		/// <summary>
		/// Scans the barcode.
		/// </summary>
		/// <returns>The barcode.</returns>
		//TODO Use ZebraXing to do this part
		public async System.Threading.Tasks.Task<int> scanBarcode (MobileBarcodeScanner barcodeScanner)
		{
			var upc = await barcodeScanner.Scan ();

			if (upc != null) 
			{
				Console.WriteLine ("Scanned Barcode: " + upc.Text);
				this.setUPC (int.Parse (upc.Text));
			}

			return 0;//TODO Do we need to return something else here?
		}

		/// <summary>
		/// Sets the upc.
		/// </summary>
		/// <param name="upc">Upc.</param>
		public void setUPC(int upc)
		{
			lastUPC = upc;
		}
		/// <summary>
		/// Gets the upc.
		/// </summary>
		/// <returns>The upc.</returns>
		public int getUPC()
		{
			return lastUPC;
		}
	}
}
