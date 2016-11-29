using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Ingrediscan
{
	public class CartPageCell : ViewCell
	{
		public CartPageCell ()
		{
			var image = new Image {
				HorizontalOptions = LayoutOptions.StartAndExpand,
			};

			image.SetBinding (Image.SourceProperty, "Image");

			var label = new Label {
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.Start,
			};

			label.SetBinding (Label.TextProperty, "Name");

			var tick = new Switch {
				HorizontalOptions = LayoutOptions.EndAndExpand,
			};

			tick.SetBinding (Switch.IsEnabledProperty, "Switch");

			tick.PropertyChanged += (sender, e) => {
				Console.WriteLine (tick.IsEnabled);
				//TODO Do something in the list with this new value
			};

			var layout = new StackLayout {
				//Padding = new Thickness (5, 0, 0, 0),
				Orientation = StackOrientation.Horizontal,
				//HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { image, label, tick }
			};

			View = layout;
		}
	}
}
