using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using DbScheduleCell = Repository.Entities.ScheduleCell;

namespace DataLayer.Converters
{
	public static class ScheduleCellConverter
	{
		public static ScheduleCell Convert(DbScheduleCell dbScheduleCell)
		{
			if (dbScheduleCell == null) return null;

			return new ScheduleCell()
			{
				ClassroomId = dbScheduleCell.ClassroomId,
				Classroom = ClassroomConverter.Convert(dbScheduleCell.Classroom),

				PeriodTimeslotId = dbScheduleCell.PeriodTimeslotId,
				PeriodTimeslot = PeriodTimeslotConverter.Convert(dbScheduleCell.PeriodTimeslot),

				TeachingUnitId = dbScheduleCell.TeachingUnitId,
				TeachingUnit = TeachingUnitConverter.Convert(dbScheduleCell.TeachingUnit)
			};
		}

		public static DbScheduleCell Convert(ScheduleCell schedule)
		{
			if (schedule == null) return null;

			return new DbScheduleCell()
			{
				ClassroomId = schedule.ClassroomId,
				PeriodTimeslotId = schedule.PeriodTimeslotId,
				TeachingUnitId = schedule.TeachingUnitId,
			};
		}
	}
}
