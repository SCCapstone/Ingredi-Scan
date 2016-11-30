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
						Text = "Well... We don't have anything to put here yet.",
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}
