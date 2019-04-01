using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities
{
	public class Group
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public int CoursesNumber { get; set; }

		public int StudentsCount { get; set; }

		public int? DepartmentId { get; set; }
		public Department Department { get; set; }

		public int? ParentGroupId { get; set; }
		public Group ParentGroup { get; set; }

		public ICollection<Group> ChildGroups { get; set; } = new List<Group>();
	}
}
