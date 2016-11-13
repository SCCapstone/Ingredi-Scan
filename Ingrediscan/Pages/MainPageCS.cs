using System;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class MainPageCS : MasterDetailPage
	{
		MasterPageCS masterPage;

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
			if (item != null) {
				Detail = new NavigationPage ((Page)Activator.CreateInstance (item.TargetType));
				masterPage.ListView.SelectedItem = null;
				IsPresented = false;
			}
		}
    
	}
}
