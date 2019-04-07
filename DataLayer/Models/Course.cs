using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
	public class Course
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object obj)
		{
			return obj is Course course ? Id == course.Id : false;
		}
	}
}
