using System;

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
			if (item != null && item.TargetType.FullName != prevPageStr) 
			{
				Detail = new NavigationPage ((Page)Activator.CreateInstance (item.TargetType));
				Console.WriteLine (item.TargetType + " Finished");
				masterPage.ListView.SelectedItem = null;
				IsPresented = false;

				if (prevPageStr == "Ingrediscan.SettingsPage")
				{
					Settings.saveSettings ();
				}
				else if (prevPageStr == "Ingrediscan.CartPage")
				{
					GlobalVariables.saveRecipes ();
				}

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
