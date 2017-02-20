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

			var checkbox = new Button {
				HorizontalOptions = LayoutOptions.Start,
				Image = "drawable/unchecked.png",
				BackgroundColor = Color.Transparent,
			};

			//checkbox.SetBinding (Image.SourceProperty, new Binding ("CheckBox", BindingMode.Default,
			  //                                                      new CheckBoxConverter (), null));

			checkbox.Clicked += (sender, e) => {
				if(checkbox.Image == "drawable/unchecked.png")
				{
					checkbox.Image = "drawable/checked.png";
				}
				else
				{
					checkbox.Image = "drawable/unchecked.png";
				}
				//TODO Do something in the list with this new value
			};

			var layout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Children = { checkbox, label, image }
			};

			View = layout;
		}
	}
}
