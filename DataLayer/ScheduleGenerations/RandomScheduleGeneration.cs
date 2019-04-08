using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Helpers;
using DataLayer.Models;

namespace DataLayer.ScheduleGenerations
{
	public class RandomScheduleGeneration : BaseScheduleGeneration
	{
		Random Random { get; } = new Random();

		public RandomScheduleGeneration(int countSchedules) 
			: base(countSchedules) { }


		public override List<Schedule> Run(List<Classroom> classrooms, List<PeriodTimeslot> periodTimeslots, List<TeachingUnit> teachingUnits)
		{
			var freeTimeslots = GetFreeTimeslots(classrooms, periodTimeslots);
			var schedules = new List<Schedule>();

			for (var current = 0; current < CountSchedules; current++)
			{
				var schedule = new Schedule();

				var isDone = false;
				while (!isDone)
				{
					// Статические жёсткие ограничения
					teachingUnits.ForEach(
					unit => unit.FreeTimeslots = freeTimeslots
						.Where(t => unit.Group.StudentsCount < t.classroom.Capacity)                // Вместимость аудиторий
						.Where(t => unit.ClassroomTypes.Contains(t.classroom.ClassroomType))        // Тип аудиторий
						.Where(t => !unit.Teacher.BanPeriodTimeslots.Contains(t.periodTimeslot))    // Ограничения преподавателей на таймслоты
						.ToList());

					schedule.ScheduleCells.Clear();

					teachingUnits = teachingUnits.Shuffle();

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

							var index = Random.Next(teachingUnits[i].FreeTimeslots.Count);
							var timeslot = teachingUnits[i].FreeTimeslots[index];

							schedule.ScheduleCells.Add(new ScheduleCell(timeslot.classroom, timeslot.periodTimeslot, teachingUnits[i]));

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

					if (isNext) continue;

					isDone = true;
				}

				schedules.Add(schedule);
			}			

			return schedules;
		}		
	}
}
