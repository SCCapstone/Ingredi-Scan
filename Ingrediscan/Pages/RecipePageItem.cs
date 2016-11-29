using System;
namespace Ingrediscan
{
	public class RecipePageItem
	{
		public class RecipePageIngredients
		{
			public string Name { get; set; }
			public string Image { get; set; }
		}

		public class RecipePageStep
		{
			//public string Number { get; set; }
			public string Step { get; set; }
		}
	}
}
