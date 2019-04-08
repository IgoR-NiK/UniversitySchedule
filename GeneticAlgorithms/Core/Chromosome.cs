using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithms.Core
{
	public abstract class Chromosome : IEquatable<Chromosome>
	{
		public bool Evaluated { get; internal set; }
		public int Id { get; internal set; }
		public int Generation { get; internal set; }
		public int Age { get; internal set; }
		public int Parent1 { get; internal set; }
		public int Parent2 { get; internal set; }
		public double Value { get; set; }
		public abstract bool Equals(Chromosome other);

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
