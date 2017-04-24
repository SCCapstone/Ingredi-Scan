using System;

using XLabs.Forms.Controls;
using Xamarin.Forms;
using System.Threading.Tasks;

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
					"interactive shopping cart where you can mark each item off as you shop. Not only can you find a recipe by name or item on the Search " +
					"page, but also by using the barcode scanner on any item to quickly receive a list of recipes that use the scanned item. We have " +
					"also provided you with the ability to permanently filter your search results by cuisine, dietary needs, or food intolerances " +
					"by simply visiting the Settings.",
				Font = Font.SystemFontOfSize(NamedSize.Medium),
				Style = Application.Current.Resources["aboutBodyLabelStyle"] as Style
			};

            // declaring a new label crashes the app when about page is opened up
            /*var jokeText = new Label
            {
                Text = "joke",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                //Style = Application.Current.Resources["jokeTextLabelStyle"] as Style
            };*/

            var jokeText = new  Xamarin.Forms.Button
            {
                Text = "joke",
                Font = Font.SystemFontOfSize(15),
                WidthRequest = 280,
                HeightRequest = 200,
                BackgroundColor = Color.FromHex("#1D89E4"),
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                //				Style = Application.Current.Resources["jokeTextStyle"] as Style
                AutomationId = "JokeText"
            };

            var jokeClose = new Xamarin.Forms.Button
            {
                Text = "Close",
                Font = Font.SystemFontOfSize(30),
                WidthRequest = 180,
                HeightRequest = 60,
                BackgroundColor = Color.FromHex("#1D89E4"),
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.EndAndExpand,
                //				Style = Application.Current.Resources["jokeCloseStyle"] as Style
                AutomationId = "JokeClose"
            };

            var jokeButton = new Xamarin.Forms.Button
            {
                Text = "Jokes and Trivia",
                Font = Font.SystemFontOfSize(20),
                WidthRequest = 200,
                HeightRequest = 60,
                BackgroundColor = Color.FromHex("#1D89E4"),
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                //				Style = Application.Current.Resources["jokeButtonStyle"] as Style
                AutomationId = "JokeButton"
            };

            var triviaText = new Xamarin.Forms.Button
            {
                Text = "trivia",
                Font = Font.SystemFontOfSize(15),
                WidthRequest = 280,
                HeightRequest = 200,
                BackgroundColor = Color.FromHex("#1D89E4"),
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                //				Style = Application.Current.Resources["triviaTextStyle"] as Style
                AutomationId = "TriviaText"
            };

            /*var triviaButton = new Xamarin.Forms.Button
            {
                Text = "Trivia",
                Font = Font.SystemFontOfSize(30),
                WidthRequest = 180,
                HeightRequest = 60,
                BackgroundColor = Color.FromHex("#1D89E4"),
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                //				Style = Application.Current.Resources["triviaButtonStyle"] as Style
                AutomationId = "TrivaButton"
            };*/

            PopupLayout jokePopUp = new PopupLayout();

            var PopUp = new StackLayout
            {
                WidthRequest = 300,
                HeightRequest = 480,
                BackgroundColor = Color.White,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    jokeText,
                    triviaText,
                    jokeClose
                }
            };

            var stackContent = new StackLayout
			{
				Padding = new Thickness(20, 15, 20, 10),
				Children = {
					//logo,
					intro,
					body,
                    jokeButton
					}
			};

          jokeButton.Clicked += async (sender, e) =>
            {
                stackContent.IsVisible = false;
                jokePopUp.ShowPopup(PopUp);
                jokeText.Text = await getJoke();
                triviaText.Text = await getTrivia();
                
               
            };
            jokeText.Clicked += async (sender, e) =>
            {

                jokeText.Text =  await getJoke();
            };

            triviaText.Clicked += async (sender, e) =>
            {
                triviaText.Text = await getTrivia();
            };

            jokeClose.Clicked += (sender, e) =>
            {
                if (jokePopUp.IsPopupActive)
                {
                    jokePopUp.DismissPopup();// Close the PopUp
                }
                stackContent.IsVisible = true;

            };


            jokePopUp.Content = stackContent;
             Content = new Xamarin.Forms.ScrollView
            {

                  Content = jokePopUp
                //Content = stackContent
			}; 
		}

        public async Task<string> getJoke()
        {
            SpoonacularClasses.RandomJoke x = await REST_API.GET_RandomJoke();
            return x.text;
            //var joke = await REST_API.GET_RandomJoke();
            //return joke.text; // not correct yet
       }
        public async Task<string> getTrivia()
        {
            SpoonacularClasses.RandomTrivia y = await REST_API.GET_RandomTrivia();
            return y.text;
        }
    }
}


