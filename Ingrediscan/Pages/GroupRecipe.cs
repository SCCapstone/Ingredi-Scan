using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Ingrediscan
{
	public class GroupRecipe : ObservableCollection<RecipePageItem.RecipePageStep>
	{
		public GroupRecipe (string aNumb)
		{
			Number = aNumb;
		}

		public string Number { get; set; }
	}
}
