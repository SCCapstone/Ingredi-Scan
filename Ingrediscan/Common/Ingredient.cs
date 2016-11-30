using System;
namespace Ingrediscan
{
	public class Ingredient
	{
		private string name = "";
		private int amount = 0;
		//private int upc = 0;
		private string units = "";
		private string id = "";
		private string image = "";
		private string formattedName = "";
		private bool itemSwitch = false;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Ingrediscan.Ingredient"/> class.
		/// </summary>
		/// <param name="aName">A name.</param>
		/// <param name="anAmount">An amount.</param>
		/// <param name="anUPC">An upc.</param>
		public Ingredient (string aName, int anAmount, /*int anUPC,*/ string aUnit, string anID, string anImage, bool aSwitch)
		{
			this.setName (aName);
			this.setAmount (anAmount);
			//this.setUPC (anUPC);
			this.setUnit (aUnit);
			this.setID (anID);
			this.setImage (anImage);
			this.setFormattedName ();
			this.setSwitch (aSwitch);
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

		/*/// <summary>
		/// Sets the upc.
		/// </summary>
		/// <param name="anUPC">An upc.</param>
		public void setUPC(int anUPC)
		{
			upc = anUPC;
		}*/

		/// <summary>
		/// Sets the unit.
		/// </summary>
		/// <param name="aUnit">A unit.</param>
		public void setUnit(string aUnit)
		{
			units = aUnit;
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
		/// Sets the name of the formatted.
		/// </summary>
		private void setFormattedName()
		{
			string formattedString = amount + " " + units + " ";

			foreach(string s in name.Split(' '))
			{
				var str = char.ToUpper(s [0]) + s.Substring (1, s.Length - 1);
				formattedString += str + " ";
			}

			formattedName = formattedString;
		}

		public void setSwitch(bool aSwitch)
		{
			itemSwitch = aSwitch;
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

		/*/// <summary>
		/// Gets the upc.
		/// </summary>
		/// <returns>The upc.</returns>
		public int getUPC ()
		{
			return upc;
		}*/

		/// <summary>
		/// Gets the units.
		/// </summary>
		/// <returns>The units.</returns>
		public string getUnits()
		{
			return units;
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
		/// Gets the formatted string.
		/// </summary>
		/// <returns>The formatted string.</returns>
		public string getFormattedName()
		{
			return formattedName;
		}

		public bool getSwitch()
		{
			return itemSwitch;
		}
	}
}
