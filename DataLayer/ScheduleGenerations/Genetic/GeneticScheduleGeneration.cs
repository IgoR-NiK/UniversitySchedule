using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Helpers;
using DataLayer.Models;
using GeneticAlgorithms.Core;

namespace DataLayer.ScheduleGenerations.Genetic
{
	public class GeneticScheduleGeneration : BaseScheduleGeneration
	{
		public GeneticAlgorithm<TimeslotChromosome> GeneticAlgorithm { get; }
		public int CountIterations { get; }

		public GeneticScheduleGeneration(int countSchedules) 
			: base(countSchedules)
		{
			var rnd = new Random(1);
			GeneticAlgorithm = new GeneticAlgorithm<TimeslotChromosome>(() => new TimeslotChromosome(), rnd);
			CountIterations = 30;

			Solutions.AppearenceCount.MinimalPoolSize(GeneticAlgorithm, 100);
			Solutions.MutationOrigins.Random(GeneticAlgorithm, 0.5);
			Solutions.CrossFamilies.Random(GeneticAlgorithm, 0.5);
			Solutions.Selections.Threashold(GeneticAlgorithm, 30);			
		}

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

							var timeslot = GetFreeTimeslot(teachingUnits[i], schedule);

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

		private (Classroom classroom, PeriodTimeslot periodTimeslot) GetFreeTimeslot(TeachingUnit teachingUnit, Schedule schedule)
		{
			GeneticAlgorithm.Refresh();

			TimeslotSolutions.Appearences.Random(GeneticAlgorithm, teachingUnit);
			TimeslotSolutions.Mutators.Modulo(GeneticAlgorithm, teachingUnit);
			TimeslotSolutions.Crossovers.HalfSum(GeneticAlgorithm);
			TimeslotSolutions.Evaluation.SetEvaluate(GeneticAlgorithm, teachingUnit, schedule);
			
			for (var i = 0; i < CountIterations; i++)
			{
				GeneticAlgorithm.MakeIteration();
			}

			var index = GeneticAlgorithm.ChromosomePool.First().Code;
			var timeslot = teachingUnit.FreeTimeslots[index];

			return timeslot;
		}
	}
}
