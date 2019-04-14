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
			var count = 0;
			return count;
		}
	}
}
