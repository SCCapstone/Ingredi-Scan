﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Ingrediscan
{
	public class MasterPageCS : ContentPage
	{
		public ListView ListView { get { return listView; } }

		ListView listView;

		public MasterPageCS ()
		{
			var masterPageItems = new List<MasterPageItem> ();
			masterPageItems.Add (new MasterPageItem {
				Title = "Scan",
				IconSource = "Resources/drawable/scan.png",
				TargetType = typeof (ScanPage)
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "Search for Recipes",
				IconSource = "Resources/drawable/search.png",
				TargetType = typeof (SearchPage)
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "Shopping Cart",
				IconSource = "Resources/drawable/cart.png",
				TargetType = typeof (CartPage)
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "Settings",
				IconSource = "Resources/drawable/settings.png",
				TargetType = typeof (SettingsPage)
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "About",
				IconSource = "Resources/drawable/about.png",
				TargetType = typeof (AboutPage)
			});

			listView = new ListView {
				ItemsSource = masterPageItems,
				ItemTemplate = new DataTemplate (() => {
					var imageCell = new ImageCell ();
					imageCell.SetBinding (TextCell.TextProperty, "Title");
					imageCell.SetBinding (ImageCell.ImageSourceProperty, "IconSource");
					return imageCell;
				}),
				VerticalOptions = LayoutOptions.Fill,
				SeparatorVisibility = SeparatorVisibility.None
			};

			Padding = new Thickness (0, 20, 0, 0);
			Icon = "Resources/drawable/hamburger.png";
			Title = "Navigation Menu";
			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children =
				{
					listView
				}
			};
		}
	}
}
