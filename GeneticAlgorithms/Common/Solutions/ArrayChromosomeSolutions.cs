using System;
using System.Collections.Generic;
using System.Text;

using GeneticAlgorithms.Common.Chromosomes;
using GeneticAlgorithms.Core;

namespace GeneticAlgorithms.Common.Solutions
{
	public class ArrayChromosomeSolutions
	{
		public static class Appearences
		{
			public static void ByElement<G, T>(GeneticAlgorithm<G> alg, Func<Random, T> elementGenerator)
				where G : ArrayChromosome<T>, IEquatable<G>
				where T : IEquatable<T>
			{
				alg.PerformAppearence = () =>
				{
					var g = alg.CreateEmptyChromosome();
					g.SetCode(z => elementGenerator(alg.Rnd));
					return g;
				};
			}

			public static void Bool<G>(GeneticAlgorithm<G> a)
				where G : ArrayChromosome<bool>, IEquatable<G>
			{
				ByElement(a, rnd => rnd.Next(2) == 0);
			}
		}
		
		public static class Mutators
		{
			public static void ByElement<G, T>(GeneticAlgorithm<G> alg, Func<T, T> elementMutation)
				where G : ArrayChromosome<T>, IEquatable<G>
				where T : IEquatable<T>
			{
				alg.PerformMutation = gen =>
				{
					var g = alg.CreateEmptyChromosome();
					g.CheckLength(gen);
					g.SetCode(p => gen.Code[p]);
					var position = alg.Rnd.Next(g.Code.Length);
					g.Code[position] = elementMutation(g.Code[position]);
					return new G[] { g };
				};
			}

			public static void Bool<G>(GeneticAlgorithm<G> alg)
				where G : ArrayChromosome<bool>, IEquatable<G>
			{
				ByElement<G, bool>(alg, c => !c);
			}
		}

		public static class Crossovers
		{
			public static void Mix<G, T>(GeneticAlgorithm<G> alg)
				where G : ArrayChromosome<T>, IEquatable<G>
				where T : IEquatable<T>
			{
				alg.PerformCrossover = (g1, g2) =>
				{
					var g = alg.CreateEmptyChromosome();
					g.CheckLength(g1, g2);
					g.SetCode(i => alg.Rnd.Next(2) == 0 ? g1.Code[i] : g2.Code[i]);
					return new G[] { g };
				};
			}
		}
	}
}
