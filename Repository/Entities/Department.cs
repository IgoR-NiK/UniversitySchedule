using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities
{
	/// <summary>
	/// Кафедра.
	/// </summary>
	public class Department
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public int? FacultyId { get; set; }
		public Faculty Faculty { get; set; }

		public ICollection<Group> Groups { get; set; } = new List<Group>();
		public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
	}
}