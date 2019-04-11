using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Models;

namespace DataLayer.ScheduleGenerations.Genetic.EvaluationFunctions
{
	/// <summary>
	/// Желательно планировать лекции в начале дня. 
	/// </summary>
	public static class Function1
	{
		static int K { get; } = 2;

		public static double Value(Schedule schedule)
		{
			return 1 / (1 + Count(schedule));
		}

		/// <summary>
		/// Общее количество лекционных занятий, которым запланирована "пара" с порядковым номером k (в пределах дня)
		/// и k > K, где K - максимальный номер пары, которая ещё считается "утренней". 
		/// </summary>
		public static int Count(Schedule schedule)
		{
			return schedule.ScheduleCells.Count(
				x => x.TeachingUnit.LessonTypeId == 1 && x.PeriodTimeslot.DayTimeslotId > K);
		}
	}
}
