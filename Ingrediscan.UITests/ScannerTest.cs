using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace Ingrediscan.UITests
{
	[TestFixture]
	public class ScannerTest
	{
		AndroidApp app;

		[SetUp]
		public void BeforeEachTest ()
		{
			app = ConfigureApp.Android.StartApp ();
		}

		[Test]
		public void ScanPageTest ()
		{
			Func<AppQuery, AppQuery> MyButton = c => c.Button ("ScanButton");

			app.Tap (MyButton);
			AppResult [] results = app.Query (MyButton);
			//app.Screenshot ("Button clicked once.");

			Assert.NotNull (results);
		}
	}
}
