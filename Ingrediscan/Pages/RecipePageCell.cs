using System;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class RecipePageCell : ViewCell
	{
		Label label;
		StackLayout layout;

		public RecipePageCell ()
		{
			label = new Label {
				VerticalTextAlignment = TextAlignment.Center,
				FontSize = 20
			};
			label.SetBinding (Label.TextProperty, "Step");

			layout = new StackLayout {
				Padding = new Thickness (10, 0, 0, 0),
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { label }
			};

			View = layout;

		}
	}
}
