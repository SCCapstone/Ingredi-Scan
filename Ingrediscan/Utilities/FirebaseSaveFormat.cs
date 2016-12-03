using System;
using System.Collections.Generic;

namespace Ingrediscan
{
	public class FirebaseSaveFormat
	{
		public bool exists = false;

		public string user { get; set; }
		public List<string> history { get; set; }
		public Dictionary<string, Recipe> cart { get; set; }
		public SearchSettings searchSettings { get; set; }
		public List<Recipe> savedRecipes { get; set; }
	}

	public class SearchSettings
	{
		public Dictionary<string, bool> cuisine { get; set; }
		public Dictionary<string, bool> diets { get; set; }
		public Dictionary<string, bool> intolerances { get; set; }
	}
}
