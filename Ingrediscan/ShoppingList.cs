using System;
using System.Collections.Generic;

namespace Ingrediscan
{
	public class ShoppingList : FormTemplate
	{
		public List<Recipe> recipes = new List<Recipe> ();
		public List<Ingredient> ingredients = new List<Ingredient> ();
		public Dictionary<Recipe, bool> dictRecipes = new Dictionary<Recipe, bool> ();
		public Dictionary<Ingredient, bool> dictIngredients = new Dictionary<Ingredient, bool> ();

		public ShoppingList ()
		{
		}

		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <param name="anIng">An ing.</param>
		public void addItem(Ingredient anIng)
		{
			ingredients.Add (anIng);
			dictIngredients.Add (anIng, false);
		}

		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <param name="aRec">A rec.</param>
		public void addItem(Recipe aRec)
		{
			recipes.Add (aRec);
			dictRecipes.Add (aRec, false);
		}

		/// <summary>
		/// Removes the item.
		/// </summary>
		/// <param name="anIng">An ing.</param>
		public void removeItem(Ingredient anIng)
		{
			if (ingredients.Contains (anIng)) 
			{
				ingredients.Remove (anIng);
			}
		}

		/// <summary>
		/// Removes the item.
		/// </summary>
		/// <param name="aRec">A rec.</param>
		public void removeItem(Recipe aRec)
		{
			if (recipes.Contains (aRec)) 
			{
				recipes.Remove (aRec);
			}
		}

		/// <summary>
		/// Checks the item.
		/// </summary>
		/// <param name="anIng">An ing.</param>
		public void checkItem(Ingredient anIng)
		{
			if(dictIngredients.ContainsKey(anIng))
			{
				dictIngredients [anIng] = !dictIngredients [anIng];
			}
		}

		/// <summary>
		/// Checks the item.
		/// </summary>
		/// <param name="aRec">A rec.</param>
		public void checkItem (Recipe aRec)
		{
			if (dictRecipes.ContainsKey (aRec)) 
			{
				dictRecipes [aRec] = !dictRecipes [aRec];
			}
		}
	}
}
