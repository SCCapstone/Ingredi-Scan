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
				HorizontalOptions = LayoutOptions.EndAndExpand,
			};

			image.SetBinding (Image.SourceProperty, "Image");

			var label = new Label {
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand,
			};

			label.SetBinding (Label.TextProperty, "Name");

			var tick = new Switch {
				HorizontalOptions = LayoutOptions.Start,
			};

			tick.SetBinding (Switch.IsEnabledProperty, "Switch");

			tick.PropertyChanged += (sender, e) => {
				Console.WriteLine (tick.IsEnabled);
				//TODO Do something in the list with this new value
			};

			var layout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Children = { tick, label, image }
			};

			View = layout;
		}
	}
}
