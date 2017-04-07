using System;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class AboutPage : ContentPage
	{
		public AboutPage()
		{
			Title = "About Us";

			Image logo = new Image();
			logo.Source = "drawable/logo.png";
			logo.HeightRequest = 180;
			logo.HorizontalOptions = LayoutOptions.CenterAndExpand;

			var intro = new Label
			{
				Text = "\n Thanks for downloading IngrediScan! We hope to make your shopping, cooking, and eating more efficient and enjoyable! \n",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				Style = Application.Current.Resources["aboutIntroLabelStyle"] as Style
			};

			var body = new Label
			{
				Text = "IngrediScan is the brainchild of Peter Clark, brought to life by himself along with the team of Kendall Evans, Jeremy Day," +
					" Connor Bailie, and Logan Dowd. Together we have worked to bring you an application that handles all of your meal planning needs " +
					"in one place. \n \n IngrediScan has the ability to search for and save recipes, and then add their ingredients directly into an " +
					"interactive shopping cart where you can mark each item off as you shop. We have also provided you with the ability to permanently " +
					"filter your search results by cuisine, dietary needs, or food intolerances by simply visiting the Settings.",
				Font = Font.SystemFontOfSize(NamedSize.Medium),
				Style = Application.Current.Resources["aboutBodyLabelStyle"] as Style
			};

			Padding = new Thickness(20, 15, 20, 10);
			Content = new StackLayout
			{
				Children = {
					logo,
					intro,
					body
					}
			};

		}
	}
}

