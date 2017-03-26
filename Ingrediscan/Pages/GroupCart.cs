using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ingrediscan
{
	public class GroupCart : ObservableCollection<CartPageItem.RecipeIngredients>
	{
		public GroupCart (string name)//, string image)
		{
			Name = name;
			//Image = image;
		}

		public string Name { get; set; }
		//public string Image { get; set; }
	}
}
