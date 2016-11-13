using System;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			Title = "Settings Page";
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
