using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Models;

namespace DataLayer.ScheduleGenerations.Genetic.EvaluationFunctions
{
	/// <summary>
	/// Количество учебных занятий в день, проводимых каждым преподавателем, 
	/// не должно превышать заданнного значения
	/// </summary>
	public static class Function4
	{
		static int K { get; } = 2;

		public static double Value(Schedule schedule)
		{
			var value = 1.0 / (1 + Count(schedule));
			return value;
		}

		/// <summary>
		/// Суммарное количество (по всем преподавателям и всем дням) превышений количества запланированных "пар"
		/// в день относительно максимального допустимого количества K
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
					count += Math.Max(day.Count() - K, 0);
				}
			}

			return count;
		}
	}
}
