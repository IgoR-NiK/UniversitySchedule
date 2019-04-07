using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using DbPeriodTimeslot = Repository.Entities.PeriodTimeslot;

namespace DataLayer.Converters
{
	public static class PeriodTimeslotConverter
	{
		public static PeriodTimeslot Convert(DbPeriodTimeslot dbPeriodTimeslot)
		{
			if (dbPeriodTimeslot == null) return null;

			return new PeriodTimeslot()
			{
				Id = dbPeriodTimeslot.Id,

				WeekId = dbPeriodTimeslot.WeekId,
				Week = WeekConverter.Convert(dbPeriodTimeslot.Week),

				DayId = dbPeriodTimeslot.DayId,
				Day = DayConverter.Convert(dbPeriodTimeslot.Day),

				DayTimeslotId = dbPeriodTimeslot.DayTimeslotId,
				DayTimeslot = DayTimeslotConverter.Convert(dbPeriodTimeslot.DayTimeslot)
			};
		}

		public static DbPeriodTimeslot Convert(PeriodTimeslot periodTimeslot)
		{
			if (periodTimeslot == null) return null;

			return new DbPeriodTimeslot()
			{
				Id = periodTimeslot.Id,
				WeekId = periodTimeslot.WeekId,
				DayId = periodTimeslot.DayId,
				DayTimeslotId = periodTimeslot.DayTimeslotId
			};
		}
	}
}
