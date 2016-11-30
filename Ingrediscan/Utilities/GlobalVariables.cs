using System;
using System.Collections.Generic;
using System.IO;
using ZXing.Mobile;

namespace Ingrediscan
{
	public static class GlobalVariables
	{
		//public static BarcodeScanner barScan = new BarcodeScanner ();
		//public static MobileBarcodeScanner barcodeScanner = new MobileBarcodeScanner ();
		public static List<Recipe> CurrentRecipes = new List<Recipe> ();

		public static void clearRecipeList ()
		{
			GlobalVariables.CurrentRecipes.Clear ();
		}

		public static void saveRecipes ()
		{
			// TODO Write to file
			Console.WriteLine ("SAVING...");
			SaveAndLoad save = new SaveAndLoad ();
			string file = "";

			file += GlobalVariables.CurrentRecipes.Count + "\n";

			for (int i = 0; i < GlobalVariables.CurrentRecipes.Count; ++i) {
				Recipe item = GlobalVariables.CurrentRecipes [i];
				file += item.getName () + "," + item.getID () + "," + item.getImage () + "," + item.getPrepTime () +
							"," + item.getCookTime () + "\n";
				file += item.getIngredientList ().Count + "\n";
				for (int j = 0; j < item.getIngredientList ().Count; ++j) {
					Ingredient subItem = item.getIngredientList () [j];
					file += subItem.getName () + "," + subItem.getAmount () + "," + subItem.getUnits () + "," + subItem.getID () +
					               "," + subItem.getImage () + "," + subItem.getSwitch() + "\n";
				}
			}

			Console.WriteLine (file);
			save.SaveText ("currentCart.txt", file);
			Console.WriteLine ("SAVING COMPLETED");
		}

		public static void loadRecipes ()
		{
			clearRecipeList ();

			try {
				SaveAndLoad load = new SaveAndLoad ();

				string file = load.LoadText ("currentCart.txt");
				Console.WriteLine ("LOADING..." + file);
				//Stream stream = File.Open (file, FileMode.Open);
				if (file != "") {
					using (StreamReader reader = new StreamReader (file)) {

						string line = "";//reader.ReadToEnd ();
										 //Console.WriteLine (line);

						int recipe_n = int.Parse (reader.ReadLine ());

						for (int i = 0; i < recipe_n; ++i) {
							line = reader.ReadLine ();
							string [] items = line.Split (',');
							List<Ingredient> ingredients = new List<Ingredient> ();
							int ing_n = int.Parse (reader.ReadLine ());
							for (int j = 0; j < ing_n; ++j) {
								string [] ing_items = reader.ReadLine ().Split (',');
								Ingredient ing = new Ingredient (ing_items [0], int.Parse (ing_items [1]), ing_items [2], 
								                                 ing_items [3], ing_items [4], bool.Parse(ing_items [5]));
								ingredients.Add (ing);
							}
							Recipe recipe = new Recipe (ingredients, items [0], items [1], items [2], int.Parse (items [3]), 
							                            int.Parse (items [4]));

							GlobalVariables.CurrentRecipes.Add (recipe);
						}
					}
				}

				//stream.Close ();
				Console.WriteLine ("LOAD DONE");
			} catch (IOException e) {
				Console.WriteLine (e.Message);
			}

		}
	}
}
