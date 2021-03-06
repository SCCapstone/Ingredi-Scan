﻿using Ingrediscan.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
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
				Style = Application.Current.Resources["labelStyle"] as Style
			};

			label.SetBinding (Label.TextProperty, "Name");

            var recipeLabel = new Label
            {
                IsVisible = false
            };
            recipeLabel.SetBinding(Label.TextProperty, "RecipeName");

			var checkbox = new Button
			{
				Style = Application.Current.Resources["checkboxStyle"] as Style
            };
            checkbox.SetBinding(Button.ImageProperty, "CheckBoxName");
           /* if(CartPage.markedItems[key])
            {
                checkbox.Image = "drawable/checked.png";
            }
            else
            {
                checkbox.Image = "drawable/unchecked.png";
            }*/
                    //checkbox.SetBinding (Image.SourceProperty, new Binding ("CheckBox", BindingMode.Default,
                    //                                                      new CheckBoxConverter (), null));

            checkbox.Clicked += (sender, e) => {
				if(checkbox.Image == "drawable/unchecked.png")
				{
					//checkbox.Image = "selected.png"
					checkbox.Image = "drawable/selected.png";
					checkbox.BorderWidth = 0;
					label.FontAttributes = FontAttributes.None;
                    CartPage.markedItems[recipeLabel.Text + "!" + label.Text] = true;
				}
				else
				{
					checkbox.Image = "drawable/unchecked.png";
					checkbox.BorderWidth = 0.5;
					label.FontAttributes = FontAttributes.None;
                    CartPage.markedItems[recipeLabel.Text+ "!" + label.Text] = false;
                }
                CartPage.saveCart();
                //Console.WriteLine(recipeLabel.Text   + " " + label.Text);
                //Console.WriteLine(CartPage.markedItems[recipeLabel.Text + " " + label.Text]);
				//TODO Do something in the list with this new value
			};

			var layout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Children = { checkbox, label, image, recipeLabel }
			};

			View = layout;
		}
	}
}
