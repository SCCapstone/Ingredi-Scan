using System;
namespace Ingrediscan
{
	public class SearchResultItem
	{
		public string ImageSource { get; set; }
		public string Text { get; set; }
		//public string Detail { get; set; } TODO Think of a way to grab details at a low cost from the API
		public Type TargetType { get; set; }
		public string Id { get; set; }
	}
}
