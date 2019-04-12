using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Models;

namespace DataLayer.ScheduleGenerations.Genetic.EvaluationFunctions
{
	/// <summary>
	/// Количество избыточных мест в аудитории по отношению к количеству студентов не должно быть большим
	/// </summary>
	public static class Function3
	{
		public static double Value(Schedule schedule)
		{
			var value = 1.0 / (1 + Count(schedule));
			return value;
		}

		/// <summary>
		/// Суммарное количество "избыточных" мест в учебных аудиториях.
		/// Для каждого учебного занятия это - разность между количеством посадочных мест 
		/// в аудитории и количеством студентов в учебной группе
		/// </summary>
		public static int Count(Schedule schedule)
		{
			var count = schedule.ScheduleCells.Sum(
				x => x.Classroom.Capacity - x.TeachingUnit.Group.StudentsCount);
			return count;
		}
	}
}
