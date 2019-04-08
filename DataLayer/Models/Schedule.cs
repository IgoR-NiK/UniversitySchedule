using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
	public class Schedule
	{
		public List<ScheduleCell> ScheduleCells { get; set; } = new List<ScheduleCell>();
	}
}
