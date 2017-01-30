using System;

using Xamarin.Forms;

namespace Ingrediscan
{
	public class MasterPageItem
	{
		public string Title { get; set; }
		public string IconSource { get; set; }
		public Type TargetType { get; set; }
		public Action Action { get; set; }
	}
}
