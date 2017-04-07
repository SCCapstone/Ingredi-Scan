using Ingrediscan.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Ingrediscan
{
	public class CartPageHeader : ViewCell
	{

		public CartPageHeader()
		{
			this.Height = 100;

			var recTitle = new Label
			{
				Style = Application.Current.Resources["headerLabelStyle"] as Style,
				Font = Font.SystemFontOfSize(NamedSize.Large)

			};
			recTitle.SetBinding(Label.TextProperty, "Name");

			View = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = 100,
				BackgroundColor = Color.Transparent,
				Padding = 5,
				Orientation = StackOrientation.Horizontal,
				Children = { recTitle }
			};
		}

	}
}