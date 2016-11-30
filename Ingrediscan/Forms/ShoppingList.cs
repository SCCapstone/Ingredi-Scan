using System;
using System.Collections.Generic;

namespace Ingrediscan
{
	public class ShoppingList
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

		/// <summary>
		/// Returns a list of all the ingredients, including those in the recipe.
		/// </summary>
		/// <remarks>
		/// This is, of course, subject to change. If we want to add ingredients to
		/// the ingredients list at the time the user adds the recipe to their recipe list
		/// that is an option as well. However, we should still try to keep track of the
		/// recipes that are saved on the list to allow the user to remove entire recipes
		/// their shopping list.
		/// </remarks>
		/// <returns>A list of ingredients that contains 'ingredients' as well as all the
		/// ingredients from every recipe in 'recipes'.
		/// </returns>
		public List<Ingredient> getAllIngredients()
		{
			List<Ingredient> allIngredients = ingredients;
			foreach(Recipe r in recipes)
			{
				allIngredients.AddRange(r.getIngredientList());
			}
			return allIngredients;
		}
	}
}
