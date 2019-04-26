using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Models;
using DataLayer.ScheduleGenerations.Genetic.EvaluationFunctions;
using GeneticAlgorithms.Core;

namespace DataLayer.ScheduleGenerations.Genetic
{
	public class GeneticScheduleGeneration : BaseScheduleGeneration
	{
		public GeneticAlgorithm<TimeslotChromosome> LocalGA { get; private set; }
		public GeneticAlgorithm<PermutationChromosome> GlobalGA { get; private set; }
		
		public int CountIterationsLocalGA { get; private set; }
		public int CountIterationsGlobalGA { get; private set; }
		
		public GeneticScheduleGeneration(int countSchedules) 
			: base(countSchedules)
		{
			LocalGA = new GeneticAlgorithm<TimeslotChromosome>(() => new TimeslotChromosome());
			CountIterationsLocalGA = 10;

			Solutions.AppearenceCount.MinimalPoolSize(LocalGA, 30);
			Solutions.MutationOrigins.Random(LocalGA, 0.5);
			Solutions.CrossFamilies.Random(LocalGA, 0.5);
			Solutions.Selections.Threashold(LocalGA, 10);	
		}

		public override List<Schedule> Run(List<Classroom> classrooms, List<PeriodTimeslot> periodTimeslots, List<TeachingUnit> teachingUnits)
		{
			GlobalGA = new GeneticAlgorithm<PermutationChromosome>(() => new PermutationChromosome(teachingUnits.Count))
			{
				RemoveEqualGenes = false
			};
			CountIterationsGlobalGA = 100;

			Solutions.AppearenceCount.MinimalPoolSize(GlobalGA, 30);
			Solutions.MutationOrigins.Random(GlobalGA, 0.5);
			Solutions.CrossFamilies.Random(GlobalGA, 0.5);
			Solutions.Selections.Threashold(GlobalGA, 10);
						
			PermutationSolutions.Appearences.Shuffle(GlobalGA);
			PermutationSolutions.Mutators.Swap(GlobalGA);
			PermutationSolutions.Crossovers.Сomposition(GlobalGA);
			

			var freeTimeslots = GetFreeTimeslots(classrooms, periodTimeslots);

			GlobalGA.Evaluate = chromosome =>
			{
				var schedule = new Schedule();

				// Статические жёсткие ограничения
				teachingUnits.ForEach(
					unit => unit.FreeTimeslots = freeTimeslots
						.Where(t => unit.Group.StudentsCount < t.classroom.Capacity)                // Вместимость аудиторий
						.Where(t => unit.ClassroomTypes.Contains(t.classroom.ClassroomType))        // Тип аудиторий
						.Where(t => !unit.Teacher.BanPeriodTimeslots.Contains(t.periodTimeslot))    // Ограничения преподавателей на таймслоты
						.ToList());

				for (var i = 0; i < chromosome.Code.Length; i++)
				{
					var index = chromosome.Code[i];
					var teachingUnit = teachingUnits[index];

					for (var j = 0; j < teachingUnit.CountInPeriodTimeslot; j++)
					{
						if (teachingUnit.FreeTimeslots.Count == 0)
						{
							chromosome.Value = -100;
							return;
						}

						var timeslot = GetFreeTimeslot(teachingUnit, schedule);

						schedule.ScheduleCells.Add(new ScheduleCell(timeslot.classroom, timeslot.periodTimeslot, teachingUnit));

						teachingUnit.FreeTimeslots.RemoveAll(t =>
							t.periodTimeslot.Week.Equals(timeslot.periodTimeslot.Week) &&
							t.periodTimeslot.Day.Equals(timeslot.periodTimeslot.Day));

						for (var p = i + 1; p < chromosome.Code.Length; p++)
						{
							var k = chromosome.Code[p];

							teachingUnits[k].FreeTimeslots.Remove(timeslot);

							teachingUnits[k].FreeTimeslots.RemoveAll(t =>
								teachingUnits[k].Teacher.Equals(teachingUnit.Teacher) &&
								t.periodTimeslot.Equals(timeslot.periodTimeslot));

							teachingUnits[k].FreeTimeslots.RemoveAll(t =>
								(teachingUnits[k].Group.Equals(teachingUnit.Group) ||
								teachingUnits[k].Group.ParentGroupId == teachingUnit.Group.Id ||
								teachingUnits[k].Group.ChildGroups.Contains(teachingUnit.Group)) &&
								t.periodTimeslot.Equals(timeslot.periodTimeslot));
						}
					}
				}

				chromosome.Schedule = schedule;
				chromosome.Value = EvaluationCalculation.Calculate(schedule);
			};

			for (var i = 0; i < CountIterationsGlobalGA; i++)
			{
				GlobalGA.MakeIteration();
			}

			var schedules = GlobalGA.ChromosomePool
				.Take(CountSchedules)
				.Select(x => x.Schedule)
				.Where(x => x != null)
				.ToList();

			return schedules;
		}

		private (Classroom classroom, PeriodTimeslot periodTimeslot) GetFreeTimeslot(TeachingUnit teachingUnit, Schedule schedule)
		{
			LocalGA.Refresh();

			TimeslotSolutions.Appearences.Random(LocalGA, teachingUnit);
			TimeslotSolutions.Mutators.Modulo(LocalGA, teachingUnit);
			TimeslotSolutions.Crossovers.HalfSum(LocalGA);
			TimeslotSolutions.Evaluation.SetEvaluate(LocalGA, teachingUnit, schedule);
			
			for (var i = 0; i < CountIterationsLocalGA; i++)
			{
				LocalGA.MakeIteration();
			}

			var index = LocalGA.ChromosomePool.First().Code;
			var timeslot = teachingUnit.FreeTimeslots[index];

			return timeslot;
		}
	}
}
