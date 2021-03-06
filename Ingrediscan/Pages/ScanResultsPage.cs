﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ingrediscan.Utilities;
using Xamarin.Forms;

namespace Ingrediscan
{
	public class ScanResultsPage : ContentPage
	{
		ListView resultsView = new ListView ();
		//List<SearchResultItem> recipes = new List<SearchResultItem> ();

		public ScanResultsPage (SpoonacularClasses.FindByUPC resultsFromUPC)
		{
			PrintItem (resultsFromUPC);
            //if (!resultsFromUPC.title.Equals(""))
            //{
                resultsView = new ListView
                {
                    //ItemsSource = recipes,
                    ItemTemplate = new DataTemplate(() =>
                    {
                        var imageCell = new ImageCell();
                        imageCell.SetBinding(TextCell.TextProperty, "Text");
                        imageCell.SetBinding(ImageCell.ImageSourceProperty, "ImageSource");
                        return imageCell;
                    }),
                    VerticalOptions = LayoutOptions.StartAndExpand,
                };
                bool finished = true;
                resultsView.ItemSelected += async (sender, e) =>
                {
                    if (Navigation.NavigationStack.Count < 3 && finished == true)
                    {
                        finished = false;
                        var id = ((SearchResultItem)e.SelectedItem).Id;
                        var recipe = await REST_API.GET_RecipeInformation(id, false);
                        await Navigation.PushAsync(new RecipePage(recipe));
                        ((ListView)sender).SelectedItem = null;
                        finished = true;
                    }
                    else if (finished == false)
                    {
                        ((ListView)sender).SelectedItem = null;
                    }
                };

			    resultsView.ItemsSource = this.CreateListViewFromUPC(resultsFromUPC).Result;

                Title = "Search Results";
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Start,
                    Children = {
                    resultsView,
                },
                    Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5)
                };
            //}

           /* else
            {
                Navigation.PopAsync();
                Android.Widget.Toast.MakeText(Forms.Context, "An error occurred. It's possible the barcode is not a valid ingredient or not " +
                                        "currently in Spoonacular's database.", Android.Widget.ToastLength.Short).Show();
            }*/
		}

        public async Task<List<SearchResultItem>> CreateListViewFromUPC(SpoonacularClasses.FindByUPC resultsFromUPC)
        {
            var items = await REST_API.GET_FindByIngredients(resultsFromUPC.title);
            if (items.Count > 0)
            {
                List<SearchResultItem> searchResultItems = new List<SearchResultItem>();
                items.ForEach(x => searchResultItems.Add(new SearchResultItem
                {
                    ImageSource = x.image,
                    Text = x.title,
                    TargetType = typeof(RecipePage),
                    Id = x.id
                }));
                return searchResultItems;

            }
            else
            {
                Navigation.PopAsync();
                Android.Widget.Toast.MakeText(Forms.Context, "An error occurred. It's possible the barcode is not a valid ingredient or not " +
                                        "currently in Spoonacular's database.", Android.Widget.ToastLength.Short).Show();
                return null;
            }
        }
		public void PrintItem(SpoonacularClasses.FindByUPC resultsFromUPC)
		{
			Console.WriteLine ("ID: " + resultsFromUPC.id);
			Console.WriteLine ("Title: " + resultsFromUPC.title);
			Console.WriteLine ("Price: " + resultsFromUPC.price);
			Console.WriteLine ("Likes: " + resultsFromUPC.likes);
			Console.WriteLine ("Badges: ");
			resultsFromUPC.badges.ForEach (x => Console.WriteLine (x));
			Console.WriteLine ("Important Badges: ");
			resultsFromUPC.important_badges.ForEach (x => Console.WriteLine (x));
			Console.WriteLine ("Nutrition Widget: " + resultsFromUPC.nutrition_widget);
			Console.WriteLine ("Serving Size: " + resultsFromUPC.serving_size);
			Console.WriteLine ("Number of Servings: " + resultsFromUPC.number_of_servings);
			Console.WriteLine ("Spoonacular Score: " + resultsFromUPC.spoonacular_score);
			Console.WriteLine ("Breadcrumbs: ");
			resultsFromUPC.breadcrumbs.ForEach (x => Console.WriteLine (x));
			Console.WriteLine ("Generated Text: " + resultsFromUPC.generated_text);
			Console.WriteLine ("Ingredient Count: " + resultsFromUPC.ingredientCount);
			Console.WriteLine ("Images: ");
			resultsFromUPC.images.ForEach (x => Console.WriteLine (x));
		}
	}
}
