using System;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class CartPageCell : ViewCell
	{
		Label title, label;
		StackLayout layout;

		public CartPageCell ()
		{
			title = new Label {
				VerticalTextAlignment = TextAlignment.Center
			};
			title.SetBinding (Label.TextProperty, "Recipe");

			label = new Label {
				VerticalTextAlignment = TextAlignment.Center,
				FontSize = 20
			};
			label.SetBinding (Label.TextProperty, "Step");

			//TODO Implement ListView?
			var text = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness (0, 0, 0, 0),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { title, label }
			};

			layout = new StackLayout {
				Padding = new Thickness (10, 0, 0, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { text }
			};

			View = layout;
		}
	}
}
