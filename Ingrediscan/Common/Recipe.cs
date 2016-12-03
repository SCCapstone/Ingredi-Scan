using System;
using System.Collections.Generic;
using Ingrediscan.Utilities;

namespace Ingrediscan
{
	public class Recipe
	{
		public List<Ingredient> ingredients { get; set; }
		public string name { get; set; }
		public string id { get; set; }
		public string image { get; set; }
		public int prepTime { get; set; }
		public int cookTime { get; set; }
		//public bool isFavorite { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Ingrediscan.Recipe"/> class.
		/// </summary>
		/// <param name="ingList">Ing list.</param>
		/// <param name="aName">A name.</param>
		/// <param name="anID">An identifier.</param>
		/// <param name="aPrepTime">A prep time.</param>
		public Recipe (List<Ingredient> ingList, string aName, string anID, string anImage, int aPrepTime, int aCookTime)//, bool isFave)
		{
			ingredients = ingList;
			name = aName;
			id = anID;
			image = anImage;
			prepTime = aPrepTime;
			cookTime = aCookTime;
			//isFavorite = isFave;
		}

		public void addToCart()
		{
			//GlobalVariables.CurrentRecipes.Add (this);
			Globals.firebaseData.cart.Add (this.name, this);
			SaveAndLoad.SaveToFirebase (Globals.firebaseData);
		}
	}
}
