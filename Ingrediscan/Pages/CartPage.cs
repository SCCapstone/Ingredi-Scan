using System;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class CartPage : ContentPage
	{
		public CartPage ()
		{
			Title = "Cart Page";
			Content = new StackLayout {
				Children = {
					new Label {
						Text = "Todo - list data goes here",
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}
