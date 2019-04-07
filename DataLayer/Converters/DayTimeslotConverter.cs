using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using DbDayTimeslot = Repository.Entities.DayTimeslot;

namespace DataLayer.Converters
{
	public static class DayTimeslotConverter
	{
		public static DayTimeslot Convert(DbDayTimeslot dbDayTimeslot)
		{
			if (dbDayTimeslot == null) return null;

			return new DayTimeslot()
			{
				Id = dbDayTimeslot.Id,
				Name = dbDayTimeslot.Name,
				StartTime = dbDayTimeslot.StartTime
			};
		}

		public static DbDayTimeslot Convert(DayTimeslot dayTimeslot)
		{
			if (dayTimeslot == null) return null;

			return new DbDayTimeslot()
			{
				Id = dayTimeslot.Id,
				Name = dayTimeslot.Name,
				StartTime = dayTimeslot.StartTime
			};
		}
	}
}
