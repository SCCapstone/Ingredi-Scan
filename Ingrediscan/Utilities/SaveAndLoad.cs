using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency (typeof (Ingrediscan.SaveAndLoad))]
namespace Ingrediscan
{
	public class SaveAndLoad
	{
		public void SaveText (string filename, string text)
		{
			var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var filePath = Path.Combine (documentsPath, filename);
			File.WriteAllText (filePath, text);
		}

		public string LoadText (string filename)
		{
			var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var filePath = Path.Combine (documentsPath, filename);
			return filePath;
		}
	}
}
