using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
	public class Faculty
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ShortName { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object obj)
		{
			return obj is Faculty faculty ? Id == faculty.Id : false;
		}
	}
}
