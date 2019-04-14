using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Models;

namespace DataLayer.ScheduleGenerations.Genetic.EvaluationFunctions
{
	/// <summary>
	/// Количество учебных занятий в день, на которых должен присутствовать студент, 
	/// не должно превышать заданнного значения
	/// </summary>
	public static class Function6
	{
		static int K { get; } = 1;

		public static double Value(Schedule schedule)
		{
			var value = 1.0 / (1 + Count(schedule));
			return value;
		}

		/// <summary>
		/// Суммарное количество (по всем учебным группам и всем дням) превышений количества 
		/// запланированных студентам "пар" в день относительно максимального допустимого количества K
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
					count += Math.Max(day.Count() - K, 0);
				}
			}

			return count;
		}
	}
}
