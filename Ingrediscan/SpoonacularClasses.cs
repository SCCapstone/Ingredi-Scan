using System;
namespace Ingrediscan
{
	public class SpoonacularClasses
	{
		public class FindByIngredients
		{
			public int id { get; set; }
			public string title { get; set; }
			public string image { get; set; }
			public int usedIngredientCount { get; set; }
			public int missedIngredientCount { get; set; }
			public int likes { get; set; }
		}

	}
}
