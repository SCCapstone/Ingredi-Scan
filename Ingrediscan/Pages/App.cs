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
					new Setter { Property = Label.TextColorProperty, Value = "#000000" },
					//new Setter { Property = Label.FontSizeProperty, Value = Font.SystemFontOfSize(NamedSize.Small) },
					new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
					new Setter { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.StartAndExpand }
				}
			};
			Application.Current.Resources.Add("labelStyle", labelStyle);

			var headerLabelStyle = new Style(typeof(Label))
			{
				Setters = {
					new Setter { Property = Label.FontFamilyProperty, Value = "sans-serif-light" },
					new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("#095594") },
					new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold },
					new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
					new Setter { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Center},
				}
			};
			Application.Current.Resources.Add("headerLabelStyle", headerLabelStyle);

			var checkboxStyle = new Style(typeof(Button))
			{
				Setters = {
					new Setter { Property = Button.BorderWidthProperty, Value = 0.5 },
					new Setter { Property = Button.BorderColorProperty, Value = Color.Black },
					new Setter { Property = Button.HorizontalOptionsProperty, Value = LayoutOptions.Start },
					new Setter { Property = Button.BackgroundColorProperty, Value = Color.Transparent },
					new Setter { Property = Button.WidthRequestProperty, Value = 40 },
					new Setter { Property = Button.HeightRequestProperty, Value = 10 }
				}
			};
			Application.Current.Resources.Add("checkboxStyle", checkboxStyle);

			var itemDeleteButtonStyle = new Style(typeof(Button))
			{
				Setters = {
					new Setter { Property = Button.HorizontalOptionsProperty, Value = LayoutOptions.EndAndExpand },
					new Setter { Property = Button.BackgroundColorProperty, Value = Color.Transparent },
					new Setter { Property = Button.ImageProperty, Value = "drawable/remove.png" }
				}
			};
			Application.Current.Resources.Add("itemDeleteButtonStyle", itemDeleteButtonStyle);

			/*var scanButtonStyle = new Style(typeof(Button))
			{
				Setters = {
					new Setter { Property = Button.FontFamilyProperty, Value = "sans-serif-light" },
				}
			};
			Application.Current.Resources.Add("scanButtonStyle", scanButtonStyle);*/





		}
	}
}
