using System;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class App : Application
	{
		public App()
		{
			Application.Current.MainPage = new MainPageCS();

			if (Application.Current.Resources == null)
				Application.Current.Resources = new ResourceDictionary();

			var labelStyle = new Style(typeof(Label))
			{
				Setters = {
					new Setter { Property = Label.FontFamilyProperty, Value = "sans-serif-light" },
					new Setter { Property = Label.TextColorProperty, Value = "#000000" }
				}
			};
			Application.Current.Resources.Add("labelStyle", labelStyle); 



		}
	}
}
