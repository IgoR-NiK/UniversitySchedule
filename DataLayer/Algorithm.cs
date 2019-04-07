using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Models;

namespace DataLayer
{
	public class Algorithm
	{
		public Dictionary<(int classroomId, int periodTimeslotId), int> Run(
			List<Classroom> classrooms, List<PeriodTimeslot> periodTimeslots, List<TeachingUnit> teachingUnits)
		{
			var freeTimeslots = GetFreeTimeslots(classrooms, periodTimeslots);	
			teachingUnits.ForEach(
				unit => unit.FreeTimeslots = freeTimeslots
					.Where(t => unit.Group.StudentsCount < t.classroom.Capacity)
					.Where(t => unit.ClassroomTypes.Contains(t.classroom.ClassroomType))
					.Where(t => !unit.Teacher.BanPeriodTimeslots.Contains(t.periodTimeslot))
					.ToList());
						


			var schedule = new Dictionary<(int classroomId, int periodTimeslotId), int>();
			return schedule;
		}


		/// <summary>
		/// Возвращает разрешенные таймслоты с учетом ограничения аудиторий.
		/// </summary>
		private List<(Classroom classroom, PeriodTimeslot periodTimeslot)> GetFreeTimeslots(List<Classroom> classrooms, List<PeriodTimeslot> periodTimeslots)
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
