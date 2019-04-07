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
	}
}
