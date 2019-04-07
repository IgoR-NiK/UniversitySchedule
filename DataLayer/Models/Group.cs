using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
	public class Group
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int CoursesNumber { get; set; }
		public int StudentsCount { get; set; }

		public int? DepartmentId { get; set; }
		public Department Department { get; set; }

		public int? ParentGroupId { get; set; }

		public List<Group> ChildGroups { get; set; } = new List<Group>();

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object obj)
		{
			return obj is Group group ? Id == group.Id : false;
		}
	}
}
