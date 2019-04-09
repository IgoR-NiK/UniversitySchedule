using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithms.Core
{
	public class GeneticAlgorithm<T> where T : Chromosome, IEquatable<T>
	{
		public Func<T> CreateEmptyChromosome { get; set; }
		public Func<int> GetAppearenceCount { get; set; }
		public Func<T> PerformAppearence { get; set; }
		public Func<IEnumerable<T>> GetMutationOrigins { get; set; }
		public Func<T, IEnumerable<T>> PerformMutation { get; set; }
		public Func<IEnumerable<(T parent1, T parent2)>> GetCrossoverFamilies { get; set; }
		public Func<T, T, IEnumerable<T>> PerformCrossover { get; set; }
		public Func<IEnumerable<T>, IEnumerable<T>> PerformSelection { get; set; }

		public event Action<T> Evaluate;
		public event Action PerformBanking;

		public event Action IterationBegins;
		public event Action IterationCompleted;
		public event Action EvaluationBegins;
		public event Action EvaluationCompleted;

		public Random Rnd { get; }

		public List<T> Pool { get; } = new List<T>();
		public List<T> Buffer { get; } = new List<T>();
		public List<T> Bank { get; } = new List<T>();

		public IEnumerable<Chromosome> ChromosomePool => Pool.OrderByDescending(c => c.Value);
		public IEnumerable<Chromosome> ChromosomeBank => Bank.OrderByDescending(c => c.Value);

		public int CurrentIteration { get; private set; }

		public bool CanKeepOldGenesInBuffer { get; } = true;
		public bool ReevaluateOldGenes { get; } = false;
		public bool RemoveEqualGenes { get; } = true;

		public bool IsBanking => PerformBanking != null;

		public T RandomChromosomeFromPool => Pool[Rnd.Next(Pool.Count)];


		public GeneticAlgorithm(Func<T> createEmptyChromosome, Random rnd = null)
		{
			Rnd = rnd ?? new Random();
			CreateEmptyChromosome = createEmptyChromosome;
		}		
		

		public void MakeIteration()
		{
			CurrentIteration++;
			IterationBegins?.Invoke();

			if (GetAppearenceCount != null && PerformAppearence != null)
			{
				var count = GetAppearenceCount();
				for (var i = 0; i < count; i++)
					Buffer.Add(PerformAppearence());				
			}

			if (Pool.Count != 0 && GetMutationOrigins != null && PerformMutation != null)
			{
				var mutationSource = GetMutationOrigins();
				foreach (var source in mutationSource)
				{
					var mutants = PerformMutation(source);

					if (mutants == null)
						continue;

					Buffer.AddRange(mutants);
				}
			}

			if (Pool.Count != 0 && GetCrossoverFamilies != null && PerformCrossover != null)
			{
				var pairs = GetCrossoverFamilies();
				foreach (var pair in pairs)
				{
					var childs = PerformCrossover(pair.parent1, pair.parent2);

					if (childs == null)
						continue;

					Buffer.AddRange(childs);
				}
			}

			if (ReevaluateOldGenes)
				foreach (var g in Pool)
					g.IsEvaluated = false;

			if (CanKeepOldGenesInBuffer)
				Buffer.AddRange(Pool);

			Pool.Clear();

			if (RemoveEqualGenes)
				for (var i = Buffer.Count - 1; i >= 0; i--)
					for (var j = i - 1; j >= 0; j--)
						if (Buffer[i].Equals(Buffer[j]))
						{
							Buffer.RemoveAt(j);
							i--;
						}

			EvaluationBegins?.Invoke();

			foreach (var e in Buffer.Where(z => !z.IsEvaluated))
				Evaluate(e);

			EvaluationCompleted?.Invoke();

			Pool.AddRange(PerformSelection?.Invoke(Buffer) ?? Buffer);

			Buffer.Clear();

			foreach (var g in Pool)
			{
				g.IsEvaluated = true;
				g.Age++;
			}

			PerformBanking?.Invoke();
			IterationCompleted?.Invoke();
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
	}
}
