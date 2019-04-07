using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
	public class Department
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public int? FacultyId { get; set; }
		public Faculty Faculty { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object obj)
		{
			return obj is Department department ? Id == department.Id : false;
		}
	}
}
