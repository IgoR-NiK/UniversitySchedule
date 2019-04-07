using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using DbWeek = Repository.Entities.Week;

namespace DataLayer.Converters
{
	public static class WeekConverter
	{
		public static Week Convert(DbWeek dbWeek)
		{
			if (dbWeek == null) return null;

			return new Week()
			{
				Id = dbWeek.Id,
				Name = dbWeek.Name
			};
		}

		public static DbWeek Convert(Week week)
		{
			if (week == null) return null;

			return new DbWeek()
			{
				Id = week.Id,
				Name = week.Name
			};
		}
	}
}
