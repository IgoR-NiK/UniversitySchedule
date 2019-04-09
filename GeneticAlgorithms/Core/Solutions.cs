using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GeneticAlgorithms.Core;

namespace GeneticAlgorithms.Core
{
	public class Solutions
	{
		public static class AppearenceCount
		{
			public static void MinimalPoolSize<G>(GeneticAlgorithm<G> alg, int minPoolSize)
				where G : Chromosome, IEquatable<G>
			{
				alg.GetAppearenceCount = () => minPoolSize - alg.Pool.Count;
			}
		}

		public static class MutationOrigins
		{
			public static void Random<G>(GeneticAlgorithm<G> alg, double mutationPercentage)
				where G : Chromosome, IEquatable<G>
			{
				alg.GetMutationOrigins = () =>
				{
					var res = new G[(int)(alg.Pool.Count * mutationPercentage)];

					for (int i = 0; i < res.Length; i++)
						res[i] = alg.RandomChromosomeFromPool;

					return res;
				};
			}
		}

		public static class CrossFamilies
		{
			public static void Random<G>(GeneticAlgorithm<G> alg, double familiesPercentage)
				where G : Chromosome, IEquatable<G>
			{
				alg.GetCrossoverFamilies = () =>
				{
					var res = new (G, G)[(int)(alg.Pool.Count * familiesPercentage)];

					for (int i = 0; i < res.Length; i++)
						res[i] = (alg.RandomChromosomeFromPool, alg.RandomChromosomeFromPool);

					return res;
				};
			}			
		}

		public static class Selections
		{
			public static void Threashold<G>(GeneticAlgorithm<G> alg, int maxPoolSize)
				where G : Chromosome, IEquatable<G>
			{
				alg.PerformSelection = source =>
				{
					return source.OrderBy(z => -z.Value).Take(maxPoolSize);
				};
			}
		}
	}
}
