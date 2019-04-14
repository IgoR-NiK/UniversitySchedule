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
			var count = 0;
			return count;
		}
	}
}
