using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities
{
	public class Role
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[StringLength(300)]
		public string Description { get; set; }

		public ICollection<User> Users { get; set; } = new List<User>();
		public ICollection<RoleWebPage> RoleWebPages { get; set; } = new List<RoleWebPage>();
	}
}
