using System;
using System.Collections.Generic;

namespace Ingrediscan
{
	public class UPCItemDBClasses
	{
		public class UPCJson
		{
			public string code { get; set; }
			public int total { get; set; }
			public int offset { get; set; }
			public List<Item> items { get; set; } // Nested class of UPCItemDBClasses - I.E. Below

			public void DisplayItemsResponse ()
			{
				Console.WriteLine ("Display Items Response");
				Console.WriteLine (code);
				Console.WriteLine (total);
				Console.WriteLine (offset);
				for (int i = 0; i < items.Count; ++i)
					items [i].DisplayItems ();
			}
		}

		public class Item
		{
			public string ean { get; set; }
			public string title { get; set; }
			public string upc { get; set; }
			public string gtin { get; set; }
			public string description { get; set; }
			public string brand { get; set; }
			public string model { get; set; }
			public string color { get; set; }
			public string size { get; set; }
			public string dimension { get; set; }
			public string weight { get; set; }
			public string currency { get; set; }
			public double lowest_recorded_price { get; set; }
			public string [] images { get; set; }
			public List<Offers> offers { get; set; } // Nested class of UPCItemDBClasses - I.E. Below

			public void DisplayItems ()
			{
				Console.WriteLine (ean);
				Console.WriteLine (title);
				Console.WriteLine (upc);
				Console.WriteLine (gtin);
				Console.WriteLine (description);
				Console.WriteLine (brand);
				Console.WriteLine (model);
				Console.WriteLine (color);
				Console.WriteLine (size);
				Console.WriteLine (dimension);
				Console.WriteLine (weight);
				Console.WriteLine (currency);
				Console.WriteLine (lowest_recorded_price);
				for (int i = 0; i < images.Length; ++i) {
					Console.WriteLine (images [i]);
				}

				for (int i = 0; i < offers.Count; ++i)
					offers [i].DisplayOffers ();
			}

		}

		public class Offers
		{
			public string merchant { get; set; }
			public string domain { get; set; }
			public string title { get; set; }
			public string currency { get; set; }
			public string list_price { get; set; } // TODO Actually double, but creates an error since offers is sometimes null
			public string price { get; set; } // TODO Actually double, but creates an error since offers is sometimes null
			public string shipping { get; set; }
			public string condition { get; set; }
			public string availability { get; set; }
			public string link { get; set; }
			public string updated_t { get; set; }

			public void DisplayOffers ()
			{
				Console.WriteLine (merchant);
				Console.WriteLine (domain);
				Console.WriteLine (title);
				Console.WriteLine (currency);
				Console.WriteLine (list_price);
				Console.WriteLine (price);
				Console.WriteLine (shipping);
				Console.WriteLine (condition);
				Console.WriteLine (availability);
				Console.WriteLine (link);
				Console.WriteLine (updated_t);
			}
		}
	}
}
