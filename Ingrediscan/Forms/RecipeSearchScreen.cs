using System;
using System.Collections.Generic;

namespace Ingrediscan
{
	public class RecipeSearchScreen
	{
		private string searchString = "";
		private Queue<string> latestSearches = new Queue<string> ();
		private static int latestSearchesLimit = 10;

		public RecipeSearchScreen ()
		{
		}

		/// <summary>
		/// Sets the search string.
		/// </summary>
		/// <param name="aSearch">A search.</param>
		public void setSearchString(string aSearch)
		{
			searchString = aSearch;
			this.addToLatestSearches (aSearch);
		}

		/// <summary>
		/// Adds to latest searches.
		/// </summary>
		/// <param name="aSearch">A search.</param>
		public void addToLatestSearches(string aSearch)
		{
			if(latestSearches.Count == latestSearchesLimit)
			{
				latestSearches.Dequeue ();
			}

			latestSearches.Enqueue (aSearch);
		}

		/// <summary>
		/// Removes from latest searches. For instance, when a user swipes to delete a search.
		/// </summary>
		/// <param name="aSearch">A search.</param>
		public void removeFromLatestSearches(string aSearch)
		{
			Queue<string> temp = new Queue<string> ();
			if(latestSearches.Contains(aSearch))
			{
				if(!latestSearches.Peek().Equals(aSearch))
				{
					temp.Enqueue (aSearch);
				}
			}

			latestSearches = temp;
		}

		/// <summary>
		/// Gets the search string.
		/// </summary>
		/// <returns>The search string.</returns>
		public string getSearchString()
		{
			return searchString;
		}
		/// <summary>
		/// Gets the latest searches.
		/// </summary>
		/// <returns>The latest searches.</returns>
		public Queue<string> getLatestSearches()
		{
			return latestSearches;
		}
	}
}
