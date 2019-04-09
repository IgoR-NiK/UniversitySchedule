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
					var res = new Tuple<G, int>[(int)(alg.Pool.Count * mutationPercentage)];

					for (int i = 0; i < res.Length; i++)
						res[i] = new Tuple<G, int>(alg.RandomChromosomeFromPool(), 1);

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
					var res = new Tuple<G, G, int>[(int)(alg.Pool.Count * familiesPercentage)];

					for (int i = 0; i < res.Length; i++)
						res[i] = new Tuple<G, G, int>(alg.RandomChromosomeFromPool(), alg.RandomChromosomeFromPool(), 1);

					return res;
				};
			}			
		}

		public static class Selections
		{
			public static void Threashold<G>(GeneticAlgorithm<G> alg, int maxPoolSize)
				where G : Chromosome, IEquatable<G>
			{
				alg.PerformSelection = (from, to) =>
				{
					to.AddRange(from.OrderBy(z => -z.Value).Take(maxPoolSize));
				};
			}
		}
	}
}
