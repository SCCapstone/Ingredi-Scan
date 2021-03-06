﻿using System;
using Xamarin.Forms;

namespace Ingrediscan
{
	public class MainPageCS : MasterDetailPage
	{
		MasterPageCS masterPage;
		string prevPageStr = "Ingrediscan.ScanPage";

		public MainPageCS ()
		{
			MasterBehavior = MasterBehavior.Split;
			masterPage = new MasterPageCS ();
			Master = masterPage;
			Detail = new NavigationPage (new ScanPage ());
			masterPage.ListView.ItemSelected += OnItemSelected;
		}

		void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MasterPageItem;
			if (item != null && item.Action != null)
			{
				Console.WriteLine("Invoking action.");
				item.Action.Invoke();
			}
			if (item != null && item.TargetType.FullName != prevPageStr) 
			{
                if(item.TargetType.FullName == "Ingrediscan.CartPage")
                {
                    CartPage.UpdateCheckBoxes();
                }
				Detail = new NavigationPage ((Page)Activator.CreateInstance (item.TargetType));
				Console.WriteLine (item.TargetType + " Finished");
				masterPage.ListView.SelectedItem = null;
				IsPresented = false;

				prevPageStr = item.TargetType.FullName;

			}
			else
			{
				masterPage.ListView.SelectedItem = null;
				IsPresented = false;
			}
		}
	}
}
