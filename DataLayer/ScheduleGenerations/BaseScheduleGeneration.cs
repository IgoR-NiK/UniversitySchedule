using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;

namespace DataLayer.ScheduleGenerations
{
	public abstract class BaseScheduleGeneration
	{
		public int CountSchedules { get; }

		public BaseScheduleGeneration(int countSchedules)
		{
			CountSchedules = countSchedules;
		}


		public abstract List<Schedule> Run(List<Classroom> classrooms, List<PeriodTimeslot> periodTimeslots, List<TeachingUnit> teachingUnits);


		/// <summary>
		/// Возвращает разрешенные таймслоты с учетом ограничения аудиторий.
		/// </summary>
		protected List<(Classroom classroom, PeriodTimeslot periodTimeslot)> GetFreeTimeslots(List<Classroom> classrooms, List<PeriodTimeslot> periodTimeslots)
		{
			var result = new List<(Classroom classroom, PeriodTimeslot periodTimeslot)>();

			foreach (var classroom in classrooms)
			{
				foreach (var periodTimeslot in periodTimeslots)
				{
					if (!classroom.BanPeriodTimeslots.Contains(periodTimeslot))
					{
						result.Add((classroom, periodTimeslot));
					}
				}
			}

			return result;
		}
	}
}
