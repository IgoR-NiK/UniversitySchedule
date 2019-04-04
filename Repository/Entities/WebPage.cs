using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Страница.
	/// </summary>
	public class WebPage
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[StringLength(300)]
		public string Path { get; set; }

		public ICollection<BanRoleWebPage> BanRoleWebPages { get; set; } = new List<BanRoleWebPage>();
	}
}
