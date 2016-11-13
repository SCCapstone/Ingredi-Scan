using System;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class App : Application
	{
		public App ()
		{
			Application.Current.MainPage = new MainPageCS ();
		}
	}
}
