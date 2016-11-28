using System;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class MainPageCS : MasterDetailPage
	{
		MasterPageCS masterPage;
		string prevPage = "";

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
			if (item != null) 
			{
				Detail = new NavigationPage ((Page)Activator.CreateInstance (item.TargetType));
				Console.WriteLine (item.TargetType + " Finished");
				masterPage.ListView.SelectedItem = null;
				IsPresented = false;

				if (prevPage == "Ingrediscan.SettingsPage")
				{
					Settings.saveSettings ();
				}
				else if (prevPage == "Ingrediscan.CartPage")
				{
					GlobalVariables.saveRecipes ();
				}

				prevPage = item.TargetType.FullName;
			}
		}
	}
}
