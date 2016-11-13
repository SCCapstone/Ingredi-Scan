using System;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class AboutPage : ContentPage
	{
		public AboutPage ()
		{
			Title = "About Page";
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
