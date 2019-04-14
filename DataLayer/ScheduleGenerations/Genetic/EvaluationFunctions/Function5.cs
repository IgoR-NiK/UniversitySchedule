using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Models;

namespace DataLayer.ScheduleGenerations.Genetic.EvaluationFunctions
{
	/// <summary>
	/// Учебные занятия, планируемые преподавателю на конкретный день, должны располагаться 
	/// в смежных таймслотах (т.е. без "окон")
	/// </summary>
	public static class Function5
	{
		public static double Value(Schedule schedule)
		{
			var value = 1.0 / (1 + Count(schedule));
			return value;
		}

		/// <summary>
		/// Суммарное количество (по всем преподавателям и всем дням) "окон" в расписаниях преподавателей
		/// </summary>
		public static int Count(Schedule schedule)
		{
			var teachers = schedule.ScheduleCells.GroupBy(x => x.TeachingUnit.TeacherId);

			var count = 0;
			foreach (var teacher in teachers)
			{
				var days = teacher.GroupBy(x => (x.PeriodTimeslot.WeekId, x.PeriodTimeslot.DayId));
				foreach (var day in days)
				{
					var cells = day.OrderBy(x => x.PeriodTimeslot.DayTimeslotId);
					var o = cells.Last().PeriodTimeslot.DayTimeslotId - cells.First().PeriodTimeslot.DayTimeslotId - day.Count() + 1;
					count += o;
				}
			}

			return count;
		}
	}
}
