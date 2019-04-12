using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataLayer.Models;
using DataLayer.ScheduleGenerations.Genetic.EvaluationFunctions;
using GeneticAlgorithms.Core;

namespace DataLayer.ScheduleGenerations.Genetic
{
	class TimeslotSolutions
	{
		public static class Appearences
		{
			public static void Random<G>(GeneticAlgorithm<G> alg, TeachingUnit teachingUnit)
				where G : TimeslotChromosome, IEquatable<G>
			{
				alg.PerformAppearence = () =>
				{
					var g = alg.CreateEmptyChromosome();
					g.Code = alg.Rnd.Next(teachingUnit.FreeTimeslots.Count);
					return g;
				};
			}
		}

		public static class Mutators
		{
			public static void Modulo<G>(GeneticAlgorithm<G> alg, TeachingUnit teachingUnit)
				where G : TimeslotChromosome, IEquatable<G>
			{
				alg.PerformMutation = gen =>
				{
					var g = alg.CreateEmptyChromosome();
					g.Code = (gen.Code * 17) % teachingUnit.FreeTimeslots.Count;
					return new G[] { g };
				};
			}
		}

		public static class Crossovers
		{
			public static void HalfSum<G>(GeneticAlgorithm<G> alg)
				where G : TimeslotChromosome, IEquatable<G>
			{
				alg.PerformCrossover = (gen1, gen2) =>
				{
					var g = alg.CreateEmptyChromosome();
					g.Code = (gen1.Code + gen2.Code) / 2;
					return new G[] { g };
				};
			}
		}

		public static class Evaluation
		{
			public static void SetEvaluate<G>(GeneticAlgorithm<G> alg, TeachingUnit teachingUnit, Schedule schedule)
				where G : TimeslotChromosome, IEquatable<G>
			{
				alg.Evaluate = chromosome =>
				{
					var slot = teachingUnit.FreeTimeslots[chromosome.Code];
					var cell = new ScheduleCell(slot.classroom, slot.periodTimeslot, teachingUnit);

					schedule.ScheduleCells.Add(cell);

					chromosome.Value = EvaluationCalculation.Calculate(schedule);

					schedule.ScheduleCells.Remove(cell);
				};
			}
		}
	}
}
