using System;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class SearchPage : ContentPage
	{
		public SearchPage ()
		{
			Title = "Search Page";
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
