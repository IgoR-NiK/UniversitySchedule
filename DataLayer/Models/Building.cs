using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
	public class Building
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
			return obj is Building building ? Id == building.Id : false;
		}
	}
}
