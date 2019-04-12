using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Helpers;
using GeneticAlgorithms.Core;

namespace DataLayer.ScheduleGenerations.Genetic
{
	class PermutationSolutions
	{
		public static class Appearences
		{
			public static void Shuffle<G>(GeneticAlgorithm<G> alg)
				where G : PermutationChromosome, IEquatable<G>
			{
				alg.PerformAppearence = () =>
				{
					var g = alg.CreateEmptyChromosome();

					g.Shuffle(alg.Rnd);

					return g;
				};
			}
		}

		public static class Mutators
		{
			public static void Swap<G>(GeneticAlgorithm<G> alg)
				where G : PermutationChromosome, IEquatable<G>
			{
				alg.PerformMutation = gen =>
				{
					var g = alg.CreateEmptyChromosome();

					Array.Copy(gen.Code, g.Code, gen.Code.Length);
					g.Swap(alg.Rnd);

					return new G[] { g };
				};
			}
		}

		public static class Crossovers
		{
			public static void Сomposition<G>(GeneticAlgorithm<G> alg)
				where G : PermutationChromosome, IEquatable<G>
			{
				alg.PerformCrossover = (gen1, gen2) =>
				{
					var g1 = alg.CreateEmptyChromosome();
					var g2 = alg.CreateEmptyChromosome();

					for (var i = 0; i < gen1.Code.Length; i++)
					{
						g1.Code[i] = gen2.Code[gen1.Code[i]];
						g2.Code[i] = gen1.Code[gen2.Code[i]];
					}

					return new G[] { g1, g2 };
				};
			}
		}
	}
}
