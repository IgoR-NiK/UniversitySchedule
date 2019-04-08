using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GeneticAlgorithms.Common.Chromosomes;
using GeneticAlgorithms.Core;

namespace GeneticAlgorithms.Common
{
	public class Solutions
	{
		public class AppearenceCount
		{
			public static void MinimalPoolSize<G>(GeneticAlgorithm<G> alg, int minPoolSize)
				where G : Chromosome
			{
				alg.GetAppearenceCount = () => minPoolSize - alg.Pool.Count;
			}
		}

		public class MutationOrigins
		{
			public static void Random<G>(GeneticAlgorithm<G> alg, double mutationPercentage)
			where G : Chromosome
			{
				alg.GetMutationOrigins = delegate
				{
					var res = new Tuple<G, int>[(int)(alg.Pool.Count * mutationPercentage)];
					for (int i = 0; i < res.Length; i++)
						res[i] = new Tuple<G, int>(alg.RandomChromosomeFromPool(), 1);
					return res;
				};
			}

		}

		public class CrossFamilies
		{
			public static void Random<G>(GeneticAlgorithm<G> alg, Func<int, double> familiesCount)
				where G : Chromosome
			{
				alg.GetCrossoverFamilies = delegate
				{
					var res = new Tuple<G, G, int>[(int)familiesCount(alg.Pool.Count)];
					for (int i = 0; i < res.Length; i++)
						res[i] = new Tuple<G, G, int>(alg.RandomChromosomeFromPool(), alg.RandomChromosomeFromPool(), 1);
					return res;
				};
			}			
		}

		public class Selections
		{
			public static void Threashold<G>(GeneticAlgorithm<G> alg, int maxPoolSize)
				where G : Chromosome
			{
				alg.PerformSelection = delegate (List<G> from, List<G> to)
				{
					to.AddRange(from.OrderBy(z => -z.Value).Take(maxPoolSize));
				};
			}
		}
	}

	public class ArrayGeneSolutions
	{
		public class Crossover
		{
			public static void Mix<G>(GeneticAlgorithm<G> alg)
				where G : ArrayChromosome
			{
				alg.PerformCrossover = delegate (G g1, G g2)
				{
					var g = alg.CreateEmptyChromosome();
					g.CheckLength(g1, g2);
					g.SetObjectCode(i => alg.Rnd.Next(2) == 0 ? g1.ObjectCode.GetValue(i) : g2.ObjectCode.GetValue(i));
					return g;
				};
			}			

			public static void Permutation<G>(GeneticAlgorithm<G> a)
				where G : ArrayChromosome<int>
			{
				a.PerformCrossover = delegate (G g1, G g2)
				{
					var g = a.CreateEmptyChromosome();
					g.CheckLength(g1, g2);
					for (int i = 0; i < g.Code.Length; i++)
						g.Code[i] = g1.Code[g2.Code[i]];
					return g;
				};
			}
		}

		public class Mutators
		{
			public static void ByElement<G, T>(GeneticAlgorithm<G> alg, Func<Random, T, T> elementMutation)
				where G : ArrayChromosome<T>
				where T : IEquatable<T>
			{
				alg.PerformMutation = delegate (G source)
				{
					var g = alg.CreateEmptyChromosome();
					g.CheckLength(source);
					g.SetCode(p => source.Code[p]);
					var position = alg.Rnd.Next(g.Code.Length);
					g.Code[position] = elementMutation(alg.Rnd, g.Code[position]);
					return g;
				};
			}

			public static void Bool<G>(GeneticAlgorithm<G> a)
				where G : ArrayChromosome<bool>
			{
				ByElement<G, bool>(a, (rnd, c) => !c);
			}
		}

		public class Appearences
		{
			public static void ByElement<G, T>(GeneticAlgorithm<G> alg, Func<Random, T> elementGenerator)
				where G : ArrayChromosome<T>
				where T : IEquatable<T>
			{
				alg.PerformAppearence = delegate
				{
					var g = alg.CreateEmptyChromosome();
					g.SetCode(z => elementGenerator(alg.Rnd));
					return g;
				};
			}

			public static void Bool<G>(GeneticAlgorithm<G> a)
				where G : ArrayChromosome<bool>
			{
				ByElement(a, rnd => rnd.Next(2) == 0);
			}

			public static void Permutation<G>(GeneticAlgorithm<G> alg)
				where G : ArrayChromosome<int>
			{
				alg.PerformAppearence = delegate
				{
					var g = alg.CreateEmptyChromosome();
					var c = new bool[g.Code.Length];
					for (var i = 0; i < g.Code.Length; i++)
					{
						var place = alg.Rnd.Next(c.Length);
						while (c[place])
						{
							place++;
							if (place >= c.Length)
								place -= c.Length;
						}
						g.Code[place] = i;
						c[place] = true;
					}
					return g;
				};
			}
		}
	}
}
