using System;
namespace Ingrediscan
{
	public class MenuItem
	{
		private int formID = 0;

		public MenuItem ()
		{
		}

		/// <summary>
		/// Sets the form identifier.
		/// </summary>
		/// <param name="id">Identifier.</param>
		public void setFormID (int id)
		{
			formID = id;
		}
		/// <summary>
		/// Gets the form identifier.
		/// </summary>
		/// <returns>The form identifier.</returns>
		public int getFormID()
		{
			return formID;
		}
	}
}
