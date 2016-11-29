using System;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class RecipePageCell : ViewCell
	{
		Label label;
		StackLayout layout;

		public RecipePageCell ()
		{
			/*title = new Label {
				VerticalTextAlignment = TextAlignment.Center
			};
			title.SetBinding (Label.TextProperty, "Number");*/

			label = new Label {
				VerticalTextAlignment = TextAlignment.Center,
				FontSize = 20
			};
			label.SetBinding (Label.TextProperty, "Step");

			//TODO Implement ListView?
			/*var text = new ListView {
				HasUnevenRows = true,
				ItemsSource = 
				//Orientation = StackOrientation.Vertical,
				//Padding = new Thickness (0, 0, 0, 0),
				//HorizontalOptions = LayoutOptions.StartAndExpand,
				//Children = { title, label }
			};*/

			layout = new StackLayout {
				Padding = new Thickness (10, 0, 0, 0),
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { label }
			};

			View = layout;

		}

		/*protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			var recipe = (RecipePageItem.RecipePageStep)BindingContext;

			// rough translation of character-count to cell height
			// doesn't always work, but close enough for now
			if (recipe.Step.Length > 75)
				this.Height = 130;
			else if (recipe.Step.Length > 60)
				this.Height = 100;
			else if (recipe.Step.Length > 30)
				this.Height = 80;
			else
				this.Height = 60;
		}*/

	}
}
