using System;
using System.Collections.Generic;

namespace Ingrediscan
{
	public class Recipe
	{
		private List<Ingredient> ingredients = new List<Ingredient> ();
		private string name = "";
		private string id = "";
		private string image = "";
		private int prepTime = 0;
		private int cookTime = 0;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Ingrediscan.Recipe"/> class.
		/// </summary>
		/// <param name="ingList">Ing list.</param>
		/// <param name="aName">A name.</param>
		/// <param name="anID">An identifier.</param>
		/// <param name="aPrepTime">A prep time.</param>
		public Recipe (List<Ingredient> ingList, string aName, string anID, string anImage, int aPrepTime, int aCookTime)
		{
			this.setIngredientList (ingList);
			this.setName (aName);
			this.setID (anID);
			this.setPrepTime (aPrepTime);
			this.setCookTime (aCookTime);
			this.setImage (anImage);
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
		/// Adds to ingredient list.
		/// </summary>
		/// <param name="item">Item.</param>
		public void addToIngredientList(Ingredient item)
		{
			ingredients.Add (item);
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
		public void setID(string anID)
		{
			id = anID;
		}
		/// <summary>
		/// Sets the image.
		/// </summary>
		/// <param name="anImage">An image.</param>
		public void setImage(string anImage)
		{
			image = anImage;
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
		/// Sets the cook time.
		/// </summary>
		/// <param name="aCookTime">A cook time.</param>
		public void setCookTime(int aCookTime)
		{
			cookTime = aCookTime;
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
		public string getID()
		{
			return id;
		}
		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <returns>The image.</returns>
		public string getImage()
		{
			return image;
		}
		/// <summary>
		/// Gets the prep time.
		/// </summary>
		/// <returns>The prep time.</returns>
		public int getPrepTime()
		{
			return prepTime;
		}

		/// <summary>
		/// Gets the cook time.
		/// </summary>
		/// <returns>The cook time.</returns>
		public int getCookTime()
		{
			return cookTime;
		}

		public void addToCart()
		{
			GlobalVariables.CurrentRecipes.Add (this);
		}
	}
}
