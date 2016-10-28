using System;
using System.Collections.Generic;

namespace Ingrediscan
{
	public class Recipe
	{
		public List<Ingredient> ingredients = new List<Ingredient> ();
		public string name = "";
		public int id = 0;
		public int prepTime = 0;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Ingrediscan.Recipe"/> class.
		/// </summary>
		/// <param name="ingList">Ing list.</param>
		/// <param name="aName">A name.</param>
		/// <param name="anID">An identifier.</param>
		/// <param name="aPrepTime">A prep time.</param>
		public Recipe (List<Ingredient> ingList, string aName, int anID, int aPrepTime)
		{
			this.setIngredientList (ingList);
			this.setName (aName);
			this.setID (anID);
			this.setPrepTime (aPrepTime);
		}

		/// <summary>
		/// Sets the ingredient list.
		/// </summary>
		/// <param name="ingList">Ing list.</param>
		public void setIngredientList(List<Ingredient> ingList)
		{
			ingredients = ingList;
		}
		/// <summary>
		/// Sets the name.
		/// </summary>
		/// <param name="aName">A name.</param>
		public void setName(string aName)
		{
			name = aName;
		}
		/// <summary>
		/// Sets the identifier.
		/// </summary>
		/// <param name="anID">An identifier.</param>
		public void setID(int anID)
		{
			id = anID;
		}
		/// <summary>
		/// Sets the prep time.
		/// </summary>
		/// <param name="aPrepTime">A prep time.</param>
		public void setPrepTime(int aPrepTime)
		{
			prepTime = aPrepTime;
		}

		/// <summary>
		/// Gets the ingredient list.
		/// </summary>
		/// <returns>The ingredient list.</returns>
		public List<Ingredient> getIngredientList()
		{
			return ingredients;
		}
		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <returns>The name.</returns>
		public string getName()
		{
			return name;
		}
		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <returns>The identifier.</returns>
		public int getID()
		{
			return id;
		}
		/// <summary>
		/// Gets the prep time.
		/// </summary>
		/// <returns>The prep time.</returns>
		public int getPrepTime()
		{
			return prepTime;
		}
	}
}
