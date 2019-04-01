using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
	public class RoleWebPage
	{
		public int RoleId { get; set; }
		public Role Role { get; set; }

		public int WebPageId { get; set; }
		public WebPage WebPage { get; set; }
	}
}
