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
			};

			image.SetBinding (Image.SourceProperty, "Image");

			var label = new Label {
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand,
			};

			label.SetBinding (Label.TextProperty, "Name");

			var deleteButton = new Button {
				HorizontalOptions = LayoutOptions.EndAndExpand,
				BackgroundColor = Color.Transparent,
				Image = "drawable/remove.png"
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
