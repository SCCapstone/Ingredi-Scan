using System;
namespace Ingrediscan
{
	public class Ingredient
	{
		private string name = "";
		private int amount = 0;
		private int upc = 0;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Ingrediscan.Ingredient"/> class.
		/// </summary>
		/// <param name="aName">A name.</param>
		/// <param name="anAmount">An amount.</param>
		/// <param name="anUPC">An upc.</param>
		public Ingredient (string aName, int anAmount, int anUPC)
		{
			this.setName (aName);
			this.setAmount (anAmount);
			this.setUPC (anUPC);
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
		/// Sets the amount.
		/// </summary>
		/// <param name="anAmount">An amount.</param>
		public void setAmount(int anAmount)
		{
			amount = anAmount;
		}
		/// <summary>
		/// Sets the upc.
		/// </summary>
		/// <param name="anUPC">An upc.</param>
		public void setUPC(int anUPC)
		{
			upc = anUPC;
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
		/// Gets the amount.
		/// </summary>
		/// <returns>The amount.</returns>
		public int getAmount()
		{
			return amount;
		}
		/// <summary>
		/// Gets the upc.
		/// </summary>
		/// <returns>The upc.</returns>
		public int getUPC ()
		{
			return upc;
		}
	}
}
