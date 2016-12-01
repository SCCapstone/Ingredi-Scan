using System.Collections.Generic;

namespace Ingrediscan.Utilities
{
	public class Auth0Json
	{
		public string email { get; set; }
		public bool email_verified { get; set; }
		public string name { get; set; }
		public string given_name { get; set; }
		public string family_name { get; set; }
		public string picture { get; set; }
		public string locale { get; set; }
		public string clientID { get; set; }
		public string updated_at { get; set; }
		public string user_id { get; set; }
		public string nickname { get; set; }
		public List<Identity> identities { get; set; }
		public string created_at { get; set; }
		public string sub { get; set; }
	}

	public class Identity
	{
		public string provider { get; set; }
		public string user_id { get; set; }
		public string connection { get; set; }
		public bool isSocial { get; set; }
	}
}