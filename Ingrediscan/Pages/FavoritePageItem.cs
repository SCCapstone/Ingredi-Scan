using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Ingrediscan
{
	public class FavoritePageItem
	{
		public string Name { get; set; }
		public string Image { get; set; }
		//public Button RemoveButton { get; set; }
		public string Id { get; set; }

		public bool Enabled { get; set; }
		public bool Visible { get; set; }
	}
}
