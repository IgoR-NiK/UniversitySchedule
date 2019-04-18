using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using DbDay = Repository.Entities.Day;

namespace DataLayer.Converters
{
	public static class DayConverter
	{
		public static Day Convert(DbDay dbDay)
		{
			if (dbDay == null) return null;

			return new Day()
			{
				Id = dbDay.Id,
				Name = dbDay.Name,
				DayOfWeek = dbDay.DayOfWeek
			};
		}

		public static DbDay Convert(Day day)
		{
			if (day == null) return null;

			return new DbDay()
			{
				Id = day.Id,
				Name = day.Name,
				DayOfWeek = day.DayOfWeek
			};
		}
	}
}
