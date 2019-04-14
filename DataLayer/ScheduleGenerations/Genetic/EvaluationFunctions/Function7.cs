using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Models;

namespace DataLayer.ScheduleGenerations.Genetic.EvaluationFunctions
{
	/// <summary>
	/// Учебные занятия, планируемые студенту на конкретный день, должны располагаться 
	/// в смежных таймслотах (т.е. без "окон")
	/// </summary>
	public static class Function7
	{
		public static double Value(Schedule schedule)
		{
			var value = 1.0 / (1 + Count(schedule));
			return value;
		}

		/// <summary>
		/// Суммарное количество (по всем учебным группам и всем дням) "окон" в расписаниях студентов
		/// </summary>
		public static int Count(Schedule schedule)
		{
			var groups = schedule.ScheduleCells
				.Where(x => x.TeachingUnit.Group.ChildGroups.Count == 0)
				.GroupBy(x => x.TeachingUnit.GroupId);

			var count = 0;
			foreach (var group in groups)
			{
				var threadCells = schedule.ScheduleCells
					.Where(x => x.TeachingUnit.Group.ChildGroups.Any(y => y.Id == group.Key));

				var cells = group.ToList();
				cells.AddRange(threadCells);

				var days = cells.GroupBy(x => (x.PeriodTimeslot.WeekId, x.PeriodTimeslot.DayId));
				foreach (var day in days)
				{
					var dayCells = day.OrderBy(x => x.PeriodTimeslot.DayTimeslotId);
					var o = dayCells.Last().PeriodTimeslot.DayTimeslotId - dayCells.First().PeriodTimeslot.DayTimeslotId - day.Count() + 1;
					count += o;
				}
			}

			return count;
		}
	}
}
