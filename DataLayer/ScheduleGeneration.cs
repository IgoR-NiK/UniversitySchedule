using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Algorithms;
using DataLayer.Models;

namespace DataLayer
{
	public class ScheduleGeneration
	{
		IAlgorithm Algorithm { get; }

		public ScheduleGeneration(IAlgorithm algorithm)
		{
			Algorithm = algorithm;
		}

		public List<Schedule> Run(
			List<Classroom> classrooms, List<PeriodTimeslot> periodTimeslots, List<TeachingUnit> teachingUnits)
		{
			// Ограничения аудиторий на таймслоты
			var freeTimeslots = GetFreeTimeslots(classrooms, periodTimeslots);
			var schedule = new List<Schedule>();

			var isDone = false;
			while (!isDone)
			{
				// Статические жёсткие ограничения
				teachingUnits.ForEach(
				unit => unit.FreeTimeslots = freeTimeslots
					.Where(t => unit.Group.StudentsCount < t.classroom.Capacity)				// Вместимость аудиторий
					.Where(t => unit.ClassroomTypes.Contains(t.classroom.ClassroomType))		// Тип аудиторий
					.Where(t => !unit.Teacher.BanPeriodTimeslots.Contains(t.periodTimeslot))	// Ограничения преподавателей на таймслоты
					.ToList());

				schedule.Clear();

				teachingUnits = Algorithm.GetTeachingUnits(teachingUnits);

				var isNext = false;
				for (var i = 0; i < teachingUnits.Count; i++)
				{
					for (var j = 0; j < teachingUnits[i].CountInPeriodTimeslot; j++)
					{
						if (teachingUnits[i].FreeTimeslots.Count == 0)
						{
							isNext = true;
							break;
						}							

						var timeslot = Algorithm.GetTimeslot(teachingUnits[i]);
						schedule.Add(new Schedule(timeslot.classroom, timeslot.periodTimeslot, teachingUnits[i]));

						teachingUnits[i].FreeTimeslots.RemoveAll(t =>
							t.periodTimeslot.Week.Equals(timeslot.periodTimeslot.Week) &&
							t.periodTimeslot.Day.Equals(timeslot.periodTimeslot.Day));

						for (var k = i + 1; k < teachingUnits.Count; k++)
						{
							teachingUnits[k].FreeTimeslots.Remove(timeslot);
							teachingUnits[k].FreeTimeslots.RemoveAll(t =>
								teachingUnits[k].Teacher.Equals(teachingUnits[i].Teacher) &&
								t.periodTimeslot.Equals(timeslot.periodTimeslot));

							teachingUnits[k].FreeTimeslots.RemoveAll(t =>
								(teachingUnits[k].Group.Equals(teachingUnits[i].Group) ||
								teachingUnits[k].Group.ParentGroupId == teachingUnits[i].Group.Id ||
								teachingUnits[k].Group.ChildGroups.Contains(teachingUnits[i].Group)) &&
								t.periodTimeslot.Equals(timeslot.periodTimeslot));
						}
					}

					if (isNext) break;
				}

				if (isNext)	continue;

				isDone = true;
			}

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
