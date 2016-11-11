
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Ingrediscan
{
	[Activity (Label = "Ingrediscan")]
	public class SettingsActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Settings);

			// Create your application here
			Button back = FindViewById<Button> (Resource.Id.backToMain);

			back.Click += delegate {
				
				StartActivity (typeof(MainActivity));
				//Finish ();
			};
		}
	}
}
