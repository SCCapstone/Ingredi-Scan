using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ingrediscan
{
	public class Group : ObservableCollection<CartPageItem.Ingredients>
	{
		public Group (string name)//, string image)
		{
			Name = name;
			//Image = image;
		}

		public string Name { get; set; }
		//public string Image { get; set; }
	}
}
