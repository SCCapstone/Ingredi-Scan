using System;
using System.Collections.Generic;
using Ingrediscan.Utilities;
using Xamarin.Forms;

namespace Ingrediscan
{
	public class FavoritePageCell : ViewCell
	{
		public FavoritePageCell ()
		{
			var image = new Image {
				HorizontalOptions = LayoutOptions.StartAndExpand,
			//	HeightRequest = 100,
			//	WidthRequest = 60
			};

			image.SetBinding (Image.SourceProperty, "Image");

			var label = new Label {
				Style = Application.Current.Resources["labelStyle"] as Style
			};

			label.SetBinding (Label.TextProperty, "Name");

			var deleteButton = new Button {
				Style = Application.Current.Resources["itemDeleteButtonStyle"] as Style
			};

			deleteButton.SetBinding (Button.IsEnabledProperty, "Enabled");
			deleteButton.SetBinding (Button.IsVisibleProperty, "Visible");

			//checkbox.SetBinding (Image.SourceProperty, new Binding ("CheckBox", BindingMode.Default,
			//                                                      new CheckBoxConverter (), null));

			deleteButton.Clicked += (sender, e) => {
				foreach (var tempR in Globals.firebaseData.savedRecipes)
				{
					if (label.Text == tempR.name) 
					{
						Globals.firebaseData.savedRecipes.Remove (tempR);
						SaveAndLoad.SaveToFirebase (Globals.firebaseData);
						break;
					}
				}
				foreach(var tempFaveR in FavoriteRecipesPage.savedRecipes)
				{
					if(label.Text == tempFaveR.Name)
					{
						FavoriteRecipesPage.savedRecipes.Remove (tempFaveR);
						FavoriteRecipesPage.faveView.ItemsSource = FavoriteRecipesPage.savedRecipes;
						break;
					}
				}
			//TODO Do something in the list with this new value
			};

			var layout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Children = { image, label, deleteButton }
			};

			View = layout;
		}
	}
}
