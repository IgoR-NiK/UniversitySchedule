using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Entities
{
	public class Department
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public int? FacultyId { get; set; }
		public Faculty Faculty { get; set; }

		public ICollection<Group> Groups { get; set; } = new List<Group>();
	}
}