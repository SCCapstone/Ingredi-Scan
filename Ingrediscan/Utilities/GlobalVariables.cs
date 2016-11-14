using System;
using System.Collections.Generic;

using ZXing.Mobile;

namespace Ingrediscan
{
	public static class GlobalVariables
	{
		public static BarcodeScanner barScan = new BarcodeScanner ();
		public static MobileBarcodeScanner barcodeScanner = new MobileBarcodeScanner ();
		public static List<Recipe> CurrentRecipes;
	}
}
