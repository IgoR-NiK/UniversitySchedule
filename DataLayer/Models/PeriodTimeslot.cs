﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
	public class PeriodTimeslot
	{
		public int Id { get; set; }

		public int WeekId { get; set; }
		public Week Week { get; set; }

		public int DayId { get; set; }
		public Day Day { get; set; }

		public int DayTimeslotId { get; set; }
		public DayTimeslot DayTimeslot { get; set; }

		public override string ToString()
		{
			return $"{Week}, {Day}, {DayTimeslot}";
		}

		public override bool Equals(object obj)
		{
			return obj is PeriodTimeslot periodTimeslot ? Id == periodTimeslot.Id : false;
		}
	}
}
