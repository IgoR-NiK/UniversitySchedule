using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
	public class DayTimeslot
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime StartTime { get; set; }

		public override string ToString()
		{
			return $"{Name} {StartTime.ToShortTimeString()}";
		}

		public override bool Equals(object obj)
		{
			return obj is DayTimeslot dayTimeslot ? Id == dayTimeslot.Id : false;
		}
	}
}
