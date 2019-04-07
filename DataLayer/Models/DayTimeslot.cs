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
	}
}
