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
			public List<RecipeIngredients> Ingredients { get; set; }
		}

		public class RecipeIngredients
		{
			public string Name { get; set; }
			public string RecipeName { get; set; }
			public string Image { get; set; }
			public Button CheckBox { get; set; }
			public string CheckBoxName { get; set; }
		}

	}
}
