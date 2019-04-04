using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Должность преподавателя.
	/// </summary>
	public class Post
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[StringLength(300)]
		public string Description { get; set; }

		public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
	}
}
