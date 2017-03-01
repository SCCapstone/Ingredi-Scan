using System;
using System.Collections.Generic;

namespace Ingrediscan.Utilities
{
	public static class Utilities
	{
		public static string RetrieveSettings(Dictionary<string, bool> setting)
		{
			string item = "";

			foreach(var kv in setting)
			{
				if(kv.Value)
				{
					item += kv.Key + ",";
				}
			}
			if (item.Length > 0) 
			{
				item.Substring (0, item.Length - 2);
			}

			return item;
		}
	}
}
