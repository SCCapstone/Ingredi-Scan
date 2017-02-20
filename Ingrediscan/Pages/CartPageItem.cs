using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Ingrediscan
{
	public class CartPageItem
	{
		public class Recipes
		{
			public string Name { get; set; }
			public string Image { get; set; }
			public List<Ingredients> Ingredients { get; set; }
		}

		public class Ingredients
		{
			public string Name { get; set; }
			public string Image { get; set; }
			public Button CheckBox { get; set; }
		}

	}
}
