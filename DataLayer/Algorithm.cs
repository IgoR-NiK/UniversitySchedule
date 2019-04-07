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
			var schedule = new Dictionary<(int classroomId, int periodTimeslotId), int>();


			teachingUnits.ForEach(x => x.FreeTimeslots = new List<(int classroomId, int periodTimeslotId)>(freeTimeslots));
			



			return schedule;
		}


		/// <summary>
		/// Возвращает разрешенные таймслоты с учетом ограничения аудиторий.
		/// </summary>
		private List<(int classroomId, int periodTimeslotId)> GetFreeTimeslots(List<Classroom> classrooms, List<PeriodTimeslot> periodTimeslots)
		{
			var result = new List<(int classroomId, int periodTimeslotId)>();

			foreach (var classroom in classrooms)
			{
				foreach (var periodTimeslot in periodTimeslots)
				{
					if (!classroom.BanPeriodTimeslots.Contains(periodTimeslot))
					{
						result.Add((classroom.Id, periodTimeslot.Id));
					}
				}
			}

			return result;
		}
	}
}
