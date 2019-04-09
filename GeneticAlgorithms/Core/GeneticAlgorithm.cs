﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithms.Core
{
	public class GeneticAlgorithm<T> where T : Chromosome, IEquatable<T>
	{
		public Func<T> CreateEmptyChromosome;
		public Action<T> Evaluate;

		public Func<int> GetAppearenceCount;
		public Func<T> PerformAppearence;
		public Func<IEnumerable<Tuple<T, int>>> GetMutationOrigins;
		public Func<T, T> PerformMutation;
		public Func<IEnumerable<Tuple<T, T, int>>> GetCrossoverFamilies;
		public Func<T, T, T> PerformCrossover;
		public Action<List<T>, List<T>> PerformSelection;
		public Action PerformBanking;
		public event Action IterationCallBack;
		public event Action EvaluationCompleted;
		public event Action EvaluationBegins;
		public Random Rnd { get; private set; }

		public T RandomChromosomeFromPool()
		{
			return Pool[Rnd.Next(Pool.Count)];
		}

		public GeneticAlgorithm(Func<T> createEmptyChromosome, Random rnd = null)
		{
			if (rnd == null) Rnd = new Random();
			else Rnd = rnd;
			Pool = new List<T>();
			Buffer = new List<T>();
			Bank = new List<T>();
			CreateEmptyChromosome = createEmptyChromosome;
		}

		public GeneticAlgorithm(Func<T> createEmptyChromosome, int seed)
			: this(createEmptyChromosome)

		{
			Rnd = new Random(seed);
		}

		public List<T> Pool { get; private set; }
		public List<T> Buffer { get; private set; }
		public List<T> Bank { get; private set; }

		public int CurrentIteration { get; private set; }

		public bool CanKeepOldGenesInBuffer = true;
		public bool ReevaluateOldGenes = false;
		public bool DisableEqualGenes = true;

		public event Action IterationBegins;

		public bool IsBanking
		{
			get { return PerformBanking != null; }
		}

		public void MakeIteration()
		{
			IterationBegins?.Invoke();

			if (GetAppearenceCount != null && PerformAppearence != null)
			{
				var c = GetAppearenceCount();
				for (var i = 0; i < c; i++)
					Buffer.Add(PerformAppearence());
				++CurrentIteration;
			}

			if (Pool.Count != 0 && GetMutationOrigins != null && PerformMutation != null)
			{
				var mutationSource = GetMutationOrigins();
				foreach (var source in mutationSource)
				{
					for (var i = 0; i < source.Item2; i++)
					{
						var m = PerformMutation(source.Item1);
						if (m == null) continue;
						Buffer.Add(m);
					}
				}
			}

			if (Pool.Count != 0 && GetCrossoverFamilies != null && PerformCrossover != null)
			{
				var pairs = GetCrossoverFamilies();
				foreach (var pair in pairs)
				{
					for (var i = 0; i < pair.Item3; i++)
					{
						var cross = PerformCrossover(pair.Item1, pair.Item2);
						if (cross == null) continue;
						Buffer.Add(cross);
					}
				}
			}

			if (ReevaluateOldGenes)
				foreach (var e in Pool) e.Evaluated = false;

			if (CanKeepOldGenesInBuffer)
				Buffer.AddRange(Pool);

			Pool.Clear();

			if (DisableEqualGenes)
				for (var i = Buffer.Count - 1; i >= 0; i--)
					for (var j = i - 1; j >= 0; j--)
						if (Buffer[i].Equals(Buffer[j]))
						{
							Buffer.RemoveAt(j);
							j--;
							i--;
						}

			if (EvaluationBegins != null)
				EvaluationBegins();

			foreach (var e in Buffer.Where(z => !z.Evaluated))
				Evaluate(e);

			EvaluationCompleted?.Invoke();

			if (PerformSelection != null)
				PerformSelection(Buffer, Pool);
			else
				Pool.AddRange(Buffer);

			Buffer.Clear();

			foreach (var g in Pool)
			{
				g.Evaluated = true;
				g.Age++;
				if (g.Generation == 0) g.Generation = CurrentIteration;
			}

			PerformBanking?.Invoke();
			IterationCallBack?.Invoke();
		}

		public void Refresh()
		{
			Pool.Clear();
			CurrentIteration = 0;
		}

		public void Restart()
		{
			Refresh();
			Bank.Clear();
		}

		public IEnumerable<Chromosome> ChromosomePool { get { return Pool.OrderByDescending(c => c.Value); } }
		public IEnumerable<Chromosome> ChromosomeBank { get { return Bank.OrderByDescending(c => c.Value); } }
	}
}
