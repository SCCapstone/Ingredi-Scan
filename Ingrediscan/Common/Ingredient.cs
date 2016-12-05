using System;
namespace Ingrediscan
{
	public class Ingredient
	{
		public string name { get; set; }
		public double amount { get; set; }
		//private int upc = 0;
		public string units { get; set; }
		public string id { get; set; }
		public string image { get; set; }
		public string formattedName = "";
		public bool itemSwitch { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Ingrediscan.Ingredient"/> class.
		/// </summary>
		/// <param name="aName">A name.</param>
		/// <param name="anAmount">An amount.</param>
		/// <param name="anUPC">An upc.</param>
		public Ingredient (string aName, double anAmount, /*int anUPC,*/ string aUnit, string anID, string anImage, bool aSwitch)
		{
			name = aName;
			amount = anAmount;
			//this.setUPC (anUPC);
			units = aUnit;
			aUnit = anID;
			image = anImage;
			itemSwitch = aSwitch;
		}

		/// <summary>
		/// Sets the name of the formatted.
		/// </summary>
		public void setFormattedName()
		{
			string formattedString = amount + " " + units + " ";

			foreach(string s in name.Split(' '))
			{
				var str = char.ToUpper(s [0]) + s.Substring (1, s.Length - 1);
				formattedString += str + " ";
			}

			formattedName = formattedString;
		}

	}
}
