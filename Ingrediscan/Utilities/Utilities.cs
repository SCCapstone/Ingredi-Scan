using System;
using System.Collections.Generic;

namespace Ingrediscan.Utilities
{
	public static class Utilities
	{
		public static string RetrieveSettings(Dictionary<string, bool> setting)
		{
			string diets = "";

			foreach(var kv in setting)
			{
				if(kv.Value)
				{
					diets += kv.Key + ",";
				}
			}
			diets.Substring (0, diets.Length - 2);

			return diets;
		}
	}
}
